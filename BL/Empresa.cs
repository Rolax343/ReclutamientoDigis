using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result EmpresaGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var tableEmpresas = context.Empresas.FromSqlRaw("EmpresaGetAll").ToList();

                    if (tableEmpresas.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in tableEmpresas)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.Nombre = item.Nombre;
                            empresa.IdEmpresa = item.IdEmpresa;
                            empresa.Latitud = item.Latitud;
                            empresa.Longitud = item.Longitud;
                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros o no se pudieron obtener";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result EmpresaGetById(int? IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    var tableEmpresa = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();

                    if (tableEmpresa != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.Nombre = tableEmpresa.Nombre;
                        empresa.Latitud = tableEmpresa.Latitud;
                        empresa.Longitud =  tableEmpresa.Longitud;
                        result.Object = empresa;
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result EmpresaAdd(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    int rowsaffected = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Latitud}', '{empresa.Longitud}'");

                    if (rowsaffected > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al insertar el registro";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaUpdate(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    int rowsaffected = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa}, '{empresa.Nombre}', '{empresa.Latitud}', '{empresa.Longitud}'");

                    if (rowsaffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error al actualizar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaDelete (int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ReclutamientoDigisContext context = new DL.ReclutamientoDigisContext())
                {
                    int rowsaffected = context.Database.ExecuteSqlRaw($"EmpresaDelete {IdEmpresa}");

                    if(rowsaffected > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el registro";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
