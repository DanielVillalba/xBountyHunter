using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace xBountyHunter.Views
{
    public class mapPage: ContentPage
    {
        public mapPage(Models.mFugitivos fugitivo)
        {
            double lat = fugitivo.Lat;
            double lon = fugitivo.Lon;
                        
            Position pos = new Position(lat, lon);
            MapSpan span = MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(.5));
            Map capturadosMap = new Map(span);
            capturadosMap.MapType = MapType.Street;
            capturadosMap.IsShowingUser = false;
            Pin pin = new Pin();
            pin.Type = PinType.Place;
            pin.Position = pos;
            pin.Label = fugitivo.Name;
            capturadosMap.Pins.Add(pin);

            StackLayout verticalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            verticalStackLayout.Children.Add(capturadosMap);
            Content = verticalStackLayout;
        }
    }
}
