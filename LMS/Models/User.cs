using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class User
    {
        public User()
        {
            Tests = new HashSet<Test>();
        }

        public string Uuid { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public string? TipoUsuario { get; set; }

        public virtual UserType? TipoUsuarioNavigation { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
