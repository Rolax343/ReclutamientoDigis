using System;
using System.Collections.Generic;

namespace DL;

public partial class Citum
{
    public int IdCita { get; set; }

    public DateTime FechaHora { get; set; }

    public string? Url { get; set; }

    public byte? IdPiso { get; set; }

    public int? IdCandidato { get; set; }

    public byte? IdEstatusCita { get; set; }

    public virtual Candidato? IdCandidatoNavigation { get; set; }

    public virtual EstatusCitum? IdEstatusCitaNavigation { get; set; }

    public virtual Piso? IdPisoNavigation { get; set; }
}
