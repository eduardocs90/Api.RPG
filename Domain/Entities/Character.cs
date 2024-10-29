using Domain.Enum_s;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public RaceEnum? Race { get; set; }
        public int? Strength { get; set; }
        public byte[]? Image { get; set; }
        public int? Agility { get; set; }
        public int? Luck { get; set; }
        public int? Dexterity { get; set; }
        public int? HP { get; set; }
        public int? MP { get; set; }

        // Propriedade que representa a chave estrangeira
        public int UserId { get; set; }

        // Relação com Equipment (um para muitos)
        public List<Equipment>? Equipment { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
