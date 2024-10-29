using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICharacterRepository
    {
        Task<ICollection<Character>> FindAll();
        Task<Character> FindById(int id);
        Task<Character> FindByName(string name);
        Task<Character> Create(Character body);
        Task<Character> Update(Character body);
        Task<bool> Delete(Character body);
        Task<Character> FindByIdForUpdate(int id);
    }
}
