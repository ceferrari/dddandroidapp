using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using App.Application.Interfaces;
using App.Mobile.Droid.Adapters;
using App.Mobile.Droid.Helpers;
using BarChart;
using Microsoft.Extensions.DependencyInjection;

namespace App.Mobile.Droid.Activities
{
    [Activity(MainLauncher = false, LaunchMode = LaunchMode.SingleTask, Theme = "@style/AppTheme")]
    public sealed class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.ActionBar);
            
            SetContentView(Resource.Layout.Main);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
            Initialize();

            if (savedInstanceState != null)
            {
                ActionBar.SelectTab(ActionBar.GetTabAt(savedInstanceState.GetInt("tab")));
            }
        }

        protected override void OnSaveInstanceState(Bundle savedInstanceState)
        {
            savedInstanceState.PutInt("tab", ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(savedInstanceState);
        }

        private void Initialize()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetDisplayShowTitleEnabled(false);
            AddTab("USUÁRIOS", new Users());
            AddTab("ANÚNCIOS", new Adverts());
        }

        private void AddTab(string tabText, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.FragmentContainer);
                if (fragment != null)
                {
                    e.FragmentTransaction.Remove(fragment);
                }
                e.FragmentTransaction.Add(Resource.Id.FragmentContainer, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) 
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }
    }

    public class Users : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.UsersFragment, container, false);

            var customerService = App.Provider.GetService<ICustomerService>();
            var customers = customerService.GetAll();

            var listView = view.FindViewById<ListView>(Resource.Id.MainList);
            listView.Adapter = new CustomersAdapter(Activity, customers);

            var btnSignOut = view.FindViewById<Button>(Resource.Id.MainSignOut);
            btnSignOut.Click += SignOut;

            return view;
        }

        private async void SignOut(object sender, EventArgs args)
        {
            await Task.WhenAll(
                SessionManager.SingOut(Activity),
                App.StartActivity(Activity, typeof(LoginActivity), true),
                App.Toast(Activity, "Logout efetuado com sucesso!")
            );
        }
    }

    public class Adverts : Fragment
    {
        private View _view;
        private readonly Random _random = new Random();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            _view = inflater.Inflate(Resource.Layout.AdvertsFragment, container, false);
            
            var btnRandom = _view.FindViewById<Button>(Resource.Id.AdvertsRandomize);
            btnRandom.Click += Randomize;
            btnRandom.PerformClick();

            return _view;
        }

        private void Randomize(object sender, EventArgs args)
        {
            var data = new[]
            {
                GetRandomNumber(), GetRandomNumber(), GetRandomNumber(), GetRandomNumber(), GetRandomNumber(), GetRandomNumber()
            };

            var chart = new BarChartView(Activity)
            {
                ItemsSource = Array.ConvertAll(data, v => new BarModel { Value = v }),
                BarColor = Color.LightSkyBlue,
                LegendHidden = true,
                Id = Resource.Id.AdvertsChartContainer
            };

            var chartContainer = _view.FindViewById(Resource.Id.AdvertsChartContainer);
            var parent = (ViewGroup)chartContainer.Parent;
            var index = parent.IndexOfChild(chartContainer);
            parent.RemoveView(chartContainer);
            parent.AddView(chart, index);
        }

        public int GetRandomNumber(int minimum = 10, int maximum = 90)
        {
            return _random.Next(minimum, maximum);
        }
    }
}