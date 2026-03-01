using DbModel.demoDb;
using DtoModel.Vehiculo;

namespace Mvc.Repository.vehiculo
{
    public static class VehiculoMapping
    {
        public static VehiculoDto ToDto(this Vehiculo vehiculo)
        {
            return new VehiculoDto
            {
                Id = vehiculo.Id,
                Placa = vehiculo.Placa,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Anio = vehiculo.Anio,
                Color = vehiculo.Color,
                IdPropietarioPersona = vehiculo.IdPropietarioPersona,
                IdCasa = vehiculo.IdCasa,
                Estado = vehiculo.Estado,
                UserCreate = vehiculo.UserCreate,
                UserUpdate = vehiculo.UserUpdate,
                DateCreated = vehiculo.DateCreated,
                DateUpdate = vehiculo.DateUpdate,
            };
        }

        public static Vehiculo ToEntity(this VehiculoDto dto)
        {
            return new Vehiculo
            {
                Id = dto.Id,
                Placa = dto.Placa,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Anio = dto.Anio,
                Color = dto.Color,
                IdPropietarioPersona = dto.IdPropietarioPersona,
                IdCasa = dto.IdCasa,
                Estado = dto.Estado,
                UserCreate = dto.UserCreate,
                UserUpdate = dto.UserUpdate,
                DateCreated = dto.DateCreated,
                DateUpdate = dto.DateUpdate,
            };
        }
    }
}
