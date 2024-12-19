using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vacante
    {
        public int? IdVacante { get; set; }
        public string? Nombre { get; set; }
        public string? FechaPublicacion { get; set; }
        public string? FechaLimite { get; set; }
        public string? UrlVacante { get; set; }
        public ML.EstatusVacante? EstatusVacante { get; set; }

        public List<object>? Vacantes { get; set; }
    }
}
