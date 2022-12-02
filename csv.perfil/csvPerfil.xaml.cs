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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace scanner.csv.csv.perfil
{
    /// <summary>
    /// Lógica de interacción para csvPerfil.xaml
    /// </summary>
    public partial class csvPerfil : UserControl
    {

        private MainWindow mainWindow;

        public csvPerfil(csvPerfilPrincipal csvPerfilPrincipal, MainWindow mainWindow2)
        {
            InitializeComponent();

            if ((Int32)Application.Current.Resources["colegio_predeterminado"] < 1)
            {
                var mensajeError = new MensajesError("Es necesario que se defina un colegio predeterminado", true);
            }

            mainWindow = mainWindow2;






            cargarData();
        }

        public void cargarData()
        {

            // cargo los escaners

            var colegiosList = new List<ComboData>();

            var anioList = new List<ComboData>();

            var escanerList = new List<ComboData>();

            var templatesList = new List<ComboData>();




            //this.escaners.ItemsSource = MainWindow.configuracion.dataEscanners;





            if (MainWindow.configuracion.dataEscanners.Count > 0)
            {

                var i = 1;

                foreach (JValue escaner in MainWindow.configuracion.dataEscanners)
                {

                    escanerList.Add(new ComboData() { Id = Convert.ToInt32(i), Value = (String)escaner });


                    i = i + 1;
                }

            }


            if (Login.colegios_asignados.Count > 0)
            {

                foreach (JObject colegio in Login.colegios_asignados)
                {

                    colegiosList.Add(new ComboData() { Id = Convert.ToInt32(colegio["id"]), Value = (String)colegio["nombre"] });

                }

            }

            this.colegios.ItemsSource = colegiosList;
            this.colegios.DisplayMemberPath = "Value";
            this.colegios.SelectedValuePath = "Id";

            if (System.Windows.Application.Current.Resources["colegio_predeterminado"] != null && System.Windows.Application.Current.Resources["colegio_predeterminado"] != "")
            {

                //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);


                this.colegios.SelectedItem = System.Windows.Application.Current.Resources["colegio_predeterminado"];

                this.colegios.SelectedValue = System.Windows.Application.Current.Resources["colegio_predeterminado"];

            }

            if (Login.templates_asignados_completos.Count > 0)
            {

                foreach (JObject templates in Login.templates_asignados_completos)
                {

                    templatesList.Add(new ComboData() { Id = Convert.ToInt32(templates["id"]), Value = (String)templates["nombre"] });

                }

            }

            this.templates.ItemsSource = templatesList;
            this.templates.DisplayMemberPath = "Value";
            this.templates.SelectedValuePath = "Id";


            if (System.Windows.Application.Current.Resources["template_predeterminado"] != null && System.Windows.Application.Current.Resources["template_predeterminado"] != "")
            {

                //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);


                this.templates.SelectedItem = System.Windows.Application.Current.Resources["template_predeterminado"];

                this.templates.SelectedValue = System.Windows.Application.Current.Resources["template_predeterminado"];

            }

            if (Login.anios_asignados.Count > 0)
            {

                foreach (JObject anio in Login.anios_asignados)
                {



                    anioList.Add(new ComboData() { Id = Convert.ToInt32(anio["id"]), Value = (String)anio["anio_academico"] });

                    // MessageBox.Show("Login.Usuario.anio_predeterminado : " + anioList.ToString());

                }
            }

            this.anios.ItemsSource = anioList;
            this.anios.DisplayMemberPath = "Value";
            this.anios.SelectedValuePath = "Id";



            if (System.Windows.Application.Current.Resources["anio_predeterminado"] != null && System.Windows.Application.Current.Resources["anio_predeterminado"] != "")
            {

                //MessageBox.Show("Login.Usuario.anio_predeterminado : " + System.Windows.Application.Current.Resources["anio_predeterminado"]);

                //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);


                this.anios.SelectedItem = System.Windows.Application.Current.Resources["anio_predeterminado"];

                this.anios.SelectedValue = System.Windows.Application.Current.Resources["anio_predeterminado"];

            }

            this.escaners.ItemsSource = escanerList;
            this.escaners.DisplayMemberPath = "Value";
            this.escaners.SelectedValuePath = "Id";

            if (System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] != null && System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] != "")
            {

                

                if (MainWindow.configuracion.dataEscanners.Count > 0)
                {

                    var i = 1;

                    foreach (JValue escaner in MainWindow.configuracion.dataEscanners)
                    {

                        //MessageBox.Show("scanner: " + System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"]   + "     scanner: " + System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] + "   if " + ((String)System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] == (String)escaner));


                        if ((String)System.Windows.Application.Current.Resources["nombre_scanner_predeterminado"] == (String)escaner)
                        {

                            //MessageBox.Show("i : " + i);

                            this.escaners.SelectedItem = i;

                            this.escaners.SelectedValue = i;

                        }


                        i = i + 1;
                    }

                }



            }





        }

        private void BottomGuardar_Click(object sender, RoutedEventArgs e)
        {

            Object escanerSeleccionado = escaners.SelectedItem;



            if (colegios.SelectedIndex == -1)//Nothing selected
            {

                var mensajeError = new MensajesError("Selecciona un Colegio", true);

                return;

            }

            if (escaners.SelectedIndex == -1)//Nothing selected
            {

                var mensajeError = new MensajesError("Selecciona un Escaner", true);

                return;

            }

            if (templates.SelectedIndex == -1)//Nothing selected
            {

                var mensajeError = new MensajesError("Selecciona un Template", true);

                return;

            }


            if (!(escaners.SelectedIndex == -1 && colegios.SelectedIndex == -1 && templates.SelectedIndex == -1))//Nothing selected
            {

                var selectedColegio = ((ComboData)colegios.SelectedItem).Id;

                var selectedScanner = ((ComboData)escaners.SelectedItem).Value;

                var selectedTemplate = ((ComboData)templates.SelectedItem).Id;

                //MessageBox.Show("selectedScanner: " + selectedScanner);

                var usuario = perfilService.setPerfilUsuario(selectedScanner, Convert.ToInt32(System.Windows.Application.Current.Resources["anio_predeterminado"]), selectedColegio, selectedTemplate);

                System.Windows.Application.Current.Resources["template_predeterminado"] = selectedTemplate;

                mainWindow.seleccionarEntorno(0);

            }




            //usuarioService.setPerfilUsuario(escanerSeleccionado.ToString(), 1,1);

        }
    }
}
