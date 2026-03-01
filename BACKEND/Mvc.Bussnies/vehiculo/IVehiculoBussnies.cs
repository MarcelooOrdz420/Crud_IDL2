using DtoModel.Vehiculo;

namespace Mvc.Bussnies.vehiculo
{
    public interface IVehiculoBussnies
    {
        Task<List<VehiculoDto>> GetAll();
        Task<VehiculoDto?> GetById(int id);
        Task<VehiculoDto> Create(VehiculoDto request);
        Task<VehiculoDto?> Update(VehiculoDto request);
        Task Delete(int id);
    }
}
