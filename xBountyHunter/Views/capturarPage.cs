using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace xBountyHunter.Views
{
    public class capturarPage : ContentPage
    {
        private Models.mFugitivos Fugitivo = new Models.mFugitivos();
        private Extras.databaseManager DB = new Extras.databaseManager();
        private Image img;
        private Label fugitivoSuelto;
        private Button bcapturar;
        private Button beliminar;
        private StackLayout imageContainer;
        private Button bfoto;
        private StackLayout verticalStackLayout;

        private string imagePath;

        public capturarPage(Models.mFugitivos fugitivo )
        {
            Fugitivo.Name = fugitivo.Name;
            Fugitivo.ID = fugitivo.ID;

            fugitivoSuelto = new Label
            {
                Text = "El fugitivo sigue suelto...",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center
            };

            bcapturar = new Button
            {
                Text = "Capturar",
                WidthRequest = 200,
                BorderColor = Color.Black,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center
            };

            beliminar = new Button
            {
                Text = "Eliminar",
                WidthRequest = 200,
                BorderColor = Color.Black,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center
            };

            imageContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 100,
                HeightRequest = 100,
                BackgroundColor = Color.Gray
            };

            img = new Image
            {
                Aspect = Aspect.Fill,
                WidthRequest = 100,
                HeightRequest = 100
            };

            bfoto = new Button
            {
                Text = "Tomar Foto",
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200
            };


            verticalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            bcapturar.Clicked += Bcapturar_Clicked;
            beliminar.Clicked += Beliminar_Clicked;
            bfoto.Clicked += Bfoto_Clicked;
            Title = Fugitivo.Name;

            imageContainer.Children.Add(img);

            verticalStackLayout.Children.Add(fugitivoSuelto);
            verticalStackLayout.Children.Add(imageContainer);
            verticalStackLayout.Children.Add(bfoto);
            verticalStackLayout.Children.Add(bcapturar);
            verticalStackLayout.Children.Add(beliminar);

            Content = verticalStackLayout;

        }

        private async void Bfoto_Clicked(object sender, EventArgs e)
        {
            imagePath = await DependencyService.Get<DependencyServices.ICamera>().takePhoto();
            if (imagePath == "" || imagePath == "Cancel" || imagePath == null)
                bcapturar.IsEnabled = false;
            else
            {
                img.Source = ImageSource.FromFile(imagePath);
                bcapturar.IsEnabled = true;
            }
        }

        private async void Bcapturar_Clicked(object sender, EventArgs e)
        {
            string udid = DependencyService.Get<DependencyServices.IUDID>().getUDID();
            Dictionary<string, string> location = DependencyService.Get<DependencyServices.IGeoLocation>().getLocation();



            Extras.webServiceConnection ws = new Extras.webServiceConnection(this);
            Fugitivo.Capturado = true;
            Fugitivo.Foto = imagePath;
            Fugitivo.Lat = Convert.ToDouble(location["Lat"]);
            Fugitivo.Lon = Convert.ToDouble(location["Lon"]);
            int result = DB.updateItem(Fugitivo);


            string message = ws.connectPOST(udid);
            if (result == 1)
                await DisplayAlert("Capturado", "El fugitivo " + Fugitivo.Name + " ha sido capturado\n" + message, "Aceptar");
            else
                await DisplayAlert("Error", "Error al capturar fugitivo", "Aceptar");
            DB.closeConnection();
            MessagingCenter.Send<Page>(this, "Update");
            await Navigation.PopAsync();
        }

        private async void Beliminar_Clicked(object sender, EventArgs e)
        {
            int result = DB.deleteItem(Fugitivo.ID);
            if (result == 1)
                await DisplayAlert("Eliminado", "El fugitivo " + Fugitivo.Name + " ha sido eliminado", "Aceptar");
            else
                await DisplayAlert("Error", "Error al borrar el fugitivo", "Aceptar");
            DB.closeConnection();
            MessagingCenter.Send<Page>(this, "Update");
            await Navigation.PopAsync();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<DependencyServices.IGeoLocation>().activarGPS();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DependencyService.Get<DependencyServices.IGeoLocation>().apagarGPS();
        }
    }
}
