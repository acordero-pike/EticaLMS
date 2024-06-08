using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Test
    {
        public string? Uuid { get; set; } = null!;
        public string? Usuario { get; set; }
        public string? Modulo { get; set; }
        public double? Nota { get; set; }
        public bool? Calificado { get; set; }

        public virtual Module? ModuloNavigation { get; set; }
        public virtual User? UsuarioNavigation { get; set; }
    }
}
