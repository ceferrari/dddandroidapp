using System.Threading.Tasks;

namespace App.Mobile.Shared.Interfaces.Helpers
{
    public interface IAlertHelper
    {
        Task<MessageResult> ShowDialog(
            string title, 
            string message, 
            bool cancelable = false,
            MessageResult positiveButton = MessageResult.Ok, 
            MessageResult negativeButton = MessageResult.None,
            MessageResult neutralButton = MessageResult.None, 
            int iconAttribute = 0);
    }

    public enum MessageResult
    {
        None,
        Ok,
        Cancel,
        Abort,
        Retry,
        Ignore,
        Yes,
        No
    }
}