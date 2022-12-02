using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner.csv.Data.logica
{
    class PruebaAlumnoEnviarWeb
    {

        public int id { get; set; }
        public string primera_formula { get; set; }
        public int prueba_id { get; set; }
        public int curso_id { get; set; }
        public int activo { get; set; }

        public List<csvformulasModel> formulas_datos;

        public List<csvLecturaAlternativas2> respuestasMayores;

        public List<csvSubRegionModelWeb> regionesFinales;

        public List<csvLecturaAlternativas2> alternativas;


    }
}
