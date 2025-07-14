using System.ComponentModel.DataAnnotations;

namespace FiapAcademyAdmin.Application.DTOs.Command.Turma
{
    public class AdicionarAlunoTurmaCommandDTO
    {
        public int TurmaId { get; set; }

        public int AlunoId { get; set; }
    }
} 