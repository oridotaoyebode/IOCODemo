using System;
using System.Threading.Tasks;
using IOCO.Demo.Services.Navigation;
using IOCO.Demo.ViewModels;
using IOCO.Demo.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IOCO.Demo
{
    public partial class App : Application
    {
        static App()
        {
            BuildDependencies();
        }
        public App()
        {

            InitializeComponent();
            InitNavigation();
        }
        public static void BuildDependencies()
        {

            Locator.Instance.Build();
        }

        Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
