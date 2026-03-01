using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepo.Mapping;
using Mvc.Repository.PersonaTipoDocumentoRepo.Contratos;
using Mvc.Repository.PersonaTipoDocumentoRepo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.PersonaTipoDocumentoRepo.Implementacion
{
    public class PersonaTipoDocumentoRepository : IPersonaTipoDocumentoRepository
    {

        private readonly _demoContext _db;

        //Estamos generando un constructor 
        //este nos permite inicializar cualquier recurso que necesitemos
        //para nuestra clase, como conexiones a bases de datos, servicios externos, etc.
        public PersonaTipoDocumentoRepository(_demoContext db)
        {
            _db = db;
        }

        public Task<PersonaTipoDocumentoDto> Create(PersonaTipoDocumentoDto request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaTipoDocumentoDto>> GetAll()
        {
            List<PersonaTipoDocumentoDto> res = new List<PersonaTipoDocumentoDto>();
            List<PersonaTipoDocumento> data = await _db.PersonaTipoDocumento.ToListAsync();
            res = PersonaTipoDocumentoMapping.ToDtoList(data);
            return res;
        }

        public Task<PersonaTipoDocumentoDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonaTipoDocumentoDto> Update(PersonaTipoDocumentoDto request)
        {
            throw new NotImplementedException();
        }
    }
}
