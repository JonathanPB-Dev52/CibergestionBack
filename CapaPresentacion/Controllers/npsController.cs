using CapaDatos.Models;
using CapaNegocio.Bissnes;
using CapaNegocio.Contract;
using CapaNegocio.Contract.ModelsJWT;
using CapaNegocio.Contract.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class npsController : ControllerBase
    {
        private readonly Inps _nps;
        public npsController(Inps nps)
        {
            _nps = nps;
        }

        [HttpGet]
        [Route("ConsultarCalificaciones")]
        public CalificacionReponse ConsultarCalificaciones()
        {
            CalificacionReponse calificacionReponse = new CalificacionReponse();
            try
            {
                List<TblCliente> tblClientes = new List<TblCliente>();
                tblClientes = _nps.ConsultarCalificacion();

                calificacionReponse.ID = 0;
                calificacionReponse.mensaje = "Consulta Exitosa";
                calificacionReponse.objeto = tblClientes;
            }catch (Exception ex)
            {
                calificacionReponse.ID = 1;
                calificacionReponse.mensaje = "Error metodo consultar calificacion";
                calificacionReponse.objeto = ex.Message;
            }

            return calificacionReponse;
        }

        [HttpGet]
        [Route("CalcularNPS")]
        public CalificacionReponse CalcularNPS()
        {
            CalificacionReponse calificacionReponse = new CalificacionReponse();
            try
            {
                reponseNPS ResponseNPS = new reponseNPS();
                ResponseNPS = _nps.calcularNps();

                calificacionReponse.ID = 0;
                calificacionReponse.mensaje = "NPS Exitosa";
                calificacionReponse.objeto = ResponseNPS;
            }
            catch (Exception ex)
            {
                calificacionReponse.ID = 1;
                calificacionReponse.mensaje = "Error metodo calcular NPS";
                calificacionReponse.objeto = ex.Message;
            }

            return calificacionReponse;
        }

    }
}
