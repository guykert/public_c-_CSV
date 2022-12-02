using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.models
{
    public class Sede
    {
        public int id { get; set; }
        public string tipo_sede_id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string descripcion { get; set; }
        public string sede_padre_id { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string usuario_id { get; set; }
        public string fecha_creacion { get; set; }
        public string creado_por { get; set; }
        public string fecha_modificacion { get; set; }
        public string modificado_por { get; set; }
        public string activo { get; set; }
        public string area { get; set; }
    }
}
