using AutoMapper;
using Domain.Entities;
using Service.Dto.CharacterDto;
using Service.Dto.EquipmentDto;
using Service.Dto.UserDto;

namespace Service.Mapping
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<Character, CharacterDTO>();
            CreateMap<Equipment, EquipmentDTO>();
        }

    }
}
