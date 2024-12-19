using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cita
    {
        public static ML.Result CitaAdd(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    int rowsaffected = 0;
                    if (candidato.Cita.Piso.IdPiso == null)
                    {
                        rowsaffected = context.Database.ExecuteSqlRaw($"CitaAdd '{candidato.Cita.FechaHora}', '{candidato.Cita.Url}', null, {candidato.IdCandidato}, {candidato.Cita.EstatusCita.IdEstatusCita}");

                    }
                    else
                    {
                        rowsaffected = context.Database.ExecuteSqlRaw($"CitaAdd '{candidato.Cita.FechaHora}', '{candidato.Cita.Url}', {candidato.Cita.Piso.IdPiso}, {candidato.IdCandidato}, {candidato.Cita.EstatusCita.IdEstatusCita}");
                    }

                    if (rowsaffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrió un error al insertar el registro";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage += e.Message;
                result.Ex = e;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result CitaUpdate(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    int rowsaffected = 0;
                    if (candidato.Cita.Piso.IdPiso == null)
                    {
                        rowsaffected = context.Database.ExecuteSqlRaw($"CitaUpdate {candidato.Cita.IdCita}, '{candidato.Cita.FechaHora}', '{candidato.Cita.Url}', null, {candidato.IdCandidato}, {candidato.Cita.EstatusCita.IdEstatusCita}");

                    }
                    else
                    {
                        rowsaffected = context.Database.ExecuteSqlRaw($"CitaUpdate {candidato.Cita.IdCita},'{candidato.Cita.FechaHora}', '{candidato.Cita.Url}', {candidato.Cita.Piso.IdPiso}, {candidato.IdCandidato}, {candidato.Cita.EstatusCita.IdEstatusCita}");
                    }

                    if (rowsaffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrió un error al insertar el registro";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage += e.Message;
                result.Ex = e;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result CitaDelete(int IdCita)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var rowsaffected = context.Database.ExecuteSqlRaw($"CitaDelete {IdCita}");

                    if (rowsaffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrió un error al eliminar el registro";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage += e.Message;
                result.Ex = e;
                result.Correct = false;
            }
            return result;
        }
    }
}
