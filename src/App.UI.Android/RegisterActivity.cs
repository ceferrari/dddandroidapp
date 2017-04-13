using Android.App;
using Android.OS;

namespace App.UI.Android
{
    [Activity(Label = "App.UI.Android", MainLauncher = false, Icon = "@drawable/icon")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}