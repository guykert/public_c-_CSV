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

namespace scanner.csv.services
{
    class cursosService
    {

        private const string URL = "http://www.desarrollos-csv.com/api/cursos/";

        public static string token { get; set; }

        
        public static void getCursos(int selectedPruebasCategoria, int selectedRamos,ref JArray cursos_api)
        {
            string action = "get-cursos";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.GET);
            request.AddHeader("Authorization", "Bearer " + Login.token);
            request.AddParameter("categoria_prueba", selectedPruebasCategoria);
            request.AddParameter("ramo", selectedRamos);
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

                    cursos_api = data["Cursos"] as JArray;








                }

            }

        }




    }
}
