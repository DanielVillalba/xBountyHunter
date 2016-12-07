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

using Xamarin.Forms;
using Android.Telephony;
using xBountyHunter.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(AndroidUDID))]
namespace xBountyHunter.Droid
{
    public class AndroidUDID: DependencyServices.IUDID
    {
        public string getUDID()
        {
            Context cnt = Android.App.Application.Context;
            TelephonyManager tm = (TelephonyManager)cnt.GetSystemService(Context.TelephonyService); 
            return tm.DeviceId;
        }
    }
}