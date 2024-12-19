using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Piso
    {
        public static ML.Result GetAllPisos()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var tablePisos = (from EstatusPisolinq in context.Pisos
                                      select new
                                      {
                                          IdPiso = EstatusPisolinq.IdPiso,
                                          Nombre = EstatusPisolinq.Nombre
                                      }).ToList();
                    if (tablePisos.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in tablePisos)
                        {
                            ML.Piso piso = new ML.Piso();
                            piso.IdPiso = item.IdPiso;
                            piso.Nombre = item.Nombre;
                            result.Objects.Add(piso);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los pisos de la Base de datos";
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
