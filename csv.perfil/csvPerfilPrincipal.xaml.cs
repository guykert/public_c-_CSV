using Fw.Data.logicas;
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

namespace scanner.csv.csv.perfil
{
    /// <summary>
    /// Lógica de interacción para csvPerfilPrincipal.xaml
    /// </summary>
    public partial class csvPerfilPrincipal : UserControl
    {

        csvPerfilModel dataPerfil = new csvPerfilModel();


        public csvPerfilPrincipal(MainWindow mainWindow)
        {
            InitializeComponent();

            gridPrinccipal.Children.Add(new csvPerfil(this, mainWindow));
        }
    }
}
