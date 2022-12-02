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
    class templateService
    {

        private const string URL = "http://www.desarrollos-csv.com/api/templates/";

        public static string token { get; set; }

        




        public static string setTemplates(Login login, string json)
        {

            string action = "guardar-templates";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.POST);
            request.AddHeader("Authorization", "Bearer " + Login.token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);


            JObject data = null;

            string error = null;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //System.Windows.MessageBox.Show("Error : el sitio no responde" + response);



                System.Windows.Application.Current.Resources["MessageLog"] = "Error : No se pudo actualizar la data";
                System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;
                System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
                System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
                System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Hidden;
                if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                {
                    System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                    System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                }


                //System.Windows.MessageBox.Show("Error : el sitio no responde");

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
                    data = null;


                }

                //System.Windows.MessageBox.Show("response.Content : " + response.Content);

                if (data != null)
                {
                    //Array web_ids = (Array)data["ids_retorno"];


                    JArray value = null;

                    value = data["ids_retorno"] as JArray;

                    //var web_ids = (Array)data["ids_retorno"];

  

                    //System.Windows.MessageBox.Show("web_ids : " + web_ids);

                    //error = (string)data["error"];


                    if (error != null)
                    {

                        System.Windows.Application.Current.Resources["MessageLog"] = error;
                        System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;
                        System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
                        System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
                        System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Hidden;
                        if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                        {
                            System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                            System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                        }

                    }
                    else
                    {
                        
                        System.Windows.Application.Current.Resources["PermitirAcceso"] = "true";
                        if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                        {
                            System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                            System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                        }

                    }
                }



            }


            return response.Content;

        }

        public static void getTemplate(Login login, int id)
        {
            string action = "get-template";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.GET);
            request.AddHeader("Authorization", "Bearer " + Login.token);
            request.AddParameter("id", id);
            var response = client.Execute(request);



            //System.Windows.MessageBox.Show("response.Content : " + response.Content);

            // genero una variable data del tipo jzon
            JObject data = null;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
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

               

            }

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


    }
}
