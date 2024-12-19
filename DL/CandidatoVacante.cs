using System;
using System.Collections.Generic;

namespace DL;

public partial class CandidatoVacante
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

    public string? NombreVacante { get; set; }

    public int? IdCita { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Url { get; set; }

    public byte? IdPiso { get; set; }

    public string? NombrePiso { get; set; }

    public byte? IdEstatusCita { get; set; }

    public string? NombreEstatusCita { get; set; }
}
