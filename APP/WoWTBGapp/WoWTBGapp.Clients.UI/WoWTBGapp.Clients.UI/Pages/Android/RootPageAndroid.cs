using FormsToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.Utils;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    public class RootPageAndroid : MasterDetailPage
    {
        Dictionary<int, WoWTBGappNavigationPage> pages;

        DeepLinkPage page;

        bool isRunning = false;

        public RootPageAndroid()
        {
            pages = new Dictionary<int, WoWTBGappNavigationPage>();
            Master = new MenuPage(this);

            pages.Add(0, new WoWTBGappNavigationPage(new ItemCardsView()));

            Detail = pages[0];

            MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
            {
                page = p;

                if (isRunning)
                {
                    await GoToDeepLink();
                }
            });
        }



        public async Task NavigateAsync(int menuId)
        {
            WoWTBGappNavigationPage newPage = null;

            if (!pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    //case (int)AppPage.Feed: //Feed
                    //    pages.Add(menuId, new EvolveNavigationPage(new FeedPage()));
                    //    break;
                    //case (int)AppPage.Sessions://sessions
                    //    pages.Add(menuId, new EvolveNavigationPage(new SessionsPage()));
                    //    break;
                    //case (int)AppPage.Events://events
                    //    pages.Add(menuId, new EvolveNavigationPage(new EventsPage()));
                    //    break;
                    //case (int)AppPage.MiniHacks://Mini-Hacks
                    //    newPage = new EvolveNavigationPage(new MiniHacksPage());
                    //    break;
                    //case (int)AppPage.Sponsors://sponsors
                    //    newPage = new EvolveNavigationPage(new SponsorsPage());
                    //    break;
                    //case (int)AppPage.Venue: //venue
                    //    newPage = new EvolveNavigationPage(new VenuePage());
                    //    break;
                    //case (int)AppPage.ConferenceInfo://Conference info
                    //    newPage = new EvolveNavigationPage(new ConferenceInformationPage());
                    //    break;
                    //case (int)AppPage.FloorMap://Floor Maps
                    //    newPage = new EvolveNavigationPage(new FloorMapsCarouselPage());
                    //    break;
                    //case (int)AppPage.Settings://Settings
                    //    newPage = new EvolveNavigationPage(new SettingsPage());
                    //    break;
                    //case (int)AppPage.Evals:
                    //    newPage = new EvolveNavigationPage(new EvaluationsPage());
                    //    break;
                    case (int)AppPage.ItemCards:
                        newPage = new WoWTBGappNavigationPage(new ItemCardsView());
                        break;
                        
                }
            }

            if (newPage == null)
            {
                newPage = pages[menuId];
            }

            if (newPage == null)
                return;

            //if we are on the same tab and pressed it again.
            if (Detail == newPage)
            {
                await newPage.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }

            isRunning = true;

            await GoToDeepLink();

        }

        async Task GoToDeepLink()
        {
            if (page == null)
                return;

            var p = page.Page;
            var id = page.Id;

            page = null;

            switch (p)
            {

                //case AppPage.Session:
                //    await NavigateAsync((int)AppPage.Sessions);
                //    var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession(id);
                //    if (session == null)
                //        break;
                //    await Detail.Navigation.PushAsync(new SessionDetailsPage(session));
                //    break;
            }

        }
    }
}
