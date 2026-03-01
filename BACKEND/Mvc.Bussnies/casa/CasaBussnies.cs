using DtoModel.Casa;
using Mvc.Repository.casa;

namespace Mvc.Bussnies.casa
{
    public class CasaBussnies : ICasaBussnies
    {
        private readonly ICasaRepository _repo;

        public CasaBussnies(ICasaRepository repo)
        {
            _repo = repo;
        }

        public Task<List<CasaDto>> GetAll() => _repo.GetAll();
        public Task<CasaDto?> GetById(int id) => _repo.GetById(id);
        public Task<CasaDto> Create(CasaDto request) => _repo.Create(request);
        public Task<CasaDto?> Update(CasaDto request) => _repo.Update(request);
        public Task Delete(int id) => _repo.Delete(id);
    }
}