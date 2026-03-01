using DbModel.demoDb;
using DtoModel.Vehiculo;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Repository.vehiculo
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly _demoContext _db;

        public VehiculoRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<List<VehiculoDto>> GetAll()
        {
            var data = await _db.Vehiculo.AsNoTracking().ToListAsync();
            return data.Select(v => v.ToDto()).ToList();
        }

        public async Task<VehiculoDto?> GetById(int id)
        {
            var vehiculo = await _db.Vehiculo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return vehiculo == null ? null : vehiculo.ToDto();
        }

        public async Task<VehiculoDto> Create(VehiculoDto request)
        {
            if (request.UserCreate <= 0) request.UserCreate = 1;

            var entity = request.ToEntity();
            await _db.Vehiculo.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task<VehiculoDto?> Update(VehiculoDto request)
        {
            var entity = await _db.Vehiculo.FindAsync(request.Id);
            if (entity == null) return null;

            entity.Placa = request.Placa;
            entity.Marca = request.Marca;
            entity.Modelo = request.Modelo;
            entity.Anio = request.Anio;
            entity.Color = request.Color;
            entity.IdPropietarioPersona = request.IdPropietarioPersona;
            entity.IdCasa = request.IdCasa;
            entity.Estado = request.Estado;
            entity.UserUpdate = request.UserUpdate;
            entity.DateUpdate = request.DateUpdate;

            await _db.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.Vehiculo.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
