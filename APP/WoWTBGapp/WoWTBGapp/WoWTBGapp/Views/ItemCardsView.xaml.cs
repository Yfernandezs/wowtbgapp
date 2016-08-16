using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wowtbgapp.api.Comunicacion;
using wowtbgapp.api.Models;
using Xamarin.Forms;

namespace WoWTBGapp.Views
{
    public partial class ItemCardsView : ContentPage, INotifyPropertyChanged
    {

        public ObservableCollection<ItemCard> ItemCards = new ObservableCollection<ItemCard>();

        public ItemCardsView()
        {
            InitializeComponent();

            // Requerido para hacerle saber a Xamarin.Forms que si contexto Binding es esta pantalla del código.
            BindingContext = this;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Get cards from Web API.

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
    }
}
