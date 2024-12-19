using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class CorreoController : Controller
    {
        public ActionResult EnviarCorreo()
        {      
            string candidatoJson = TempData["Candidato"].ToString();
            ML.Candidato candidato = JsonConvert.DeserializeObject<ML.Candidato>(candidatoJson);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "htmlplantillas", "plantillacorreo.html");
            string body = "";
            using (StreamReader leer =  new StreamReader(path))
            {
                body = leer.ReadToEnd();
            }
            body = body.Replace("{Nombre}", candidato.Nombre);
            if (candidato.Cita.IdCita == null)
            {
                body = body.Replace("{agendado-modificado}", "agendado");
            } else
            {
                body = body.Replace("{agendado-modificado}", "modificado");
            }
            if (candidato.Cita.Piso.IdPiso == null)
            {
                body = body.Replace("{TipoEntrevista}", "virtual");
                body = body.Replace("{Link-Piso}", "Link:");
                candidato.Cita.Url = "https://www.google.com";
                body = body.Replace("{link}", candidato.Cita.Url);
                body = body.Replace("{botonurl}", candidato.Cita.Url);
            } else
            {
                body = body.Replace("{TipoEntrevista}", "presencial");
                body= body.Replace("{Link-Piso}", "Piso:");
                if (candidato.Cita.Piso.IdPiso == 1)
                {
                    body = body.Replace("{link}", "9");
                    candidato.Cita.Url = "https://www.google.com";
                    body = body.Replace("{botonurl}", candidato.Cita.Url);
                }
                else if (candidato.Cita.Piso.IdPiso == 2)
                {
                    body = body.Replace("{link}", "14");
                    candidato.Cita.Url = "https://www.google.com";
                    body = body.Replace("{botonurl}", candidato.Cita.Url);
                }
                
            }
            string[] fechapartes = candidato.Cita.FechaHora.Split(' ');
            string fecha = fechapartes[0];
            string hora = fechapartes[1].Substring(0,5);
            body = body.Replace("{Fecha}", fecha);
            body = body.Replace("{Hora}", hora);
            try
            {
                string correo = "rolaxendro.best@gmail.com";
                string contraseña = "xfxb ddfq dopu ryqo";
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, contraseña),
                    EnableSsl = true
                };
                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Rolax"),
                    Subject = "Correo de Prueba",
                    Body = body,
                    IsBodyHtml = true
                };

                mensaje.To.Add("jguevaraflores3@gmail.com");
                smtpClient.Send(mensaje);
                ViewBag.Mensaje = "Se registro la cita exitosamente y se envio el correo de notificacion";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return PartialView("Modal");
        }
    }
}
