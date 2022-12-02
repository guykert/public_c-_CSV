using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.Data.logica
{
    class csvSubRegionModelWeb
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
        public int region_id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string imagen_Local { get; set; }
        public string imagen_nombre { get; set; }

    }
}
