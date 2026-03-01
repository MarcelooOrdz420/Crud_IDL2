using DtoModel.Persona;
using Mvc.Repository.General.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.PersonaTipoDocumentoRepo.Contratos
{
    public interface IPersonaTipoDocumentoRepository : ICrudRepository<PersonaTipoDocumentoDto>, IDisposable
    {

    }
}
