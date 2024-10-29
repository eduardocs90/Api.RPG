using Domain.Entities;
using Domain.Repositories;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraData.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        public readonly ContextDb _db;

        public CharacterRepository(ContextDb db)
        {
            _db = db;
        }


        public async Task<ICollection<Character>> FindAll() => await _db.Characters.Include(x => x.Equipment).ToListAsync();


        public async Task<Character> FindById(int id) => await _db.Characters.Include(x=> x.Equipment).SingleOrDefaultAsync(c => c.Id == id);


        public async Task<Character> FindByName(string name) => await _db.Characters.Include(x => x.Equipment).SingleOrDefaultAsync(c => c.Name == name);

        public async Task<Character> FindByIdForUpdate(int id) => await _db.Characters.SingleOrDefaultAsync(u => u.Id == id);

        public async Task<Character> Create(Character body)
        {
            _db.Characters.Add(body);
            await _db.SaveChangesAsync();
            return body;
        }

        public async Task<Character> Update(Character body)
        {
            _db.Characters.Update(body);
            await _db.SaveChangesAsync();
            return body;
        }

        public async Task<bool> Delete(Character body)
        {
            _db.Characters.Remove(body);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

