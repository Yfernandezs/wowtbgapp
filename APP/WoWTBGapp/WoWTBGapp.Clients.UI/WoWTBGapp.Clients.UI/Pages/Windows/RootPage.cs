using FormsToolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.Utils;
using Xamarin.Forms;
using MenuItem = WoWTBGapp.Clients.Portable.MenuItem;

namespace WoWTBGapp.Clients.UI
{
    public class RootPageWindows : MasterDetailPage
    {
        Dictionary<AppPage, Page> pages;

        MenuPageUWP menu;

        public static bool IsDesktop { get; set; }

        public RootPageWindows()
        {
            //MasterBehavior = MasterBehavior.Popover;
            pages = new Dictionary<AppPage, Page>();

            var items = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Character", Icon = "menu_info.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "Players", Icon = "menu_info.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "Quest", Icon = "menu_info.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "Broker", Icon = "menu_info.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "Game info", Icon = "menu_settings.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "About", Icon = "menu_settings.png", Page = AppPage.ItemCards },
                new MenuItem { Name = "Settings", Icon = "menu_settings.png", Page = AppPage.ItemCards }
            };

            menu = new MenuPageUWP();
            menu.MenuList.ItemsSource = items;


            menu.MenuList.ItemSelected += (sender, args) =>
            {
                if (menu.MenuList.SelectedItem == null)
                    return;

                Device.BeginInvokeOnMainThread(() =>
                {
                    NavigateAsync(((MenuItem)menu.MenuList.SelectedItem).Page);
                    if (!IsDesktop)
                        IsPresented = false;
                });
            };

            Master = menu;
            NavigateAsync(AppPage.ItemCards);
            Title = "WoWTBGapp";
        }
        
        public void NavigateAsync(AppPage menuId)
        {
            Page newPage = null;
            if (!pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    //case AppPage.Feed: //Feed
                    //    pages.Add(menuId, new EvolveNavigationPage(new FeedPage()));
                    //    break;
                    //case AppPage.Sessions://sessions
                    //    pages.Add(menuId, new EvolveNavigationPage(new SessionsPage()));
                    //    break;
                    //case AppPage.Events://events
                    //    pages.Add(menuId, new EvolveNavigationPage(new EventsPage()));
                    //    break;
                    //case AppPage.MiniHacks://Mini-Hacks
                    //    newPage = new EvolveNavigationPage(new MiniHacksPage());
                    //    break;
                    //case AppPage.Sponsors://sponsors
                    //    newPage = new EvolveNavigationPage(new SponsorsPage());
                    //    break;
                    //case AppPage.Evals: //venue
                    //    newPage = new EvolveNavigationPage(new EvaluationsPage());
                    //    break;
                    //case AppPage.Venue: //venue
                    //    newPage = new EvolveNavigationPage(new VenuePage());
                    //    break;
                    //case AppPage.ConferenceInfo://Conference info
                    //    newPage = new EvolveNavigationPage(new ConferenceInformationPage());
                    //    break;
                    //case AppPage.FloorMap://Floor Maps
                    //    newPage = new EvolveNavigationPage(new FloorMapsCarouselPage());
                    //    break;
                    //case AppPage.Settings://Settings
                    //    newPage = new EvolveNavigationPage(new SettingsPage());
                    //    break;
                    case AppPage.ItemCards:
                        newPage = new WoWTBGappNavigationPage(new ItemCardsView());
                        break;
                }
            }

            if (newPage == null)
                newPage = pages[menuId];

            if (newPage == null)
                return;

            Detail = newPage;
            //await Navigation.PushAsync(newPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
        }

    }
}
