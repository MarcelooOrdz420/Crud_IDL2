using DtoModel.Persona;
using Mvc.Repository.PersonaRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Bussnies.Persona
{
    public class PersonaBussnies : IPersonaBussnies
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaBussnies(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaDto> Create(PersonaDto request)
        {
            PersonaDto result = await _personaRepository.Create(request);
            return result;
        }

        public async Task<List<PersonaDto>> GetAll()
        {
            List<PersonaDto> lista = await _personaRepository.GetAll();
            return lista;
        }

        public async Task<PersonaDto?> GetById(int id)
        {
            PersonaDto person = await _personaRepository.GetById(id);

            return person;

        }

        public async Task<PersonaDto?> Update(PersonaDto request)
        {
            PersonaDto? personDb = await _personaRepository.GetById(request.Id);

            if (personDb == null)
            {
                throw new Exception("Persona a actualizar no existe");
            }

            
            PersonaDto result = await _personaRepository.Update(request);

            return result;

        }

        public async Task Delete(int id)
        {
            await _personaRepository.Delete(id);
        }
    }
}
