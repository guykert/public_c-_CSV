using Fw.Data.logicas;
using Microsoft.Win32;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using TWAINWorkingGroupToolkit;

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para csvTemplateForm.xaml
    /// </summary>
    public partial class csvTemplateForm : UserControl
    {

        private csvTemplatesDb objetoDB = new csvTemplatesDb();

        csvTemplatePrincipal templaate_principal;

        private bool editar = false;

        private int idData;

        private IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;

        public csvTemplateForm(csvTemplatePrincipal templaate_principal, csvTemplateModel data)
        {


            System.Windows.Application.Current.Resources["coger_driver"] = "true";


            this.templaate_principal = templaate_principal;

            InitializeComponent();

            if (data.id > 0)
            {
                this.editar = true;

                this.idData = data.id;

                cargarData(data);
            }

            iniciarObjetoLectura();

        }

        public void iniciarObjetoLectura()
        {



            if (System.Windows.Application.Current.Resources["coger_driver"] != "true")
            {

                try
                {



                    MainWindow.win2 = new frmPrincipal2();

                    MainWindow.win2.aszIdentity = MainWindow.win2.m_twainScanner.GetDrivers(ref MainWindow.win2.szDefault);

                    //Config configuracion = new Config();

                    MainWindow.configuracion = new Config();

                    MainWindow.configuracion.dataEscanners = new List<string>();

                    foreach (string sz in MainWindow.win2.aszIdentity)
                    {

                        MainWindow.szIdentity = sz;

                        var aszIdentity2 = TWAINCSToolkit.CsvParse(sz);

                        //MessageBox.Show("Home -> Home-> sz : " + aszIdentity2[11].ToString());



                        //MainWindow.dataEscanners.Add("test");

                        //MessageBox.Show("Home -> Home-> sz : " + MainWindow.dataEscanners);
                        MainWindow.configuracion.dataEscanners.Add(aszIdentity2[11].ToString());




                        //MessageBox.Show("Home -> Home-> sz : " + configuracion.dataEscanners);



                        //MainWindow.configuracion.test = aszIdentity2[11].ToString();


                        //if (sz.Contains(Login.Usuario.nombre_scanner_predeterminado))
                        //{
                        //    m_blExit = false;
                        //    this.szIdentity = sz;
                        //    break;
                        //}
                    }

                    MainWindow.aszIdentity = MainWindow.win2.aszIdentity;

                    System.Windows.Application.Current.Resources["coger_driver"] = "true";

                }
                catch (COMException ex)
                {
                    MessageBox.Show("No fue posible seleccionar el driver : " + ex.Message);
                }



            }







        }


        public void cargarData(csvTemplateModel data)
        {

            //MessageBox.Show(data.nombre);

            txtNombre.Text = data.nombre;
            txtDescripcion.Text = data.descripcion;
            txtImagen.Text = data.imagen;

            if (data.cuadrados == 1)
            {

                cuadrados.IsChecked = true;

            }


        }

        private void BtnSearchImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtImagen.Text = openFileDialog.FileName;
        }

        private void BottomGuardar_Click(object sender, RoutedEventArgs e)
        {

            /// <summary>
            /// Guardamos los registros
            /// Me falta ver los temas de validaciones
            /// </summary>

            if (editar == false)
            {
                try
                {

                    objetoDB.insertar(txtNombre.Text, txtDescripcion.Text, txtImagen.Text, (bool)cuadrados.IsChecked, System.Windows.Application.Current.Resources["usuario_id"].ToString());

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

                    objetoDB.editar(txtNombre.Text, txtDescripcion.Text, txtImagen.Text, (bool)cuadrados.IsChecked, System.Windows.Application.Current.Resources["usuario_id"].ToString(), idData);


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

            this.templaate_principal.index();

        }

        private void BottomVolver_Click(object sender, RoutedEventArgs e)
        {
            this.templaate_principal.index();
        }

        private void BottomEscanearTemplate_Click(object sender, RoutedEventArgs e)
        {
            pasar_hojas_escaneo();
        }

        private async void pasar_hojas_escaneo()
        {



            MainWindow.win2.cantidad_imagenes = 0;
            MainWindow.win2.szDefault = "";

            MainWindow.win2.escaneo_finalizado = false;



            Application.Current.Resources["MessageLog"] = "Obtenemos el driver ";

            if (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != "")
            {
                //    //confirmo si el scanner está conectado y prendido

                MainWindow.win2.m_blExit = true;

                foreach (string sz in MainWindow.aszIdentity)
                {
                    if (sz.Contains(Login.Usuario.nombre_scanner_predeterminado))
                    {
                        MainWindow.win2.m_blExit = false;
                        MainWindow.win2.szIdentity = sz;
                        break;
                    }
                }
                if (MainWindow.win2.m_blExit)
                {
                    return;
                }


                try
                {

                    MainWindow.win2.szStatus = "";
                    MainWindow.win2.sts = MainWindow.win2.m_twainScanner.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_OPENDS", ref MainWindow.win2.szIdentity, ref MainWindow.win2.szStatus);


                }
                catch (COMException ex)
                {
                    MessageBox.Show("error al enviar solicitud al scanner : " + ex.Message);
                }


                if (MainWindow.win2.sts == TWAINCSToolkit.STS.BUMMER)
                {
                    //MessageBox.Show("El scanner ya se encuentra asignado");
                    MainWindow.win2.m_blExit = true;

                }
                else
                {


                    if (MainWindow.win2.sts != TWAINCSToolkit.STS.SUCCESS)
                    {
                        MessageBox.Show("No se puede abrir el escáner (¿está encendido y enchufado?)");
                        MainWindow.win2.m_blExit = true;
                        MainWindow.win2.m_twainScanner.Cleanup();



                        System.Windows.Application.Current.Resources["escanner_inicializado"] = "false";



                        return;
                    }

                }

                // Sacar los caracteres inseguros. Lamentablemente, mono nos deja aquí abajo ...
                MainWindow.win2.m_szProductDirectory = TWAINCSToolkit.CsvParse(MainWindow.win2.szIdentity)[11];
                foreach (char c in new char[41]
                                { '\x00', '\x01', '\x02', '\x03', '\x04', '\x05', '\x06', '\x07',
                              '\x08', '\x09', '\x0A', '\x0B', '\x0C', '\x0D', '\x0E', '\x0F', '\x10', '\x11', '\x12',
                              '\x13', '\x14', '\x15', '\x16', '\x17', '\x18', '\x19', '\x1A', '\x1B', '\x1C', '\x1D',
                              '\x1E', '\x1F', '\x22', '\x3C', '\x3E', '\x7C', ':', '*', '?', '\\', '/'
                                }
                        )
                {
                    MainWindow.win2.m_szProductDirectory = MainWindow.win2.m_szProductDirectory.Replace(c, '_');
                }

                // Estamos haciendo transferencias de memoria (TWSX_MEMORY == 2) ...
                MainWindow.win2.szStatus = "";
                MainWindow.win2.szCapability = "ICAP_XFERMECH,TWON_ONEVALUE,TWTY_UINT16,2";
                MainWindow.win2.sts = MainWindow.win2.m_twainScanner.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref MainWindow.win2.szCapability, ref MainWindow.win2.szStatus);
                if (MainWindow.win2.sts != TWAINCSToolkit.STS.SUCCESS)
                {
                    MainWindow.win2.m_blExit = true;
                    return;
                }

                // Decida si desea o no mostrar los mensajes de la ventana del conductor ...
                MainWindow.win2.szStatus = "";
                MainWindow.win2.szCapability = "CAP_INDICATORS,TWON_ONEVALUE,TWTY_BOOL," + (MainWindow.win2.m_blIndicators ? "1" : "0");
                MainWindow.win2.sts = MainWindow.win2.m_twainScanner.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref MainWindow.win2.szCapability, ref MainWindow.win2.szStatus);
                if (MainWindow.win2.sts != TWAINCSToolkit.STS.SUCCESS)
                {
                    MainWindow.win2.m_blExit = true;
                    return;
                }

                try
                {

                    MainWindow.pruebasBitmaps = MainWindow.win2.pruebasBitmaps;

                    MainWindow.win2.pruebasBitmaps.Clear();

                    string szTwmemref;
                    string szStatus = "";
                    TWAINCSToolkit.STS sts;

                    Application.Current.Resources["MessageLog"] = "Iniciando Escaneo de hojas ";

                    szTwmemref = "0,0," + this.Handle;

                    sts = MainWindow.win2.m_twainScanner.Send("DG_CONTROL", "DAT_USERINTERFACE", "MSG_ENABLEDS", ref szTwmemref, ref szStatus);


                }
                catch (COMException ex)
                {
                    MessageBox.Show(ex.Message);
                }




                //MessageBox.Show("Home -> Home-> szIdentity : " + win2.szIdentity);
            }
            else
            {
                // veo la cantidad de scanners

                MessageBox.Show("coger_driver2 : ");

                //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click : " + aszIdentity.Count());

                if (MainWindow.win2.aszIdentity.Count() == 1)
                {


                    MainWindow.win2.m_blExit = true;
                    foreach (string sz in MainWindow.win2.aszIdentity)
                    {

                        MainWindow.win2.szIdentity = sz;


                        MainWindow.win2.aszIdentity = TWAINCSToolkit.CsvParse(sz);
                        //MessageBox.Show("Home -> Home-> sz : " + aszIdentity[11].ToString());

                    }
                    if (MainWindow.win2.m_blExit)
                    {
                        return;
                    }


                }

            }



            // Wait until condition is false
            while (MainWindow.win2.escaneo_finalizado == false)
            {



                //Console.WriteLine("Excel is busy");
                await Task.Delay(25);
            }

            if (MainWindow.win2.escaneo_finalizado != false)
            {
                txtImagen.Text = System.Windows.Application.Current.Resources["ruta_template"].ToString();
            }

            //lectura_hojas();

        }


    }
}
