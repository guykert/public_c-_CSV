using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.Data.logica
{
    class csvEscanearModel
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int template_id { get; set; }
        public string carpeta { get; set; }
        public string fecha_aplicacion { get; set; }

    }
}
