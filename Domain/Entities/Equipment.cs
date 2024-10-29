using Domain.Enum_s;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public EquipmentClassEnum Class { get; set; }
        public int? Damage { get; set; }
        public int? Resistance { get; set; }
        public string? Effect { get; set; }
        public byte[]? Image { get; set; }

        // Propriedade que representa a chave estrangeira
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character? Character { get; set; }
    }
}
