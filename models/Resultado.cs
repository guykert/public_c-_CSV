using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace scanner.csv.models
{
    public class Resultado
    {

        public int id { get; set; }
        public string rut { get; set; }
        public int alumno_existe { get; set; }
        public string errores { get; set; }
        public string nombre { get; set; }
        public string url { get; set; }

        public int prueba_alumno_id { get; set; }

    }
}
