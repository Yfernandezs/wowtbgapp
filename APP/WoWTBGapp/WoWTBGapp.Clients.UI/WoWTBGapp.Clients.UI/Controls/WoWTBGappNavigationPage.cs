using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    public class WoWTBGappNavigationPage : NavigationPage
    {
        public WoWTBGappNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public WoWTBGappNavigationPage()
        {
            Init();
        }

        void Init()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                BarBackgroundColor = Color.FromHex("FAFAFA");
            }
            else
            {
                BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
                BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            }
        }
    }
}
