using Android.Content;
using Android.Preferences;

namespace App.Mobile.Droid.Helpers
{
    public class SessionManager
    {
        private const string PrefUserName = "username";

        public static ISharedPreferences GetSharedPreferences(Context ctx)
        {
            return PreferenceManager.GetDefaultSharedPreferences(ctx);
        }

        public static string GetUserName(Context ctx)
        {
            return GetSharedPreferences(ctx).GetString(PrefUserName, "");
        }

        public static void SingIn(Context ctx, string userName)
        {
            var editor = GetSharedPreferences(ctx).Edit();
            editor.PutString(PrefUserName, userName);
            editor.Commit();
        }

        public static void SingOut(Context ctx)
        {
            var editor = GetSharedPreferences(ctx).Edit();
            editor.Clear();
            editor.Commit();
        }

        public static bool IsActive(Context ctx)
        {
            return GetUserName(ctx).Length > 0;
        }
    }
}