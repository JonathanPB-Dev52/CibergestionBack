using CapaDatos.Models;
using CapaNegocio.Contract.ModelsJWT;
using CapaNegocio.Contract.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Contract
{
    public interface Inps
    {
        public string RegistrarCalificacion(string identificacion, int calificacion);
        public List<TblCliente> ConsultarCalificacion();
        public reponseNPS calcularNps();
    }
}
