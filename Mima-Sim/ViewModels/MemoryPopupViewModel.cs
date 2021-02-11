using MimaSim.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public MemoryPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());
        }

        public ViewModelActivator Activator => new ViewModelActivator();
        public ICommand CloseCommand { get; set; }
    }
}