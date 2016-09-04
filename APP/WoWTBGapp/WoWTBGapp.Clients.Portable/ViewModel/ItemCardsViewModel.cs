using FormsToolkit;
using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoWTBGapp.DataObjects;
using WoWTBGapp.Utils;

using Xamarin.Forms;

namespace WoWTBGapp.Clients.Portable
{
    public class ItemCardsViewModel : ViewModelBase
    {
        public ItemCardsViewModel(INavigation navigation) : base(navigation)
        {
            Title = "Item Cards";
        }
        
#region Properties

        public ObservableRangeCollection<ItemCard> Cards { get; } = new ObservableRangeCollection<ItemCard>();
        public ObservableRangeCollection<Grouping<string, ItemCard>> CardsGrouped { get; } = new ObservableRangeCollection<Grouping<string, ItemCard>>();

        public List<RequirementImageData> requirementImageData;

        ItemCard selectedCard;

        public ItemCard SelectedCard
        {
            get { return SelectedCard; }
            set
            {
                selectedCard = value;

                OnPropertyChanged();

                if (selectedCard == null)
                {
                    return;
                }

                MessagingService.Current.SendMessage(MessageKeys.NavigateToItemCard, selectedCard);

                SelectedCard = null;
            }
        }

        bool groupingEnabled = true;

        public bool GroupingEnabled
        {
            get { return groupingEnabled; }
            set
            {
                groupingEnabled = value;

                OnPropertyChanged();
            }
        }

#endregion Properties


#region Sorting


        void SortCards()
        {
            var cards = Cards.GroupByPrimaryType();
            
            if (Device.OS != TargetPlatform.Windows)
            {
                CardsGrouped.ReplaceRange(cards);
            }
            else
            {
                if (WinUIUpdater != null)
                {
                    WinUIUpdater.UpdateItemCardsPageList(CardsGrouped, cards);
                }
            }
        }


        #endregion Sorting


        #region Commands

        ICommand itemCardSelectedCommand;
        public ICommand ItemCardSelectedCommand =>
        itemCardSelectedCommand ?? (itemCardSelectedCommand = new Command<ItemCard>(async (t) => await ItemCardSelectedCommandAsync(t)));

        async Task ItemCardSelectedCommandAsync(ItemCard card)
        {
            Toast.SendToast("Item Card: " + card.Name, 0);
        }

        ICommand forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync()));

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadCardsAsync(true);
        }

        ICommand loadCardsCommand;
        public ICommand LoadCardsCommand =>
            loadCardsCommand ?? (loadCardsCommand = new Command<bool>(async (f) => await ExecuteLoadCardsAsync()));

        async Task<bool> ExecuteLoadCardsAsync(bool force = false)
        {
            if (IsBusy)
                return false;

            try
            {
                IsBusy = true;
                
                var itemCards = await DataAccessManager.ItemCardAccess.GetItemsAsync(force).ConfigureAwait(false);

                Cards.ReplaceRange(itemCards);

                SortCards();
            }
            catch (Exception ex)
            {
                Logger.Report(ex, "Method", "ExecuteLoadCardsAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }

        ICommand loadRequirementImageDataCommand;
        public ICommand LoadRequirementImageDataCommand =>
            loadRequirementImageDataCommand ?? (loadRequirementImageDataCommand = new Command<bool>(async (f) => await ExecuteLoadRequirementImageDataAsync()));

        async Task<bool> ExecuteLoadRequirementImageDataAsync(bool force = false)
        {
            try
            {
                var requirements = await DataAccessManager.RequirementImageAccess.GetItemsAsync(force).ConfigureAwait(false);

                requirementImageData = new List<RequirementImageData>(requirements);
            }
            catch (Exception ex)
            {
                Logger.Report(ex, "Method", "ExecuteLoadRequirementImageDataAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }

            return true;
        }

#endregion Commands

    }
}
