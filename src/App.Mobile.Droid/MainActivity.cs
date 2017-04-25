using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Mobile.Droid.Adapters;
using App.Mobile.Droid.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Android.Content.PM;

namespace App.Mobile.Droid
{
    [Activity(MainLauncher = false, LaunchMode = LaunchMode.SingleTask)]
    public sealed class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
            Initialize();
        }

        private void Initialize()
        {
            var customerService = App.Provider.GetService<ICustomerService>();
            var customers = customerService.GetAll();

            var listView = FindViewById<ListView>(Resource.Id.MainList);
            listView.Adapter = new CustomersAdapter(this, customers);

            var btnSignOut = FindViewById<Button>(Resource.Id.MainSignOut);
            btnSignOut.Click += SignOut;
        }

        private async void SignOut(object sender, EventArgs args)
        {
            await Task.WhenAll(
                SessionManager.SingOut(this),
                App.StartActivity(this, typeof(LoginActivity), true),
                App.Toast(this, "Logout efetuado com sucesso!")
            );
        }
    }
}