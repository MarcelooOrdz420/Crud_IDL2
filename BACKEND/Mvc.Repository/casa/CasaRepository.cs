using DbModel.demoDb;
using DtoModel.Casa;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Repository.casa
{
    public class CasaRepository : ICasaRepository
    {
        private readonly _demoContext _db;

        public CasaRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<List<CasaDto>> GetAll()
        {
            var data = await _db.Casa.AsNoTracking().ToListAsync();
            return data.Select(c => c.ToDto()).ToList();
        }

        public async Task<CasaDto?> GetById(int id)
        {
            var casa = await _db.Casa.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return casa == null ? null : casa.ToDto();
        }

        public async Task<CasaDto> Create(CasaDto request)
        {
            // auditoría mínima
            if (request.UserCreate <= 0) request.UserCreate = 1;

            var entity = request.ToEntity();
            await _db.Casa.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<CasaDto?> Update(CasaDto request)
        {
            var entity = await _db.Casa.FindAsync(request.Id);
            if (entity == null) return null;

            entity.Nombre = request.Nombre;
            entity.Direccion = request.Direccion;
            entity.Referencia = request.Referencia;
            entity.IdPropietarioPersona = request.IdPropietarioPersona;
            entity.UserUpdate = request.UserUpdate;
            entity.DateUpdate = request.DateUpdate;

            await _db.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.Casa.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
