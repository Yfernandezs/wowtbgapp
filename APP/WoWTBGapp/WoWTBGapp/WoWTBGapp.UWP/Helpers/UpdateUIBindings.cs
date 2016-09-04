using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.DataObjects;
using WoWTBGapp.UWP;
using Xamarin.Forms;
using Windows.UI.Xaml;
using Windows.UI.Core;

[assembly: Dependency(typeof(UpdateUIBindings))]
namespace WoWTBGapp.UWP
{
    public class UpdateUIBindings : IUpdateUIWindows
    {
        public async void UpdateItemCardsPageList(ObservableRangeCollection<Grouping<string, ItemCard>> CardsGrouped, IEnumerable<Grouping<string, ItemCard>> Cards)
        {
            var app = (Windows.UI.Xaml.Application.Current as App);

            if (app != null && app.CurrentWindow != null)
            {
                var currentWindow = app.CurrentWindow;

                await currentWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        // Your UI update code goes here!
                        CardsGrouped.ReplaceRange(Cards);
                    }
                );
            }
        }
    }
}
