using System;
using System.Collections.Generic;

namespace DL;

public partial class Candidato
{
    public int IdCandidato { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Edad { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public byte[]? Foto { get; set; }

    public byte[]? Curriculum { get; set; }

    public int? IdVacante { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Vacante? IdVacanteNavigation { get; set; }
}
