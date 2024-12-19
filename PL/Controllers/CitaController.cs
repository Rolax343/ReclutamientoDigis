using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class CitaController : Controller
    {
        [HttpGet]
        public IActionResult Cita(int IdCandidato)
        {
            ML.Result result = BL.Candidato.CandidatoGetById(IdCandidato);
            ML.Candidato candidato = new ML.Candidato();
            candidato = (ML.Candidato)result.Object;
            candidato.Cita.EstatusCita.EstatusCitas = new List<object>();
            candidato.Cita.Piso.Pisos = new List<object>();
            ML.Result resultEstatusCita = new ML.Result();
            resultEstatusCita = BL.EstatusCita.GetAllEstatus();
            if (resultEstatusCita.Correct)
            {
                candidato.Cita.EstatusCita.EstatusCitas = resultEstatusCita.Objects;
            }
            else
            {
                ViewBag.Mensaje = "No hay Estatus para mostrar";
                return PartialView("Modal");
            }
            ML.Result resultPiso = new ML.Result();
            resultPiso = BL.Piso.GetAllPisos();
            if (resultPiso.Correct)
            {
                candidato.Cita.Piso.Pisos = resultPiso.Objects;
            }
            else
            {
                ViewBag.Mensaje = "No hay Pisos para mostrar";
                return PartialView("Modal");
            }
            return View(candidato);
        }

        [HttpPost]
        public IActionResult CitaForm(ML.Candidato candidato)
        {
            if (candidato.Cita.IdCita == null)
            {
                ML.Result result = new ML.Result();
                result = BL.Cita.CitaAdd(candidato);
                if (result.Correct)
                {
                    string candidatoJson = JsonConvert.SerializeObject(candidato);
                    TempData["Candidato"] = candidatoJson;
                    return RedirectToAction("EnviarCorreo", "Correo");
                } else
                {
                    ViewBag.Mensaje = "Ocurrio un error: " + result.ErrorMessage;
                }
            } else
            {
                ML.Result result = new ML.Result();
                result = BL.Cita.CitaUpdate(candidato);
                if (result.Correct)
                {
                    string candidatoJson = JsonConvert.SerializeObject(candidato);
                    TempData["Candidato"] = candidatoJson;
                    return RedirectToAction("EnviarCorreo", "Correo");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error: " + result.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult CitaDelete(int IdCita)
        {
                ML.Result result = BL.Cita.CitaDelete(IdCita);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "La cita fue eliminado con exito";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo eliminar la cita" + result.ErrorMessage;
                }
            return PartialView("Modal");
        }

        public ActionResult Confirmacion(int IdCita)
        {
            ViewBag.ID = IdCita;
            return PartialView("Modal_delete");
        }

        
    }
}
