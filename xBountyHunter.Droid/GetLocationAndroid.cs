using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using xBountyHunter.Droid;
using Android.Locations;

[assembly: Xamarin.Forms.Dependency(typeof(GetLocationAndroid))]
namespace xBountyHunter.Droid
{
    public class GetLocationAndroid : Java.Lang.Object, DependencyServices.IGeoLocation, ILocationListener
    {
        private LocationManager locationManager;
        private Dictionary<string, string> loc;


        public void activarGPS()
        {
            try
            {
                Context cnt = Application.Context;
                locationManager = cnt.GetSystemService(Context.LocationService) as LocationManager;
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
                System.Diagnostics.Debug.WriteLine("Activando GPD");

                Criteria criteria = new Criteria();
                criteria.Accuracy = Accuracy.Fine;
                string provider = locationManager.GetBestProvider(criteria, true);
                Location location = locationManager.GetLastKnownLocation(provider);

                System.Diagnostics.Debug.WriteLine("Location Detected..: lon -> " + location.Longitude.ToString() + " lat -> " + location.Latitude.ToString());

                if (location != null)
                {
                    newLocation(location);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void apagarGPS()
        {
            if(locationManager != null)
            {
                try
                {
                    locationManager.RemoveUpdates(this);
                    System.Diagnostics.Debug.WriteLine("Desactivando el GPS.....");
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }
        }

        public Dictionary<string, string> getLocation()
        {
            return loc;
        }

        public void OnLocationChanged(Location location)
        {
            newLocation(location);
        }

        public void OnProviderDisabled(string provider)
        {
            System.Diagnostics.Debug.WriteLine("OnProviderDisabled implementation");
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            System.Diagnostics.Debug.WriteLine("OnProviderEnabled implementation");
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            System.Diagnostics.Debug.WriteLine("OnStatusChanged implementation");
            //throw new NotImplementedException();
        }

        private void newLocation(Location location)
        {
            loc = new Dictionary<string, string>();
            loc.Add("Lat", location.Latitude.ToString());
            loc.Add("Lon", location.Longitude.ToString());
            System.Diagnostics.Debug.WriteLine("Detectando(Lat" + loc["Lat"] + ", Lon" + loc["Lon"] + ")");
        }
    }
}