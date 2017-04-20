using System.Threading.Tasks;
using Android.App;
using App.Mobile.Shared.Interfaces.Helpers;

namespace App.Mobile.Droid.Helpers
{
    public class AlertHelper : IAlertHelper
    {
        private readonly Activity _context;

        public AlertHelper(Activity activity)
        {
            _context = activity;
        }
        
        public Task<MessageResult> ShowDialog(string title, string message, bool cancelable = false, MessageResult positiveButton = MessageResult.Ok, MessageResult negativeButton = MessageResult.None, MessageResult neutralButton = MessageResult.None, int iconAttribute = global::Android.Resource.Drawable.IcDialogAlert)
        {
            var tcs = new TaskCompletionSource<MessageResult>();

            var builder = new AlertDialog.Builder(_context);
            builder.SetIconAttribute(iconAttribute);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetCancelable(cancelable);

            builder.SetPositiveButton(positiveButton != MessageResult.None ? positiveButton.ToString() : string.Empty, delegate
            {
                tcs.SetResult(positiveButton);
            });
            builder.SetNegativeButton(negativeButton != MessageResult.None ? negativeButton.ToString() : string.Empty, delegate
            {
                tcs.SetResult(negativeButton);
            });
            builder.SetNeutralButton(neutralButton != MessageResult.None ? neutralButton.ToString() : string.Empty, delegate
            {
                tcs.SetResult(neutralButton);
            });

            _context.RunOnUiThread(() =>
            {
                builder.Show();
            });

            return tcs.Task;
        }
    }
}