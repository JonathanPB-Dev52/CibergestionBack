using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Contract.ModelsJWT
{
    public class reponseNPS
    {
        public int Encuestados { get; set; }
        public int promotores { get; set; }
        public int detractores { get; set; }
        public double nps { get; set; }
    }
}
