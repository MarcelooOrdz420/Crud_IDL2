using DtoModel.Vehiculo;
using Mvc.Repository.vehiculo;

namespace Mvc.Bussnies.vehiculo
{
    public class VehiculoBussnies : IVehiculoBussnies
    {
        private readonly IVehiculoRepository _repo;

        public VehiculoBussnies(IVehiculoRepository repo)
        {
            _repo = repo;
        }

        public Task<List<VehiculoDto>> GetAll() => _repo.GetAll();
        public Task<VehiculoDto?> GetById(int id) => _repo.GetById(id);
        public Task<VehiculoDto> Create(VehiculoDto request) => _repo.Create(request);
        public Task<VehiculoDto?> Update(VehiculoDto request) => _repo.Update(request);
        public Task Delete(int id) => _repo.Delete(id);
    }
}
