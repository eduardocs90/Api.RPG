using Domain.Entities;
using Domain.Repositories;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraData.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        public readonly ContextDb _db;

        public EquipmentRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task<Equipment> Create(Equipment body)
        {
            _db.Equipments.Add(body);
            await _db.SaveChangesAsync();
            return body;
        }

        public async Task<bool> Delete(Equipment body)
        {
            _db.Equipments.Remove(body);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<ICollection<Equipment>> FindAll()
        {
            var equipment = await _db.Equipments.ToListAsync();
            return equipment;
        }

        public async Task<Equipment> FindById(int id)
        {
            var equipment = await _db.Equipments.SingleOrDefaultAsync(x => x.Id == id);
            return equipment;
        }

        public async Task<Equipment> Update(Equipment body)
        {
            _db.Equipments.Update(body);
            await _db.SaveChangesAsync();
            return body;
        }
    }
}
