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
                    Icon = "Icons/toolbar_refresh.png",
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

                // Show toast about the card.
                //ViewModel.ItemCardSelectedCommand.Execute(card);

                var itemCardDetails = new ItemCardDetailsPage();

                itemCardDetails.Card = card;
                itemCardDetails.LoadCounterImageData(ViewModel.requirementImageData);
                App.Logger.TrackPage(AppPage.ItemCardDetails.ToString(), card.Name);
                await NavigationService.PushAsync(Navigation, itemCardDetails);

                ItemCardsList.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.Cards.Count == 0)
            {
                //ViewModel.LoadRequirementImageDataCommand.Execute(true);

                ViewModel.LoadCardsCommand.Execute(false);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
