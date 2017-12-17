using Xamarin.Forms;
using Prism;
using Prism.Unity;
using NetworkingXF.Views;
using Microsoft.Practices.Unity;
using NetworkingXF.WebManager;

namespace NetworkingXF
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        /*
        public App()
        {
            InitializeComponent();

            //MainPage = new NetworkingXFPage();
        }
        */

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IWebClient, WebClient>(new ContainerControlledLifetimeManager());
        }
    }
}
