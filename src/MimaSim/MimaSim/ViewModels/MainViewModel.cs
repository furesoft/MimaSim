using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Controls.Popups;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Messages;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using MimaSim.ViewModels.Mima;
using ReactiveUI;
using Splat;
using HelpPopup = MimaSim.Controls.Popups.HelpPopup;

namespace MimaSim.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private IHighlightingDefinition _highlighting;
    private bool _isCompiled;
    private bool _runMode;
    private string[] _sampleNames;
    private LanguageName _selectedLanguage;
    private string? _selectedSample;
    private string? _source;

    public MainViewModel()
    {
        InitHighlighting();

        CPU.Instance.Clock.Stoped += () =>
        {
            RunMode = false;
        };

        Locator.Current.GetService<INetworkService>()!.Init();
        OpenErrorPopupCommand = ReactiveCommand.Create(DialogService.Open);

        OpenClockSettingsCommand =
            DialogService.CreateOpenCommand(new ClockSettingsPopupControl(), new ClockSettingsPopupViewModel());

        HelpCommand = DialogService.CreateOpenCommand(new HelpPopup(), new HelpPopupViewModel());

        StepCommand = ReactiveCommand.Create(() => CPU.Instance.Step());
        StopCommand = ReactiveCommand.Create(() => CPU.Instance.Clock.Stop());

        LanguageNames =
            new ObservableCollection<LanguageName>(Enum.GetNames<LanguageName>()
                .Select(lang => Enum.Parse<LanguageName>(lang)));

        ViewRawCommand = ReactiveCommand.Create(() =>
        {
            DialogService.Open(new DisassemblyViewPopup(), new DisassemblyPopupViewModel());
        });

        OpenNetworkSettingsCommand =
            DialogService.CreateOpenCommand(new NetworkCardPopupControl(), new NetworkCardPopupViewModel());

        OpenMemoryPopupCommand = DialogService.CreateOpenCommand(new MemoryPopupControl(), new MemoryPopupViewModel());

        LoadCommand = ReactiveCommand.Create(async () =>
        {
            var storage = Locator.Current.GetService<IStorageProvider>();
            var filenames = await storage!.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Programm laden"
            });

            using var reader = new StreamReader(await filenames.First().OpenReadAsync());

            Source = reader.ReadToEnd();
        });

        SaveCommand = ReactiveCommand.Create(async () =>
        {
            var storage = Locator.Current.GetService<IStorageProvider>();
            var file = await storage!.SaveFilePickerAsync(new FilePickerSaveOptions { Title = "Programm speichern" });
            await using var writer = new StreamWriter(await file!.OpenWriteAsync());

            writer.Write(Source);
        });

        DiagnosticBag diagnostics = new();
        CompileCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrEmpty(Source))
            {
                RunMode = false;
                DialogService.OpenError("Bitte einen Programmtext eingeben. Dieser darf nicht leer sein!");
                return;
            }

            var translator = SourceTextTranslatorSelector.Select(SelectedLanguage);

            diagnostics = new DiagnosticBag();
            CPU.Instance.Program = translator.ToRaw(Source, ref diagnostics);

            IsCompiled = true;

            CPU.Instance.NIC.Loopback(Encoding.Default.GetBytes("Hello World"));
        });

        RunCodeCommand = ReactiveCommand.Create(() =>
        {
            if (RunMode)
            {
                if (!diagnostics.IsEmpty)
                {
                    RunMode = false;
                    var errors = diagnostics.GetAll();

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
        });

        OpenGithubCommand = ReactiveCommand.Create(async () =>
        {
            await Locator.Current.GetService<ILauncher>()!.LaunchUriAsync(new Uri("https://github.com/furesoft/MimaSim"));
        });

        var stopObserver = MessageBus.Current.Listen<StopMessage>();
        stopObserver.Subscribe(_ =>
        {
            RunMode = false;
            CPU.Instance.Clock.Stop();
        });

        var cache = Locator.Current.GetService<ICache>();
        _source = cache!.Get<string>("input")!;
        var language = cache.Get<string>("language")!;

        SelectedLanguage = language == null ? LanguageNames.FirstOrDefault() : Enum.Parse<LanguageName>(language);
    }

    public ObservableCollection<LanguageName> LanguageNames { get; }
    public ICommand CompileCommand { get; set; }
    public ICommand LoadCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand OpenClockSettingsCommand { get; set; }
    public ICommand OpenErrorPopupCommand { get; set; }
    public ICommand OpenMemoryPopupCommand { get; set; }
    public ICommand RunCodeCommand { get; set; }
    public ICommand HelpCommand { get; set; }
    public ICommand OpenGithubCommand { get; set; }

    public ICommand OpenNetworkSettingsCommand { get; set; }

    public bool RunMode
    {
        get => _runMode;
        set => this.RaiseAndSetIfChanged(ref _runMode, value);
    }

    public bool IsCompiled
    {
        get => _isCompiled;
        set => this.RaiseAndSetIfChanged(ref _isCompiled, value);
    }

    public IHighlightingDefinition Highlighting
    {
        get => _highlighting;
        set => this.RaiseAndSetIfChanged(ref _highlighting, value);
    }

    public LanguageName SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedLanguage, value);

            SampleNames = Locator.Current.GetService<SampleLoader>()!.GetSampleNamesFor(_selectedLanguage).ToArray();

            SelectedSample = null;
            Highlighting = HighlightingManager.Instance.GetDefinitionByExtension(GetExtensionForLanguage(value));
            Locator.Current.GetService<ICache>()!.AddOrUpdate("language", value.ToString());
        }
    }

    public string? SelectedSample
    {
        get => _selectedSample;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSample, value);

            if (value == null) return;
            Source = Locator.Current.GetService<SampleLoader>()!.GetSample(SelectedLanguage, SelectedSample)!;
        }
    }

    public string[] SampleNames
    {
        get => _sampleNames;
        set => this.RaiseAndSetIfChanged(ref _sampleNames, value);
    }

    public string? Source
    {
        get => _source;
        set
        {
            this.RaiseAndSetIfChanged(ref _source, value);
            IsCompiled = false;

            if (_source == null) return;
            Locator.Current.GetService<ICache>()!.AddOrUpdate("input", _source);
        }
    }

    public ICommand StepCommand { get; set; }

    public ICommand StopCommand { get; set; }
    public ICommand ViewRawCommand { get; set; }
    public ViewModelActivator Activator => new();

    private string GetExtensionForLanguage(LanguageName language)
    {
        return language switch
        {
            LanguageName.Hochsprache => ".hoch",
            LanguageName.Assembly => ".asm",
            LanguageName.Maschinencode => ".hex",
            _ => ".asm"
        };
    }

    private void InitHighlighting()
    {
        LoadHighlighting("Maschinencode", ".hex", "Hex");
        LoadHighlighting("Assembler", ".asm", "Assembler");
        LoadHighlighting("Hochsprache", ".hoch", "Hochsprache");
    }

    private void LoadHighlighting(string name, string extension, string filename)
    {
        IHighlightingDefinition customHighlighting;
        using (var s = GetType().Assembly.GetManifestResourceStream($"MimaSim.Resources.Highligting.{filename}.xshd"))
        {
            using (XmlReader reader = new XmlTextReader(s))
            {
                customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
        }

        HighlightingManager.Instance.RegisterHighlighting(name, new[] { extension }, customHighlighting);
    }
}