using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace xBountyHunter
{
    public class xBountyApp : Application
    {
        public xBountyApp()
        {
            MainPage = new NavigationPage(new Views.MainTabbedPage());
        }
    }
}
