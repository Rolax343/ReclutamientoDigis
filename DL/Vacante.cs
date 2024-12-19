using System;
using System.Collections.Generic;

namespace DL;

public partial class Vacante
{
    public int IdVacante { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaLimite { get; set; }

    public string? UrlVacante { get; set; }

    public byte? IdEstatusVacante { get; set; }

    public virtual ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();

    public virtual EstatusVacante? IdEstatusVacanteNavigation { get; set; }
}
