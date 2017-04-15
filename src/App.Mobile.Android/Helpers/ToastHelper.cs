using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using App.Mobile.Shared.Interfaces.Helpers;

namespace App.Mobile.Android.Helpers
{
    public class ToastHelper : IToastHelper
    {
        private readonly Activity _context;

        public ToastHelper(Activity activity)
        {
            _context = activity;
        }

        public Task<bool> Show(string text, int duration = 0, ToastNotificationType type = ToastNotificationType.None)
        {
            var tcs = new TaskCompletionSource<bool>();

            _context.RunOnUiThread(() =>
            {
                Toast.MakeText(_context, text, duration == 0 ? ToastLength.Short : ToastLength.Long).Show();
            });

            return tcs.Task;
        }
    }
}