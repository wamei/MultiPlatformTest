using System;
using Android.App;
using Android.Content;
using MultiPlatformTest.Core.DependencyService;
using MultiPlatformTest.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApplicationUtil))]
namespace MultiPlatformTest.Droid.DependencyService
{
    public class ApplicationUtil : IApplicationUtil
    {
        public void Restart()
        {
            if (Android.OS.Build.VERSION.SdkInt <= Android.OS.BuildVersionCodes.P)
            {
                Context context = Android.App.Application.Context;
                Intent intent = new Intent(context, typeof(MainActivity));
                PendingIntent pi = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.OneShot);
                AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
                am.Set(AlarmType.Rtc, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), pi);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                Context context = Android.App.Application.Context;
                Intent intent = RestartActivity.createIntent(context);
                context.StartActivity(intent);
            }
        }
    }
}