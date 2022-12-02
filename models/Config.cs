using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace scanner.csv.models
{
    public class Config
    {

        public static Boolean dibujarRespuesta { get; set; }
        public static Bitmap ultimaHoja { get; set; }

        public  List<String> dataEscanners { get; set; }

        public String test { get; set; }



    }
}
