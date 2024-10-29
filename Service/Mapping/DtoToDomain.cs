using AutoMapper;
using Domain.Entities;
using Service.Dto.CharacterDto;
using Service.Dto.EquipmentDto;
using Service.Dto.UserDto;

namespace Service.Mapping
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain() {
            CreateMap<UserDTO, User>();
            CreateMap<ResponseUserDTO, User>();
            CreateMap<CharacterDTO, Character>();
            CreateMap<EquipmentDTO, Equipment>();
            CreateMap<EquipmentUpdateDTO, Equipment>();


        }
    }
}
