using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace MultiPlatformTest.Version_new
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            int port = LaunchHttpd();
            InitializeComponent();
            {
                var realm = Realm.GetInstance();
                Console.WriteLine(realm.Config.DatabasePath);
                webview.RegisterAction("loaded", data =>
                {
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
                    {
                        Core.Models.Version.List.ForEach(async (version) =>
                        {
                            await webview.EvaluateJavaScriptAsync($"addVersion('{version.Value}')");
                        });
                        await webview.EvaluateJavaScriptAsync($"setVersion('{Core.Models.Version.Current.Value}')");
                    });
                });
            }
            webview.RegisterAction("save", data =>
            {
                var realm = Realm.GetInstance();
                realm.Write(() =>
                {
                    Core.Models.Version.Current.Value = data;
                });
                DependencyService.Get<Core.DependencyService.IApplicationUtil>().Restart();
            });
            webview.Source = $"http://127.0.0.1:{port}/index.html";
        }

        // UWPの場合事前にApplicationContentUriRulesへURLの指定が必要
        private int[] portCandidates = { 8080, 8081, 8082, 8083, 8084, 8085 };

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
    }
}
