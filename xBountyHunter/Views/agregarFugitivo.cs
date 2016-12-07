using System;
using Xamarin.Forms;

namespace xBountyHunter.Views
{
    public class agregarFugitivo : ContentPage
    {
        StackLayout verticalStackLayout;
        StackLayout horizontalStackLayout;
        Button bagregar;
        Button bcancelar;
        Entry enewname;
        public agregarFugitivo()
        {
            verticalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            horizontalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center

            };

            enewname = new Entry
            {
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#d3d3d3"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            bagregar = new Button
            {
                Text = "Agregar",
                BorderColor = Color.Black,
                BorderWidth = 1
            };

            bcancelar = new Button
            {
                Text = "Cancelar",
                BorderColor = Color.Black,
                BorderWidth = 1
            };

            verticalStackLayout.Children.Add(enewname);
            verticalStackLayout.Children.Add(horizontalStackLayout);
            horizontalStackLayout.Children.Add(bagregar);
            horizontalStackLayout.Children.Add(bcancelar);

            bagregar.Clicked += bagregar_Clicked;

            Content = verticalStackLayout;

        }

        private async void bagregar_Clicked(object sender, EventArgs e)
        {
            Extras.databaseManager db = new Extras.databaseManager();
            Models.mFugitivos fugitivos = new Models.mFugitivos();
            fugitivos.Name = enewname.Text;
            fugitivos.Capturado = false;
            int result = db.insertItem(fugitivos);

            if(result == 1)
            {
                await DisplayAlert("Agregado", "Se ha agregado el fugitivo a la base de datos", "Aceptar");
                MessagingCenter.Send<Page>(this, "Update");
                await Navigation.PopAsync();
            }
        }
    }
}
