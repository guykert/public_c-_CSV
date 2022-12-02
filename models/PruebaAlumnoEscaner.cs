using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.models
{
    public class PruebaAlumnoEscaner
    {

        public string prueba_id { get; set; }
        public string curso_id { get; set; }
        public string rut { get; set; }
        public string hojanro { get; set; }
        public Hashtable respuestas { get; set; }
        public PruebaAlumnoEscaner() { }
        public PruebaAlumnoEscaner(string prueba_id, string curso_id, string rut, Hashtable respuestas)
        {
            this.prueba_id = prueba_id;
            this.curso_id = curso_id;
            this.rut = rut;
            this.respuestas = respuestas;
        }

    }
}
