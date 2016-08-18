using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using PropertyChanged;
using TestUWP.Models;
using TestUWP.Services;

namespace TestUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IFakeDataService dataService)
        {
            DataService = dataService;

            People = new ObservableCollection<Person>(dataService.GeneratePeople());

            SelectedItemChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(SelectedItemChanged);
        }

        [DoNotNotify]
        public ICommand SelectedItemChangedCommand { get; }

        [DoNotNotify]
        public IFakeDataService DataService { get; }

        [RestorableState]
        public ObservableCollection<Person> People { get; set; }

        [RestorableState]
        public Person SelectedPerson { get; set; }

        void SelectedItemChanged(SelectionChangedEventArgs itemsChangedEventArgs)
        {
            if (itemsChangedEventArgs.AddedItems.Count == 1)
            {
                SelectedPerson = itemsChangedEventArgs.AddedItems[0] as Person;
            }
        }
    }
}
