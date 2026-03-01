using DtoModel.Vehiculo;

namespace Mvc.Repository.vehiculo
{
    public interface IVehiculoRepository
    {
        Task<List<VehiculoDto>> GetAll();
        Task<VehiculoDto?> GetById(int id);
        Task<VehiculoDto> Create(VehiculoDto request);
        Task<VehiculoDto?> Update(VehiculoDto request);
        Task Delete(int id);
    }
}
