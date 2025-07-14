namespace FiapAcademyAdmin.Application.DTOs.Query.Turma
{
    public sealed class TurmaListQueryDTO
    {
        public List<TurmaQueryDTO> Turmas { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
} 