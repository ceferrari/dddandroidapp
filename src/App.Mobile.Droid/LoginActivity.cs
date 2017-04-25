using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Application.ViewModels;
using App.Mobile.Droid.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Iconize.Droid.Controls;
using System;
using System.Threading.Tasks;
using Android.Content.PM;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false, LaunchMode = LaunchMode.SingleTask)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Task.WhenAll(CheckSession());

            SetContentView(Resource.Layout.Login);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
            Initialize();
        }

        private async Task CheckSession()
        {
            if (SessionManager.IsActive(this))
            {
                await App.StartActivity(this, typeof(MainActivity), true);
                await Task.Delay(1000);
                await App.Toast(this, "Bem-vindo de volta!");
            }
        }

        private void Initialize()
        {
            const int iconSize = 24;
            var iconColor = Color.White;
            var icoEnvelope = new IconDrawable(this, "md-mail-outline").SizeDp(iconSize).Color(iconColor);
            var icoLock = new IconDrawable(this, "md-lock-outline").SizeDp(iconSize).Color(iconColor);

            var txtEmail = FindViewById<TextView>(Resource.Id.LoginEmail);
            txtEmail.SetCompoundDrawablesWithIntrinsicBounds(icoEnvelope, null, null, null);

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

            if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                await App.Toast(this, "Todos os campos devem ser preenchidos!");
                return;
            }

            var vm = new CustomerViewModel
            {
                Email = email.Text
            };

            var customerService = App.Provider.GetService<ICustomerService>();
            var customer = customerService.GetFirst(vm);
            if (customer != null)
            {
                await Task.WhenAll(
                    SessionManager.SingIn(this, customer),
                    App.StartActivity(this, typeof(MainActivity), true),
                    App.Toast(this, "Login efetuado com sucesso!")
                );
            }
            else
            {
                await App.Toast(this, "E-mail e/ou Senha inválido(s).");
            }
        }

        private async void SignUp(object sender, EventArgs args)
        {
            await App.StartActivity(this, typeof(RegisterActivity));
        }
    }
}