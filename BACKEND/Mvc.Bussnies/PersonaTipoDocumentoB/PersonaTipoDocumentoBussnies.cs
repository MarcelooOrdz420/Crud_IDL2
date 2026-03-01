using DtoModel.Persona;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaTipoDocumentoRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.PersonaTipoDocumentoB
{
    public class PersonaTipoDocumentoBussnies : IPersonaTipoDocumentoBussnies
    {
        private readonly IPersonaTipoDocumentoRepository _repo;

        public PersonaTipoDocumentoBussnies(IPersonaTipoDocumentoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PersonaTipoDocumentoDto>> GetAll()
        {
            List<PersonaTipoDocumentoDto> lista = await _repo.GetAll();
            return lista;
        }
    }
}
