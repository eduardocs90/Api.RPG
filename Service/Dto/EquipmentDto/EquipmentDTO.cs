using Domain.Enum_s;

namespace Service.Dto.EquipmentDto
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EquipmentClassEnum? Class { get; set; }
        public int? Damage { get; set; }
        public int? Resistance { get; set; }
        public string? Effect { get; set; }
        public byte[]? Image { get; set; }
        public int CharacterID { get; set; }
    }
}
