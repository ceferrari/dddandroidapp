using System.Threading.Tasks;

namespace App.Mobile.Shared.Interfaces.Helpers
{
    public interface IToastHelper
    {
        Task<bool> Show(
            string text,
            int duration,
            ToastNotificationType type);
    }

    public enum ToastNotificationType
    {
        None,
        Info,
        Success,
        Error,
        Warning,
    }
}