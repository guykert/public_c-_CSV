using System;
using System.Collections.Generic;
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
using System.IO;
using System.Drawing;


// esto es para mostrar el porcentaje de avance en una barra que se llena
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Drawing.Printing;

using TWAINWorkingGroupToolkit;
using scanner.csv.services;
using scanner.csv.models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using scanner.csv.framework;
using scanner.csv.Data.logica;

namespace scanner.csv.csv.entornoUsuarioFinal
{
    /// <summary>
    /// Lógica de interacción para csvEntornoUsuarioFinal.xaml
    /// </summary>
    /// 


    public partial class csvEntornoUsuarioFinal : UserControl
    {



        private Configuraciones configuraciones;
        private Configuraciones2 configuraciones2;
        private Imagenes imagenes;
        public List<PruebaAlumnoEscaner> pruebasAlumnos = new List<PruebaAlumnoEscaner>();
        public List<Bitmap> pruebasBitmaps = new List<Bitmap>();
        string respuestaAcademico;
        


        public static JArray cursos_api;

        public static JArray pruebas_api;

        public static JArray resuesta_pruebas_api;

        public String c = "test";

        private IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;

        csvEntornoUsuarioFinalPrincipal csvEntornoUsuarioFinalPrincipal;



        public csvEntornoUsuarioFinal(csvEntornoUsuarioFinalPrincipal entoro_usuario_final)
        {
            //MessageBox.Show("trace 1 : ");

            InitializeComponent();

            //MessageBox.Show("trace 2 : ");

            // inicializo_scanner();

            

            //MessageBox.Show("trace 3 : ");

            this.csvEntornoUsuarioFinalPrincipal = entoro_usuario_final;

            //MessageBox.Show("trace 4 : ");

            cargarData();


            iniciarObjetoLectura();


            //MainWindow.configuracion.dataEscanners = dataEscanners;

            //MessageBox.Show("trace 5 : ");

            //Application.Current.Resources["MessageLog"] = "MessageLog test 2";


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


        public void cargarData()
        {

            nombre_colegio.Content = "Colegio : " + System.Windows.Application.Current.Resources["nombre_colegio_predeterminado"];

            // cargo los escaners

            var pruebasCategoriaList = new List<ComboData>();

            var ramoList = new List<ComboData>();



            //this.escaners.ItemsSource = MainWindow.configuracion.dataEscanners;







            if (Login.pruebas_categoria_asignados.Count > 0)
            {

                foreach (JObject colegio in Login.pruebas_categoria_asignados)
                {

                    pruebasCategoriaList.Add(new ComboData() { Id = Convert.ToInt32(colegio["id"]), Value = (String)colegio["nombre"] });

                }

            }

            this.pruebas_categoria.ItemsSource = pruebasCategoriaList;
            this.pruebas_categoria.DisplayMemberPath = "Value";
            this.pruebas_categoria.SelectedValuePath = "Id";

            //if (System.Windows.Application.Current.Resources["colegio_predeterminado"] != null && System.Windows.Application.Current.Resources["colegio_predeterminado"] != "")
            //{

            //    //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);


            //    this.pruebas_categoria.SelectedItem = System.Windows.Application.Current.Resources["colegio_predeterminado"];

            //    this.pruebas_categoria.SelectedValue = System.Windows.Application.Current.Resources["colegio_predeterminado"];

            //}

            if (Login.ramos_asignados.Count > 0)
            {

                foreach (JObject ramos in Login.ramos_asignados)
                {

                    ramoList.Add(new ComboData() { Id = Convert.ToInt32(ramos["id"]), Value = (String)ramos["nombre"] });

                }

            }

            this.ramos.ItemsSource = ramoList;
            this.ramos.DisplayMemberPath = "Value";
            this.ramos.SelectedValuePath = "Id";


            //if (System.Windows.Application.Current.Resources["template_predeterminado"] != null && System.Windows.Application.Current.Resources["template_predeterminado"] != "")
            //{

            //    //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);


            //    this.ramos.SelectedItem = System.Windows.Application.Current.Resources["template_predeterminado"];

            //    this.ramos.SelectedValue = System.Windows.Application.Current.Resources["template_predeterminado"];

            //}





        }


        private void inicializo_scanner()
        {

            //MessageBox.Show("(MainWindow.aszIdentity.Count() == 1) : " + (MainWindow.aszIdentity.Count() == 1));

            //MessageBox.Show("(MainWindow.aszIdentity != null) : " + (MainWindow.aszIdentity != null));

            //if (MainWindow.aszIdentity != null)
            //{

            //    MessageBox.Show("(MainWindow.aszIdentity.Count() == 1) : " + (MainWindow.aszIdentity.Count() == 1));

            //    if (MainWindow.aszIdentity.Count() == 1)
            //    {


            //        win2.m_blExit = true;
            //        foreach (string sz in MainWindow.aszIdentity)
            //        {

            //            MainWindow.szIdentity = sz;


            //            MainWindow.aszIdentity = TWAINCSToolkit.CsvParse(sz);
            //            //MessageBox.Show("Home -> Home-> sz : " + aszIdentity[11].ToString());

            //            // Actualizo el usuario con el scanner por defecto

            //            usuarioService.setScannerPredeterminado(MainWindow.aszIdentity[11].ToString());

            //        }
            //        if (win2.m_blExit)
            //        {
            //            return;
            //        }


            //    }
            //    else
            //    {



            //        // Esta parte es en caso de que tenga más de un scanner y tenga que escoger uno como predeterminado

            //        // Consulto si tiene o no guardado un scanner predeterminado

            //        //MessageBox.Show("Login.Usuario.nombre_scanner_predeterminado : " + Login.Usuario.nombre_scanner_predeterminado);

            //        //MessageBox.Show("(Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ) : " + (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ""));

            //        if (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != "")
            //        {

            //            win2.m_blExit = true;
            //            foreach (string sz in MainWindow.aszIdentity)
            //            {

                        


            //                if (sz.Contains(Login.Usuario.nombre_scanner_predeterminado))
            //                {

            //                    //MessageBox.Show("Home -> Home-> sz : " + sz.ToString());

            //                    win2.m_blExit = false;
            //                    MainWindow.szIdentity = sz;
            //                    break;
            //                }
            //            }
            //            if (win2.m_blExit)
            //            {
            //                return;
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show("Home -> Home-> sz : ");
            //        }


            //        // DialogResult dialogresult;

            //        // SeleccionarScanner seleccionarScanner;

            //        // seleccionarScanner = new SeleccionarScanner(this.aszIdentity, this.szDefault);

            //        // seleccionarScanner.StartPosition = FormStartPosition.CenterParent;
            //        // dialogresult = seleccionarScanner.ShowDialog(this);

            //        // if (dialogresult != System.Windows.Forms.DialogResult.OK)
            //        // {
            //        //     m_blExit = true;
            //        //     return;
            //        // }

            //        //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> dialogresult : " + dialogresult);

            //        //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> System.Windows.Forms.DialogResult.OK : " + System.Windows.Forms.DialogResult.OK + "  -> dialogresult : " + dialogresult);

            //        //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> m_blExit : " + m_blExit);


            //    }

            //}

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!(pruebas_categoria.SelectedIndex == -1 && ramos.SelectedIndex == -1))//Nothing selected
            {

                var selectedPrueba = ((ComboData)pruebas.SelectedItem).Id;

                //var selectedPruebaNombre = ((ComboData)pruebas.SelectedItem).Name;

                var selectedCurso = ((ComboData)cursos.SelectedItem).Id;

                //MessageBox.Show("selectedPruebaNombre . " + selectedPrueba);

                lectura_escaneo(selectedPrueba, selectedCurso);

            }

            

        }

