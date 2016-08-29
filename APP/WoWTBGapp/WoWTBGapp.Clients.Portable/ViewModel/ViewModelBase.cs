using MvvmHelpers;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoWTBGapp.DataAccess.Abstractions;
using WoWTBGapp.DataObjects;
using WoWTBGapp.Utils;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.Portable
{
    public class ViewModelBase : BaseViewModel
    {
        protected INavigation Navigation { get; }
    

        public ViewModelBase(INavigation navigation = null)
        {
            Navigation = navigation;
        }

        public static void Init(bool mock = true)
        {

#if ENABLE_TEST_CLOUD && !DEBUG
                DependencyService.Register<ISessionStore, XamarinEvolve.DataStore.Mock.SessionStore>();
                DependencyService.Register<IFavoriteStore, XamarinEvolve.DataStore.Mock.FavoriteStore>();
                DependencyService.Register<IFeedbackStore, XamarinEvolve.DataStore.Mock.FeedbackStore>();
                DependencyService.Register<ISpeakerStore, XamarinEvolve.DataStore.Mock.SpeakerStore>();
                DependencyService.Register<ISponsorStore, XamarinEvolve.DataStore.Mock.SponsorStore>();
                DependencyService.Register<ICategoryStore, XamarinEvolve.DataStore.Mock.CategoryStore>();
                DependencyService.Register<IEventStore, XamarinEvolve.DataStore.Mock.EventStore>();
                DependencyService.Register<INotificationStore, XamarinEvolve.DataStore.Mock.NotificationStore>();
                DependencyService.Register<IMiniHacksStore, XamarinEvolve.DataStore.Mock.MiniHacksStore>();
                DependencyService.Register<ISSOClient, XamarinEvolve.Clients.Portable.Auth.XamarinSSOClient>();
                DependencyService.Register<IStoreManager, XamarinEvolve.DataStore.Mock.StoreManager>();
#else
            if (mock)
            {
                //DependencyService.Register<ISessionStore, XamarinEvolve.DataStore.Mock.SessionStore>();
                //DependencyService.Register<IFavoriteStore, XamarinEvolve.DataStore.Mock.FavoriteStore>();
                //DependencyService.Register<IFeedbackStore, XamarinEvolve.DataStore.Mock.FeedbackStore>();
                //DependencyService.Register<ISpeakerStore, XamarinEvolve.DataStore.Mock.SpeakerStore>();
                //DependencyService.Register<ISponsorStore, XamarinEvolve.DataStore.Mock.SponsorStore>();
                //DependencyService.Register<ICategoryStore, XamarinEvolve.DataStore.Mock.CategoryStore>();
                //DependencyService.Register<IEventStore, XamarinEvolve.DataStore.Mock.EventStore>();
                //DependencyService.Register<INotificationStore, XamarinEvolve.DataStore.Mock.NotificationStore>();
                //DependencyService.Register<IMiniHacksStore, XamarinEvolve.DataStore.Mock.MiniHacksStore>();
                //DependencyService.Register<ISSOClient, XamarinEvolve.Clients.Portable.Auth.XamarinSSOClient>();
                //DependencyService.Register<IStoreManager, XamarinEvolve.DataStore.Mock.StoreManager>();
            }
            else
            {
                DependencyService.Register<IItemCardAccess, WoWTBGapp.DataAccess.Nodejs.ItemCardAccess>();

                DependencyService.Register<IAccessManager, WoWTBGapp.DataAccess.Nodejs.NodejsAccessManager>();
            }
#endif
            
            //DependencyService.Register<FavoriteService>();
        }


        protected ILogger Logger { get; } = DependencyService.Get<ILogger>();
        protected IAccessManager DataAccessManager { get; } = DependencyService.Get<IAccessManager>();
        protected IToast Toast { get; } = DependencyService.Get<IToast>();

        //protected FavoriteService FavoriteService { get; } = DependencyService.Get<FavoriteService>();


        public Settings Settings
        {
            get { return Settings.Current; }
        }


        ICommand itemCardSelectedCommand;
        public ICommand ItemCardSelectedCommand =>
        itemCardSelectedCommand ?? (itemCardSelectedCommand = new Command<ItemCard>(async (t) => await ItemCardSelectedCommandAsync(t)));

        async Task ItemCardSelectedCommandAsync(ItemCard card)
        {
            Toast.SendToast("Item Card: " + card.Name, 0);
        }

        ICommand launchBrowserCommand;
        public ICommand LaunchBrowserCommand =>
        launchBrowserCommand ?? (launchBrowserCommand = new Command<string>(async (t) => await ExecuteLaunchBrowserAsync(t)));

        async Task ExecuteLaunchBrowserAsync(string arg)
        {
            if (IsBusy)
                return;

            if (!arg.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !arg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                arg = "http://" + arg;

            Logger.Track(WoWTBGappLoggerKeys.LaunchedBrowser, "Url", arg);

            var lower = arg.ToLowerInvariant();

            //if (Device.OS == TargetPlatform.iOS && lower.Contains("twitter.com"))
            //{
            //    try
            //    {
            //        var id = arg.Substring(lower.LastIndexOf("/", StringComparison.Ordinal) + 1);
            //        var launchTwitter = DependencyService.Get<ILaunchTwitter>();
            //        if (lower.Contains("/status/"))
            //        {
            //            //status
            //            if (launchTwitter.OpenStatus(id))
            //                return;
            //        }
            //        else
            //        {
            //            //user
            //            if (launchTwitter.OpenUserName(id))
            //                return;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            try
            {
            
                await CrossShare.Current.OpenBrowser(arg, new BrowserOptions
                {
                    ChromeShowTitle = true,
                    ChromeToolbarColor = new ShareColor  // Primary color (#4CAF50) == rgba (76, 175, 80, 1)
                    {
                        A = 1,
                        R = 76,
                        G = 175,
                        B = 80
                    },
                    UseSafairReaderMode = true,
                    UseSafariWebViewController = true
                });
            }
            catch
            {
            }
        }
    }
}
