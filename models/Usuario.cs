using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.models
{
    public class Usuario
    {
        public int id { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string email { get; set; }
        public string sede_predeterminada { get; set; }
        public string access_token { get; set; }
        public string nombre_scanner_predeterminado { get; set; }
        


    }
}
