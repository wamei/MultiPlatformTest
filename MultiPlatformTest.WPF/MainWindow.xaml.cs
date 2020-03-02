using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MultiPlatformTest.WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.Init();
            LoadApplication(new MultiPlatformTest.App());
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var topbar = this.Template.FindName("PART_TopAppBar", this) as Xamarin.Forms.Platform.WPF.Controls.FormsAppBar;
            if (topbar != null) topbar.MaxHeight = 0;
        }
    }
}
