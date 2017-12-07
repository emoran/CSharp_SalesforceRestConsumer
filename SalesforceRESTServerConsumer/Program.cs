using System;
using System.Net;
using System.Text;  // for class Encoding
using System.IO;    // for StreamReader
using System.Web.Script.Serialization;

using System.Collections.Generic;
using System.Net.Http;

using System.Net.Http.Headers;

namespace SalesforceRESTServerConsumer
{
    class MainClass
    {
        

        public static void Main(string[] args)
        {

            SalesforceAuthentication salesforce_auth = getSalesforceAccessToken();

            Console.WriteLine("1. Consuming getSalesforceAccessToken()");
            Console.WriteLine("Access Token:"+salesforce_auth.access_token);


            Console.WriteLine("2. Consuming consumeSalesforceService()");
            requestHTTPcClient(salesforce_auth.access_token);


            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        public static SalesforceAuthentication getSalesforceAccessToken(){
            SalesforceAuthentication authenticationObj = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://test.salesforce.com/services/oauth2/token");

                var postData = "grant_type=password";
                postData += "&client_id=CLIENTID";
                postData += "&client_secret=SECRET";
                postData += "&username=SALESFORCENAME";
                postData += "&password=PASSWOORDTOKEN";

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                authenticationObj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SalesforceAuthentication>(responseString);

                Console.WriteLine("Response: " + responseString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error response: " + ex.Message);
            }




            return authenticationObj;
        }

        public static String consumeSalesforceService(String accessToken){
            

            String res = "";
            string authToken = "Bearer" + " " + accessToken;

            try
            {
                var myUri = new Uri("https://cs21.salesforce.com/services/apexrest/RestServiceTiket/insertaSolicitud");
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;
                myHttpWebRequest.Headers.Add("Authorization", authToken);
                myHttpWebRequest.Accept = "application/json";
                myHttpWebRequest.Method = "POST";

              
                    
                Solicitud sol = new Solicitud();
                sol.nombre = "pruebaFF_1";
                sol.numeroTiket = "ABC1232H8";
                sol.datosOrigen = null;
                sol.datosDestino = null;

                ExternalRequest httpcall = new ExternalRequest();
                httpcall.request = sol;

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {               
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(httpcall);
                    Console.WriteLine("JSON: " + json);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                }

                var httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Console.WriteLine("JSON: " + httpResponse);
                var responseString = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
                res = responseString;

            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }

           
            return res;
        }

        public static async void requestHTTPcClient(String accesstoken){


            HttpClient  httpClient = new HttpClient();
            try
            {
                string resourceAddress = "https://test.salesforce.com/services/apexrest/someRest/restservice";

                DatosOrigen origen = new DatosOrigen();
                DatosDestino destino = new DatosDestino();

                Solicitud sol = new Solicitud();
                sol.nombre = "pruebaFF_1";
                sol.numeroTiket = "moran88999044";
                sol.datosOrigen = origen;
                sol.datosDestino = destino;

                ExternalRequest httpcall = new ExternalRequest();
                httpcall.request = sol;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(httpcall);

                //Console.Write("JSON request: "+json);

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

                HttpResponseMessage wcfResponse = await httpClient.PostAsync(resourceAddress, new StringContent(json, Encoding.UTF8, "application/json"));
                var contents = await wcfResponse.Content.ReadAsStringAsync();
                Console.Write("Response is: " + contents);
            }
            catch (HttpRequestException hre)
            {
                Console.Write("Error is: " + hre.Message);
            }


        }

    }
}
