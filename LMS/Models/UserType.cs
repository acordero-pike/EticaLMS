using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public string Uuid { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
