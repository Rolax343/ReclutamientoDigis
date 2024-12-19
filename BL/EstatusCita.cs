using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusCita
    {
        public static ML.Result GetAllEstatus()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var tableCitas = (from EstatusCitalinq in context.EstatusCita
                                           select new
                                           {
                                               IdEstatusCita = EstatusCitalinq.IdEstatusCita,
                                               Nombre = EstatusCitalinq.Nombre
                                           }).ToList();
                    if (tableCitas.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in tableCitas)
                        {
                            ML.EstatusCita estatuscita = new ML.EstatusCita();
                            estatuscita.IdEstatusCita = item.IdEstatusCita;
                            estatuscita.Nombre = item.Nombre;
                            result.Objects.Add(estatuscita);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los estatus de la Base de satos";
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
