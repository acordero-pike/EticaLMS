using System;
using System.Collections.Generic;

namespace LMS.Models
{
    public partial class ModuleType
    {
        public ModuleType()
        {
            Modules = new HashSet<Module>();
        }

        public string Uuid { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Module> Modules { get; set; }
    }
}
