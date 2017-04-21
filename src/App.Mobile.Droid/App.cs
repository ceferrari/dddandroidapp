using Android.App;
using Android.Graphics;
using Android.Runtime;
using App.Infra.IoC;
using App.Mobile.Droid.Helpers;
using App.Mobile.Shared.Interfaces.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Android.Support.V4.Graphics.Drawable;

namespace App.Mobile.Droid
{
    [Application(Icon = "@drawable/logo_200x200_rounded", Label = "Precizzo", Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public sealed class App : Android.App.Application
    {
        private const string DbName = "Precizzo.db";
        public static IServiceProvider Provider;
        //public static RoundedBitmapDrawable RoundedLogo;

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
            
        }

        public override void OnCreate()
        {
            AutoMapperConfig.RegisterMappings();

            RegisterServices();
            
            base.OnCreate();

            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.EntypoPlusModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                .With(new Plugin.Iconize.Fonts.IoniconsModule())
                .With(new Plugin.Iconize.Fonts.MaterialModule())
                .With(new Plugin.Iconize.Fonts.MeteoconsModule())
                .With(new Plugin.Iconize.Fonts.SimpleLineIconsModule())
                .With(new Plugin.Iconize.Fonts.TypiconsModule())
                .With(new Plugin.Iconize.Fonts.WeatherIconsModule());

            //var bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.logo_200x200);
            //RoundedLogo = RoundedBitmapDrawableFactory.Create(Resources, bitmap);
            //RoundedLogo.Circular = true;
            //RoundedLogo.SetAntiAlias(true);
        }

        private void RegisterServices()
        {
            var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = System.IO.Path.Combine(dbFolder, DbName);

            var services = new ServiceCollection();

            services.AddTransient<IAlertHelper, AlertHelper>();
            services.AddTransient<IToastHelper, ToastHelper>();

            Provider = Bootstrapper.RegisterServices(services, dbPath);
        }
    }
}