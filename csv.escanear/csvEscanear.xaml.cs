using ET.Evaluation;
using Fw.Data.logicas;
using Microsoft.Win32;
using scanner.csv.csv.escanear;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.Data;
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
using Newtonsoft.Json;
using scanner.csv.services;
using Newtonsoft.Json.Linq;

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para csvTemplate.xaml
    /// </summary>
    public partial class csvEscanear : UserControl
    {

        private csvEscanearDb objetoDB = new csvEscanearDb();

        csvEscanearPrincipal escanear_principal;

        public static JArray resuesta_pruebas_api;

        public delegate void someDelegate(string a);

        public csvEscanear(csvEscanearPrincipal escanear_principal)
        {

            this.escanear_principal = escanear_principal;

            InitializeComponent();

            mostrarTemplates();



        }

        public void mostrarTemplates()
        {

            dataGridName.ItemsSource = objetoDB.LoadData().DefaultView;

        }




        private void BottomNew_Click(object sender, RoutedEventArgs e)
        {


            this.escanear_principal.nuevoForm();



        }

        private void DataGridName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvEscanearModel data = new csvEscanearModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.fecha_aplicacion = id1.Row["fecha_aplicacion"].ToString();

                data.carpeta = id1.Row["carpeta"].ToString();

                data.template_id = Convert.ToInt32(id1.Row["template_id"].ToString());


                this.escanear_principal.editarForm(data);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.

                var PK_ID = Convert.ToInt32(id1.Row["id"].ToString());

                if (MessageBox.Show("Realmente quieres eliminar el registro", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    objetoDB.eliminar("1",Convert.ToString(PK_ID));

                    mostrarTemplates();

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvEscanearModel data = new csvEscanearModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.template_id = Convert.ToInt32(id1.Row["template_id"].ToString());


                //this.escanear_principal.ViewForm(data);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void btnEscanear_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Wait;

            // instancia la clase para evaluar examenes
            var evaluation = new Evaluator_csv();
            var alert = false;
            var alertMessage = string.Empty;

            //try
            //{

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvEscanearModel data = new csvEscanearModel();

                var listpruebaAlumnoEnviarWeb = new List<PruebaAlumnoEnviarWeb>();


                var carpeta = id1.Row["carpeta"].ToString();

                //evaluation.Evaluate_csv(ref TextBox log, Convert.ToInt32(id1.Row["id"].ToString()), ref listpruebaAlumnoEnviarWeb, out alert, out alertMessage, (message) =>
                //{
                //    // accion que se ejecuta en cada iteracion de los examenes
                //    // para poder actualizar la barra de progreso

                //    #region Actualiza Barra de progreso



                //    #endregion
                //});

                enviamosLosdatosWeb(listpruebaAlumnoEnviarWeb);


                mostrarTemplates();

            //}
            //catch (Exception err)
            //{
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();

            //    MessageBox.Show("Error evaluando el examen: " + Environment.NewLine +
            //            err.ToString());
            //}


            this.Cursor = System.Windows.Input.Cursors.Arrow;

        }

        public void enviamosLosdatosWeb(List<PruebaAlumnoEnviarWeb> listpruebaAlumnoEnviarWeb)
        {

            string json = JsonConvert.SerializeObject(listpruebaAlumnoEnviarWeb);

            string response = pruebaService.setPruebaAlumno(json, ref resuesta_pruebas_api);
                
            MessageBox.Show("Datos Guardados Exitosamenete : response. " + response);

            //string response2 = pruebaService.setPruebaSubirImagenes(json, listpruebaAlumnoEnviarWeb);



            //MessageBox.Show("Datos Guardados Exitosamenete : response2. " + response2);

        }



    }
}
