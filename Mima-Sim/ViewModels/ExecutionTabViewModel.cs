using Avalonia.Controls;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabViewModel : ReactiveObject, IActivatableViewModel
    {
        public ICommand RunCodeCommand { get; set; }
        public ICommand OpenErrorPopupCommand { get; set; }

        public ICommand OpenClockSettingsCommand { get; set; }

        public ICommand OpenMemoryPopupCommand { get; set; }

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
                    DialogService.OpenError("Bitte einen Programmtext eingeben. Dieser darf nicht leer sein!");
                }
            });
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; this.RaiseAndSetIfChanged(ref _source, value); }
        }

        private object _selectedLanguage;

        public object SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; this.RaiseAndSetIfChanged(ref _selectedLanguage, value); }
        }

        private bool _runMode;

        public bool RunMode
        {
            get { return _runMode; }
            set { _runMode = value; this.RaiseAndSetIfChanged(ref _runMode, value); }
        }

        public ICommand StepCommand { get; set; }

        public ICommand StopCommand { get; set; }

        private ICommand _viewRawCommand;

        public ICommand ViewRawCommand { get; set; }

        public ViewModelActivator Activator => new ViewModelActivator();
    }
}