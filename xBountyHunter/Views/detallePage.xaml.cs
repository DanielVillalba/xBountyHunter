using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace xBountyHunter.Views
{
    public partial class detallePage : ContentPage
    {
        private Models.mFugitivos Fugitivo = new Models.mFugitivos();
        private Extras.databaseManager DB = new Extras.databaseManager();
        public detallePage(Models.mFugitivos fugitivo)
        {
            InitializeComponent();
            Fugitivo = fugitivo;
            Title = Fugitivo.Name;
            img.Source = ImageSource.FromFile(fugitivo.Foto);
        }

        public async void beliminar_Clicked(object sender, EventArgs args)
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

        public async void bmap_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new mapPage(Fugitivo));
        }
    }
}
