using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using TestUWP.Services;

namespace TestUWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate("Main", null);
            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            SessionStateService.RegisterKnownType(typeof(Models.Person));
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance<INavigationService>(NavigationService);
            Container.RegisterInstance<ISessionStateService>(SessionStateService);
            Container.RegisterInstance<IEventAggregator>(EventAggregator);

            Container.RegisterType<IFakeDataService, FakeDataService>();

            return base.OnInitializeAsync(args);
        }
        protected override ISessionStateService OnCreateSessionStateService()
        {
            return new JsonSessionStateService();
        }
    }
}
