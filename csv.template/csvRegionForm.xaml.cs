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
    public partial class csvRegionForm : UserControl
    {

        private csvRegionDb objetoDB = new csvRegionDb();

        csvTemplatePrincipal template_principal;

        csvTemplateModel dataTemplate = new csvTemplateModel();

        private bool editar = false;

        private int idData;

        private int X;

        private int Y;

        private int Width;

        private int Height;
















        /// <summary>
        /// Tamanho original de la iamgen
        /// </summary>
        System.Drawing.Size imageSize;

        /// <summary>
        /// Definicion del rectangulo que define el tamanho total del reactivo o identificador
        /// </summary>
        UserRect baseRectangle;





        public csvRegionForm(csvTemplatePrincipal template_principal, csvRegionModel data, csvTemplateModel dataTemplate)
        {





            this.template_principal = template_principal;

            InitializeComponent();

            if (data.id > 0)
            {
                this.editar = true;

                this.idData = data.id;

                this.X = data.x;

                this.Y = data.y;

                this.Width = data.width;

                this.Height = data.height;

                cargarData(data);
            }


            this.dataTemplate = dataTemplate;


            CargarImagen();



        }






        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {

            //MessageBox.Show("Window_OnKeyDown : ");

            if (e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Left) // The Arrow-Down key
            {
                UserRect selectedRect = null;


                selectedRect = baseRectangle;

                if (selectedRect != null)
                {
                    var addX = 0;
                    var addY = 0;
                    switch (e.Key)
                    {
                        case Key.Down:
                            addY += 1;
                            break;
                        case Key.Up:
                            addY -= 1;
                            break;
                        case Key.Left:
                            addX -= 1;
                            break;
                        case Key.Right:
                            addX += 1;
                            break;
                    }

                    selectedRect.rect.X += addX;
                    selectedRect.rect.Y += addY;
                    selectedRect.UpdatePictureBox();
                }

            }


        }












        private void picImage_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("picImage_Click");
        }

        public void CargarImagen()
        {


            System.Drawing.Image image = null;

            //MessageBox.Show("this.dataTemplate.imagen : " + this.dataTemplate.imagen);

            image = System.Drawing.Image.FromFile(this.dataTemplate.imagen);

            imageSize = new System.Drawing.Size(image.Width, image.Height);

            // Try Resize if needed
            if (chkSenales.IsChecked.Value)
            {
                var newBitmap = ImageHelper.ResizeToUpperSquares(image);
                if (newBitmap != null)
                {
                    image = newBitmap;
                    imageSize = new System.Drawing.Size(newBitmap.Width, newBitmap.Height);
                }
            }

            

            PicImage.Image = image;
            PicImage.Size = imageSize;
            PicImage.Refresh();

            DrawSize();

        }

        private void DrawSize()
        {

            /// este if consulta si es un nuevo registro o si no hay nada dibujado

            if (baseRectangle == null)
            {
                var x = 48;
                var y = 327;

                if (this.editar == true)
                {

                    if (this.Width != 0 && this.Height != 0)
                    {
                        baseRectangle = new UserRect(new System.Drawing.Rectangle(this.X, this.Y, this.Width, this.Height));
                        baseRectangle.PenColor = System.Drawing.Color.Red;
                        baseRectangle.SetPictureBox(PicImage);
                    }

                }
                else
                {
                    baseRectangle = new UserRect(new System.Drawing.Rectangle(x, y, 110, 22));

                    baseRectangle.PenColor = System.Drawing.Color.Red;
                    baseRectangle.PenWeight = 1F;
                    baseRectangle.SetPictureBox(PicImage);
                    baseRectangle.mIsDrawing = true;
                    baseRectangle.mIsPlacing = true;
                    PicImage.Cursor = System.Windows.Forms.Cursors.Cross;

                }





                //baseRectangle.Click += new EventHandler(BaseRect_Click);

                //btnAddOption.Enabled = true;
            }
        }

        public void cargarData(csvRegionModel data)
        {

            //MessageBox.Show(data.nombre);

            txtNombre.Text = data.nombre;
            txtDescripcion.Text = data.descripcion;

            //MessageBox.Show("data.tipo_elemento_id : " + data.tipo_elemento_id);

            if (data.tipo_elemento_id == 1)
            {
                radIdent.IsChecked = true;
            }

            if (data.tipo_elemento_id == 2)
            {
                radReac.IsChecked = true;
            }

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

                    objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, txtTypo.Text, this.dataTemplate.id, Convert.ToInt32(baseRectangle.rect.X), Convert.ToInt32(baseRectangle.rect.Y), Convert.ToInt32(baseRectangle.rect.Width), Convert.ToInt32(baseRectangle.rect.Height), "1");

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
                    



                    objetoDB.editar(txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(txtTypo.Text), Convert.ToInt32(baseRectangle.rect.X), Convert.ToInt32(baseRectangle.rect.Y), Convert.ToInt32(baseRectangle.rect.Width), Convert.ToInt32(baseRectangle.rect.Height), "1", idData);


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

            this.template_principal.indexRegion();

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            int typo = 0;
            RadioButton rb = sender as RadioButton;



            if (rb.Content.ToString() == "Informativo (Rut)")
            {
                typo = 1;
            }

            if (rb.Content.ToString() == "Con Respuesta (pregunta)")
            {
                typo = 2;
            }

            if (rb.Content.ToString() == "Círculo guía")
            {
                typo = 3;
            }

            txtTypo.Text = typo.ToString();

        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {

            this.template_principal.indexRegion();

        }
    }




}
