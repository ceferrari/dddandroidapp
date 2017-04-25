using Android.App;
using Android.Widget;
using App.Mobile.Shared.Interfaces.Helpers;
using System.Threading.Tasks;
using Android.Views;

namespace App.Mobile.Droid.Helpers
{
    public sealed class ToastHelper : IToastHelper
    {
        public Activity Activity { get; set; }

        public Task<bool> Show(string text, int duration = 0, ToastNotificationType type = ToastNotificationType.None)
        {
            var tcs = new TaskCompletionSource<bool>();

            Activity.RunOnUiThread(() =>
            {
                var toast = Toast.MakeText(Activity, text, duration == 0 ? ToastLength.Short : ToastLength.Long);
                //toast.SetGravity(GravityFlags.Top, 0, 50);
                //toast.SetGravity(GravityFlags.Bottom, 0, 45);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            });

            return tcs.Task;
        }
    }
}