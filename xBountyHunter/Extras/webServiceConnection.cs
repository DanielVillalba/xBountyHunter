using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Text;
using Xamarin.Forms;
using xBountyHunter.Models;


namespace xBountyHunter.Extras
{
    public class webServiceConnection
    {
        private const string URL_WS1 = @"http://201.168.207.210/services/droidBHServices.svc/fugitivos";
        private const string URL_WS2 = @"http://201.168.207.210/services/droidBHServices.svc/atrapados";
        private HttpClient client;
        private Page mainPage;

        public webServiceConnection(Page page)
        {
            mainPage = page;
        }

        public void connectGET()
        {
            List<Models.mFugitivos> fugitivos = new List<Models.mFugitivos>();
            client = new HttpClient();
            try
            {
                HttpResponseMessage response = client.GetAsync(URL_WS1).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    List<Models.mFugitivos> items = JsonConvert.DeserializeObject<List<Models.mFugitivos>>(content);
                    verifyFugitivosOnDB(items);
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message == "Error: NameResolutionFailure")
                    connectGET();
                else
                    mainPage.DisplayAlert("Error", "Nose pudo conectar con los servicios web", "Aceptar");
            }
        }

        private void verifyFugitivosOnDB(List<mFugitivos> fugitivos)
        {
            List<mFugitivos> dbfugitivos = new List<mFugitivos>();
            databaseManager db = new databaseManager();
            dbfugitivos = db.selectAll();
            foreach(mFugitivos fugitivo in fugitivos)
            {
                if(!dbfugitivos.Exists(x=> x.Name == fugitivo.Name))
                {
                    fugitivo.Capturado = false;
                    db.insertItem(fugitivo);
                }
            }
            db.closeConnection();
        }

        public string connectPOST(string udid)
        {
            string result = "";
            string postBody = "{\"UDIDString\":\"" + udid + "\"}";
            client = new HttpClient();
            try
            {
                HttpContent bodyContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(URL_WS2, bodyContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Dictionary<string, string> jsondata = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    result = jsondata["mensaje"];
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message == "Error: NameResolutionFailure")
                    result = connectPOST(udid);
                else
                    mainPage.DisplayAlert("Error", "No se pudo conectar con los servicios web", "Aceptar");
            }
            return result;
        }
    }
}
 


