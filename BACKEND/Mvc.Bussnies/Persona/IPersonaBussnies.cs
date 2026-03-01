using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Bussnies.Persona
{
    public interface IPersonaBussnies
    {
        Task<List<PersonaDto>> GetAll();
        Task<PersonaDto?> GetById(int id);
        Task<PersonaDto> Create(PersonaDto request);
        Task<PersonaDto?> Update(PersonaDto request);
        Task Delete(int id);
    }
}