namespace ML
{
    public class Candidato
    {
        public int? IdCandidato { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Edad { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public byte[]? Foto { get; set; }
        public string? FotoBase64 {  get; set; }
        public byte[]? Curriculum { get; set; }
        public string? CurriculumBase64 { get; set; }
        public int? IdUniversidad { get; set; }
        public int? IdCarrera { get; set; }
        public int? IdBolsaTrabajo { get; set; }
        public ML.Vacante? Vacante { get; set; }
        public ML.Cita? Cita { get; set; }
    }
}
