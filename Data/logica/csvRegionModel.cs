using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.Data.logica
{
    class csvRegionModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int template_id { get; set; }
        public int tipo_elemento_id { get; set; }
        public string respuesta { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int creado_por { get; set; }
        public string fecha_creacion { get; set; }

    }
}
