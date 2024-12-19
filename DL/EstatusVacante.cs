using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusVacante
{
    public byte IdEstatusVacante { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vacante> Vacantes { get; set; } = new List<Vacante>();
}
