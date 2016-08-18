using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;
using TestUWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        MainPageViewModel _viewModel => DataContext as MainPageViewModel;
        INavigationService _navigationService;

        void AdaptiveStates_OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            _UpdateForVisualState(e.NewState, e.OldState);
        }

        public void _ItemClick()
        {
            if (AdaptiveStates.CurrentState == NarrowState &&
                _viewModel.SelectedPerson != null)
            {
                // Resize down to the detail item.
                _navigationService.Navigate("MainDetail", _viewModel.SelectedPerson);
            }
        }

        void _UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;

            if (isNarrow && oldState == DefaultState && _viewModel.SelectedPerson != null)
            {
                // Resize down to the detail item.
                _navigationService.Navigate("MainDetail", _viewModel.SelectedPerson);
            }
        }
    }
}
