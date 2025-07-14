using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using Moq;

namespace FiapAcademyAdmin.Tests.TestHelpers.Aluno
{
    public static class AlunoTestHelper
    {
        public static void SetupAlunoServiceForGetAll(this Mock<IAlunoService> mock, IEnumerable<AlunoEntity> alunos, int totalCount)
        {
            mock.SetupGetAllAsync(alunos);
            mock.SetupGetTotalCountAsync(totalCount);
        }

        public static void SetupAlunoServiceForGetById(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.SetupGetByIdAsync(aluno);
        }

        public static void SetupAlunoServiceForCreate(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.SetupCreateAsync(aluno);
        }

        public static void SetupAlunoServiceForUpdate(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.SetupGetByIdAsync(aluno);
            mock.SetupUpdateAsync(aluno);
        }

        public static void SetupAlunoServiceForDelete(this Mock<IAlunoService> mock, bool result = true)
        {
            mock.SetupDeleteAsync(result);
        }

        public static void SetupMapperForEntity(this Mock<AutoMapper.IMapper> mock, AlunoEntity entity)
        {
            mock.SetupMapToEntity(entity);
        }

        public static void SetupMapperForQueryDTO(this Mock<AutoMapper.IMapper> mock, AlunoQueryDTO dto)
        {
            mock.SetupMapToQueryDTO(dto);
        }

        public static void SetupMapperForQueryDTOs(this Mock<AutoMapper.IMapper> mock, IEnumerable<AlunoQueryDTO> dtos)
        {
            mock.SetupMapToQueryDTOs(dtos);
        }
    }
} 