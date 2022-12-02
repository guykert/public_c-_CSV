
using scanner.csv.csv.entornoUsuarioFinal;
using scanner.csv.csv.escanear;
using scanner.csv.csv.perfil;
using scanner.csv.csv.template;
using scanner.csv.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using TWAINWorkingGroupToolkit;

namespace scanner.csv
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal static object Snackbar;
        private string szDefault;
        private IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;
        public static List<Bitmap> pruebasBitmaps = new List<Bitmap>();

        /// <summary>
        /// Esto es para declarar la variable que llamará a twain cambiare la variable pr un nombre más común
        /// </summary>
        public TWAINCSToolkit m_twainScanner;

        public static Config configuracion { get; set; }

        public static List<String> dataEscanners { get; set; }





        public static string[] aszIdentity;
        public static string szIdentity;
        private bool isDataDirty;

        public MainWindow()
        {

            InitializeComponent();

            nombre_usuario.Text = System.Windows.Application.Current.Resources["nombre_usuario"].ToString();

            seleccionarEntorno(0);

        }


        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {

            if (System.Windows.Application.Current.Resources["escanner_inicializado"] == "true")
            {

                //MainWindow.win2.m_blExit = true;
                //MainWindow.win2.m_twainScanner.Cleanup();

                System.Windows.Application.Current.Resources["escanner_inicializado"] = "false";
            }


            Application.Current.Shutdown();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Closing called");

            if (System.Windows.Application.Current.Resources["escanner_inicializado"] == "true")
            {

                //MainWindow.win2.m_blExit = true;
                //MainWindow.win2.m_twainScanner.Cleanup();

                System.Windows.Application.Current.Resources["escanner_inicializado"] = "false";
            }

            // If data is dirty, notify user and ask for a response
            if (this.isDataDirty)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ImagenLogo.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {

            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ImagenLogo.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            seleccionarEntorno(index);

        }

        public void cambiarVentana()
        {
            MessageBox.Show("test");
        }

        public void seleccionarEntorno(int index)
        {


            if (Convert.ToInt32(Application.Current.Resources["template_predeterminado"]) > 0 && Convert.ToInt32(Application.Current.Resources["colegio_predeterminado"]) > 0 && Application.Current.Resources["nombre_scanner_predeterminado"].ToString() != "")
            {
                switch (index)
                {
                    case 0:
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new csvEntornoUsuarioFinalPrincipal());
                        break;
                    case 1:
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new csvEscanearPrincipal());
                        break;
                    case 2:
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new csvTemplatePrincipal());
                        break;
                    case 3:
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new csvPerfilPrincipal(this));
                        break;
                    //case 4:
                    //    frmPrincipal win2 = new frmPrincipal();
                    //    win2.Show();
                    //    this.Close();
                    //    break;

                    Default:
                        GridPrincipal.Children.Clear();
                        GridPrincipal.Children.Add(new csvTemplatePrincipal());
                        break;

                }
            }
            else
            {
                GridPrincipal.Children.Clear();
                GridPrincipal.Children.Add(new csvPerfilPrincipal(this));
            }



        }

        private void ButtonCuentaMenu_Click(object sender, RoutedEventArgs e)
        {

            seleccionarEntorno(3);

        }

    }
}
