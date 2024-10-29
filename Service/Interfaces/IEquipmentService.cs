using Service.Dto.EquipmentDto;
using Service.Dto.UserDto;
using Service.Helper;

namespace Service.Interfaces
{
    public interface IEquipmentService
    {
        Task<ExceptionManager<ICollection<EquipmentDTO>>> FindAll();
        Task<ExceptionManager<EquipmentDTO>> FindByID(int id);
        Task<ExceptionManager<EquipmentDTO>> Create(EquipmentDTO body);
        Task<ExceptionManager<EquipmentDTO>> Update(EquipmentUpdateDTO body);
        Task<ExceptionManager<bool>> Delete(int id);
    }
}
