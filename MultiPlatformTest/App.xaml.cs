using System;
using System.Linq;
using System.Net;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiPlatformTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            try
            {
                MainPage = Core.VersionClassFactory.Create("MainPage", Core.Models.Version.Current) as Page;
            }
            catch
            {
                Core.Models.Version.Current = Core.Models.Version.Default;
                MainPage = Core.VersionClassFactory.Create("MainPage", Core.Models.Version.Current) as Page;
            }
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
