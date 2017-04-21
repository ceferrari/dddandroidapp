using Android.App;
using Android.OS;
using Android.Views;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V4.Graphics.Drawable;
using Android.Widget;

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

            //var imgLogo = FindViewById<ImageView>(Resource.Id.SplashLogo);
            //imgLogo.SetImageDrawable(App.RoundedLogo);

            Task.Run(() => {
                Thread.Sleep(5000);
                RunOnUiThread(() => {
                    StartActivity(typeof(LoginActivity));
                });
            });
        }
    }
}