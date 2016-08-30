using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.Clients.Portable
{
    /// <summary>
    /// This Enum help to define all the Views the application has, so it can help with deeplinking and navigation as a whole.
    /// </summary>
    public enum AppPage
    {
        //Feed,
        //Sessions,
        //Events,
        //MiniHacks,
        //Sponsors,
        //Venue,
        //FloorMap,
        //ConferenceInfo,
        //Session,
        //Speaker,
        //Sponsor,
        //Event,
        //Notification,
        //TweetImage,
        //WiFi,
        //CodeOfConduct,
        //Filter,
        //Tweet,
        //Evals,

        #region WoWTBGapp

        Login,
        Settings,
        Information,

        Characters,
        GameInfo,
        ItemCards,
        ItemCardDetails

        #endregion WoWTBGapp

    }
}
