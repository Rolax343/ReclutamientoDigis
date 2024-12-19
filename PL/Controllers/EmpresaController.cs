using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]

        public IActionResult Empresa()
        {
            ML.Result result = BL.Empresa.EmpresaGetAll();
            ML.Empresa empresa = new ML.Empresa();
            empresa.empresas = new List<object>();
            if (result.Correct)
            {
                empresa.empresas = result.Objects.ToList();
            } else
            {
                ViewBag.Mensaje = "Error: " + result.ErrorMessage;
                return PartialView("Modal");
            }
            return View(empresa);
        }

        [HttpGet]

        public IActionResult EmpresaForm(int? IdEmpresa)
        {
            if (IdEmpresa == 0 || IdEmpresa == null)
            {
                return View();
            }
            else
            {
                ML.Result result = new ML.Result();
                ML.Empresa empresa = new ML.Empresa();
                result = BL.Empresa.EmpresaGetById(IdEmpresa);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    return View(empresa);
                } else
                {
                    ViewBag.Mensaje = "Hubo un error: " + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            
        }

        [HttpPost]
        public IActionResult EmpresaAdd(ML.Empresa empresa)
        {
            if (empresa.IdEmpresa == 0 || empresa.IdEmpresa == null)
            {
                ML.Result result = BL.Empresa.EmpresaAdd(empresa);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El registro se inserto correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error: " + result.ErrorMessage;
                }
            } else
            {
                ML.Result result = BL.Empresa.EmpresaUpdate(empresa);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El registro se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error: " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult EmpresaDelete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.EmpresaDelete(IdEmpresa);
            if (result.Correct)
            {
                ViewBag.Mensaje = "La empresa fue eliminado con exito";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar la empresa" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        public ActionResult Confirmacion(int IdEmpresa)
        {
            ViewBag.ID = IdEmpresa;
            return PartialView("Modal_delete");
        }
    }
}
