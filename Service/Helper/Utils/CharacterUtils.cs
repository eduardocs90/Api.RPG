using Service.Dto.CharacterDto;
using Service.Dto.UserDto;

namespace Service.Helper.Utils
{
    public static class CharacterUtils
    {
        public static bool VerifyIdCharacter(int id)
        {
            return id <= 0 ? true : false;
        }

        public static bool VerifyStrengthCharacter(int strg)
        {
            if (strg <= 0) return false;
            if (strg >= 31) return false;
            return true;
        }

        public static bool VerifyAgilityCharacter(int agil)
        {
            if (agil <= 0) return false;
            if (agil >= 21) return false;
            return true;
        }

        public static bool VerifyDexterityCharacter(int agil)
        {
            if (agil <= 0) return false;
            if (agil >= 21) return false;
            return true;
        }

        public static bool VerifyLuckCharacter(int agil)
        {
            if (agil <= 0) return false;
            if (agil >= 7) return false;
            return true;
        }

        public static bool IsValidCharacterUpdateDTO(CharacterUpdateDTO user)
        {
            if (user == null)
                return false;

            if (
                user.Name == null ||
                user.HP == null ||
                user.Luck == null||
                user.Strength == null ||
                user.Race == null||
                user.Dexterity == null ||
                user.Agility == null)
            {
                return false;
            }

            return true;
        }

    }
}
