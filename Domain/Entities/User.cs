using Domain.Enum_s;

namespace Domain.Entities
{

    public class User
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

        // Relação com Character (um para um)
        public Character? Character { get; set; }
    }
}


