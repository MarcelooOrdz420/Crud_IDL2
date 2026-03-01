using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.PersonaTipoDocumentoB
{
    public interface IPersonaTipoDocumentoBussnies
    {
        Task<List<PersonaTipoDocumentoDto>> GetAll();

    }
}
