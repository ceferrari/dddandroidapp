using Android.App;
using Android.OS;

namespace App.Mobile.Android
{
    [Activity(MainLauncher = false, Icon = "@drawable/icon", Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
        }
    }
}