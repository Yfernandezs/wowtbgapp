using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.Utils
{
    /// <summary>
    /// This Class helps to define a set of keys to record in regars to the actions the user is taking on the app and navigation itself.
    /// </summary>
    public static class MessageKeys
    {
        public const string NavigateToCharacter = "navigate_character";
        public const string NavigateToItemCard = "navigate_itemcard";
        public const string NavigateToPlayers = "navigate_players";
        public const string NavigateToQuests = "navigate_quests";
        public const string NavigateToBroker = "navigate_broker";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
    }
}
