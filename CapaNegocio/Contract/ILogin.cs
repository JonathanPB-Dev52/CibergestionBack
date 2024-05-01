using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPersona.Models.DTO.ModelsJWT;

namespace CapaNegocio.Contract
{
    public interface ILogin
    {
        public dynamic Logueo(UserJwt Data);

        public string registrarAdmin(UserJwt Data);
    }
}
