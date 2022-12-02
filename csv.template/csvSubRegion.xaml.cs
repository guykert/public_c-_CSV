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
    public partial class csvSubRegion : UserControl
    {

        private csvSubRegionDb objetoDB = new csvSubRegionDb();

        csvRegionModel dataRegion = new csvRegionModel();

        csvTemplatePrincipal templaate_principal;

        public delegate void someDelegate(string a);

        public csvSubRegion( csvTemplatePrincipal templaate_principal, csvRegionModel data)
        {

            this.templaate_principal = templaate_principal;

            InitializeComponent();

            this.dataRegion = data;

            mostrarGrilla();

        }

        public void mostrarGrilla()
        {

            dataGridName.ItemsSource = objetoDB.LoadData(this.dataRegion.id).DefaultView;

            labNombreTemplate.Content = dataRegion.nombre;

            labDescripcionTemplate.Content = dataRegion.descripcion;


        }




        private void BottomNew_Click(object sender, RoutedEventArgs e)
        {


            this.templaate_principal.nuevoSubRegionForm();



        }

        private void DataGridName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var id1 = (DataRowView)dataGridName.SelectedItem;  //Get specific ID From                DataGrid after click on Delete Button.


                Fw.Data.logicas.LecturaAlternativas data = new Fw.Data.logicas.LecturaAlternativas();

                data.id = Convert.ToInt32(id1.Row["id"].ToString());

                data.nombre = id1.Row["nombre"].ToString();

                data.descripcion = id1.Row["descripcion"].ToString();


                data.x = Convert.ToInt32(id1.Row["x"].ToString());

                data.y = Convert.ToInt32(id1.Row["y"].ToString());

                data.width = Convert.ToInt32(id1.Row["width"].ToString());

                data.height = Convert.ToInt32(id1.Row["height"].ToString());

                data.valor = id1.Row["valor"].ToString();



                this.templaate_principal.editarSubRegionForm(data);


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


        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {
            MostrarImagen mostrarImagen = new MostrarImagen(this.dataRegion.template_id, this.dataRegion.id, "imagen_completa","region");
            mostrarImagen.Show();
        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {
            this.templaate_principal.indexRegion();
        }

        private void BtnSearchImageTemplate_Click(object sender, RoutedEventArgs e)
        {
            MostrarImagen mostrarImagen = new MostrarImagen(this.dataRegion.template_id, this.dataRegion.id, "imagen_region", "region");
            mostrarImagen.Show();
        }
    }
}
