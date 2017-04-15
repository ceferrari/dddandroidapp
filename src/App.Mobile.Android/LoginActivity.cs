using System;
using Android.App;
using Android.OS;
using Android.Widget;
using App.Mobile.Android.Helpers;
using App.Mobile.Shared.Interfaces.Helpers;

namespace App.Mobile.Android
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class LoginActivity : Activity
    {
        private AlertHelper _alert;
        private ToastHelper _toast;
        private Button _entrar;
        private Button _registrar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            _alert = new AlertHelper(this);
            _toast = new ToastHelper(this);
            _entrar = FindViewById<Button>(Resource.Id.Entrar);
            _registrar = FindViewById<Button>(Resource.Id.Registrar);

            _entrar.Click += Login;
            _registrar.Click += Register;
        }

        private async void Login(object sender, EventArgs args)
        {
            StartActivity(typeof(MainActivity));

            await _toast.Show("Login efetuado com sucesso!");
        }

        private async void Register(object sender, EventArgs args)
        {
            StartActivity(typeof(RegisterActivity));

            if (await _alert.ShowDialog("Error", "Message", true, MessageResult.Yes, MessageResult.No) == MessageResult.Yes)
            {
                await _toast.Show("Login efetuado com sucesso!");
            }
        }
    }
}