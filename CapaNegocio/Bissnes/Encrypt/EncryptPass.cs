using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Bissnes.Encrypt
{
    public class EncryptPass
    {
        public static string GetSHA2556(string password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] steam = null;
            StringBuilder sb = new StringBuilder();
            steam = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < steam.Length; i++) sb.AppendFormat("{0:x2}", steam[i]);
            return sb.ToString();
        }
    }
}
