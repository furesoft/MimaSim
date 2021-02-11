using MimaSim.Controls;
using MimaSim.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public MemoryPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());

            MemoryCells = new ObservableCollection<MemoryCellModel>();
            MemoryCells.Add(new MemoryCellModel { Value = 123 });
        }

        public ViewModelActivator Activator => new ViewModelActivator();
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<MemoryCellModel> MemoryCells { get; set; }
    }
}