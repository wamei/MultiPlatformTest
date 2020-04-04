using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiPlatformTest
{
    public partial class App : Application
    {
        // UWPの場合事前にApplicationContentUriRulesへURLの指定が必要
        private int[] portCandidates = { 8080, 8081, 8082, 8083, 8084, 8085 };

        public App()
        {
            InitializeComponent();

            int port = LaunchHttpd();
            MainPage = new MainPage() { Port = port };
        }

        private int LaunchHttpd()
        {
            foreach (int port in portCandidates)
            {
                try
                {
                    var server = new SimpleHttpd(port);
                    server.Start();
                    return port;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: port{port}: {ex.Message}");
                }

            }
            throw new Exception("Faild to start local http server");
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
