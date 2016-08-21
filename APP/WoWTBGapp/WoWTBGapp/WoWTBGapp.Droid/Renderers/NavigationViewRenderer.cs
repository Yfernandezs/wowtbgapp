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
using Xamarin.Forms;
using WoWTBGapp.Droid;
using Xamarin.Forms.Platform.Android;
using WoWTBGapp.Utils;
using FormsToolkit;
using WoWTBGapp.Clients.Portable;
using Android.Support.Design.Widget;

[assembly: ExportRenderer(typeof(WoWTBGapp.Clients.UI.NavigationView), typeof(NavigationViewRenderer))]
namespace WoWTBGapp.Droid
{
    public class NavigationViewRenderer : ViewRenderer<WoWTBGapp.Clients.UI.NavigationView, NavigationView>
    {
        NavigationView navView;

        ImageView menuImage;

        ImageView profileImage;
        TextView profileName;

        IMenuItem previousItem;

        protected override void OnElementChanged(ElementChangedEventArgs<WoWTBGapp.Clients.UI.NavigationView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            
            var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
            navView = view.JavaCast<NavigationView>();


            navView.NavigationItemSelected += NavView_NavigationItemSelected;

            Settings.Current.PropertyChanged += SettingsPropertyChanged;
            SetNativeControl(navView);

            var header = navView.GetHeaderView(0);

            menuImage = header.FindViewById<ImageView>(Resource.Id.menu_image);
            profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);
            profileName = header.FindViewById<TextView>(Resource.Id.profile_name);

            profileImage.Click += (sender, e2) => NavigateToLogin();
            profileName.Click += (sender, e2) => NavigateToLogin();

            UpdateName();
            UpdateImage();

            navView.SetCheckedItem(Resource.Id.nav_character);
        }

        void NavigateToLogin()
        {
            if (Settings.Current.IsLoggedIn)
                return;

            WoWTBGapp.Clients.UI.App.Logger.TrackPage(AppPage.ItemCards.ToString(), "navigation");

            MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
        }

        void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Current.Email))
            {
                UpdateName();
                UpdateImage();
            }
        }

        void UpdateName()
        {
            profileName.Text = Settings.Current.UserDisplayName;
        }

        void UpdateImage()
        {
            Koush.UrlImageViewHelper.SetUrlDrawable(profileImage, Settings.Current.UserAvatar, Resource.Drawable.profile_generic);

            int resource = Resource.Drawable.landscape_04;

            int random = (new Random()).Next(2, 7);

            switch (random)
            {
                case 2:
                    resource = Resource.Drawable.landscape_02;
                    break;
                case 3:
                    resource = Resource.Drawable.landscape_03;
                    break;
                case 4:
                    resource = Resource.Drawable.landscape_04;
                    break;
                case 5:
                    resource = Resource.Drawable.landscape_05;
                    break;
                case 6:
                    resource = Resource.Drawable.landscape_06;
                    break;
                default:
                    resource = Resource.Drawable.landscape_01;
                    break;
            }

            menuImage.SetImageResource(resource);
        }

        public override void OnViewRemoved(Android.Views.View child)
        {
            base.OnViewRemoved(child);

            navView.NavigationItemSelected -= NavView_NavigationItemSelected;

            Settings.Current.PropertyChanged -= SettingsPropertyChanged;
        }

        void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (previousItem != null)
            {
                previousItem.SetChecked(false);
            }

            navView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            int id = 0;

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_character:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_players:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_quests:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_broker:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_game_info:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_about:
                    id = (int)AppPage.ItemCards;
                    break;
                case Resource.Id.nav_settings:
                    id = (int)AppPage.ItemCards;
                    break;
            }

            this.Element.OnNavigationItemSelected(new WoWTBGapp.Clients.UI.NavigationItemSelectedEventArgs
            {
                Index = id
            });
        }
    }
}