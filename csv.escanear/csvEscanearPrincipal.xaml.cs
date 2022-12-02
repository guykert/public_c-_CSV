using Fw.Data.logicas;
using scanner.csv.models;
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

namespace scanner.csv.csv.escanear
{
    /// <summary>
    /// Lógica de interacción para csvEscanearPrincipal.xaml
    /// </summary>
    public partial class csvEscanearPrincipal : UserControl
    {

        csvEscanearModel dataEscanear = new csvEscanearModel();

        public csvEscanearPrincipal()
        {
            InitializeComponent();

            gridPrinccipal.Children.Add(new csvEscanear(this));


        }

        public void nuevoForm()
        {


            gridPrinccipal.Children.Clear();

            csvEscanearModel data = new csvEscanearModel();

            gridPrinccipal.Children.Add(new csvEscanearForm(this, data));


        }

        public void editarForm(csvEscanearModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());


            gridPrinccipal.Children.Add(new csvEscanearForm(this, data));


        }

        public void index()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvEscanear(this));


        }



    }
}
