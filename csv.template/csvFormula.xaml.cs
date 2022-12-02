using Fw.Data.logicas;
using Microsoft.Win32;
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

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para csvTemplate.xaml
    /// </summary>
    public partial class csvFormula : UserControl
    {

        private csvFormulaDb objetoDB = new csvFormulaDb();

        private csvSubRegionDb objetoDBSubRegion = new csvSubRegionDb();

        csvTemplateModel dataTemplate = new csvTemplateModel();

        csvTemplatePrincipal templaate_principal;

        public delegate void someDelegate(string a);

        public csvFormula( csvTemplatePrincipal templaate_principal, csvTemplateModel data)
        {

            this.templaate_principal = templaate_principal;

            InitializeComponent();

            this.dataTemplate = data;

            mostrarGrilla();

        }

        public void mostrarGrilla()
        {

            dataGridName.ItemsSource = objetoDB.LoadData(this.dataTemplate.id).DefaultView;

            labNombreTemplate.Content = dataTemplate.nombre;

            labDescripcionTemplate.Content = dataTemplate.descripcion;


        }




        private void BottomNew_Click(object sender, RoutedEventArgs e)
        {


            this.templaate_principal.nuevoFormulaForm();



        }

        private void DataGridName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvFormulaModel data = new csvFormulaModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.template_id = Convert.ToInt32(id1.Row["template_id"].ToString());

                data.formula = id1.Row["formula"].ToString();





                this.templaate_principal.editarFormulaForm(data);


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

                    mostrarGrilla();

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


                csvRegionModel data = new csvRegionModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.template_id = Convert.ToInt32(id1.Row["template_id"].ToString());

                data.tipo_elemento_id = Convert.ToInt32(id1.Row["tipo_elemento_id"].ToString());

                data.x = Convert.ToInt32(id1.Row["x"].ToString());

                data.y = Convert.ToInt32(id1.Row["y"].ToString());

                data.width = Convert.ToInt32(id1.Row["width"].ToString());

                data.height = Convert.ToInt32(id1.Row["height"].ToString());

                //MessageBox.Show("data.x : " + data.x + " data.y : " + data.y + " data.width : " + data.width + " data.height : " + data.height);


                this.templaate_principal.ViewSubRamoForm(data);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {
            this.templaate_principal.index();
        }


    }
}
