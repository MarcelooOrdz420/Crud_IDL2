using DbModel.demoDb;
using DtoModel.Casa;

namespace Mvc.Repository.casa
{
    public static class CasaMapping
    {
        public static CasaDto ToDto(this Casa casa)
        {
            return new CasaDto
            {
                Id = casa.Id,
                Nombre = casa.Nombre,
                Direccion = casa.Direccion,
                Referencia = casa.Referencia,
                IdPropietarioPersona = casa.IdPropietarioPersona,
                UserCreate = casa.UserCreate,
                UserUpdate = casa.UserUpdate,
                DateCreated = casa.DateCreated,
                DateUpdate = casa.DateUpdate,
            };
        }

        public static Casa ToEntity(this CasaDto dto)
        {
            return new Casa
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Referencia = dto.Referencia,
                IdPropietarioPersona = dto.IdPropietarioPersona,
                UserCreate = dto.UserCreate,
                UserUpdate = dto.UserUpdate,
                DateCreated = dto.DateCreated,
                DateUpdate = dto.DateUpdate,
            };
        }

        public static List<CasaDto> ToDtoList(this List<Casa> casas) => casas.Select(c => c.ToDto()).ToList();
    }
}
