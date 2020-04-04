using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace MultiPlatformTest.WPF
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Startup += App_Startup;
            app.Run();
        }

        private static void App_Startup(object sender, StartupEventArgs e)
        {
            CreateRegistry();
        }

        public static void CreateRegistry()
        {
            var filename = Process.GetCurrentProcess().MainModule.FileName;
            filename = filename.Substring(filename.LastIndexOf('\\') + 1, filename.Length - filename.LastIndexOf('\\') - 1);
            if (filename.Contains("vhost"))
            {
                filename = filename.Substring(0, filename.IndexOf('.') + 1) + "exe";
            }

            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
            key1?.SetValue(filename, 11001, RegistryValueKind.DWord);
            key1?.Close();
        }
    }
}
