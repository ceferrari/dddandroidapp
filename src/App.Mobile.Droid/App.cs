using Android.App;
using Android.Graphics;
using Android.Runtime;
using App.Infra.IoC;
using App.Mobile.Droid.Helpers;
using App.Mobile.Shared.Interfaces.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Mobile.Droid
{
    [Application(Icon = "@drawable/logo_200x200", Label = "Precizzo", Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public sealed class App : Android.App.Application
    {
        private const string DbName = "Precizzo.db";
        private readonly string _dbPath;
        public static IServiceProvider Provider;
        
        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
            var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _dbPath = System.IO.Path.Combine(dbFolder, DbName);
        }

        public override void OnCreate()
        {
            AutoMapperConfig.RegisterMappings();

            RegisterServices();

            base.OnCreate();
        }

        private void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IAlertHelper, AlertHelper>();
            services.AddTransient<IToastHelper, ToastHelper>();

            Provider = Bootstrapper.RegisterServices(services, _dbPath);
        }
    }
}