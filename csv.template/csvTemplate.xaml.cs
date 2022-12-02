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
    public partial class csvTemplate : UserControl
    {

        private csvTemplatesDb objetoDB = new csvTemplatesDb();

        private csvRegionDb objetoDBRegion = new csvRegionDb();

        private csvSubRegionDb objetoDBSubRegion = new csvSubRegionDb();

        csvTemplatePrincipal templaate_principal;

        public delegate void someDelegate(string a);

        public csvTemplate( csvTemplatePrincipal templaate_principal)
        {

            this.templaate_principal = templaate_principal;

            InitializeComponent();

            mostrarTemplates();

        }

        public void mostrarTemplates()
        {

            dataGridName.ItemsSource = objetoDB.LoadData().DefaultView;

        }




        private void BottomNew_Click(object sender, RoutedEventArgs e)
        {


            this.templaate_principal.nuevoForm();



        }

        private void DataGridName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.

                

                csvTemplateModel data = new csvTemplateModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.imagen = id1.Row["imagen"].ToString();

                data.cuadrados = Convert.ToInt32(id1.Row["cuadrados"].ToString());


                this.templaate_principal.editarForm(data);


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


                csvTemplateModel data = new csvTemplateModel();

                //MessageBox.Show("imagen : " + id1.Row["imagen"].ToString());

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.imagen = id1.Row["imagen"].ToString();


                this.templaate_principal.ViewForm(data);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void btnClonar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.




                try
                {

                    var id_template_nuevo = objetoDB.insertar_id(id1.Row["nombre"].ToString() + " clonado", id1.Row["descripcion"].ToString() + " clonado", id1.Row["imagen"].ToString(), (bool)id1.Row["cuadrados"] , System.Windows.Application.Current.Resources["usuario_id"].ToString());

                    // busco las regiones que tiene el template

                    

                    var regiones = objetoDBRegion.regiones(Convert.ToInt32(id1.Row["id"].ToString()));

                    foreach (var region in regiones)
                    {

                        // inserto la region

                        var id_region_nuevo = objetoDBRegion.insertar_id(region.nombre, region.descripcion, region.tipo_elemento_id+"", id_template_nuevo, region.x, region.y, region.width, region.height, "1");

                        // busco las subRegiones

                        var sub_regiones = objetoDBSubRegion.subRegiones(region.id);

                        foreach (var sub_region in sub_regiones)
                        {

                            objetoDBSubRegion.insertar(sub_region.nombre, sub_region.descripcion, sub_region.valor, sub_region.x, sub_region.y, sub_region.width, sub_region.height, id_region_nuevo, "1");

                        }


                    }


                    mostrarTemplates();




                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos por : " + ex);
                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }

        private void btnformulas_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                csvTemplateModel data = new csvTemplateModel();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();

                data.imagen = id1.Row["imagen"].ToString();


                this.templaate_principal.ViewFormula(data);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

        }
    }
}
