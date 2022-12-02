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
    class usuarioService
    {

        private const string URL = "http://www.desarrollos-csv.com/api/site/";

        public static string setScannerPredeterminado(string nombre_scanner_predeterminado)
        {
            string action = "actializar-scanner-predeterminado";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.GET);

            request.AddParameter("nombre_scanner_predeterminado", nombre_scanner_predeterminado);

            request.AddHeader("Authorization", "Bearer " + Login.token);
            var response = client.Execute(request);

            //MessageBox.Show("Academico.csv.services setScannerPredeterminado :  " + nombre_scanner_predeterminado);
            //MessageBox.Show("Academico.csv.services setScannerPredeterminado ok  :  " + response.Content);

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

        public static string setPerfilUsuario(string nombre_scanner_predeterminado,int anio_predeterminado,int colegio_predeterminado)
        {
            string action = "actializar-perfil-usuario";
            var client = new RestClient(URL + action);
            var request = new RestRequest("", RestSharp.Method.GET);

            request.AddParameter("nombre_scanner_predeterminado", nombre_scanner_predeterminado);
            request.AddParameter("anio_predeterminado", anio_predeterminado);
            request.AddParameter("colegio_predeterminado", colegio_predeterminado);

            request.AddHeader("Authorization", "Bearer " + Login.token);
            var response = client.Execute(request);

            //MessageBox.Show("Academico.csv.services setScannerPredeterminado :  " + nombre_scanner_predeterminado);
            //MessageBox.Show("Academico.csv.services setScannerPredeterminado ok  :  " + response.Content);

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

        public static Usuario loginUsuario(Login login,string username, string password, string version_app, ref JArray templates_asignados, ref JArray colegios_asignados, ref JArray anios_asignados, ref JArray templates_asignados_completos, ref JArray pruebas_categoria_asignados, ref JArray ramos_asignados)
        {
            string action = "login-api";
            //Concatena la acción a la URL y esto queda definico como client
            var client = new RestClient(URL + action);
            // Construimos la solicitud y definimos el metodo por GET
            var request = new RestRequest("", RestSharp.Method.GET);
            //Definimos el Content-Type
            request.AddHeader("Content-Type", "multipart/form-data");
            // enviamos el usuario y la clave esto biene del formulario del login
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            

            var usuario = new Usuario();

            //Ejecutamos la solicitud 
            var response = client.Execute(request);

            string tokenuser = null;
            string error = null;
            string version = null;
            
            // genero una variable data del tipo jzon
            JObject data = null;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                System.Windows.Application.Current.Resources["MessageLog"] = "Error de Autenticación";
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
                    tokenuser = null;
                    //genero un debug con el error que entrego yii

                }

                //System.Windows.MessageBox.Show("response.Content : " + response.Content);

                if (data != null)
                {

                    usuario.access_token = (string)data["access_token"];
                    usuario.id = (int)data["usuario_id"];
                    //tokenuser = (string)data["access_token"];
                    //login.usuario_id = (string)data["usuario_id"];
                    error = (string)data["error"];
                    version = (string)data["version"];

                    System.Windows.Application.Current.Resources["colegio_predeterminado"] = Convert.ToInt32(data["colegio_predeterminado"]);

                    System.Windows.Application.Current.Resources["anio_predeterminado"] = (string)data["anio_predeterminado"];

                    System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] = (string)data["nombre_scanner_predeterminado"];

                    System.Windows.Application.Current.Resources["template_predeterminado"] = (string)data["template_predeterminado"];

                    System.Windows.Application.Current.Resources["nombre_colegio_predeterminado"] = (string)data["nombre_colegio_predeterminado"];

                    System.Windows.Application.Current.Resources["usuario_id"] = Convert.ToInt32(data["usuario_id"]);

                    System.Windows.Application.Current.Resources["dominio"] = (string)data["dominio"];

                    System.Windows.Application.Current.Resources["nombre_usuario"] = (string)data["nombre_usuario"];

                    System.Windows.Application.Current.Resources["confirmar_templates"] = Convert.ToInt32(data["confirmar_templates"]);



                    templates_asignados = data["templates_asignados"] as JArray;

                    templates_asignados_completos = data["Template"] as JArray;

                    colegios_asignados = data["Colegios"] as JArray;

                    anios_asignados = data["Configuracion"] as JArray;

                    pruebas_categoria_asignados = data["PruebaCategoriaHijo"] as JArray; 

                    ramos_asignados = data["RamoHijo"] as JArray;











                    if (version_app != version)
                    {

                        System.Windows.Application.Current.Resources["MessageLog"] = "Nueva versión de la App : " + version + "  Pincha en el boton de ayuda y descarga la nueva versión";
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
                        if (error != null)
                        {

                            System.Windows.Application.Current.Resources["MessageLog"] = error;
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

                            System.Windows.Application.Current.Resources["PermitirAcceso"] = "true";
                            if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                            {
                                System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                                System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                            }

                        }
                    }



                }
                    


            }

 

            return usuario;




        }

        public static Usuario getUsuario(string token)
        {
            string action = "data-usuario";
            //Concatena la acción a la URL y esto queda definico como client
            var client = new RestClient(URL + action);
            // Construimos la solicitud y definimos el metodo por GET
            var request = new RestRequest("", RestSharp.Method.GET);
            //Definimos el Content-Type


            // System.Windows.MessageBox.Show(token);

            request.AddHeader("Authorization", "Bearer " + token);


            //Ejecutamos la solicitud 
            var response = client.Execute(request);

            //System.Windows.MessageBox.Show(response.Content);
            //System.Windows.MessageBox.Show(response.StatusCode.ToString());

            //Console.WriteLine(response.Content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                System.Windows.Application.Current.Resources["MessageLog"] = "Error : el sitio no responde";
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
                return JsonConvert.DeserializeObject<Usuario>(response.Content);
            }

            




        }





    }
}
