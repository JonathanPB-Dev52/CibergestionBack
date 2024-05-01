using CapaNegocio.Contract;
using CapaNegocio.Contract.ModelsJWT;
using CapaNegocio.Contract.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPersona.Models.DTO.ModelsJWT;

namespace CapaPresentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private readonly Inps _inps;
        public CalificacionController(Inps inps)
        {
            _inps = inps;
        }

        [HttpPost]
        [Route ("RegistrarCalificacion")]
        public CalificacionReponse CalificacionRegistrar([FromBody] RegisterCalificacion Data)
        {
            CalificacionReponse calificacionReponse = new CalificacionReponse();
            try
            {
                string respuesta = _inps.RegistrarCalificacion(Data.Documento, Data.calificacion);
                calificacionReponse.ID = 0;
                calificacionReponse.mensaje = respuesta;
                calificacionReponse.objeto = null;

                return calificacionReponse;

            }catch (Exception ex)
            {
                calificacionReponse.ID = 1;
                calificacionReponse.mensaje = "Error metodo registrar calificacion";
                calificacionReponse.objeto = ex.Message;
                return calificacionReponse;
            }
        }
    }
}
