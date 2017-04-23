using Android.App;
using Android.Widget;
using App.Mobile.Shared.Interfaces.Helpers;
using System.Threading.Tasks;

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
                Toast.MakeText(Activity, text, duration == 0 ? ToastLength.Short : ToastLength.Long).Show();
            });

            return tcs.Task;
        }
    }
}