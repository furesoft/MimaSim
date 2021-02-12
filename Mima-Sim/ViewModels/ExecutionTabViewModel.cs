using Avalonia.Controls;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.Messages;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System;
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

            RunCodeCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrEmpty(Source))
                {
                    if (RunMode)
                    {
                        var translator = SourceTextTranslatorSelector.Select((LanguageName)Enum.Parse(typeof(LanguageName), ((ComboBoxItem)SelectedLanguage).Content.ToString()));
                        CPU.Instance.Program = translator.ToRaw(Source, out var hasError);

                        if (hasError)
                        {
                            RunMode = false;
                            DialogService.OpenError("Der Programmcode enthält einige Fehler. Code kann nicht übersetzt werden.");
                        }

                        RegisterMap.GetRegister("IAR").SetValue(0);

                        CPU.Instance.Clock.Start();
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
        public ICommand OpenClockSettingsCommand { get; set; }
        public ICommand OpenErrorPopupCommand { get; set; }
        public ICommand OpenMemoryPopupCommand { get; set; }
        public ICommand RunCodeCommand { get; set; }

        public bool RunMode
        {
            get { return _runMode; }
            set { _runMode = value; this.RaiseAndSetIfChanged(ref _runMode, value); }
        }

        public object SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; this.RaiseAndSetIfChanged(ref _selectedLanguage, value); }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; this.RaiseAndSetIfChanged(ref _source, value); }
        }

        public ICommand StepCommand { get; set; }

        public ICommand StopCommand { get; set; }
        public ICommand ViewRawCommand { get; set; }
    }
}