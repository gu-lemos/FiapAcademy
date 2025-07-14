using AutoMapper;
using FiapAcademyAdmin.Application.DTOs.Command.Turma;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Mappings
{
    public class TurmaMappingProfile : Profile
    {
        public TurmaMappingProfile()
        {
            CreateMap<TurmaEntity, TurmaQueryDTO>()
                .ForMember(dest => dest.QuantidadeAlunos, opt => opt.MapFrom(src => src.GetQuantidadeAlunosMatriculados()))
                .ForMember(dest => dest.Alunos, opt => opt.MapFrom(src => src.GetAlunosMatriculados()));

            CreateMap<CreateTurmaCommandDTO, TurmaEntity>()
                .ConstructUsing((src, context) => new TurmaEntity(src.Nome, src.Descricao));

            CreateMap<UpdateTurmaCommandDTO, TurmaEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.Matriculas, opt => opt.Ignore());
        }
    }
} 