using Fw.Data.logicas;
using Microsoft.Win32;
using scanner.csv.csv.escanear;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para csvTemplateForm.xaml
    /// </summary>
    public partial class csvEscanearForm : UserControl
    {

        private csvEscanearDb objetoDB = new csvEscanearDb();

        private csvTemplatesDb objetoDBTall = new csvTemplatesDb();

        csvEscanearPrincipal escanear_principal;

        private bool editar = false;

        private int idData;

        public csvEscanearForm(csvEscanearPrincipal escanear_principal, csvEscanearModel data)
        {

            this.escanear_principal = escanear_principal;

            InitializeComponent();

            objetoDBTall.ComboBox(ref cbxTemplate);

            if (data.id > 0)
            {
                this.editar = true;

                this.idData = data.id;

                cargarData(data);
            }

            

        }

        public void cargarData(csvEscanearModel data)
        {

            //MessageBox.Show(data.nombre);

            txtNombre.Text = data.nombre;
            txtDescripcion.Text = data.descripcion;
            txtCarpeta.Text = data.carpeta;
            cbxTemplate.SelectedValue = data.template_id;

            //MessageBox.Show("data.fecha_aplicacion : " + data.fecha_aplicacion);

            DateTime oDate = DateTime.Parse(data.fecha_aplicacion);

            FutureDatePicker.SelectedDate = new DateTime(oDate.Year, oDate.Month, oDate.Day);














        }



        private void BottomGuardar_Click(object sender, RoutedEventArgs e)
        {

            /// <summary>
            /// Guardamos los registros
            /// Me falta ver los temas de validaciones
            /// </summary>
            /// 

            //MessageBox.Show("cbxTemplate.SelectedIndex : " + cbxTemplate.SelectedValue);

            if (editar == false)
            {
                try
                {

                    objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(cbxTemplate.SelectedValue), FutureDatePicker.Text, txtCarpeta.Text, "1");

                    volverALaHome(); 

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos por : " + ex);
                }
            }

            if (editar == true)
            {
                try
                {

                    objetoDB.editar(txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(cbxTemplate.SelectedValue), FutureDatePicker.Text, txtCarpeta.Text, "1", idData);


                    volverALaHome();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar los datos por : " + ex);
                }
            }


        }



        public void volverALaHome()
        {

            this.escanear_principal.index();

        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {
            this.escanear_principal.index();
        }

        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog folderBrowser = new OpenFileDialog();

            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."

            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;

            folderBrowser.FileName = "Selecciónar Carpeta.";

            if (folderBrowser.ShowDialog() == true)

                txtCarpeta.Text = System.IO.Path.GetDirectoryName(folderBrowser.FileName);

  

        }
    }
}
