using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiPlatformTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LaunchHttpd();

            MainPage = new MainPage();
        }

        private async void LaunchHttpd()
        {

            try
            {
                var server = new SimpleHttpd();
                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

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
