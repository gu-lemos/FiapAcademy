namespace FiapAcademyAdmin.Application.DTOs.Query.Aluno
{
    public sealed class AlunoListQueryDTO
    {
        public IEnumerable<AlunoQueryDTO> Alunos { get; init; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
} 