using Domain.Entities;
using Domain.Enum_s;

namespace Service.Dto.CharacterDto
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public RaceEnum? Race { get; set; }
        public int? Strength { get; set; }
        public int? Agility { get; set; }
        public byte[]? Image { get; set; }
        public int? Luck { get; set; }
        public int? Dexterity { get; set; }
        public int? HP { get; set; }
        public int? MP { get; set; }
        public int UserID { get; set; }

        public List<Equipment>? Equipment { get; set; }
    }
}
