﻿using Android.App;
using App.Mobile.Shared.Interfaces.Helpers;
using System.Threading.Tasks;

namespace App.Mobile.Droid.Helpers
{
    public sealed class AlertHelper : IAlertHelper
    {
        public Activity Activity { get; set; }

        public Task<MessageResult> ShowDialog(string title, string message, bool cancelable = false, MessageResult positiveButton = MessageResult.Ok, MessageResult negativeButton = MessageResult.None, MessageResult neutralButton = MessageResult.None, int iconAttribute = 0)
        {
            var tcs = new TaskCompletionSource<MessageResult>();

            var builder = new AlertDialog.Builder(Activity);
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

            Activity.RunOnUiThread(() =>
            {
                builder.Show();
            });

            return tcs.Task;
        }
    }
}