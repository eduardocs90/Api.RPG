using Domain.Entities;
using Domain.Enum_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto.UserDto
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Document { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public int? Age { get; set; }
        public MaritalStatusEnum? MaritalStatus { get; set; }
        public GenderEnum? Gender { get; set; }
        public RoleEnum Role { get; set; }
        public Character? Character { get; set; }

    }
}
