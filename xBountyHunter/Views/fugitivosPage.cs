using System;
using Xamarin.Forms;

namespace xBountyHunter.Views
{
    public class fugitivosPage : ContentPage
    {
        private ListView list = new ListView();
        public fugitivosPage()
        {
            Title = "Fugitivos";
            Extras.listaFugitivos listaFugitivos = new Extras.listaFugitivos();
            MessagingCenter.Subscribe<Page>(this, "Update", messagecallback);
            list.ItemsSource = listaFugitivos.getFugitivos();
            list.ItemTemplate = new DataTemplate(typeof(ListViewCell));
            list.ItemTapped += listItemTapped_Tapped;
            Content = list;
        }

        private void messagecallback(Page obj)
        {
            Extras.listaFugitivos listaFugitivos = new Extras.listaFugitivos();
            list.ItemsSource = listaFugitivos.getFugitivos();
        }

        private void listItemTapped_Tapped(object sender, ItemTappedEventArgs args)
        {
            Models.mFugitivos fugitivo = (Models.mFugitivos)args.Item;
            Navigation.PushAsync(new Views.capturarPage(fugitivo));
        }
    }
}
