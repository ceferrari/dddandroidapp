using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Application.ViewModels;
using App.Mobile.Droid.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false)]
    public sealed class RegisterActivity : Activity
    {
        private AlertHelper _alert;
        private ToastHelper _toast;
        private Button _save;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            _alert = new AlertHelper(this);
            _toast = new ToastHelper(this);
            _save = FindViewById<Button>(Resource.Id.RegisterSave);

            _save.Click += Save;
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

            //using (var context = App.Provider.GetService<UnitOfWork>())
            //{
            //    foreach (var contextCustomer in context.Customers)
            //    {
            //        Console.WriteLine(contextCustomer.Id);
            //    }
            //}

            var customerService = App.Provider.GetService<ICustomerService>();
            if (customerService.Create(customerViewModel))
            {
                await _toast.Show("Usuário registrado com sucesso!");
            }
        }
    }
}