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
    public partial class csvTemplateRegionesImagenForm : UserControl
    {



        csvTemplatePrincipal template_principal;

        /// <summary>
        /// Tamanho original de la iamgen
        /// </summary>
        System.Drawing.Size imageSize;

        /// <summary>
        /// Definicion del rectangulo que define el tamanho total del reactivo o identificador
        /// </summary>
        UserRect baseRectangle;





        public csvTemplateRegionesImagenForm(csvTemplatePrincipal template_principal)
        {

            this.template_principal = template_principal;

            InitializeComponent();

            CargarImagen();

        }





        public void CargarImagen()
        {


            System.Drawing.Image image = null;

            //MessageBox.Show("this.template_principal : " + this.template_principal.ToString());

            //image = System.Drawing.Image.FromFile(this.dataTemplate.imagen);

            //imageSize = new System.Drawing.Size(image.Width, image.Height);



            //PicImage.Image = image;
            //PicImage.Size = imageSize;
            //PicImage.Refresh();

            //DrawSize();

        }



        public void volverALaHome()
        {

            this.template_principal.indexRegion();

        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {

        }
    }




}
