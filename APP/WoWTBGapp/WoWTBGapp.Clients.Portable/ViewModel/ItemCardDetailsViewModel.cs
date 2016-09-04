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
    public class ItemCardDetailsViewModel : ViewModelBase
    {
        public ItemCard Card { get; set; }

        public ObservableRangeCollection<Counter> Counters { get; set; }

        public ObservableRangeCollection<CardEffect> Effects { get; set; }

        public ItemCardDetailsViewModel(INavigation navigation, ItemCard card) : base(navigation)
        {
            Card = card;

            Counters = new ObservableRangeCollection<Counter>();

            Effects = new ObservableRangeCollection<CardEffect>();

            if(Card.Effects != null)
            {
                foreach( string effect in Card.Effects)
                {
                    Effects.Add(new CardEffect() { Detail = effect });
                }
            }

            //LoadRequirementImageDataCommand.Execute(true);
        }

        //ICommand loadRequirementImageDataCommand;
        //public ICommand LoadRequirementImageDataCommand =>
        //    loadRequirementImageDataCommand ?? (loadRequirementImageDataCommand = new Command<bool>(async (f) => await ExecuteLoadRequirementImageDataAsync()));

        //async Task<bool> ExecuteLoadRequirementImageDataAsync(bool force = false)
        //{
        //    if (IsBusy)
        //        return false;

        //    try
        //    {
        //        IsBusy = true;

        //        Counters.Clear();

        //        var requirements = await DataAccessManager.RequirementImageAccess.GetItemsAsync(force).ConfigureAwait(false);

        //        foreach (Counter requirement in Card.Requirements)
        //        {
        //            var newRequirement = new Counter();

        //            newRequirement.Name = requirement.Name;
        //            newRequirement.Value = requirement.Value;

        //            var requirementData = requirements.ToList().Find(x => x.Name.Equals(requirement.Name) && (x.Value == -1 || x.Value == requirement.Value));

        //            if (requirementData != null)
        //            {
        //                newRequirement.Name = requirementData.ImageURL;
        //            }

        //            Counters.Add(newRequirement);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Report(ex, "Method", "ExecuteLoadRequirementImageDataAsync");
        //        MessagingService.Current.SendMessage(MessageKeys.Error, ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }

        //    return true;
        //}

        ICommand itemCardEffectSelectedCommand;
        public ICommand ItemCardEffectSelectedCommand =>
        itemCardEffectSelectedCommand ?? (itemCardEffectSelectedCommand = new Command<CardEffect>(async (t) => await ItemCardEffectSelectedCommandAsync(t)));

        async Task ItemCardEffectSelectedCommandAsync(CardEffect effect)
        {
            Toast.SendToast(effect.Detail, 1);
        }

    }
}
