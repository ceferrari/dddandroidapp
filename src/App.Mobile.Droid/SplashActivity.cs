using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/SplashTheme")]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryLauncher })]
    public sealed class SplashActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            await Task.WhenAll(
                App.StartActivity(this, typeof(LoginActivity), true),
                AddShortcut()
            );
        }

        private async Task AddShortcut()
        {
            await Task.Run(() =>
            {
                const string prefName = "PREF_APP_FIRST_START";
                const string prefKey = "PREF_KEY_SHORTCUT_ADDED";

                var sharedPreferences = GetSharedPreferences(prefName, FileCreationMode.Private);
                if (sharedPreferences.GetBoolean(prefKey, false))
                {
                    return;
                }

                var shortcutIntent = new Intent(this, typeof(SplashActivity));
                shortcutIntent.AddFlags(ActivityFlags.NewTask);
                shortcutIntent.AddFlags(ActivityFlags.ClearTop);

                var icon = Intent.ShortcutIconResource.FromContext(this, Resource.Drawable.logo_200x200_rounded);

                var addIntent = new Intent();
                addIntent.PutExtra(Intent.ExtraShortcutIntent, shortcutIntent);
                addIntent.PutExtra(Intent.ExtraShortcutName, "Precizzo");
                addIntent.PutExtra(Intent.ExtraShortcutIconResource, icon);
                addIntent.SetAction("com.android.launcher.action.INSTALL_SHORTCUT");
                SendBroadcast(addIntent);

                var editor = sharedPreferences.Edit();
                editor.PutBoolean(prefKey, true);
                editor.Commit();
            });
        }
    }
}