using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Iconize.Droid.Controls;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false)]
    public sealed class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
            Initialize();
        }

        private void Initialize()
        {
            const int iconSize = 24;
            var iconColor = Color.White;
            var icoPerson = new IconDrawable(this, "md-person-outline").SizeDp(iconSize).Color(iconColor);
            var icoEnvelope = new IconDrawable(this, "md-mail-outline").SizeDp(iconSize).Color(iconColor);
            var icoLock = new IconDrawable(this, "md-lock-outline").SizeDp(iconSize).Color(iconColor);

            var txtName = FindViewById<TextView>(Resource.Id.RegisterName);
            txtName.SetCompoundDrawablesWithIntrinsicBounds(icoPerson, null, null, null);

            var txtEmail = FindViewById<TextView>(Resource.Id.RegisterEmail);
            txtEmail.SetCompoundDrawablesWithIntrinsicBounds(icoEnvelope, null, null, null);

            var txtPassword = FindViewById<TextView>(Resource.Id.RegisterPassword);
            txtPassword.SetCompoundDrawablesWithIntrinsicBounds(icoLock, null, null, null);

            var txtConfirmPassword = FindViewById<TextView>(Resource.Id.RegisterConfirmPassword);
            txtConfirmPassword.SetCompoundDrawablesWithIntrinsicBounds(icoLock, null, null, null);

            var btnSave = FindViewById<Button>(Resource.Id.RegisterSave);
            btnSave.Click += Save;
        }

        private async void Save(object sender, EventArgs args)
        {
            var name = FindViewById<TextView>(Resource.Id.RegisterName);
            var email = FindViewById<TextView>(Resource.Id.RegisterEmail);
            var password = FindViewById<TextView>(Resource.Id.RegisterPassword);
            var confirmPassword = FindViewById<TextView>(Resource.Id.RegisterConfirmPassword);

            var customerViewModel = new CustomerViewModel
            {
                Id = Guid.NewGuid(),
                Name = name.Text,
                Email = email.Text,
                BirthDate = DateTime.UtcNow
            };

            var customerService = App.Provider.GetService<ICustomerService>();
            if (customerService.Create(customerViewModel))
            {
                await Task.WhenAll(
                    App.StartActivity(this, typeof(LoginActivity), true),
                    App.Toast(this, "Usuário registrado com sucesso!")
                );
            }
        }
    }
}