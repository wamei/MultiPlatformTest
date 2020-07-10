using System;
using MultiPlatformTest.Core.DependencyService;
using MultiPlatformTest.iOS.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApplicationUtil))]
namespace MultiPlatformTest.iOS.DependencyService
{
    public class ApplicationUtil : IApplicationUtil
    {
        public void Restart()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}
