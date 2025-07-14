namespace FiapAcademyAdmin.Application.DTOs.Query.Auth
{
    public sealed class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
    }
} 