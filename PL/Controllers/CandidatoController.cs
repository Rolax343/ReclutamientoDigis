using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Controllers
{
    public class CandidatoController : Controller
    {
        [HttpGet]
        public IActionResult CandidatoVisualizacion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CandidatoCita()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CandidatoForm(int? IdCandidato)
        {
            if (IdCandidato == null)
            {
                ML.Candidato candidato = new ML.Candidato();
                candidato.Vacante = new ML.Vacante();
                ML.Result resultVacante = new ML.Result();
                resultVacante = BL.Vacante.GetAllVacantes();
                if (resultVacante.Correct)
                {
                    candidato.Vacante.Vacantes = resultVacante.Objects;
                }
                else
                {
                    ViewBag.Mensaje = "No hay Vacantes para mostrar";
                    return PartialView("Modal");
                }
      
                return View(candidato);
            }
            else
            {
                ML.Candidato resultItemList = new ML.Candidato();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5217/api/");
                    var responseTask = client.GetAsync("Candidato/GetById/" + IdCandidato);
                    responseTask.Wait();
                    var resultREST = responseTask.Result;

                    if (resultREST.IsSuccessStatusCode)
                    {
                        var readTask = resultREST.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Candidato>(readTask.Result.Object.ToString());
                        
                    }
                    ML.Result resultVacante = new ML.Result();
                    resultVacante = BL.Vacante.GetAllVacantes();
                    if (resultVacante.Correct)
                    {
                        resultItemList.Vacante.Vacantes = resultVacante.Objects;
                    }
                    else
                    {
                        ViewBag.Mensaje = "No hay Vacantes para mostrar";
                        return PartialView("Modal");
                    }
                }
                return View(resultItemList);
            }
        }
        [HttpGet]
        public JsonResult GetAllVacantes()
        {
            ML.Result result = new ML.Result();
            result = BL.Vacante.GetAllVacantes();
            return Json(result);
        }

        [HttpGet]
        public JsonResult CandidatoGetAll(int IdVacante)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5217/api/");
                var responseTask = client.GetAsync("Candidato/GetAll/" + IdVacante);
                responseTask.Wait();
                var resultREST = responseTask.Result;

                if (resultREST.IsSuccessStatusCode)
                {
                    var readTask = resultREST.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Candidato resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Candidato>(resultItem.ToString());
                        result.Objects.Add(resultItemList);
                    }
                }
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult CandidatoAdd (ML.Candidato candidato, IFormFile ImagenUsuario)
        {
            if (candidato.IdCandidato == null || candidato.IdCandidato == 0)
            {
                if (ImagenUsuario.Length > 0 && ImagenUsuario != null)
                {
                    candidato.Foto = ConvertirImagenAByte(ImagenUsuario);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5217/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Add", candidato); //Serializar
                    postTask.Wait();

                    var contenido = postTask.Result;
                    var result = contenido.Content.ReadAsAsync<ML.Result>().Result;

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "El candidato fue registrado con exito";
                    }
                    else
                    {   
                        ViewBag.Mensaje = "No se pudo registrar el candidato" + result.ErrorMessage;
                    }
                }
                
                return PartialView("Modal");
            }
            else
            {
                if (ImagenUsuario.Length > 0 && ImagenUsuario != null)
                {
                    candidato.Foto = ConvertirImagenAByte(ImagenUsuario);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5217/api/");
                    var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Update", candidato); //Serializar
                    postTask.Wait();

                    var contenido = postTask.Result;
                    var result = contenido.Content.ReadAsAsync<ML.Result>().Result;

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "El candidato fue actualizado con exito";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar el candidato" + result.ErrorMessage;
                    }
                }

                return PartialView("Modal");
            }
        }
        public byte[] ConvertirImagenAByte(IFormFile Imagen)
        {
            using (var memoryStream = new MemoryStream())
            {
                Imagen.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpGet]
        public ActionResult Confirmacion(int IdCandidato)
        {
            ViewBag.ID = IdCandidato;
            return PartialView("Modal_delete");
        }

        [HttpGet]
        public ActionResult CandidatoDelete(int IdCandidato)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5217/api/");
                //HTTP POST
                var responseTask = client.GetAsync("Candidato/Delete/" + IdCandidato);
                responseTask.Wait();

                var contenido = responseTask.Result;
                var result = contenido.Content.ReadAsAsync<ML.Result>().Result;

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El candidato fue eliminado con exito";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo eliminar el candidato" + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }
    }
}
