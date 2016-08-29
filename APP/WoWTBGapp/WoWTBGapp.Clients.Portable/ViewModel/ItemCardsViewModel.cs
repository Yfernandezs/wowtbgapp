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

        #endregion Properties


        #region Sorting


        void SortCards()
        {
            CardsGrouped.ReplaceRange(Cards.GroupByPrimaryType());
        }


        #endregion Sorting


        #region Commands

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

        #endregion Commands

    }
}
