using MultiPlatformTest.Models;
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

namespace MultiPlatformTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public int Port = 8080;
        public MainPage()
        {
            InitializeComponent();
            {
                var realm = Realm.GetInstance();
                var message = realm.All<Message>().Where(msg => msg.Key == "message1").FirstOrDefault();
                if (message == null)
                {
                    message = new Message { Key = "message1", Body = "" };
                    realm.Write(() =>
                    {
                        realm.Add(message);
                    });
                }
                webview.RegisterAction("loaded", data =>
                {
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
                    {
                        await webview.EvaluateJavaScriptAsync($"setMessage('{message.Body}')");
                    });
                });
            }
            webview.RegisterAction("save", data =>
            {
                var realm = Realm.GetInstance();
                var message = realm.All<Message>().Where(msg => msg.Key == "message1").FirstOrDefault();
                realm.Write(() =>
                {
                    message.Body = data;
                });
            });
            webview.Source = $"http://127.0.0.1:{Port}/index.html";
        }
    }
}
