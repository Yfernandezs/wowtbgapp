using Newtonsoft.Json;
using FormsToolkit;
using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.DataObjects;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    public partial class ItemCardsView : ContentPage
    {
        ItemCardsViewModel vm;
        ItemCardsViewModel ViewModel => vm ?? (vm = BindingContext as ItemCardsViewModel);

        public ItemCardsView()
        {
            InitializeComponent();

            BindingContext = new ItemCardsViewModel(Navigation);

            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Refresh",
                    Icon = "toolbar_refresh.png",
                    Command = ViewModel.ForceRefreshCommand
                });
            }

            ItemCardsList.ItemTapped += (sender, e) => ItemCardsList.SelectedItem = null;

            ItemCardsList.ItemSelected += async (sender, e) =>
            {
                var card = ItemCardsList.SelectedItem as ItemCard;

                if (card == null)
                {
                    return;
                }

                ViewModel.ItemCardSelectedCommand.Execute(card);

                //var eventDetails = new EventDetailsPage();

                //eventDetails.Event = ev;
                //App.Logger.TrackPage(AppPage.Event.ToString(), ev.Title);
                //await NavigationService.PushAsync(Navigation, eventDetails);

                ItemCardsList.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.Cards.Count == 0)
            {
                ViewModel.LoadCardsCommand.Execute(false);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
