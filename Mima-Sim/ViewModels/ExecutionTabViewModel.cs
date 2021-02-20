using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.Messages;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabViewModel : ReactiveObject, IActivatableViewModel
    {
        private bool _runMode;
        private object _selectedLanguage;
        private string _source;

        public ExecutionTabViewModel()
        {
            OpenErrorPopupCommand = ReactiveCommand.Create(() => DialogService.Open());

            OpenClockSettingsCommand = DialogService.CreateOpenCommand(new ClockSettingsPopupControl(), new ClockSettingsPopupViewModel());

            StepCommand = ReactiveCommand.Create(() => CPU.Instance.Step());
            StopCommand = ReactiveCommand.Create(() => CPU.Instance.Clock.Stop());

            ViewRawCommand = DialogService.CreateOpenCommand(new RawViewPopupControl(), new RawPopupViewModel());
            OpenMemoryPopupCommand = DialogService.CreateOpenCommand(new MemoryPopupControl(), new MemoryPopupViewModel());

            LoadCommand = ReactiveCommand.Create(async () =>
            {
                var ofd = new OpenFileDialog();
                ofd.Title = "Programm laden";

                var window = App.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
                var filenames = await ofd.ShowAsync(window.MainWindow);

                Source = File.ReadAllText(filenames.First());
            });

            SaveCommand = ReactiveCommand.Create(async () =>
            {
                var svd = new SaveFileDialog();
                svd.Title = "Programm speichern";

                var window = App.Current.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;

                var filename = await svd.ShowAsync(window.MainWindow);

                File.WriteAllText(filename, Source);
            });

            RunCodeCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrEmpty(Source))
                {
                    if (RunMode)
                    {
                        var translator = SourceTextTranslatorSelector.Select((LanguageName)Enum.Parse(typeof(LanguageName), ((ComboBoxItem)SelectedLanguage).Content.ToString()));
                        DiagnosticBag diagnostics = null;
                        CPU.Instance.Program = translator.ToRaw(Source, ref diagnostics);

                        if (!diagnostics.IsEmpty)
                        {
                            RunMode = false;
                            string[] errors = diagnostics.GetAll();

                            DialogService.OpenError(string.Join('\n', errors));
                        }
                        else
                        {
                            RegisterMap.GetRegister("IAR").SetValue(0);

                            CPU.Instance.Clock.Start();
                        }
                    }
                    else
                    {
                        CPU.Instance.Clock.Stop();
                    }
                }
                else
                {
                    RunMode = false;
                    DialogService.OpenError("Bitte einen Programmtext eingeben. Dieser darf nicht leer sein!");
                }
            });

            var stopObserver = MessageBus.Current.Listen<StopMessage>();
            stopObserver.Subscribe(_ =>
            {
                RunMode = false;
                CPU.Instance.Clock.Stop();
            });
        }

        public ViewModelActivator Activator => new ViewModelActivator();
        public ICommand LoadCommand { get; set; }
        public ICommand OpenClockSettingsCommand { get; set; }
        public ICommand OpenErrorPopupCommand { get; set; }
        public ICommand OpenMemoryPopupCommand { get; set; }
        public ICommand RunCodeCommand { get; set; }

        public bool RunMode
        {
            get { return _runMode; }
            set { this.RaiseAndSetIfChanged(ref _runMode, value); }
        }

        public ICommand SaveCommand { get; set; }

        public object SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { this.RaiseAndSetIfChanged(ref _selectedLanguage, value); }
        }

        public string Source
        {
            get { return _source; }
            set { this.RaiseAndSetIfChanged(ref _source, value); }
        }

        public ICommand StepCommand { get; set; }

        public ICommand StopCommand { get; set; }
        public ICommand ViewRawCommand { get; set; }
    }
}