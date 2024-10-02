using Avalonia.Controls;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Messages;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Avalonia.Platform.Storage;
using MimaSim.Samples;
using MimaSim.ViewModels.Mima;
using Splat;

namespace MimaSim.ViewModels;

public class ExecutionTabViewModel : ReactiveObject, IActivatableViewModel
{
    private bool _runMode;
    private LanguageName _selectedLanguage;
    private string _selectedSample;
    private string _source;
    private string[] _sampleNames;

    public ObservableCollection<LanguageName> LanguageNames { get; }
    public ViewModelActivator Activator => new();
    public ICommand LoadCommand { get; set; }
    public ICommand OpenClockSettingsCommand { get; set; }
    public ICommand OpenErrorPopupCommand { get; set; }
    public ICommand OpenMemoryPopupCommand { get; set; }
    public ICommand RunCodeCommand { get; set; }

    public bool RunMode
    {
        get => _runMode;
        set => this.RaiseAndSetIfChanged(ref _runMode, value);
    }

    public ICommand SaveCommand { get; set; }

    public LanguageName SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedLanguage, value);

            SampleNames = Locator.Current.GetService<SampleLoader>().GetSampleNamesFor(_selectedLanguage).ToArray();
            SelectedSample = SampleNames.FirstOrDefault();
        }
    }

    public string SelectedSample
    {
        get => _selectedSample;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSample, value);
        }
    }

    public string[] SampleNames
    {
        get => _sampleNames;
        set => this.RaiseAndSetIfChanged(ref _sampleNames, value);
    }

    public string Source
    {
        get => _source;
        set => this.RaiseAndSetIfChanged(ref _source, value);
    }

    public ICommand StepCommand { get; set; }

    public ICommand StopCommand { get; set; }
    public ICommand ViewRawCommand { get; set; }

    public ExecutionTabViewModel()
    {
        OpenErrorPopupCommand = ReactiveCommand.Create(() => DialogService.Open());

        OpenClockSettingsCommand = DialogService.CreateOpenCommand(new ClockSettingsPopupControl(), new ClockSettingsPopupViewModel());

        StepCommand = ReactiveCommand.Create(() => CPU.Instance.Step());
        StopCommand = ReactiveCommand.Create(() => CPU.Instance.Clock.Stop());

        LanguageNames = new ObservableCollection<LanguageName>(Enum.GetNames<LanguageName>().Select(_ => Enum.Parse<LanguageName>(_)));
        SelectedLanguage = LanguageNames.FirstOrDefault();

        ViewRawCommand = ReactiveCommand.Create(() =>
        {
            DialogService.Open(new RawViewPopupControl(), new RawPopupViewModel());
        });

        OpenMemoryPopupCommand = DialogService.CreateOpenCommand(new MemoryPopupControl(), new MemoryPopupViewModel());

        IStorageProvider storage = Locator.Current.GetService<IStorageProvider>();

        LoadCommand = ReactiveCommand.Create(async () =>
        {
            var filenames = await storage.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Programm laden"
            });

            var reader = new StreamReader(await filenames.First().OpenReadAsync());

            Source = reader.ReadToEnd();
        });

        SaveCommand = ReactiveCommand.Create(async () =>
        {
            var file = await storage.SaveFilePickerAsync(new() { Title = "Programm speichern" });
            await using var writer = new StreamWriter(await file.OpenWriteAsync());

            writer.Write(Source);
        });

        RunCodeCommand = ReactiveCommand.Create(() =>
        {
            if (!string.IsNullOrEmpty(Source))
            {
                if (RunMode)
                {
                    var translator = SourceTextTranslatorSelector.Select(SelectedLanguage);
                    DiagnosticBag diagnostics = new();
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

                    BusRegistry.DeactivateAll();
                    BusRegistry.DeactivateBus("controlunit_iar");
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
}