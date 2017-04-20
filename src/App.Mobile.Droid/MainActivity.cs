using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Mobile.Droid.Helpers;
using System;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false)]
    public class MainActivity : Activity
    {
        private ToastHelper _toast;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            _toast = new ToastHelper(this);

            var btnSignOut = FindViewById<Button>(Resource.Id.MainSignOut);
            btnSignOut.Click += SignOut;
        }

        private async void SignOut(object sender, EventArgs args)
        {
            SessionManager.SingOut(this);

            StartActivity(typeof(LoginActivity));

            await _toast.Show("Logout efetuado com sucesso!");
        }
    }
}