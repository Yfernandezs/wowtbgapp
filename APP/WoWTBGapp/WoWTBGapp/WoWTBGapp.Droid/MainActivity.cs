
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.AppIndexing;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using FormsToolkit;
using FormsToolkit.Droid;
using Plugin.Permissions;
using Refractored.XamForms.PullToRefresh.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.Clients.UI;
using WoWTBGapp.Utils;
using Xamarin.Forms.Platform.Android.AppLinks;
using Xamarin;
using ImageCircle.Forms.Plugin.Droid;

namespace WoWTBGapp.Droid
{
    [Activity(Label = "WoWTBGapp", Icon = "@drawable/logoWoW", LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private static MainActivity current;
        public static MainActivity Current { get { return current; } }

        GoogleApiClient client;

        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            AndroidAppLinks.Init(this);
            Toolkit.Init();
            
            PullToRefreshLayoutRenderer.Init();
            typeof(Color).GetProperty("Accent", BindingFlags.Public | BindingFlags.Static).SetValue(null, Color.FromHex("#757575"));

            ImageCircleRenderer.Init(); // Inicializamos primero el paquete del control plugin de CircleImage.

            InitializeHockeyApp();

            LoadApplication(new App());

            //if (!Settings.Current.PushNotificationsEnabled)
            //    return;

            //DataRefreshService.ScheduleRefresh(this);

        }

        void InitializeHockeyApp()
        {
            if (string.IsNullOrWhiteSpace(ApiKeys.HockeyAppAndroid) || ApiKeys.HockeyAppAndroid == nameof(ApiKeys.HockeyAppAndroid))
            {
                return;
            }
            
            HockeyApp.Android.CrashManager.Register(this, ApiKeys.HockeyAppAndroid);
            //HockeyApp.UpdateManager.Register(this, ApiKeys.HockeyAppAndroid);

            HockeyApp.Android.Metrics.MetricsManager.Register(this, Application, ApiKeys.HockeyAppAndroid);

        }

    }
}

