using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBountyHunter.Extras
{
    public class listaFugitivos
    {
        public List<Models.mFugitivos> ocFugitivos;
        public databaseManager DB = new databaseManager();
        public listaFugitivos()
        {
            //ocFugitivos = new List<Models.mFugitivos>();
            //ocFugitivos.Add(new Models.mFugitivos
            //{
            //    Name = "Daniel Villalba",
            //});
            //ocFugitivos.Add(new Models.mFugitivos
            //{
            //    Name = "Fernando Villalba",
            //});
            //ocFugitivos.Add(new Models.mFugitivos
            //{
            //    Name = "Alexa Villalba",
            //});
            //ocFugitivos.Add(new Models.mFugitivos
            //{
            //    Name = "Reyna Bustillos",
            //});
        }

        public List<Models.mFugitivos> getFugitivos()
        {
            ocFugitivos = DB.selectNoCaptured();
            return ocFugitivos;
        }

        public List<Models.mFugitivos> getCapturados()
        {
            ocFugitivos = DB.selectCaptured();

            foreach(Models.mFugitivos fugi in ocFugitivos)
            {
                System.Diagnostics.Debug.WriteLine("fugi" + fugi.Name);
            }

            return ocFugitivos;
        }
    }
}
