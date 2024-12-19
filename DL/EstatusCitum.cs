using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusCitum
{
    public byte IdEstatusCita { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
