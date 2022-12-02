using ET.Evaluation;
using Fw.Data.logicas;
using Microsoft.Win32;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using UserRectDemo;


namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para csvTemplateForm.xaml
    /// </summary>
    public partial class csvFormulaForm : UserControl
    {

        private csvFormulaDb objetoDB = new csvFormulaDb();

        csvTemplatePrincipal template_principal;

        csvTemplateModel dataTemplate = new csvTemplateModel();

        private bool editar = false;

        private int idData;

















        /// <summary>
        /// Tamanho original de la iamgen
        /// </summary>
        System.Drawing.Size imageSize;

        /// <summary>
        /// Definicion del rectangulo que define el tamanho total del reactivo o identificador
        /// </summary>
        UserRect baseRectangle;





        public csvFormulaForm(csvTemplatePrincipal template_principal, csvFormulaModel data, csvTemplateModel dataTemplate)
        {





            this.template_principal = template_principal;

            InitializeComponent();

            if (data.id > 0)
            {
                this.editar = true;

                this.idData = data.id;



                cargarData(data);
            }


            this.dataTemplate = dataTemplate;






        }

        public void cargarData(csvFormulaModel data)
        {

            //MessageBox.Show(data.nombre);

            txtNombre.Text = data.nombre;
            txtDescripcion.Text = data.descripcion;

            txtFormula.Text = data.formula;

            //MessageBox.Show("data.tipo_elemento_id : " + data.tipo_elemento_id);


            //txtImagen.Text = data.imagen;


        }


        private void BottomGuardar_Click(object sender, RoutedEventArgs e)
        {

            /// <summary>
            /// Guardamos los registros
            /// Me falta ver los temas de validaciones
            /// </summary>
            /// 

            

            if (editar == false)
            {
                try
                {

                    objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, txtFormula.Text, this.dataTemplate.id, "1");

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
                    



                    objetoDB.editar(txtNombre.Text, txtDescripcion.Text, txtFormula.Text, "1", idData);


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

            this.template_principal.indexFormula();

        }



        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {

            this.template_principal.index();

        }
    }




}
