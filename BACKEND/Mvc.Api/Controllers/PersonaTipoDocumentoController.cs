using DtoModel.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Persona;
using Mvc.Bussnies.PersonaTipoDocumentoB;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaTipoDocumentoController : ControllerBase
    {
        private readonly IPersonaTipoDocumentoBussnies _bussnies;

        public PersonaTipoDocumentoController(IPersonaTipoDocumentoBussnies bussnies)
        {
            _bussnies = bussnies;
        }


        [HttpGet]
        public async Task<ActionResult<List<PersonaTipoDocumentoDto>>> Get()
        {

            return Ok(await _bussnies.GetAll());
        }
    }
}



