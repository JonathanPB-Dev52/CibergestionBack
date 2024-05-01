using System;
using System.Collections.Generic;

namespace CapaDatos.Models
{
    public partial class TblRolAdmin
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contra { get; set; }
        public int? IdRol { get; set; }

        //public virtual RolUsuario? IdRolNavigation { get; set; }
    }
}
