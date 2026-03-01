using DtoModel.Vehiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.vehiculo;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoBussnies _vehiculoBussnies;

        public VehiculoController(IVehiculoBussnies vehiculoBussnies)
        {
            _vehiculoBussnies = vehiculoBussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehiculoDto>>> GetAll()
            => Ok(await _vehiculoBussnies.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDto>> GetById(int id)
        {
            var vehiculo = await _vehiculoBussnies.GetById(id);
            if (vehiculo == null) return NotFound(new { message = "Vehiculo no encontrado" });
            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<VehiculoDto>> Create([FromBody] VehiculoDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var vehiculo = await _vehiculoBussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = vehiculo.Id }, vehiculo);
        }

        [HttpPut]
        public async Task<ActionResult<VehiculoDto>> Update([FromBody] VehiculoDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var vehiculo = await _vehiculoBussnies.Update(request);
            if (vehiculo == null) return NotFound(new { message = "Vehiculo no encontrado" });
            return Ok(vehiculo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var vehiculo = await _vehiculoBussnies.GetById(id);
            if (vehiculo == null) return NotFound(new { message = "Vehiculo no encontrado" });

            await _vehiculoBussnies.Delete(id);
            return NoContent();
        }
    }
}
