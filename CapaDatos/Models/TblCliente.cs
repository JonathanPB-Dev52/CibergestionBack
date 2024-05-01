using System;
using System.Collections.Generic;

namespace CapaDatos.Models
{
    public partial class TblCliente
    {
        public int Id { get; set; }
        public string? Documento { get; set; }
        public int? Calificacion { get; set; }
        public int? IdRol { get; set; }

        //public virtual RolUsuario? IdRolNavigation { get; set; }
    }
}
