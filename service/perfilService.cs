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
using System.Windows;
using System.Windows.Media;

namespace scanner.csv.services
{
    class perfilService
    {

        private const string URL = "http://www.desarrollos-csv.com/api/site/";


        public static string setPerfilUsuario(string nombre_scanner_predeterminado,int anio_predeterminado,int colegio_predeterminado, int template_predeterminado)
        {
            string action = "actializar-perfil-usuario";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.GET);

            request.AddParameter("nombre_scanner_predeterminado", nombre_scanner_predeterminado);
            request.AddParameter("anio_predeterminado", anio_predeterminado);
            request.AddParameter("colegio_predeterminado", colegio_predeterminado);
            request.AddParameter("template_predeterminado", template_predeterminado);
            

            request.AddHeader("Authorization", "Bearer " + System.Windows.Application.Current.Resources["Token"]);
            var response = client.Execute(request);

            //System.Windows.MessageBox.Show("usuario_id :  " + System.Windows.Application.Current.Resources["usuario_id"]);
            //System.Windows.MessageBox.Show("Token :  " + System.Windows.Application.Current.Resources["Token"]);
            //System.Windows.MessageBox.Show("nombre_scanner_predeterminado  :  " + nombre_scanner_predeterminado);
            //System.Windows.MessageBox.Show("anio_predeterminado  :  " + anio_predeterminado);
            //System.Windows.MessageBox.Show("colegio_predeterminado  :  " + colegio_predeterminado);
            //System.Windows.MessageBox.Show("template_predeterminado  :  " + template_predeterminado);

            //System.Windows.MessageBox.Show(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                System.Windows.Application.Current.Resources["MessageLog"] = "Error : el sitio no responde";
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

                return null;

            }
            else
            {
                return response.Content;
            }





        }





    }
}