        private async void lectura_escaneo(int selectedPrueba, int selectedCurso)
        {

            progressbar.IsIndeterminate = true;
            this.Cursor = System.Windows.Input.Cursors.Wait;

            MainWindow.win2.escaneo_finalizado = false;

            log.Visibility = Visibility.Visible;

            if (System.Windows.Application.Current.Resources["escanner_inicializado"] != "true")
            {
                MainWindow.win2.inicializo_scanner();
            }

            await Task.Run(async () =>
            {


                Application.Current.Resources["MessageLog"] = "Iniciando Escaneo ";

                //MessageBox.Show("escaneo_finalizado : " + win2.escaneo_finalizado);

                Application.Current.Resources["MessageLog"] = "Comenzamos la lectura ";

                pasar_hojas_escaneo();

                



                while (MainWindow.win2.escaneo_finalizado == false)
                {
                    await Task.Delay(500);
                }

                Application.Current.Resources["MessageLog"] = "Escaneo Finalizado";

                if (System.Windows.Application.Current.Resources["escanner_inicializado"] == "true")
                {



                    lectura_hojas(selectedPrueba, selectedCurso);


                    MainWindow.pruebasBitmaps.Clear();

                    //MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + MainWindow.pruebasBitmaps.ToString());

                    //MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + MainWindow.pruebasBitmaps.Count());


                    //MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + MainWindow.win2.a_bitmap.ToString());

                    //MessageBox.Show("Home -> LoadImage -> pruebasBitmaps : " + MainWindow.win2.a_bitmap.Count());



                }

                


                //MessageBox.Show("escaneo_finalizado3 : " + win2.escaneo_finalizado);

            });




            this.Cursor = System.Windows.Input.Cursors.Arrow;
            progressbar.IsIndeterminate = false;


            //MessageBox.Show(" Proceso Completo : ");

        }



