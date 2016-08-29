using FormsToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataObjects;
using WoWTBGapp.Utils;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.Portable
{
    /// <summary>
    /// This Class helps to define a service that will help to update on the server the information when using it to favorite something on the app.
    /// </summary>
    public class FavoriteService
    {
        ItemCard sessionQueued;

        public FavoriteService()
        {
            MessagingService.Current.Subscribe(MessageKeys.LoggedIn, async (s) =>
            {
                if (sessionQueued == null)
                    return;

                await ToggleFavorite(sessionQueued);
            });
        }

        public async Task<bool> ToggleFavorite(ItemCard session)
        {
            if (!Settings.Current.IsLoggedIn)
            {
                sessionQueued = session;
                DependencyService.Get<ILogger>().TrackPage(AppPage.Login.ToString(), "Favorite");
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
                return false;
            }

            sessionQueued = null;

            //var store = DependencyService.Get<IFavoriteStore>();
            //session.IsFavorite = !session.IsFavorite;//switch first so UI updates :)
            //if (!session.IsFavorite)
            //{
            //    DependencyService.Get<ILogger>().Track(WoWTBGappLoggerKeys.FavoriteRemoved, "Title", session.Title);

            //    var items = await store.GetItemsAsync();
            //    foreach (var item in items.Where(s => s.SessionId == session.Id))
            //    {
            //        await store.RemoveAsync(item);
            //    }
            //}
            //else
            //{
            //    DependencyService.Get<ILogger>().Track(WoWTBGappLoggerKeys.FavoriteAdded, "Title", session.Title);
            //    await store.InsertAsync(new Favorite { SessionId = session.Id });
            //}

            //Settings.Current.LastFavoriteTime = DateTime.UtcNow;
            return true;
        }
    }
}
