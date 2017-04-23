using Android.Content;
using App.Application.ViewModels;
using System.Threading.Tasks;

namespace App.Mobile.Droid.Helpers
{
    public sealed class SessionManager
    {
        private const string PrefSession = "session";
        private const string PrefUserId = "userId";
        private const string PrefUserName = "userName";
        private const string PrefUserEmail = "userEmail";

        public static ISharedPreferences GetSharedPreferences(Context ctx)
        {
            return ctx.GetSharedPreferences(PrefSession, FileCreationMode.Private);
        }

        public static string GetUserId(Context ctx)
        {
            return GetSharedPreferences(ctx).GetString(PrefUserId, "");
        }

        public static string GetUserName(Context ctx)
        {
            return GetSharedPreferences(ctx).GetString(PrefUserName, "");
        }

        public static string GetUserEmail(Context ctx)
        {
            return GetSharedPreferences(ctx).GetString(PrefUserEmail, "");
        }

        public static async Task SingIn(Context ctx, CustomerViewModel customer)
        {
            await Task.Run(() =>
            {
                var editor = GetSharedPreferences(ctx).Edit();
                editor.PutString(PrefUserId, customer.Id.ToString());
                editor.PutString(PrefUserName, customer.Name);
                editor.PutString(PrefUserEmail, customer.Email);
                editor.Commit();
            });
        }

        public static async Task SingOut(Context ctx)
        {
            await Task.Run(() =>
            {
                var editor = GetSharedPreferences(ctx).Edit();
                editor.Clear();
                editor.Commit();
            });
        }

        public static bool IsActive(Context ctx)
        {
            return 
                GetUserId(ctx).Length > 0 && 
                GetUserName(ctx).Length > 0 &&
                GetUserEmail(ctx).Length > 0;
        }
    }
}