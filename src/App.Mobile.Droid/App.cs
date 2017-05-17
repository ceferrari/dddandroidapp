using Android.App;
using Android.Content;
using Android.Runtime;
using App.Infra.IoC;
using App.Mobile.Droid.Helpers;
using App.Mobile.Shared.Interfaces.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Views;
using App.Mobile.Droid.Activities;
using Plugin.Iconize.Droid.Controls;
using ActionBar = Android.Support.V7.App.ActionBar;

namespace App.Mobile.Droid
{
    [Application(LargeHeap = true)]
    public sealed class App : Android.App.Application
    {
        private const string DbName = "Precizzo.db";
        public static IServiceProvider Provider;
        public static AlertHelper AlertHelper;
        public static ToastHelper ToastHelper;

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
            
        }

        public override async void OnCreate()
        {
            base.OnCreate();

            await Task.WhenAll(
                RegisterServices(),
                RegisterMappings(),
                RegisterIconizePlugins(),
                CreateInstances()
            );
        }

        private async Task RegisterServices()
        {
            await Task.Run(() =>
            {
                var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var dbPath = System.IO.Path.Combine(dbFolder, DbName);

                Provider = Bootstrapper.RegisterServices(new ServiceCollection(), dbPath);
            });
        }

        private async Task RegisterMappings()
        {
            await Task.Run(() =>
            {
                AutoMapperConfig.RegisterMappings();
            });
        }

        private async Task RegisterIconizePlugins()
        {
            await Task.Run(() =>
            {
                Plugin.Iconize.Iconize
                    //.With(new Plugin.Iconize.Fonts.EntypoPlusModule())
                    .With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                    .With(new Plugin.Iconize.Fonts.IoniconsModule())
                    .With(new Plugin.Iconize.Fonts.MaterialModule());
                    //.With(new Plugin.Iconize.Fonts.MeteoconsModule())
                    //.With(new Plugin.Iconize.Fonts.SimpleLineIconsModule())
                    //.With(new Plugin.Iconize.Fonts.TypiconsModule())
                    //.With(new Plugin.Iconize.Fonts.WeatherIconsModule());
            });
        }

        private async Task CreateInstances()
        {
            await Task.Run(() =>
            {
                AlertHelper = new AlertHelper();
                ToastHelper = new ToastHelper();
            });
        }

        public static async Task StartActivity(Activity currentActivity, Type newActivityType, bool finishCurrent = false)
        {
            await Task.Run(() =>
            {
                if (currentActivity.GetType() == newActivityType)
                {
                    return;
                }

                var intent = new Intent(currentActivity, newActivityType);
                currentActivity.StartActivity(intent);
                if (finishCurrent)
                {
                    currentActivity.Finish();
                }
            });
        }

        public static async Task<MessageResult> Alert(Activity activity, string title, string message, bool cancelable = false, MessageResult positiveButton = MessageResult.Ok, MessageResult negativeButton = MessageResult.None, MessageResult neutralButton = MessageResult.None, int iconAttribute = 0)
        {
            AlertHelper.Activity = activity;
            return await AlertHelper.ShowDialog(title, message, cancelable, positiveButton, negativeButton, neutralButton, iconAttribute);
        }

        public static async Task<bool> Toast(Activity activity, string text, int duration = 0, ToastNotificationType type = ToastNotificationType.None)
        {
            ToastHelper.Activity = activity;
            return await ToastHelper.Show(text, duration, type);
        }
    }
}