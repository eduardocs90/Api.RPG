using Service.Dto.EquipmentDto;
using System.Reflection;

namespace Service.Helper.Utils
{
    public class EquipmentUtils
    {

        public class VerifyNullAndEmpty
        {
            public string? Message { get; set; }
            public bool Validation { get; set; }
        }

        public static VerifyNullAndEmpty ValidationEquipmentCreate(EquipmentDTO body)
        {
            PropertyInfo[] properties = typeof(EquipmentDTO).GetProperties();

            int i = 0;
            while (i < properties.Length)
            {
                var property = properties[i];

                if (property.Name != "Id" && property.Name != "Image")
                {

                    if (property.PropertyType == typeof(string))
                    {
                        string value = (string)property.GetValue(body);

                        if (string.IsNullOrWhiteSpace(value))
                        {
                            return new VerifyNullAndEmpty()
                            {
                                Message = $"O campo {property.Name} deve ser Preenchido",
                                Validation = false
                            };
                        }
                    }

                    else if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                    {
                        var nullableValue = property.GetValue(body);

                        if (nullableValue != null)
                        {
                            return new VerifyNullAndEmpty()
                            {
                                Message = $"O campo {property.Name} deve ser preenchido",
                                Validation = false
                            };
                        }
                    }

                }
                i++;
            }
            return new VerifyNullAndEmpty() { Message="", Validation = true };

        }
    }
}
