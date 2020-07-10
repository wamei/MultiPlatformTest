using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MultiPlatformTest.Droid
{
    [Activity(Name = "MultiPlatformTest.RestartActivity", ExcludeFromRecents = true, Exported = false, LaunchMode = LaunchMode.SingleInstance, Theme = "@style/MainTheme", Process = ":restart_process")]
    public class RestartActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string EXTRA_MAIN_PID = "RestartActivity.main_pid";

        public static Intent createIntent(Context context)
        {
            Intent intent = new Intent(context, typeof(RestartActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            intent.PutExtra(EXTRA_MAIN_PID, System.Diagnostics.Process.GetCurrentProcess().Id);
            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            int mainPid = Intent.GetIntExtra(EXTRA_MAIN_PID, -1);
            System.Diagnostics.Process.GetProcessById(mainPid).Kill();

            Context context = Application.Context;
            Intent restartIntent = new Intent(context, typeof(MainActivity));
            restartIntent.SetAction(Intent.ActionMain);
            restartIntent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(restartIntent);

            Finish();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}