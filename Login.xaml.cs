using Newtonsoft.Json.Linq;
using scanner.csv.models;
using scanner.csv.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public static JArray templates_asignados;

        public static JArray templates_asignados_completos;

        public static JArray colegios_asignados;

        public static JArray pruebas_categoria_asignados;

        public static JArray ramos_asignados;

        public static JArray anios_asignados;


        //URL Principal de accesos al servicio REST
        //Esto está en yii2 en el académico
        // private const string URL = "http://academico.pv/api/web/v1/escaner/";
        private const string URL = "http://localhost/desarrollo_csv/api/site/";
        //private const string URLCALCULO = "http://academico.pv/admin/prueba/calcular-puntajes";
        //public const string URLCORREGIRRUT = "http://academico.pv/conectate-corregir-rut";
        public static string token { get; set; }
        public static int usuario_id { get; set; }
        public static Usuario Usuario { get; set; }

        public string version_app = "1.0";

        public Login()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Visible;


                string username = txtUsername.Text;
                string password = txtPassword.Password;




                realizarComunicacion(username, password);
            }

        }

        public static void error_string(string error, Boolean tipo_error)
        {

            System.Windows.Application.Current.Resources["MessageLog"] = error;
            if (tipo_error == false)
            {

                System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.YellowGreen;

            }
            else
            {

                System.Windows.Application.Current.Resources["BackgroundLog"] = Brushes.Red;


            }
            System.Windows.Application.Current.Resources["ForegroundLog"] = Brushes.White;
            System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Visible;
            if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
            {
                System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
            }
            else
            {
                System.Windows.Application.Current.Resources["heigntLog"] = 28d;
                System.Windows.Application.Current.Resources["heigntLog2"] = 24d;
            }

        }

        private async void realizarComunicacion(string username, string password)
        {

            System.Windows.Application.Current.Resources["PermitirAcceso"] = "false";



            error_string("Login : Identificando", false);


            await Task.Run(() =>
            {



                var usuario = usuarioService.loginUsuario(this, username, password, version_app, ref templates_asignados, ref colegios_asignados, ref anios_asignados, ref templates_asignados_completos, ref pruebas_categoria_asignados, ref ramos_asignados);

                Login.token = usuario.access_token;

                System.Windows.Application.Current.Resources["Token"] = Login.token;

                Login.usuario_id = usuario.id;


                


                //Login.token = (string)Login.token["access_token"];


                // System.Windows.MessageBox.Show("Token : " + System.Windows.Application.Current.Resources["Token"]);


                if (System.Windows.Application.Current.Resources["Token"] != null)
                {

                    if (System.Windows.Application.Current.Resources["PermitirAcceso"] == "true")
                    {




                        //System.Windows.MessageBox.Show(Login.token);

                        Login.Usuario = usuarioService.getUsuario(Login.token);

                        //System.Windows.MessageBox.Show("realizarComunicacion : Login.Usuario. " + Login.Usuario);

                        //System.Windows.MessageBox.Show(Login.Usuario.nombre + " " + Login.Usuario.apellido_paterno);

                        //PruebaCategoria[] pruebaCategorias = JsonConvert.DeserializeObject<PruebaCategoria[]>(getPruebaCategorias());
                        //Ramo[] ramos = JsonConvert.DeserializeObject<Ramo[]>(getRamo());


                        // Confirmo si los templates están actualizados o si le corresponden al usuarios


                        error_string("Generando Estructura de Base De Datos", false);


                        //System.Windows.MessageBox.Show("confirmar_templates : " + System.Windows.Application.Current.Resources["confirmar_templates"]);


                        if (Convert.ToInt32(System.Windows.Application.Current.Resources["confirmar_templates"]) == 1)
                        {

                            //chequearTemplatesAsignados(templates_asignados);

                            error_string("Respaldando templates", false);



                            //respaldarTablasWeb();

                        }













                        //this.Hide();

                        //Home home = new Home();

                        //home.lblUsuario.Text = Login.Usuario.nombre + " " + Login.Usuario.apellido_paterno;



                        //home.Show();
                    }

                }
                else
                {

                    error_string("Error de Autenticación", true);

                }

            });

            // System.Windows.MessageBox.Show("realizarComunicacion : PermitirAcceso. " + System.Windows.Application.Current.Resources["PermitirAcceso"]);


            if (System.Windows.Application.Current.Resources["PermitirAcceso"] == "true")
            {


                System.Windows.Application.Current.Resources["MessageLog"] = "";
                System.Windows.Application.Current.Resources["VisibilityLog"] = Visibility.Hidden;
                if (System.Windows.Application.Current.Resources["MessageLog"].ToString().Count() > 35)
                {
                    System.Windows.Application.Current.Resources["heigntLog"] = 48d;
                    System.Windows.Application.Current.Resources["heigntLog2"] = 45d;
                }



                var form = new MainWindow();
                form.Show();
                //form.FormClosing += Form_FormClosing;
                this.Hide();
            }




        }

        private void BtnLogin1_Click(object sender, RoutedEventArgs e)
        {




            System.Windows.Application.Current.Resources["VisibilityProgress"] = Visibility.Visible;


            string username = txtUsername.Text;
            string password = txtPassword.Password;




            realizarComunicacion(username, password);





        }

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {


            System.Diagnostics.Process.Start(System.Windows.Application.Current.Resources["dominio"] + "/site/ayuda-externo?id=1");


        }



    }
}