        private async void pasar_hojas_escaneo()
        {



            MainWindow.win2.cantidad_imagenes = 0;
            MainWindow.win2.szDefault = "";

            MainWindow.win2.escaneo_finalizado = false;



            Application.Current.Resources["MessageLog"] = "Obtenemos el driver ";

            //MessageBox.Show("coger_driver : " + System.Windows.Application.Current.Resources["coger_driver"]);

            


            

            

            //if (System.Windows.Application.Current.Resources["coger_driver"] != "true")
            //{

            //    //MessageBox.Show("win2.szDefault : " + (win2.m_twainScanner));

            //    win2.aszIdentity = win2.m_twainScanner.GetDrivers(ref win2.szDefault);

            //    System.Windows.Application.Current.Resources["coger_driver"] = "true";

            //    //MessageBox.Show("coger_driver2 : " + System.Windows.Application.Current.Resources["coger_driver"]);

            //}
            //else
            //{




            //    //MessageBox.Show("win2.szDefault2 : " + (win2.m_twainScanner));


            //    win2.aszIdentity = win2.m_twainScanner.GetDrivers(ref win2.szDefault);

            //    //System.Windows.Application.Current.Resources["coger_driver"] = "true";

            //    //this.win2 = new frmPrincipal();

            //    //var mainWindow = new MainWindow();

            //    //mainWindow.inicializarEscanner();


            //}

            //MessageBox.Show("coger_driver3 : " + System.Windows.Application.Current.Resources["coger_driver"]);







            //MessageBox.Show("(Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ) : " + (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != ""));
            if (Login.Usuario.nombre_scanner_predeterminado != null && Login.Usuario.nombre_scanner_predeterminado != "")
            {
                //    //confirmo si el scanner está conectado y prendido

                //MessageBox.Show("coger_driver1 : ");

                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  szIdentity1 : " + szIdentity);

                MainWindow.win2.m_blExit = true;

                

                //MessageBox.Show("win2.m_blExit : " + win2.m_blExit);



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



                //MessageBox.Show("coger_driver2 : " + System.Windows.Application.Current.Resources["coger_driver"]);





                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  szIdentity2 : " + szIdentity);


                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  szIdentity3 : " + szIdentity);

                // MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.szStatus : " + win2.szStatus);

                try
                {

                    MainWindow.win2.szStatus = "";
                    MainWindow.win2.sts = MainWindow.win2.m_twainScanner.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_OPENDS", ref MainWindow.win2.szIdentity, ref MainWindow.win2.szStatus);


                }
                catch (COMException ex)
                {
                    MessageBox.Show("error al enviar solicitud al scanner : " + ex.Message);
                }






                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.sts : " + MainWindow.win2.sts);

                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  TWAINCSToolkit.STS.SUCCESS : " + TWAINCSToolkit.STS.SUCCESS);

                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  (this.sts == TWAINCSToolkit.STS.BUMMER) : " + (MainWindow.win2.sts == TWAINCSToolkit.STS.BUMMER));

                if (MainWindow.win2.sts == TWAINCSToolkit.STS.BUMMER)
                {
                    //MessageBox.Show("El scanner ya se encuentra asignado");
                    MainWindow.win2.m_blExit = true;

                }
                else
                {

                    //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  this.sts : " + this.sts);

                    //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  TWAINCSToolkit.STS.SUCCESS : " + TWAINCSToolkit.STS.SUCCESS);

                    //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  (this.sts != TWAINCSToolkit.STS.SUCCESS) : " + (this.sts != TWAINCSToolkit.STS.SUCCESS));

                    if (MainWindow.win2.sts != TWAINCSToolkit.STS.SUCCESS)
                    {
                        MessageBox.Show("No se puede abrir el escáner (¿está encendido y enchufado?)");
                        MainWindow.win2.m_blExit = true;
                        MainWindow.win2.m_twainScanner.Cleanup();
                        


                        System.Windows.Application.Current.Resources["escanner_inicializado"] = "false";



                        return;
                    }

                }


                //MessageBox.Show("Home -> Home-> m_buttonOpen_Click ->  m_blExit : " + win2.m_blExit);

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

                //MessageBox.Show("Home -> Home-> m_szProductDirectory : " + win2.m_szProductDirectory);

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

                    // Comience a escanear en silencio si detectamos que se admite el uso de datos personalizados.De lo contrario, active la GUI del controlador para que el usuario pueda cambiar la configuración...
                    //if (configuraciones.IsCustomDsDataSupported())
                    //{
                    //    szTwmemref = "0,0," + this.Handle;
                    //}
                    //else
                    //{
                    //    szTwmemref = "1,0," + this.Handle;
                    //}

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

                        // Actualizo el usuario con el scanner por defecto

                        usuarioService.setScannerPredeterminado(MainWindow.win2.aszIdentity[11].ToString());

                    }
                    if (MainWindow.win2.m_blExit)
                    {
                        return;
                    }


                }
                else
                {

                    // Esta parte es en cao de que tenga más de un scanner y tenga que escoger uno como predeterminado

                    // DialogResult dialogresult;

                    // SeleccionarScanner seleccionarScanner;

                    // seleccionarScanner = new SeleccionarScanner(this.aszIdentity, this.szDefault);

                    // seleccionarScanner.StartPosition = FormStartPosition.CenterParent;
                    // dialogresult = seleccionarScanner.ShowDialog(this);

                    // if (dialogresult != System.Windows.Forms.DialogResult.OK)
                    // {
                    //     m_blExit = true;
                    //     return;
                    // }

                    //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> dialogresult : " + dialogresult);

                    //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> System.Windows.Forms.DialogResult.OK : " + System.Windows.Forms.DialogResult.OK + "  -> dialogresult : " + dialogresult);

                    //MessageBox.Show("frmPrincipal -> ToolStripButton6_Click -> m_blExit : " + m_blExit);


                }

            }



