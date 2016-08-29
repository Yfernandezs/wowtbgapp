using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoWTBGapp.Clients.Portable;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using WoWTBGapp.Droid;

[assembly: Dependency(typeof(Toaster))]
namespace WoWTBGapp.Droid
{
    public class Toaster : IToast
    {
        public void SendToast(string message, int length)
        {
            var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;

            Device.BeginInvokeOnMainThread(() =>
            {
                Toast.MakeText(context, message, length == 0 ? ToastLength.Short : ToastLength.Long).Show();
            });

        }
    }
}