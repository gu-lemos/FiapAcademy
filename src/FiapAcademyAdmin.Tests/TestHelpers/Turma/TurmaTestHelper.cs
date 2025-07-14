using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using Moq;

namespace FiapAcademyAdmin.Tests.TestHelpers.Turma
{
    public static class TurmaTestHelper
    {
        public static void SetupTurmaServiceForGetAll(this Mock<ITurmaService> mock, IEnumerable<TurmaEntity> turmas, int totalCount)
        {
            mock.SetupGetAllAsync(turmas);
            mock.SetupGetTotalCountAsync(totalCount);
        }

        public static void SetupTurmaServiceForGetById(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.SetupGetByIdAsync(turma);
        }

        public static void SetupTurmaServiceForCreate(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.SetupCreateAsync(turma);
        }

        public static void SetupTurmaServiceForUpdate(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.SetupGetByIdAsync(turma);
            mock.SetupUpdateAsync(turma);
        }

        public static void SetupTurmaServiceForDelete(this Mock<ITurmaService> mock)
        {
            mock.SetupDeleteAsync();
        }

        public static void SetupMapperForEntity(this Mock<AutoMapper.IMapper> mock, TurmaEntity entity)
        {
            mock.SetupMapToEntity(entity);
        }

        public static void SetupMapperForQueryDTO(this Mock<AutoMapper.IMapper> mock, TurmaQueryDTO dto)
        {
            mock.SetupMapToQueryDTO(dto);
        }

        public static void SetupMapperForQueryDTOs(this Mock<AutoMapper.IMapper> mock, IEnumerable<TurmaQueryDTO> dtos)
        {
            mock.SetupMapToQueryDTOs(dtos);
        }
    }
} 