            // Wait until condition is false
            while (MainWindow.win2.escaneo_finalizado == false)
            {



                //Console.WriteLine("Excel is busy");
                await Task.Delay(25);
            }

            

            //lectura_hojas();

        }


        private async void lectura_hojas(int selectedPrueba, int selectedCurso)
        {



            // instancia la 
            var evaluation = new Evaluator_csv();
            var alert = false;
            var alertMessage = string.Empty;

            //try
            //{

            //este es el id del template




            csvEscanearModel data = new csvEscanearModel();

            var listpruebaAlumnoEnviarWeb = new List<PruebaAlumnoEnviarWeb>();

            int  errores_al_evaluar = 0;

            //var carpeta = id1.Row["carpeta"].ToString();

            evaluation.Evaluate_csv(selectedPrueba, selectedCurso, ref listpruebaAlumnoEnviarWeb, MainWindow.pruebasBitmaps, ref errores_al_evaluar);


            if (errores_al_evaluar == 0)
            {

                //MessageBox.Show("lectura_hojas 3. ");

                Application.Current.Resources["MessageLog"] = "Iniciamos envío de información a la Web. ";

                //enviamosLosdatosWeb(listpruebaAlumnoEnviarWeb);

            }



            //mostrarTemplates();

            //}
            //catch (Exception err)
            //{
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();

            //    MessageBox.Show("Error evaluando el examen: " + Environment.NewLine +
            //            err.ToString());
            //}


            

        }

        //public void enviamosLosdatosWeb(List<PruebaAlumnoEnviarWeb> listpruebaAlumnoEnviarWeb)
        //{
        //    //MessageBox.Show("enviamosLosdatosWeb 1. ");
        //    string json = JsonConvert.SerializeObject(listpruebaAlumnoEnviarWeb);

        //    //MessageBox.Show("json :  " + json);

        //    string response = pruebaService.setPruebaAlumno(json, ref resuesta_pruebas_api);
        //    // MessageBox.Show("enviamosLosdatosWeb 3. ");

        //    Application.Current.Resources["MessageLog"] = "Datos Guardados Exitosamenete : response. " + response;



        //    MainWindow.win2.m_twainScanner.StopSession();
        //    //win2.WriteTriplet("DG_CONTROL", "DAT_USERINTERFACE", "MSG_STOPFEEDER", "SUCCESS");
        //    //string response2 = pruebaService.setPruebaSubirImagenes(json, listpruebaAlumnoEnviarWeb);

        //    //string directoryPath = @"imagenes_escaneadas";

        //    //Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);

        //    Dispatcher.Invoke((Action)(() =>
        //    {//this refer to form in WPF application 
        //        this.csvEntornoUsuarioFinalPrincipal.resultados(resuesta_pruebas_api);
        //    }));





        //    //MessageBox.Show("Datos Guardados Exitosamenete : response2. " + response2);

        //}

        private void pruebas_categoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            categoria_prueba_ramo_selected();

        }

        public void categoria_prueba_ramo_selected()
        {

            if (pruebas_categoria.SelectedIndex == -1)//Nothing selected
            {

                //var mensajeError = new MensajesError("Selecciona un Categiría de prueba", true);

                return;

            }

            if (ramos.SelectedIndex == -1)//Nothing selected
            {

                //var mensajeError = new MensajesError("Selecciona un Ramo", true);

                return;

            }

            var cursosList = new List<ComboData>();

            this.cursos.ItemsSource = cursosList;
            this.cursos.DisplayMemberPath = "Value";
            this.cursos.SelectedValuePath = "Id";



            pruebas.IsEnabled = false;
            boton_escanear.IsEnabled = false;

            if (!(pruebas_categoria.SelectedIndex == -1 && ramos.SelectedIndex == -1))//Nothing selected
            {



                var pruebasList = new List<ComboData>();


                this.pruebas.ItemsSource = pruebasList;
                this.pruebas.DisplayMemberPath = "Value";
                this.pruebas.SelectedValuePath = "Id";



                var selectedPruebasCategoria = ((ComboData)pruebas_categoria.SelectedItem).Id;

                var selectedRamos = ((ComboData)ramos.SelectedItem).Id;

                //MessageBox.Show("selectedScanner: " + selectedScanner);



                cursosService.getCursos(selectedPruebasCategoria, selectedRamos,ref cursos_api);





                //this.escaners.ItemsSource = MainWindow.configuracion.dataEscanners;







                if (cursos_api.Count > 0)
                {

                    foreach (JObject cursos_a in cursos_api)
                    {

                        cursosList.Add(new ComboData() { Id = Convert.ToInt32(cursos_a["id"]), Value = (String)cursos_a["nombre"] });

                    }

                }

                this.cursos.ItemsSource = cursosList;
                this.cursos.DisplayMemberPath = "Value";
                this.cursos.SelectedValuePath = "Id";

                cursos.IsEnabled = true;
                card_cursos.IsEnabled = true;


            }


            


        }

        private void ramos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            categoria_prueba_ramo_selected();

        }

        private void cursos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cursos.SelectedIndex == -1)//Nothing selected
            {

                //var mensajeError = new MensajesError("Selecciona un Categiría de prueba", true);

                return;

            }

            boton_escanear.IsEnabled = false;



            if (!(cursos.SelectedIndex == -1))//Nothing selected
            {

                pruebas.IsEnabled = false;

                var cursoCategoria = ((ComboData)cursos.SelectedItem).Id;

                var selectedPruebasCategoria = ((ComboData)pruebas_categoria.SelectedItem).Id;

                var selectedRamos = ((ComboData)ramos.SelectedItem).Id;

                pruebas.IsEnabled = true;

                card_pruebas.IsEnabled = true;



                //MessageBox.Show("selectedScanner: " + selectedScanner);



                pruebaService.getPruebas(selectedPruebasCategoria, selectedRamos, cursoCategoria, ref pruebas_api);

                var pruebasList = new List<ComboData>();



                //this.escaners.ItemsSource = MainWindow.configuracion.dataEscanners;







                if (pruebas_api.Count > 0)
                {

                    foreach (JObject pruebas_a in pruebas_api)
                    {

                        pruebasList.Add(new ComboData() { Id = Convert.ToInt32(pruebas_a["id"]), Value = (String)pruebas_a["nombre"] });

                    }

                }

                this.pruebas.ItemsSource = pruebasList;
                this.pruebas.DisplayMemberPath = "Value";
                this.pruebas.SelectedValuePath = "Id";


            }



        }

        private void pruebas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!(cursos.SelectedIndex == -1))//Nothing selected
            {

                boton_escanear.IsEnabled = true;

            }

        }
    }
}
