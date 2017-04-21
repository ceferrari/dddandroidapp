using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Application.ViewModels;
using App.Mobile.Droid.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Android.Graphics;
using Plugin.Iconize.Droid.Controls;
using Plugin.Iconize.Fonts;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false)]
    public class LoginActivity : Activity
    {
        private AlertHelper _alert;
        private ToastHelper _toast;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (SessionManager.IsActive(this))
            {
                StartActivity(typeof(MainActivity));
                return;
            }

            SetContentView(Resource.Layout.Login);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            _alert = new AlertHelper(this);
            _toast = new ToastHelper(this);

            var icoEnvelope = new IconDrawable(this, "ion-ios-email-outline")
                .SizeDp(24)
                .Color(Color.Gray);

            var txtEmail = FindViewById<TextView>(Resource.Id.LoginEmail);
            txtEmail.SetCompoundDrawablesWithIntrinsicBounds(icoEnvelope, null, null, null);

            var icoLock = new IconDrawable(this, "ion-ios-locked-outline")
                .SizeDp(24)
                .Color(Color.Gray);

            var txtPassword = FindViewById<TextView>(Resource.Id.LoginPassword);
            txtPassword.SetCompoundDrawablesWithIntrinsicBounds(icoLock, null, null, null);

            var btnSignIn = FindViewById<Button>(Resource.Id.LoginSignIn);
            btnSignIn.Click += SignIn;

            var btnSignUp = FindViewById<Button>(Resource.Id.LoginSignUp);
            btnSignUp.Click += SignUp;
        }

        private async void SignIn(object sender, EventArgs args)
        {
            var email = FindViewById<TextView>(Resource.Id.LoginEmail);
            var password = FindViewById<TextView>(Resource.Id.LoginPassword);

            var customerViewModel = new CustomerViewModel
            {
                Email = email.Text
            };

            var customerService = App.Provider.GetService<ICustomerService>();
            if (customerService.Exists(customerViewModel))
            {
                StartActivity(typeof(MainActivity));
                await _toast.Show("Login efetuado com sucesso!");
            }
            else
            {
                await _toast.Show("E-mail e/ou Senha inválido(s).");
            }
        }

        private void SignUp(object sender, EventArgs args)
        {
            StartActivity(typeof(RegisterActivity));

            //if (await _alert.ShowDialog("Error", "Message", true, MessageResult.Yes, MessageResult.No) == MessageResult.Yes)
            //{
            //    await _toast.Show("Login efetuado com sucesso!");
            //}
        }
    }
}