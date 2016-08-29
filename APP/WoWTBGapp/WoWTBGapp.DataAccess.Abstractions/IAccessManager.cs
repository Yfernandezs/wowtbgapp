using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.DataAccess.Abstractions
{
    public interface IAccessManager
    {
        bool IsInitialized { get; }

        Task InitializeAsync();

        IItemCardAccess ItemCardAccess { get; }
        //IFavoriteStore FavoriteStore { get; }
        //IFeedbackStore FeedbackStore { get; }
        //ISessionStore SessionStore { get; }
        //ISpeakerStore SpeakerStore { get; }
        //ISponsorStore SponsorStore { get; }
        //IEventStore EventStore { get; }
        //IMiniHacksStore MiniHacksStore { get; }
        //INotificationStore NotificationStore { get; }


        Task<WebResponse> GetDataAsync(List<object> parameters, string APIMethod, int timeoutSeconds = 30);

        //Task<bool> SyncAllAsync(bool syncUserSpecific);

        //Task DropEverythingAsync();
    }
}
