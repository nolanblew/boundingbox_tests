using System.Collections.Generic;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using TestUWP.Models;

namespace TestUWP.ViewModels
{
    public class MainDetailPageViewModel : ViewModelBase
    {
        public MainDetailPageViewModel() { }

        [RestorableState]
        public Person Person { get; set; }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            Person = e.Parameter as Person;
        }
    }
}
