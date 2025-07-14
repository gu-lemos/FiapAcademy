using AutoMapper;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Matricula;

namespace FiapAcademyAdmin.Application.Mappings
{
    public class AlunoMappingProfile : Profile
    {
        public AlunoMappingProfile()
        {
            CreateMap<AlunoEntity, AlunoQueryDTO>()
                .ForMember(dest => dest.Turmas, opt => opt.MapFrom(src =>
                    src.Matriculas.Where(m => m.Turma != null).Select(m => m.Turma.Nome).ToList()));

            CreateMap<CreateAlunoCommandDTO, AlunoEntity>()
                .ConstructUsing((src, context) => new AlunoEntity(
                    src.Nome,
                    src.DataNascimento,
                    src.Cpf,
                    src.Email,
                    src.Senha ?? string.Empty
                ));

            CreateMap<UpdateAlunoCommandDTO, AlunoEntity>()
                .ConstructUsing((src, context) => new AlunoEntity(
                    src.Nome,
                    src.DataNascimento,
                    src.Cpf,
                    src.Email,
                    src.Senha ?? string.Empty
                ));

            CreateMap<MatriculaEntity, MatriculaQueryDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => 
                    src.Aluno != null ? src.Aluno.Nome : string.Empty))
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => 
                    src.Turma != null ? src.Turma.Nome : string.Empty));
        }
    }
} 