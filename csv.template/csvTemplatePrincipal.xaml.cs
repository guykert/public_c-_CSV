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

namespace scanner.csv.csv.template
{
    /// <summary>
    /// Lógica de interacción para csvTemplatePrincipal.xaml
    /// </summary>
    public partial class csvTemplatePrincipal : UserControl
    {

        csvTemplateModel dataTemplate = new csvTemplateModel();

        csvRegionModel dataRegion = new csvRegionModel();


        

        public csvTemplatePrincipal()
        {
            InitializeComponent();



            gridPrinccipal.Children.Add(new csvTemplate(this));




        }

        public void nuevoForm()
        {


            gridPrinccipal.Children.Clear();

            csvTemplateModel data = new csvTemplateModel();

            gridPrinccipal.Children.Add(new csvTemplateForm(this, data));

 
        }

        public void editarForm(csvTemplateModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());


            gridPrinccipal.Children.Add(new csvTemplateForm(this, data));


        }


        public void ViewForm(csvTemplateModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());

            this.dataTemplate = data;


            gridPrinccipal.Children.Add(new csvRegion(this, data));


        }

        public void ViewFormula(csvTemplateModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());

            this.dataTemplate = data;


            gridPrinccipal.Children.Add(new csvFormula(this, data));


        }

        public void indexFormula()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvFormula(this, this.dataTemplate));


        }

        public void ViewSubRamoForm(csvRegionModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());

            this.dataRegion = data;


            gridPrinccipal.Children.Add(new csvSubRegion(this, data));


        }

        public void index()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvTemplate(this));


        }

        public void indexRegion()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvRegion(this, this.dataTemplate));


        }

        public void verImagenGeneralTemplateForm()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvTemplateRegionesImagenForm(this));


        }

        public void nuevoRegionForm()
        {


            gridPrinccipal.Children.Clear();

            csvRegionModel data = new csvRegionModel();

            gridPrinccipal.Children.Add(new csvRegionForm(this, data, this.dataTemplate));


        }

        public void nuevoFormulaForm()
        {


            gridPrinccipal.Children.Clear();

            csvFormulaModel data = new csvFormulaModel();

            gridPrinccipal.Children.Add(new csvFormulaForm(this, data, this.dataTemplate));


        }

        public void editarFormulaForm(csvFormulaModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());


            gridPrinccipal.Children.Add(new csvFormulaForm(this, data, this.dataTemplate));


        }



        public void nuevoSubRegionForm()
        {


            gridPrinccipal.Children.Clear();



            Fw.Data.logicas.LecturaAlternativas data = new Fw.Data.logicas.LecturaAlternativas();

            gridPrinccipal.Children.Add(new csvSubRegionForm(this, data, this.dataRegion, this.dataTemplate));


        }

        public void editarRegionForm(csvRegionModel data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());


            gridPrinccipal.Children.Add(new csvRegionForm(this, data, this.dataTemplate));


        }

        public void editarSubRegionForm(Fw.Data.logicas.LecturaAlternativas data)
        {



            gridPrinccipal.Children.Clear();


            //MessageBox.Show("csvTemplatePrincipal -> editarForm : " + data.ToString());


            gridPrinccipal.Children.Add(new csvSubRegionForm(this, data, this.dataRegion, this.dataTemplate));


        }


        public void indexSubRegion()
        {


            gridPrinccipal.Children.Clear();

            gridPrinccipal.Children.Add(new csvSubRegion(this, this.dataRegion));


        }

    }
}
