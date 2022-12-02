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
using System.IO;
using System.Drawing;


// esto es para mostrar el porcentaje de avance en una barra que se llena
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Drawing.Printing;

using TWAINWorkingGroupToolkit;
using scanner.csv.services;
using scanner.csv.models;
using System.Diagnostics;
using ET.Evaluation;
using Fw.Data.logicas;
using Newtonsoft.Json.Linq;
using System.Data;

namespace scanner.csv.csv.entornoUsuarioFinal
{
    /// <summary>
    /// Lógica de interacción para csvEntornoUsuarioFinal.xaml
    /// </summary>
    /// 


    public partial class csvEntornoUsuarioFinalResultados : UserControl
    {


        csvEntornoUsuarioFinalPrincipal csvEntornoUsuarioFinalPrincipal;

        public JArray resuesta_pruebas_api;


        public csvEntornoUsuarioFinalResultados(csvEntornoUsuarioFinalPrincipal entoro_usuario_final, JArray resuesta_pruebas_api)
        {
            InitializeComponent();

            this.csvEntornoUsuarioFinalPrincipal = entoro_usuario_final;

            this.resuesta_pruebas_api = resuesta_pruebas_api;

            cargarData();

            

            //Application.Current.Resources["MessageLog"] = "MessageLog test 2";


        }

        public void cargarData()
        {

            var listResultados = new List<Resultado>();

            //System.Windows.MessageBox.Show(" this.resuesta_pruebas_api :  " + this.resuesta_pruebas_api);

            try
            {

                foreach (JObject suggestion in this.resuesta_pruebas_api)
                {

                    listResultados.Add(new Resultado() { id = Convert.ToInt32(suggestion["id"]), rut = (String)suggestion["rut"], alumno_existe = (int)suggestion["alumno_existe"], errores = (String)suggestion["errores"], nombre = (String)suggestion["nombre_alumno"], prueba_alumno_id = (int)suggestion["prueba_alumno_id"] });

                }

            }
            catch (COMException ex)
            {
                MessageBox.Show("Problema con la respuesta del servidor : " + ex.Message);
            }



            dataGridName.ItemsSource = listResultados;





        }




        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {




            this.csvEntornoUsuarioFinalPrincipal.index();

            //Application.Current.MainWindow.Close();

            //var form = new MainWindow();
            //form.Show();


        }




        private void btnView_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var id1 = (Resultado)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvTemplateModel data = new csvTemplateModel();

                //MessageBox.Show("dominio : " + System.Windows.Application.Current.Resources["dominio"]);

                //MessageBox.Show("url : " + System.Windows.Application.Current.Resources["dominio"] + "prueba-alumno/view?id=" + id1.prueba_alumno_id);


                System.Diagnostics.Process.Start(System.Windows.Application.Current.Resources["dominio"] + "prueba-alumno/view?id=" + id1.prueba_alumno_id);



            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void DataGridName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
