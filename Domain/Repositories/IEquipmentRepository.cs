using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEquipmentRepository
    {
        Task<ICollection<Equipment>> FindAll();
        Task<Equipment> FindById(int id);
        Task<Equipment> Create(Equipment equipment);
        Task<Equipment> Update(Equipment equipment);
        Task<bool> Delete(Equipment equipment);

    }
}
