using DtoModel.Casa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.casa;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CasaController : ControllerBase
    {
        private readonly ICasaBussnies _casaBussnies;

        public CasaController(ICasaBussnies casaBussnies)
        {
            _casaBussnies = casaBussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<CasaDto>>> GetAll()
            => Ok(await _casaBussnies.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<CasaDto>> GetById(int id)
        {
            var casa = await _casaBussnies.GetById(id);
            if (casa == null) return NotFound(new { message = "Casa no encontrada" });
            return Ok(casa);
        }

        [HttpPost]
        public async Task<ActionResult<CasaDto>> Create([FromBody] CasaDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var casa = await _casaBussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = casa.Id }, casa);
        }

        [HttpPut]
        public async Task<ActionResult<CasaDto>> Update([FromBody] CasaDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var casa = await _casaBussnies.Update(request);
            if (casa == null) return NotFound(new { message = "Casa no encontrada" });
            return Ok(casa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var casa = await _casaBussnies.GetById(id);
            if (casa == null) return NotFound(new { message = "Casa no encontrada" });

            await _casaBussnies.Delete(id);
            return NoContent();
        }
    }
}