using Newtonsoft.Json.Linq;
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

namespace scanner.csv.csv.entornoUsuarioFinal
{
    /// <summary>
    /// Lógica de interacción para csvEntornoUsuarioFinalPrincipal.xaml
    /// </summary>
    /// 

    public partial class csvEntornoUsuarioFinalPrincipal : UserControl
    {
        public csvEntornoUsuarioFinalPrincipal()
        {
            InitializeComponent();

            gridPrinccipal.Children.Add(new csvEntornoUsuarioFinal(this));

        }

        public void index()
        {

            //MessageBox.Show("trace csvEntornoUsuarioFinalPrincipal 1 : ");

            //gridPrinccipal.Children.Clear();

            //gridPrinccipal.Children.OfType<csvEntornoUsuarioFinalResultados>().FirstOrDefault().

            gridPrinccipal.Children.Remove(gridPrinccipal.Children.OfType<csvEntornoUsuarioFinalResultados>().FirstOrDefault());


            gridPrinccipal.Children.OfType<csvEntornoUsuarioFinal>().FirstOrDefault().Visibility = Visibility.Visible;

            //MessageBox.Show("trace csvEntornoUsuarioFinalPrincipal 2 : ");

            //gridPrinccipal.Children.Add(new csvEntornoUsuarioFinal(this));

            //MessageBox.Show("trace csvEntornoUsuarioFinalPrincipal 3 : ");


        }


        public void resultados(JArray resuesta_pruebas_api)
        {

            gridPrinccipal.Children.OfType<csvEntornoUsuarioFinal>().FirstOrDefault().Visibility = Visibility.Hidden;

            gridPrinccipal.Children.Add(new csvEntornoUsuarioFinalResultados(this, resuesta_pruebas_api));

        }




    }
}
