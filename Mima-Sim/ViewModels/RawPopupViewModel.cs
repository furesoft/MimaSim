using MimaSim.Controls;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class RawPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        private string _raw;

        public RawPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Raw = GetRawString();

                /* handle activation */
                Disposable
                    .Create(() => { /* handle deactivation */ })
                    .DisposeWith(disposables);
            });
        }

        public ViewModelActivator Activator => new ViewModelActivator();

        public ICommand CloseCommand { get; set; }

        public string Raw
        {
            get { return _raw; }
            set { _raw = value; this.RaiseAndSetIfChanged(ref _raw, value); }
        }

        private string GetRawString()
        {
            var raw = CPU.Instance.Program;

            return string.Join(' ', raw.Select(_ =>
            {
                return (_).ToString("x");
            })).ToUpper();
        }
    }
}