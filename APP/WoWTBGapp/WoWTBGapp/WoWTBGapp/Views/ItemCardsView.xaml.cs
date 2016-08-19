using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wowtbgapp.api.Comunicacion;
using wowtbgapp.api.Models;
using Xamarin.Forms;

namespace WoWTBGapp.Views
{
    public partial class ItemCardsView : ContentPage, INotifyPropertyChanged
    {

        public ObservableCollection<ItemCard> ItemCards = new ObservableCollection<ItemCard>();

        private bool isNotBusy = true;

        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set
            {
                isNotBusy = value;

                this.OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public ItemCardsView()
        {
            InitializeComponent();

            // Requerido para hacerle saber a Xamarin.Forms que si contexto Binding es esta pantalla del código.
            BindingContext = this;

            ItemCardsList.ItemTapped += (sender, e) => ItemCardsList.SelectedItem = null;

            ItemCardsList.ItemSelected += async (sender, e) =>
            {
                var card = ItemCardsList.SelectedItem as ItemCard;
                if (card == null)
                    return;

                //var eventDetails = new EventDetailsPage();

                //eventDetails.Event = ev;
                //App.Logger.TrackPage(AppPage.Event.ToString(), ev.Title);
                //await NavigationService.PushAsync(Navigation, eventDetails);

                await this.DisplayAlert( "Item Card", $"Seleccionó la carta: {card.Name}", "OK");

                ItemCardsList.SelectedItem = null;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Get cards from Web API.
            LoadEventsCommand.Execute(false);
        }

        ICommand forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync()));

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadEventsAsync(true);
        }

        ICommand loadEventsCommand;
        public ICommand LoadEventsCommand =>
            loadEventsCommand ?? (loadEventsCommand = new Command<bool>(async (f) => await ExecuteLoadEventsAsync()));

        async Task<bool> ExecuteLoadEventsAsync(bool force = false)
        {
            if (IsBusy)
                return false;

            try
            {
                IsBusy = true;
                IsNotBusy = false;

#if DEBUG
                //await Task.Delay(1000);
#endif

                //Events.ReplaceRange(await StoreManager.EventStore.GetItemsAsync(force));

                //Title = "Events (" + Events.Count(e => e.StartTime.HasValue && e.StartTime.Value > DateTime.UtcNow) + ")";

                //SortEvents();

                // Instanciamos el objeto para contactar el servicio web.
                var rest = new RestService("sergio", "1234");

                // Obtenemos una respuesta del servicio.
                var respuesta = await rest.GetDataAsync(null, "api/itemcards");

                // Procedemos si no es nulo y la respuesta fue exitosa.
                if (respuesta != null && respuesta.EsRespuestaExitosa)
                {
                    try
                    {
                        // Deserializamos los datos formato JSON obtenidos por el servicio Web en una colección de empleados.
                        var tempCards = JsonConvert.DeserializeObject<List<ItemCard>>(respuesta.DatosObtenidos);

                        ItemCards = new ObservableCollection<ItemCard>(tempCards);
                    }
                    catch //( Exception ex)
                    {
                        ItemCards.Clear();
                    }
                }
                else
                {
                    await this.DisplayAlert(
                                "Advertencia",
                                respuesta != null ? respuesta.MensajeError : "No fue posible establecer la conexión con el servidor",
                                "OK");
                }

                // Le decimos al control de ListView cuál es su fuente de datos.
                ItemCardsList.ItemsSource = ItemCards;

            }
            catch (Exception ex)
            {
                //Logger.Report(ex, "Method", "ExecuteLoadEventsAsync");
                //MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
                IsNotBusy = true;
            }

            return true;
        }
    }


}
