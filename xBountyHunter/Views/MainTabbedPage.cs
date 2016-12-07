using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xBountyHunter.Views
{
    class MainTabbedPage: TabbedPage
    {
        public MainTabbedPage()
        {
            updateDB();
            ToolbarItem btnAgregar = new ToolbarItem("Agreagar", "", btnAgregar_onClick);
            ToolbarItems.Add(btnAgregar);

            Title = "X Bounty Hunter";

            if(Device.OS == TargetPlatform.iOS)
            {
                Padding = new Thickness(0, 20, 0, 0);
            }

            Children.Add(new fugitivosPage());
            Children.Add(new capturadosPage());
            Children.Add(new acercaDePage());

        }

        private void btnAgregar_onClick()
        {
            Navigation.PushAsync(new Views.agregarFugitivo());
        }

        private void updateDB()
        {
            Extras.webServiceConnection WS = new Extras.webServiceConnection(this);
            WS.connectGET();
        }
    }
}
