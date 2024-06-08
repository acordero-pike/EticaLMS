using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class Module
    {
        public Module()
        {
            Tests = new HashSet<Test>();
        }

        public string Uuid { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoModulo { get; set; }

        public virtual ModuleType? TipoModuloNavigation { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
