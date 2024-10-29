using Service.Dto.UserDto;
using System.Text.RegularExpressions;

namespace Service.Helper.Utils
{
    public static class UserUtils
    {

        public static bool VerifyIdUser(int id)
        {
            return id <= 0 ? true : false;
        }

        public static bool VerifyEmail(string email)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            return !string.IsNullOrEmpty(email) && emailRegex.IsMatch(email);
           
        }

        public static bool VerifyPassword(string password)
        {
            // Expressão regular para uma senha forte de 8 a 20 caracter, Com ao  menos uma Letra maiúscula, uma minuscula, caracter especial e um numro

            Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$");

          // Verifica se a senha atende aos requisitos
            return !string.IsNullOrEmpty(password) && passwordRegex.IsMatch(password);
        }

        public static bool VerifyDocument(string document)
        {
            if (string.IsNullOrEmpty(document)) return false;

            string cpf = new string(document.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;


            int[] firstDigitMultipliers = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondDigitMultipliers = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfWithoutVerificationDigits = cpf.Substring(0, 9);
            int firstVerificationDigit = CalculateVerificationDigit(cpfWithoutVerificationDigits, firstDigitMultipliers);
            int secondVerificationDigit = CalculateVerificationDigit(cpfWithoutVerificationDigits + firstVerificationDigit, secondDigitMultipliers);


            if (cpf.EndsWith($"{firstVerificationDigit}{secondVerificationDigit}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int CalculateVerificationDigit(string partialCpf, int[] multipliers)
        {
            int sum = 0;
            for (int i = 0; i < partialCpf.Length; i++)
            {
                sum += int.Parse(partialCpf[i].ToString()) * multipliers[i];
            }

            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }


        public static bool IsValidUserUpdateDTO(UserUpdateDTO user)
        {
            if (user == null)
                return false;

            if (
                user.Name == null ||
                user.Email == null ||
                user.Document == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidUserDTO(UserDTO user)
        {
            if (user == null)
                return false;

            if (
                user.Name == null ||
                user.Email == null ||
                user.Password == null ||
                user.Document == null)
            {
                return false;
            }

            return true;
        }

    }
}
