using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio.Bissnes.Validations
{
    public class ValidDocumento
    {
        public static bool validDocumento(string documento)
        {
            // Verificar si el documento es nulo o vacío
            if (string.IsNullOrEmpty(documento))
            {
                return false;
            }

            // Expresión regular para validar el documento
            // - No inicia con cero
            // - Contiene más de 5 caracteres
            // - Solo contiene números del 1 al 9 (sin cero al inicio)
            string documentoPattern = @"^[1-9][0-9]{5,}$"; // Número de identificación que no inicia con cero y tiene más de 5 dígitos

            // Verificar si el documento coincide con el patrón de la expresión regular
            if (!Regex.IsMatch(documento, documentoPattern))
            {
                return false;
            }

            return true;
        }
    }
}
