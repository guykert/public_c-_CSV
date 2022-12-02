using ET.Evaluation;
using Fw.Data.logicas;
using Microsoft.Win32;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class csvSubRegionForm : UserControl
    {

        private csvSubRegionDb objetoDB = new csvSubRegionDb();

        csvTemplatePrincipal template_principal;

        csvRegionModel dataRegion = new csvRegionModel();

        csvTemplateModel dataTemplate = new csvTemplateModel();

        

        private bool editar = false;

        private int idData;

        private int X;

        private int Y;

        private int Width;

        private int Height;

        private int region_X;

        private int region_Y;

        private int region_Width;

        private int region_Height;

        private string valor;

        /// <summary>
        /// Tamanho original de la iamgen
        /// </summary>
        System.Drawing.Size imageSize;

        /// <summary>
        /// Definicion del rectangulo que define el tamanho total del reactivo o identificador
        /// </summary>
        UserRect baseRectangle;


        UserRect baseRectangleRegion;


        public csvSubRegionForm(csvTemplatePrincipal template_principal, Fw.Data.logicas.LecturaAlternativas data, csvRegionModel dataRegion, csvTemplateModel dataTemplate)
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

                this.valor = data.valor;



                cargarData(data);
            }

            this.region_X = dataRegion.x;

            this.region_Y = dataRegion.y;

            this.region_Width = dataRegion.width;

            this.region_Height = dataRegion.height;


            this.dataRegion = dataRegion;

            this.dataTemplate = dataTemplate;

            CargarImagen();

            //InputBindings.Add(new KeyBinding(
            //    new WindowCommand(this)
            //    {
            //        ExecuteDelegate = _saveAndNew
            //    }, new KeyGesture(Key.N, ModifierKeys.Control)));

        }

        private void _saveAndNew()
        {
            MessageBox.Show("_saveAndNew");
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("picImage_Click");
        }

        public void CargarImagen()
        {


            System.Drawing.Image image = null;

            image = System.Drawing.Image.FromFile(this.dataTemplate.imagen);

            imageSize = new System.Drawing.Size(image.Width, image.Height);

            //// Try Resize if needed
            //if (chkSenales.IsChecked.Value)
            //{
            //    var newBitmap = ImageHelper.ResizeToUpperSquares(image);
            //    if (newBitmap != null)
            //    {
            //        image = newBitmap;
            //        imageSize = new System.Drawing.Size(newBitmap.Width, newBitmap.Height);
            //    }
            //}

            PicImage.Image = image;
            PicImage.Size = imageSize;

            
            PicImage.Refresh();

            

            DrawSizeRegion();
            
            SetSubImage();

            DrawSize();

            PicImage.Margin = new System.Windows.Forms.Padding(0, 0, 30, 30);




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

        private void DrawSizeRegion()
        {

            /// este if consulta si es un nuevo registro o si no hay nada dibujado

            if (baseRectangle == null)
            {
                var x = 48;
                var y = 327;

                // MessageBox.Show("this.region_X : " + this.region_X + " this.region_Y : " + this.region_Y + " this.region_Width : " + this.region_Width + " this.region_Height : " + this.region_Height);

                baseRectangleRegion = new UserRect(new System.Drawing.Rectangle(this.region_X, this.region_Y, this.region_Width, this.region_Height));

                //baseRectangleRegion.PenColor = System.Drawing.Color.Red;
                //baseRectangle.PenWeight = 1F;
                //baseRectangle.SetPictureBox(PicImage);
                //baseRectangle.mIsDrawing = true;
                //baseRectangle.mIsPlacing = true;
                //PicImage.Cursor = System.Windows.Forms.Cursors.Cross;

                //baseRectangle.Click += new EventHandler(BaseRect_Click);

                //btnAddOption.Enabled = true;
            }
        }

        private void SetSubImage()
        {

            //MessageBox.Show("SetSubImage");

            Bitmap mainImage = null;

            try
            {
                //if (!File.Exists(txtImage.Text)) return;

                // get subelement
                mainImage = System.Drawing.Image.FromFile(this.dataTemplate.imagen) as Bitmap;

                // Try Resize if needed
                //if (chkSenales.Checked)
                //{
                //    var newBitmap = ImageHelper.ResizeToUpperSquares(mainImage);
                //    if (newBitmap != null)
                //    {
                //        mainImage = newBitmap;
                //    }
                //}

                var subElementImage = mainImage.Clone(baseRectangleRegion.rect, mainImage.PixelFormat);
                PicImage.Image = subElementImage;
                PicImage.Size = subElementImage.Size;
                PicImage.Refresh();
                picSubelementos.Size = subElementImage.Size;
                picSubelementos.Refresh();
            }
            catch (Exception err)
            {
                try
                {
                    if (mainImage != null) mainImage.Dispose();
                }
                catch (Exception) { }

                MessageBox.Show("Error: " + err.Message);
            }
        }



        public void cargarData(Fw.Data.logicas.LecturaAlternativas data)
        {

            //MessageBox.Show(data.nombre);

            txtNombre.Text = data.nombre;
            txtDescripcion.Text = data.descripcion;
            txtValor.Text = data.valor;

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

                    objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, txtValor.Text, Convert.ToInt32(baseRectangle.rect.X), Convert.ToInt32(baseRectangle.rect.Y), Convert.ToInt32(baseRectangle.rect.Width), Convert.ToInt32(baseRectangle.rect.Height), this.dataRegion.id, "1");

                    //objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, this.dataRegion.id, "1");

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
                    



                    objetoDB.editar(txtNombre.Text, txtDescripcion.Text, txtValor.Text, Convert.ToInt32(baseRectangle.rect.X), Convert.ToInt32(baseRectangle.rect.Y), Convert.ToInt32(baseRectangle.rect.Width), Convert.ToInt32(baseRectangle.rect.Height), "1", idData);


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

            this.template_principal.indexSubRegion();

        }



        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {

            this.template_principal.indexSubRegion();

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


    }
}
