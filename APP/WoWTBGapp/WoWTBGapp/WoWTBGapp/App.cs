using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoWTBGapp.Views;
using Xamarin.Forms;

namespace WoWTBGapp
{
    public class App : Application
    {
        public App()
        {
            //// The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};


            // Se crea un NavigationPage para poder movernos entre diferentes pantallas y le damos la primera pantalla que debe mostrar
            // junto con la definición de otras propiedades para lograr una apariencia similar en Android y iOS.
            MainPage = new NavigationPage(new ItemCardsView()) { BarBackgroundColor = Color.FromHex("009900"), BarTextColor = Color.White };

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
