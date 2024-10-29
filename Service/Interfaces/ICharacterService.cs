using Service.Dto.CharacterDto;
using Service.Helper;

namespace Service.Interfaces
{
    public interface ICharacterService
    {
        Task<ExceptionManager<ICollection<CharacterDTO>>> FindAll();
        Task<ExceptionManager<CharacterDTO>> FindById(int id);
        Task<ExceptionManager<CharacterDTO>> Create(CharacterDTO body);
        Task<ExceptionManager<CharacterDTO>> Update(CharacterUpdateDTO body);
        Task<ExceptionManager<bool>> Delete(int id);

    }
}
