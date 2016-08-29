using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    public partial class WoWTBGGroupHeaderView : ContentView
    {
        public WoWTBGGroupHeaderView()
        {
            InitializeComponent();
        }
    }


    public class WoWTBGGroupHeader : ViewCell
    {
        public WoWTBGGroupHeader()
        {
            View = new WoWTBGGroupHeaderView();
        }
    }
}
