using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Vacante
    {
        public static ML.Result GetAllVacantes ()
        {
            ML.Result result = new ML.Result ();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var tableCandidatos = (from candidatolinq in context.Vacantes
                                           select new
                                           {
                                               IdVacante = candidatolinq.IdVacante,
                                               Nombre = candidatolinq.Nombre,
                                               FechaPublicacion = candidatolinq.FechaPublicacion,
                                               FechaLimite = candidatolinq.FechaLimite,
                                               UrlVacante = candidatolinq.UrlVacante,
                                               IdEstatusVacante = candidatolinq.IdEstatusVacante
                                           }).ToList ();
                    if (tableCandidatos.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in tableCandidatos)
                        {
                            ML.Vacante vacante = new ML.Vacante ();
                            vacante.EstatusVacante = new ML.EstatusVacante ();
                            vacante.IdVacante = item.IdVacante;
                            vacante.Nombre = item.Nombre;
                            vacante.FechaPublicacion = Convert.ToString(item.FechaPublicacion);
                            vacante.FechaLimite = Convert.ToString(item.FechaLimite);
                            vacante.UrlVacante = item.UrlVacante;
                            vacante.EstatusVacante.IdEstatusVacante = item.IdEstatusVacante;
                            result.Objects.Add(vacante);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay vacantes o no se pudo recuperar las vacantes de la Base de satos";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }
            return result;
        }
    }
}
