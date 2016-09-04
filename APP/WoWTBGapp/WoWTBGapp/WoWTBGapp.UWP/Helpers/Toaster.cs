using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toaster))]
namespace WoWTBGapp.UWP
{ 
    public class Toaster : IToast
    {
        public void SendToast(string message, int length)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            });
        }
    }
}
