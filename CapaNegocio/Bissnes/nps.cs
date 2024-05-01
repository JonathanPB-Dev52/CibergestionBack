using CapaDatos.Models;
using CapaNegocio.Bissnes.Validations;
using CapaNegocio.Contract;
using CapaNegocio.Contract.ModelsJWT;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Bissnes
{
    public class nps : Inps
    {

        public IConfiguration _configuration;

        public nps(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public reponseNPS calcularNps()
        {
            using (CibergestionPruebaContext ciberseguridad = new CibergestionPruebaContext(_configuration))
            {

                reponseNPS ReponseNPS = new reponseNPS();
                int promotores = 0;
                int TotalEncuentados = 0;
                int detractores = 0;

                var encuentados = ciberseguridad.TblClientes.ToList();
                foreach (var cliente in encuentados)
                {
                    if (cliente.Calificacion >= 9 && cliente.Calificacion <= 10)
                    {
                        promotores++;
                    }
                    else if (cliente.Calificacion >= 0 && cliente.Calificacion <= 6)
                    {
                        detractores++;
                    }
                    TotalEncuentados++;
                }

                if (TotalEncuentados > 0)
                {
                    double nps = ((double)(promotores - detractores) / TotalEncuentados) * 100;
                    ReponseNPS.promotores = promotores;
                    ReponseNPS.detractores = detractores;
                    ReponseNPS.Encuestados = TotalEncuentados;
                    ReponseNPS.nps = nps;
                }

                return ReponseNPS;
            }
        }

        public List<TblCliente> ConsultarCalificacion()
        {
            List<TblCliente> tblClientes = new List<TblCliente> ();
                using (CibergestionPruebaContext ciberseguridad = new CibergestionPruebaContext(_configuration))
                {
                    tblClientes = ciberseguridad.TblClientes.ToList();
                    foreach (var item in tblClientes)
                    {
                        item.Documento = item.Documento.Trim();
                    }
            }
            return tblClientes;
        }

        public string RegistrarCalificacion(string identificacion, int calificacion)
        {
            try
            {
                using (CibergestionPruebaContext ciberseguridad = new CibergestionPruebaContext(_configuration)) {
                    if (!ValidDocumento.validDocumento(identificacion))
                    {
                        return "Documento inválido";
                    }

                    var identificacionExist = ciberseguridad.TblClientes.Where(x => x.Documento == identificacion).ToList();
                    if (identificacionExist.Any())
                    {
                        return "Documento existente";
                    }
                    else
                    {
                        TblCliente tblCliente = new TblCliente();
                        tblCliente.Documento = identificacion;
                        tblCliente.Calificacion = calificacion;
                        tblCliente.IdRol = 2;

                            ciberseguridad.TblClientes.Add(tblCliente);
                            ciberseguridad.SaveChanges();

                        return "Calificación registrada";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
