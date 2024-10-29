using Domain.Entities;
using Service.Dto.TokenDto;
using Service.Dto.UserDto;
using Service.Helper;

namespace Service.Interfaces
{
    public  interface IUserService
    {
        Task<ExceptionManager<ResponseLoginDTO>> GenerateToken(LoginDTO loginDTO);
        Task<ExceptionManager<ICollection<ResponseUserDTO>>> FindAll();
        Task<ExceptionManager<ResponseUserDTO>> FindByID(int id);
        Task<ExceptionManager<ResponseUserDTO>>Create(UserDTO body);
        Task<ExceptionManager<ResponseUserDTO>>Update(UserUpdateDTO body);
        Task<ExceptionManager<bool>> Delete(int id);

    }
}
