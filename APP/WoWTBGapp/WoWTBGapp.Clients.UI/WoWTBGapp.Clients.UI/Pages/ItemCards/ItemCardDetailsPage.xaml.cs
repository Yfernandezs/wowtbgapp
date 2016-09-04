using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.Clients.Portable;
using WoWTBGapp.DataObjects;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    public partial class ItemCardDetailsPage : ContentPage
    {
        ItemCardDetailsViewModel vm;
        ItemCardDetailsViewModel ViewModel => vm ?? (vm = BindingContext as ItemCardDetailsViewModel);

        public ItemCard Card
        {
            get { return ViewModel.Card; }
            set { BindingContext = new ItemCardDetailsViewModel(Navigation, value); }
        }

        public ItemCardDetailsPage()
        {
            InitializeComponent();
            
            ListViewCounters.ItemSelected += (sender, e) =>
            {
                ListViewCounters.SelectedItem = null;
            };

            ListViewEffects.ItemSelected += async (sender, e) =>
            {
                var effect = ListViewEffects.SelectedItem as CardEffect;

                if (effect == null)
                {
                    return;
                }

                // Show toast about the Effect.
                ViewModel.ItemCardEffectSelectedCommand.Execute(effect);

                ListViewEffects.SelectedItem = null;
            };
        }

        public void LoadCounterImageData(List<RequirementImageData> requirements)
        {
            if (requirements != null)
            {
                List<Counter> counters = new List<Counter>();

                foreach (Counter requirement in Card.Requirements)
                {
                    var newRequirement = new Counter();

                    newRequirement.Name = requirement.Name;
                    newRequirement.Value = requirement.Value;

                    var requirementData = requirements.ToList().Find(x => x.Name.Equals(requirement.Name) && (x.Value == -1 || x.Value == requirement.Value));

                    if (requirementData != null)
                    {
                        newRequirement.Name = requirementData.ImageURL;
                    }

                    counters.Add(newRequirement);
                    //ViewModel.Counters.Add(newRequirement);
                }

                ViewModel.Counters.AddRange(counters);

                //var adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.Counters.Count + 1;
                //ListViewCounters.HeightRequest = (ViewModel.Counters.Count * ListViewCounters.RowHeight) - adjust;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = null;

            //var adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.Counters.Count + 1;
            //ListViewCounters.HeightRequest = (ViewModel.Counters.Count * ListViewCounters.RowHeight) - adjust;


            //adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.Effects.Count + 1;
            //ListViewEffects.HeightRequest = (ViewModel.Effects.Count * ListViewEffects.RowHeight) - adjust;

            //ViewModel.Counters.CollectionChanged += (sender, e) =>
            //{
            //    adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.Counters.Count + 1;
            //    ListViewCounters.HeightRequest = (ViewModel.Counters.Count * ListViewCounters.RowHeight) - adjust;
            //};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var adjust = Device.OS != TargetPlatform.Android ? -1 : -ViewModel.Counters.Count + 1;
            ListViewCounters.HeightRequest = (ViewModel.Counters.Count * ListViewCounters.RowHeight) + adjust;


            adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.Effects.Count + 1;
            ListViewEffects.HeightRequest = (ViewModel.Effects.Count * ListViewEffects.RowHeight) + adjust;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
