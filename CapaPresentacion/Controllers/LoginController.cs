using CapaDatos.Models;
using CapaNegocio.Bissnes;
using CapaNegocio.Contract;
using CapaNegocio.Contract.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPersona.Models.DTO.ModelsJWT;

namespace CapaPresentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin login;
        public LoginController(ILogin _login)
        {
            login = _login;
        }
        [HttpPost]
        [Route("IniciarSession")]
        public dynamic IniciarSession([FromBody] UserJwt Data)
        {
            var repuesta = login.Logueo(Data);
            return repuesta;
        }

        [HttpPost]
        [Route("RegistrarAdmin")]
        public dynamic RegistrarAdmin([FromBody] UserJwt Data)
        {
            CalificacionReponse calificacionReponse = new CalificacionReponse();
            try
            {
                string mensaje = login.registrarAdmin(Data);

                calificacionReponse.ID = 0;
                calificacionReponse.mensaje = mensaje;
                calificacionReponse.objeto = null;
            }
            catch (Exception ex)
            {
                calificacionReponse.ID = 1;
                calificacionReponse.mensaje = "Error metodo crear admin";
                calificacionReponse.objeto = ex.Message;
            }

            return calificacionReponse;
        }
    }
}
