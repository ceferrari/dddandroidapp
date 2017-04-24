using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using App.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using App.Mobile.Droid.Helpers;
using Object = Java.Lang.Object;

namespace App.Mobile.Droid.Adapters
{
    public class CustomersAdapter : BaseAdapter
    {
        private readonly Activity _activity;
        private readonly IEnumerable<CustomerViewModel> _customers;

        public override int Count { get; }

        public CustomersAdapter(Activity activity, IEnumerable<CustomerViewModel> customers)
        {
            _activity = activity;
            _customers = customers;
            Count = _customers.Count();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Object GetItem(int position)
        {
            return null;
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);

            //view.SetBackgroundResource(Resource.Drawable.back);

            view.SetBackgroundColor(SessionManager.GetUserId(_activity)
                .Equals(_customers.ElementAt(position).Id.ToString())
                ? Color.LightSkyBlue
                : Color.White);

            var text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = _customers.ElementAt(position).Name;
            text1.SetTextColor(Color.Black);

            var text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            text2.Text = _customers.ElementAt(position).Email;
            text2.SetTextColor(Color.Gray);

            return view;
        }
    }
}