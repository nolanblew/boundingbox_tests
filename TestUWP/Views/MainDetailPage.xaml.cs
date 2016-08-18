using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using Prism.Windows.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainDetailPage : Page
    {
        public MainDetailPage()
        {
            this.InitializeComponent();
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            if (_ShouldGoToWideState())
            {
                _NavigateBackForWideState();
            }
            else
            {
                Window.Current.SizeChanged += Window_SizeChanged;
            }
        }

        readonly INavigationService _navigationService;

        void Window_SizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
        {
            if (_ShouldGoToWideState())
            {
                _NavigateBackForWideState();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
            base.OnNavigatingFrom(e);
        }

        void _NavigateBackForWideState()
        {
            _navigationService.GoBack();
        }

        bool _ShouldGoToWideState() { return Window.Current.Bounds.Width >= 720; }
    }
}
