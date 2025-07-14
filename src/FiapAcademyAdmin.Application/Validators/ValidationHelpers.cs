using System.Text.RegularExpressions;

namespace FiapAcademyAdmin.Application.Validators
{
    public static class ValidationHelpers
    {
        public static bool BeValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var numbers = Regex.Replace(cpf, @"[^\d]", "");
            
            if (numbers.Length != 11)
                return false;

            if (numbers.All(c => c == numbers[0]))
                return false;

            return true;
        }

        public static bool BeStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }

        public static bool BeStrongPasswordWhenProvided(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return true;

            return BeStrongPassword(password);
        }
    }
} 