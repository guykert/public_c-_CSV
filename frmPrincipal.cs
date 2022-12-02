using scanner.csv.csv.entornoUsuarioFinal;
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
    public partial class frmPrincipal : Form
    {
        public int childFormNumber = 0;

        public int cantidad_imagenes = 0;
        public List<PruebaAlumnoEscaner> pruebasAlumnos = new List<PruebaAlumnoEscaner>();

        public frmPrincipal()
        {

            //MessageBox.Show("trace frmPrincipal 1 : ");

            
            
            InitializeComponent();

            //MessageBox.Show("trace frmPrincipal 2 : ");

            inicializo_scanner();


            //MessageBox.Show("trace frmPrincipal 3 : ");

        }


        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var frm = new frmTiposElemento();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var frm = new frmPlantillas();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var frm = new frmExamenes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmOpciones();
            form.ShowDialog();
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {

            var form = new InterfazVieja();
            form.ShowDialog();

        }

        //private void ToolStripButton6_Click(object sender, EventArgs e)
        //{

        //    this.cantidad_imagenes = 0;
        //    this.szDefault = "";

        //    this.aszIdentity = m_twainScanner.GetDrivers(ref this.szDefault);

        //    //MessageBox.Show("this.aszIdentity : " + this.aszIdentity);

        //    MessageBox.Show("(Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ) : " + (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ""));
        //    if (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != "")
        //    {
        //        //    //confirmo si el scanner está conectado y prendido

        //        //MessageBox.Show("ToolStripButton6_Click : " + szIdentity);

        //        m_blExit = true;
        //        foreach (string sz in aszIdentity)
        //        {
        //            if (sz.Contains(Login.Usuario.nombre_scanner_predeterminado))
        //            {
        //                m_blExit = false;
        //                this.szIdentity = sz;
        //                break;
        //            }
        //        }
        //        if (m_blExit)
        //        {
        //            return;
        //        }

        //        MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  szIdentity2 : " + szIdentity);


        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  szIdentity3 : " + szIdentity);

        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.szStatus : " + this.szStatus);

        //        this.szStatus = "";
        //        this.sts = m_twainScanner.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_OPENDS", ref this.szIdentity, ref this.szStatus);




        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.sts : " + this.sts);

        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  TWAINCSToolkit.STS.SUCCESS : " + TWAINCSToolkit.STS.SUCCESS);

        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  (this.sts == TWAINCSToolkit.STS.BUMMER) : " + (this.sts == TWAINCSToolkit.STS.BUMMER));

        //        if (this.sts == TWAINCSToolkit.STS.BUMMER)
        //        {
        //            //MessageBox.Show("El scanner ya se encuentra asignado");
        //            m_blExit = true;

        //        }
        //        else
        //        {

        //            //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.sts : " + this.sts);

        //            //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  TWAINCSToolkit.STS.SUCCESS : " + TWAINCSToolkit.STS.SUCCESS);

        //            //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  (this.sts != TWAINCSToolkit.STS.SUCCESS) : " + (this.sts != TWAINCSToolkit.STS.SUCCESS));

        //            if (this.sts != TWAINCSToolkit.STS.SUCCESS)
        //            {
        //                MessageBox.Show("No se puede abrir el escáner (¿está encendido y enchufado?)");
        //                m_blExit = true;
        //                SetButtons(EBUTTONSTATE.SCANNCLOSED);


        //                return;
        //            }

        //        }


        //        //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  m_blExit : " + m_blExit);

        //        // Sacar los caracteres inseguros. Lamentablemente, mono nos deja aquí abajo ...
        //        m_szProductDirectory = TWAINCSToolkit.CsvParse(szIdentity)[11];
        //        foreach (char c in new char[41]
        //                        { '\x00', '\x01', '\x02', '\x03', '\x04', '\x05', '\x06', '\x07',
        //                      '\x08', '\x09', '\x0A', '\x0B', '\x0C', '\x0D', '\x0E', '\x0F', '\x10', '\x11', '\x12',
        //                      '\x13', '\x14', '\x15', '\x16', '\x17', '\x18', '\x19', '\x1A', '\x1B', '\x1C', '\x1D',
        //                      '\x1E', '\x1F', '\x22', '\x3C', '\x3E', '\x7C', ':', '*', '?', '\\', '/'
        //                        }
        //                )
        //        {
        //            m_szProductDirectory = m_szProductDirectory.Replace(c, '_');
        //        }

        //        //MessageBox.Show("Home -> Home-> m_szProductDirectory : " + m_szProductDirectory);

        //        // Estamos haciendo transferencias de memoria (TWSX_MEMORY == 2) ...
        //        szStatus = "";
        //        szCapability = "ICAP_XFERMECH,TWON_ONEVALUE,TWTY_UINT16,2";
        //        sts = m_twainScanner.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref szCapability, ref szStatus);
        //        if (sts != TWAINCSToolkit.STS.SUCCESS)
        //        {
        //            m_blExit = true;
        //            return;
        //        }

        //        // Decida si desea o no mostrar los mensajes de la ventana del conductor ...
        //        szStatus = "";
        //        szCapability = "CAP_INDICATORS,TWON_ONEVALUE,TWTY_BOOL," + (m_blIndicators ? "1" : "0");
        //        sts = m_twainScanner.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref szCapability, ref szStatus);
        //        if (sts != TWAINCSToolkit.STS.SUCCESS)
        //        {
        //            m_blExit = true;
        //            return;
        //        }

        //        try
        //        {
        //            pruebasAlumnos.Clear();
        //            pruebasBitmaps.Clear();
        //            string szTwmemref;
        //            string szStatus = "";
        //            TWAINCSToolkit.STS sts;

        //            // Comience a escanear en silencio si detectamos que se admite el uso de datos personalizados.De lo contrario, active la GUI del controlador para que el usuario pueda cambiar la configuración...
        //            //if (configuraciones.IsCustomDsDataSupported())
        //            //{
        //            //    szTwmemref = "0,0," + this.Handle;
        //            //}
        //            //else
        //            //{
        //            //    szTwmemref = "1,0," + this.Handle;
        //            //}

        //            szTwmemref = "0,0," + this.Handle;

        //            sts = m_twainScanner.Send("DG_CONTROL", "DAT_USERINTERFACE", "MSG_ENABLEDS", ref szTwmemref, ref szStatus);
        //            SetButtons(EBUTTONSTATE.SCANNING);

        //        }
        //        catch (COMException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }




        //        //MessageBox.Show("Home -> Home-> szIdentity : " + szIdentity);
        //    }
        //    else
        //    {
        //        // veo la cantidad de scanners

        //        //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click : " + aszIdentity.Count());

        //        if (aszIdentity.Count() == 1)
        //        {


        //            m_blExit = true;
        //            foreach (string sz in aszIdentity)
        //            {

        //                this.szIdentity = sz;


        //                aszIdentity = TWAINCSToolkit.CsvParse(sz);
        //                //MessageBox.Show("Home -> Home-> sz : " + aszIdentity[11].ToString());

        //                // Actualizo el usuario con el scanner por defecto

        //                usuarioService.setScannerPredeterminado(aszIdentity[11].ToString());

        //            }
        //            if (m_blExit)
        //            {
        //                return;
        //            }


        //        }
        //        else
        //        {

        //            // Esta parte es en cao de que tenga más de un scanner y tenga que escoger uno como predeterminado

        //            // DialogResult dialogresult;

        //            // SeleccionarScanner seleccionarScanner;

        //            // seleccionarScanner = new SeleccionarScanner(this.aszIdentity, this.szDefault);

        //            // seleccionarScanner.StartPosition = FormStartPosition.CenterParent;
        //            // dialogresult = seleccionarScanner.ShowDialog(this);
  
        //            // if (dialogresult != System.Windows.Forms.DialogResult.OK)
        //            // {
        //            //     m_blExit = true;
        //            //     return;
        //            // }

        //            //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> dialogresult : " + dialogresult);

        //            //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> System.Windows.Forms.DialogResult.OK : " + System.Windows.Forms.DialogResult.OK + "  -> dialogresult : " + dialogresult);

        //            //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> m_blExit : " + m_blExit);

  
        //        }

        //    }

            

        //}




        public  void inicializo_scanner()
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
            LoadImage(a_bitmap);

            //guardar_imagenes(a_bitmap, a_szFile);


            //MessageBox.Show("Home -> TWAINCSToolkit.MSG -> a_bitmap : " + a_bitmap);


            //MessageBox.Show("final : " + TWAINCSToolkit.MSG.ENDXFER);

            return (TWAINCSToolkit.MSG.ENDXFER);

        }

        public void guardar_imagenes(Bitmap a_bitmap,string a_szFile)
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
            a_bitmap.Save(@"imagenes_escaneadas\\prueba_" + localDate.ToString("yyyyMMdd_Hmmss") + "_" + substring +  ".jpg", ImageFormat.Jpeg); //+ pruebaAlumno.rut+ ".tif");


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
        /// <param name="a_ebuttonstate"></param>
        private void SetButtons(EBUTTONSTATE a_ebuttonstate)
        {
            switch (a_ebuttonstate)
            {
                default:

                case EBUTTONSTATE.CLOSED:
                    //m_buttonOpen.Enabled = true;
                    //m_buttonClose.Enabled = false;
                    //m_buttonSetup.Enabled = false;
                    //m_buttonScan.Enabled = false;
                    //m_buttonStop.Enabled = false;
                    //m_buttonTransfDB.Enabled = false;
                    break;

                case EBUTTONSTATE.OPEN:
                    //m_buttonOpen.Enabled = false;
                    //m_buttonClose.Enabled = true;
                    //m_buttonSetup.Enabled = true;
                    //m_buttonScan.Enabled = true;
                    //m_buttonStop.Enabled = false;
                    //m_buttonTransfDB.Enabled = true;
                    break;

                case EBUTTONSTATE.SCANNING:
                    //m_buttonOpen.Enabled = false;
                    //m_buttonClose.Enabled = false;
                    //m_buttonSetup.Enabled = false;
                    //m_buttonScan.Enabled = false;
                    //m_buttonStop.Enabled = true;
                    //m_buttonTransfDB.Enabled = false;
                    break;

                case EBUTTONSTATE.PROCESSING:
                    //m_buttonOpen.Enabled = false;
                    //m_buttonClose.Enabled = false;
                    //m_buttonSetup.Enabled = false;
                    //m_buttonScan.Enabled = false;
                    //m_buttonStop.Enabled = true;
                    //m_buttonTransfDB.Enabled = false;
                    break;

                case EBUTTONSTATE.SCANNCLOSED:
                    //m_buttonOpen.Enabled = false;
                    //m_buttonClose.Enabled = true;
                    //m_buttonSetup.Enabled = false;
                    //m_buttonScan.Enabled = false;
                    //m_buttonStop.Enabled = false;
                    //m_buttonTransfDB.Enabled = false;
                    break;

            }
        }

        /// <summary>
        /// Nuestro botón dice ...
        /// </summary>
        private enum EBUTTONSTATE
        {
            CLOSED,
            OPEN,
            SCANNING,
            PROCESSING,
            SCANNCLOSED
        }

        /// <summary>
        /// Cargar una imagen en la lista pruebasAlumno.
        /// </summary>
        /// <param name="a_bitmap"></param>
        private void LoadImage(Bitmap a_bitmap)
        {
            // esto se ejecuta al leer las hojas cuando pasan por el scanner

            // Esto agrega las imagenes a este arreglo al parecer
            pruebasBitmaps.Add(a_bitmap);
            MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + pruebasBitmaps.ToString());

            MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + pruebasBitmaps.Count());
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

        public void ToolStripButton7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// We're being closed, clean up nicely...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void FormScan_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    // Get rid of the toolkit...
        //    if (m_twainScanner != null)
        //    {
        //        m_twainScanner.Cleanup();
        //        m_twainScanner = null;
        //    }

        //    // This will prevent ReportImage from doing anything as we close...
        //    //m_graphics1 = null;

        //    // Bye-bye logging...
        //    TWAINWorkingGroup.Log.Close();
        //}

    }
}
