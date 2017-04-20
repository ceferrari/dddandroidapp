using Android.App;
using Android.OS;
using Android.Views;
using System.Threading;
using System.Threading.Tasks;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Splash);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            Task.Run(() => {
                Thread.Sleep(5000);
                RunOnUiThread(() => {
                    StartActivity(typeof(LoginActivity));
                });
            });
        }
    }
}