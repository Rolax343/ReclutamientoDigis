using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        [HttpGet]
        [Route("api/Candidato/GetAll/{IdVacante}")]
        public IActionResult CandidatoGetAll(int IdVacante)
            {
            var result = BL.Candidato.CandidatoGetAll(IdVacante);
            if (result.Correct)
            {
                return Ok(result);
            } else { 
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Candidato/GetById/{IdCandidato}")]
        public IActionResult CandidatoGetById(int IdCandidato)
        {
            var result = BL.Candidato.CandidatoGetById(IdCandidato);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Candidato/Add")]
        public IActionResult CandidatoAdd([FromBody]ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            result = BL.Candidato.CandidatoAdd(candidato);
            if (result.Correct)
            {
                return Ok(result);
            } else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Candidato/Update")]
        public IActionResult CandidatoUpdate(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            result = BL.Candidato.CandidatoUpdate(candidato);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Candidato/Delete/{IdCandidato}")]
        public IActionResult Delete(int IdCandidato)
        {
            var result = BL.Candidato.CandidatoDelete(IdCandidato);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
