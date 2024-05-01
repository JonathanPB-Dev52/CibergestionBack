using CapaDatos.Models;
using CapaNegocio.Bissnes.Encrypt;
using CapaNegocio.Bissnes.Validations;
using CapaNegocio.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiPersona.Models.DTO.ModelsJWT;

namespace CapaNegocio.Bissnes
{
    public class Login : ILogin
    {
        public IConfiguration _configuration;

        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public dynamic Logueo(UserJwt Data)
        {
            if (!ValidCorreo.ValidateEmail(Data.Usuario))
            {
                return new
                {
                    success = true,
                    mesagge = "Correo invalio",
                    result = ""
                };
            }
            using (CibergestionPruebaContext cibergestionPruebaContext = new CibergestionPruebaContext(_configuration))
            {
                string user = Data.Usuario;
                string pass = Data.Passwork;

                string passEncrypt = EncryptPass.GetSHA2556(pass);

                TblRolAdmin usuario = cibergestionPruebaContext.TblRolAdmins.FirstOrDefault(x => x.NombreUsuario.Trim() == user && x.Contra.Trim() == passEncrypt);

                if (usuario == null)
                {
                    return new
                    {
                        success = false,
                        message = "Credenciales incorrestas",
                        result = ""
                    };
                }
                usuario.NombreUsuario = usuario.NombreUsuario.Trim();
                usuario.Contra = usuario.Contra.Trim();

                var jwt = _configuration.GetSection("JWT").Get<Jwt>();

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject));
                claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
                claims.AddClaim(new Claim("id", usuario.Id.ToString()));
                claims.AddClaim(new Claim("NombreUsu", usuario.NombreUsuario));
                claims.AddClaim(new Claim("Rol", usuario.IdRol.ToString()));

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = singIn
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(token);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);


                return new
                {
                    success = true,
                    mesagge = "Logueo Exitoso",
                    result = tokenCreado
                };
            }
        }

        public string registrarAdmin(UserJwt Data)
        {
            try
            {
                if (!ValidCorreo.ValidateEmail(Data.Usuario))
                {
                    return "Correo inválido";
                }
                if (!ValidPass.ValidatePassword(Data.Passwork))
                {
                    return "Contraseña inválida, debe contener al menos una mayuscula con numeros y caracteres especiales. Sin espacios";
                }

                using (CibergestionPruebaContext cibergestionPruebaContext = new CibergestionPruebaContext(_configuration))
                {
                    var UserExist = cibergestionPruebaContext.TblRolAdmins.Where(x=> x.NombreUsuario  == Data.Usuario).ToList();
                    if(UserExist.Any())
                    {
                        return "Correo ya esta registrado";
                    }
                    else
                    {
                        TblRolAdmin tblRolAdmin = new TblRolAdmin();
                        tblRolAdmin.NombreUsuario = Data.Usuario.Trim();
                        tblRolAdmin.Contra = EncryptPass.GetSHA2556(Data.Passwork);
                        tblRolAdmin.IdRol = 1;
                        cibergestionPruebaContext.TblRolAdmins.Add(tblRolAdmin);
                        cibergestionPruebaContext.SaveChanges();

                        return "Administrador registrado exitosamente";
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
