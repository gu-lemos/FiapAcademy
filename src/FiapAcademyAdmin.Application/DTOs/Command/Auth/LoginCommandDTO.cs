namespace FiapAcademyAdmin.Application.DTOs.Command.Auth
{
    public sealed class LoginCommandDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
} 