using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using scanner.csv.models;
using System.IO;
using System.Collections;
using System.Windows;
using System.Windows.Media;
using scanner.csv.Data.logica;

namespace scanner.csv.services
{
    class pruebaService
    {

        private const string URL = "http://www.desarrollos-csv.com/api/site/";

        private const string URL_pruebas = "http://www.desarrollos-csv.com/api/pruebas/";

        public static string token { get; set; }


        public static string setPruebaAlumno(string json, ref JArray resuesta_pruebas_api)
        {

            string action = "guardarpruebasescaneadas";
            var client = new RestClient(URL_pruebas + action);
            var request = new RestRequest("", RestSharp.Method.POST);
            request.AddHeader("Authorization", "Bearer " + System.Windows.Application.Current.Resources["Token"]);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);


            //System.Windows.MessageBox.Show(" response.Content :  " + response.Content);

            // genero una variable data del tipo jzon
            JObject data = null;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                System.Windows.Application.Current.Resources["MessageLog"] = "Error En la carga de datos";
                System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;
                System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
                System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
                System.Windows.Application.Current.Resources["PermitirAcceso"] = "false";
                System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Hidden;

                if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                {
                    System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                    System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                }

            }
            else
            {



                // Trata de asignar la variable con los datos obtenidos
                try
                {
                    //System.Windows.MessageBox.Show("Response " + JObject.Parse(response.Content));
                    data = JObject.Parse(response.Content);



                }
                catch (JsonReaderException ex)
                {
                    // Si no puede asignar es por que el objeto no viene en formato jzon
                    // Dejo el token en null
                    //data = null;
                    //tokenuser = null;
                    //genero un debug con el error que entrego yii

                }

                //System.Windows.MessageBox.Show("response.Content : " + response.Content);

                if (data != null)
                {


                    resuesta_pruebas_api = data["resuesta_pruebas_api"] as JArray;


                }



            }



            return response.Content;

        }

        public static string setPruebaSubirImagenes(string json, List<PruebaAlumnoEnviarWeb> listpruebaAlumnoEnviarWeb)
        {











            var fullFileName = "test 2_1_Q_prueba_20190919_111834_0002.jpg";
            var filepath = @"C:\Temp\test 2_1_Q_prueba_20190919_111834_0002.jpg";


            string action = "guardarpruebasubirarchivo";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.POST);
            request.AddHeader("Authorization", "Bearer " + Login.token);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //request.AlwaysMultipartFormData = true;
            request.AddHeader("Content-Type", "multipart/form-data");
            //request.AddFile(Path.GetFileNameWithoutExtension(fullFileName), filepath, "multipart/form-data");



            //foreach (var pruebaAlumnoEnviarWeb in listpruebaAlumnoEnviarWeb)
            //{

            //    foreach (var regionesFinales in pruebaAlumnoEnviarWeb.regionesFinales)
            //    {


            //        MessageBox.Show("regionesFinales.imagen_nombre : " + regionesFinales.imagen_nombre +  "       regionesFinales.imagen_Local : " + regionesFinales.imagen_Local);

            //    }

            //}


            foreach (var pruebaAlumnoEnviarWeb in listpruebaAlumnoEnviarWeb)
            {

                foreach (var regionesFinales in pruebaAlumnoEnviarWeb.regionesFinales)
                {


                    request.AddFile(Path.GetFileNameWithoutExtension(regionesFinales.imagen_nombre), regionesFinales.imagen_Local);

                }

            }
            var response = client.Execute(request);

            return response.Content;

        }

        public static void getPruebas(int selectedPruebasCategoria, int selectedRamos, int cursoCategoria, ref JArray pruebas_api)
        {
            string action = "get-pruebas";
            var client = new RestClient(URL_pruebas + action);
            var request = new RestRequest("", RestSharp.Method.GET);
            request.AddHeader("Authorization", "Bearer " + System.Windows.Application.Current.Resources["Token"]);
            request.AddParameter("categoria_prueba", selectedPruebasCategoria);
            request.AddParameter("ramo", selectedRamos);
            request.AddParameter("curso", cursoCategoria);
            var response = client.Execute(request);



            //System.Windows.MessageBox.Show("response.Content : " + response.Content);

            // genero una variable data del tipo jzon
            JObject data = null;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                System.Windows.Application.Current.Resources["MessageLog"] = "Error : obtener ramos asignados";
                System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;
                System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
                System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
                System.Windows.Application.Current.Resources["PermitirAcceso"] = "false";
                System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Hidden;
                if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                {
                    System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                    System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                }

            }
            else
            {



                // Trata de asignar la variable con los datos obtenidos
                try
                {
                    //System.Windows.MessageBox.Show("Response " + JObject.Parse(response.Content));
                    data = JObject.Parse(response.Content);



                }
                catch (JsonReaderException ex)
                {

                    System.Windows.Application.Current.Resources["MessageLog"] = "Error : obtener templates asignados";
                    System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;
                    System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
                    System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
                    System.Windows.Application.Current.Resources["PermitirAcceso"] = "false";
                    System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Hidden;
                    if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                    {
                        System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                        System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                    }

                }



                if (data != null)
                {

                    pruebas_api = data["Pruebas"] as JArray;








                }

            }

        }


    }
}
