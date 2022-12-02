
using scanner.csv.models;
using scanner.csv.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TWAINWorkingGroupToolkit;

namespace scanner.csv
{
    public partial class frmPrincipal2 : Form
    {
        public int childFormNumber = 0;

        public int cantidad_imagenes = 0;

        public frmPrincipal2()
        {

            //MessageBox.Show("trace frmPrincipal 1 : ");



            InitializeComponent();

            //MessageBox.Show("trace frmPrincipal 2 : ");

            inicializo_scanner();


            //MessageBox.Show("trace frmPrincipal 3 : ");

        }





        public void inicializo_scanner()
        {




            if (System.Windows.Application.Current.Resources["escanner_inicializado"] != "true")
            {

                //MessageBox.Show("inicializo_scanner ");

                // Init other stuff...
                m_blIndicators = false;
                m_blExit = false;
                m_iUseBitmap = 0;

                // Create our image capture object...

                //this.FormClosing += new FormClosingEventHandler(FormScan_FormClosing);

                try
                {
                    m_twainScanner = new TWAINCSToolkit
                    (
                        this.Handle,
                        WriteOutput,
                        ReportImage,
                        null,
                        "TWAIN Working Group",
                        "TWAIN Sharp",
                        "TWAIN Sharp Scan App",
                        2,
                        3,
                        new string[] { "DF_APP2", "DG_CONTROL", "DG_IMAGE" },
                        "USA",
                        "testing...",
                        "ENGLISH_USA",
                        1,
                        0,
                        false,
                        true,
                        RunInUiThread,
                        this



                    );
                }
                catch
                {
                    m_twainScanner = null;
                    m_blExit = true;
                    MessageBox.Show
                    (
                        "Unable to start, the most likely reason is that the TWAIN\n" +
                        "Data Source Manager is not installed on your system.\n\n" +
                        "An internet search for 'TWAIN DSM' will locate it and once\n" +
                        "installed, you should be able to proceed.\n\n" +
                        "You can also try the following link:\n" +
                        "http://sourceforge.net/projects/twain-dsm/",
                        "Error Starting TWAIN CS Scan"
                    );
                    return;
                }

                System.Windows.Application.Current.Resources["escanner_inicializado"] = "true";

            }





        }





        /// <summary>
        /// Si es verdadero, entonces muestre los mensajes de la ventana del controlador mientras estamos escaneando. Establece esto en el constructor ...
        /// </summary>
        public bool m_blIndicators;

        /// <summary>
        /// Utilizar si algo realmente malo sucede ...
        /// </summary>
        public bool m_blExit;

        public int m_iUseBitmap;

        public string szDefault;

        /// <summary>
        /// Esto es para declarar la variable que llamará a twain cambiare la variable pr un nombre más común
        /// </summary>
        public TWAINCSToolkit m_twainScanner;

        /// <summary>
        /// Salida de depuración que podemos monitorear, este es solo un lugar
        /// titular para esta aplicación particular ...
        /// </summary>
        /// <param name="a_szOutput"></param>
        public void WriteOutput(string a_szOutput)
        {
            // We're leaving...
            if (m_blClosing)
            {
                return;
            }

            // Let us be called from any thread.  We don't care if a_szOutput changes
            // on the fly (it's incredibly unlikely that it will), so we're not going
            // to wait for the thread to finish...
            if (this.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate () { WriteOutput(a_szOutput); }));
                return;
            }

            // Display the text...

