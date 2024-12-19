using System;
using System.Collections.Generic;

namespace DL;

public partial class Piso
{
    public byte IdPiso { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
