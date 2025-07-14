using System.ComponentModel.DataAnnotations;

namespace FiapAcademyAdmin.Application.DTOs.Command.Turma
{
    public class RemoverAlunoTurmaCommandDTO
    {
        [Required(ErrorMessage = "ID da turma é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da turma deve ser maior que zero")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "ID do aluno é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do aluno deve ser maior que zero")]
        public int AlunoId { get; set; }
    }
} 