            //MessageBox.Show("a_szOutput : " + a_szOutput);
            //m_richtextboxOutput.AppendText(a_szOutput);
            //m_richtextboxOutput.SelectionStart = m_richtextboxOutput.Text.Length;
            //m_richtextboxOutput.Refresh();
        }

        /// <summary>
        /// Manejar una imagen. a_bitmap se pasa por referencia para que esta función pueda disponer y anularlo para obtener acceso al archivo que lo respalda. 
        /// La función del kit de herramientas de llamada nunca realizará ninguna acción con un_bitmap después de que esta función vuelva.
        /// </summary>
        /// <param name="a_szDg">Grupo de datos que precedió a esta llamada.</param>
        /// <param name="a_szDat">Tipo de argumento de datos que precedió a esta llamada</param>
        /// <param name="a_szMsg">Mensaje que precedió a esta llamada</param>
        /// <param name="a_sts">Estado actual</param>
        /// <param name="a_bitmap">C# bitmap de la imagen.</param>
        /// <param name="a_szFile">Nombre de archivo, si está haciendo una transferencia de archivos esto lo cambiare de forma temporal para poder guardar mejor las imagenes</param>
        /// <param name="a_szTwimageinfo">datos recopilados para nosotros</param>
        /// <param name="a_abImage">una matriz de bytes de la imagen</param>
        /// <param name="a_iImageOffset">Byte offset donde comienzan los datos de la imagen</param>
        public TWAINCSToolkit.MSG ReportImage
        (
            string a_szTag,
            string a_szDg,
            string a_szDat,
            string a_szMsg,
            TWAINCSToolkit.STS a_sts,
            Bitmap a_bitmap,
            string a_szFile,
            string a_szTwimageinfo,
            byte[] a_abImage,
            int a_iImageOffset
        )
        {
            //System.Diagnostics.Debug.Write(" a_szFile : " + a_szFile + " a_szTwimageinfo" + a_szTwimageinfo);



            // Nos vamos
            /*if (m_graphics1 == null)
            {
                return (TWAINCSToolkit.MSG.RESET);
            }*/

            //a_szFile = "C:\\escanerpdv2\\imagenes_escaneadas" + "img";


            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szFile : " + a_szFile);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szFile : " + a_szFile);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szTwimageinfo : " + a_szTwimageinfo);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szTag : " + a_szTag);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szDg : " + a_szDg);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szDat : " + a_szDat);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_szMsg : " + a_szMsg);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_sts : " + a_sts);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_bitmap : " + a_bitmap);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_abImage : " + a_abImage);
            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_iImageOffset : " + a_iImageOffset);

            // a_szMsg alaparece como reser al parecer se da una buelta demás


            // Seamos llamados desde cualquier hilo ...
            if (this.InvokeRequired)
            {

                //MessageBox.Show("Seamos llamados desde cualquier hilo ... ");

                // Necesitamos una copia del mapa de bits, porque no vamos a esperar a que vuelva el hilo. Tenga cuidado al usar EndInvoke. 
                // Es posible crear una situación de interbloqueo al presionar el botón Detener.
                BeginInvoke(new MethodInvoker(delegate () { ReportImage(a_szTag, a_szDg, a_szDat, a_szMsg, a_sts, a_bitmap, a_szFile, a_szTwimageinfo, a_abImage, a_iImageOffset); }));
                return (TWAINCSToolkit.MSG.ENDXFER);
            }


            // Estamos procesando el final del escaneo ...
            if (a_bitmap == null)
            {
                // Informe de errores, pero solo si los indicadores del controlador se han apagado, de lo contrario, le daremos al usuario varios cuadros de diálogo con el mismo error ...


                //if (!m_blIndicators && (a_sts != TWAINCSToolkit.STS.SUCCESS))
                //{
                //    MessageBox.Show("El scanner precenta un error al parecer tiene una hoja atascada: " + a_sts);



                //}

                // Get ready for the next scan...

                //MessageBox.Show("Ncantidad de scanners : " + EBUTTONSTATE.OPEN);
                //MessageBox.Show("Ncantidad de scanners2 : " + pruebasBitmaps.Count);
                //MessageBox.Show("Ncantidad de scanners3 : " + TWAINCSToolkit.MSG.ENDXFER);

                //csvEntornoUsuarioFinalPrincipal UsuariofinalPrincipal = new csvEntornoUsuarioFinalPrincipal();

                //csvEntornoUsuarioFinal Usuariofinal = new csvEntornoUsuarioFinal(UsuariofinalPrincipal);

                //Usuariofinal.progressbar.IsIndeterminate = false;
                escaneo_finalizado = true;
                this.cantidad_imagenes = 0;


                //SetButtons(EBUTTONSTATE.OPEN);
                //MessageBox.Show(" Total de hojas escaneadas : " + pruebasBitmaps.Count);
                return (TWAINCSToolkit.MSG.ENDXFER);
            }

            a_bitmap = new Bitmap(a_bitmap, new Size(1600, 2200));

            LoadImage(a_bitmap);

            if (System.Windows.Application.Current.Resources["generar_template"] == "true")
            {
                guardar_imagenes(a_bitmap, a_szFile);
            }

            //guardar_imagenes(a_bitmap, a_szFile);


            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_bitmap : " + a_bitmap);


            //MessageBox.Show("final : " + TWAINCSToolkit.MSG.ENDXFER);

            return (TWAINCSToolkit.MSG.ENDXFER);

        }

        public void guardar_imagenes(Bitmap a_bitmap, string a_szFile)
        {
            this.cantidad_imagenes++;
            //MessageBox.Show("guardar_imagenes : ");


            String searchString = "img";

            int startIndex = a_szFile.IndexOf(searchString) + 5;

            String searchString2 = ".";

            int endIndex = a_szFile.IndexOf(searchString2) - 3;

            String substring = a_szFile.Substring(startIndex, endIndex + searchString.Length - startIndex);


            //MessageBox.Show("Home -> startIndex : " + startIndex + " -> endIndex : " + endIndex + " -> substring : " + substring);



            //BeginInvoke((Action)delegate { backgroundWorker.ReportProgress(y); }); 

            DateTime localDate = DateTime.Now;

            //string newFolder = "imagenes_escaneadas";

            //string path = System.IO.Path.Combine(
            //   Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            //   newFolder
            //);

            //if (!System.IO.Directory.Exists(path))
            //{
            //    try
            //    {
            //        System.IO.Directory.CreateDirectory(path);
            //    }
            //    catch (IOException ie)
            //    {
            //        Console.WriteLine("IO Error: " + ie.Message);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("General Error: " + e.Message);
            //    }
            //}

            //System.IO.File.Exists(filename)
            //MessageBox.Show("Home -> guardar_imagenes : " + localDate.ToString("yyyyMMdd_Hmmss"));

            System.IO.Directory.CreateDirectory(@"imagenes_escaneadas");

            //MessageBox.Show("C:\\escanerpdv\\prueba_" + localDate.ToString("yyyyMMdd_Hmmss") + ".tif");
            //C:\wamp64\www\desarrollo_csv\app_visual\imagenes_escanear\escanerpdv2\

            System.Windows.Application.Current.Resources["ruta_template"] = "imagenes_escaneadas\\prueba_" + localDate.ToString("yyyyMMdd_Hmmss") + "_" + substring + ".jpg";

            a_bitmap.Save(@"imagenes_escaneadas\\prueba_" + localDate.ToString("yyyyMMdd_Hmmss") + "_" + substring + ".jpg", ImageFormat.Jpeg); //+ pruebaAlumno.rut+ ".tif");


        }



        /// <summary>
        /// TWAIN necesita ayuda, si queremos que se ejecute en nuestro hilo de interfaz de usuario principal ...
        /// </summary>
        /// <param name="control">el control para ejecutar en</param>
        /// <param name="code">el código para ejecutar</param>
        static public void RunInUiThread(Object a_object, Action a_action)
        {

            //MessageBox.Show("RunInUiThread : ");

            Control control = (Control)a_object;
            if (control.InvokeRequired)
            {
                control.Invoke(new TWAINCSToolkit.RunInUiThreadDelegate(RunInUiThread), new object[] { a_object, a_action });
                return;
            }
            a_action();
        }

        public string[] aszIdentity;

        public string szIdentity;

        /// <summary>
        /// Configure nuestros botones para que coincida con nuestro estado actual ...
        /// </summary>



        /// <summary>
        /// Cargar una imagen en la lista pruebasAlumno.
        /// </summary>
        /// <param name="a_bitmap"></param>
        private void LoadImage(Bitmap a_bitmap)
        {
            // esto se ejecuta al leer las hojas cuando pasan por el scanner

            // Esto agrega las imagenes a este arreglo al parecer
            pruebasBitmaps.Add(a_bitmap);

            // Asigna una imagen a esta variable con 
            //Config.ultimaHoja = a_bitmap;




        }

        public List<Bitmap> pruebasBitmaps = new List<Bitmap>();

        public string szStatus;

        public TWAINCSToolkit.STS sts;

        /// <summary>
        /// Usamos este nombre (modificado y hecho seguro para el sistema de archivos) como el lugar donde pondremos personalizar la data
        /// </summary>
        public string m_szProductDirectory;

        public string szCapability;
        public bool escaneo_finalizado = false;

        // Let us know when we're shutting down...
        private bool m_blClosing;
    }
}
