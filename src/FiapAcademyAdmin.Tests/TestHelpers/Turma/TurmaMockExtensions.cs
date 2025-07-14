using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using Moq;

namespace FiapAcademyAdmin.Tests.TestHelpers.Turma
{
    public static class TurmaMockExtensions
    {
        public static void SetupGetAllAsync(this Mock<ITurmaService> mock, IEnumerable<TurmaEntity> turmas)
        {
            mock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>()))
                .ReturnsAsync(turmas);
        }

        public static void SetupGetTotalCountAsync(this Mock<ITurmaService> mock, int totalCount)
        {
            mock.Setup(s => s.GetTotalCountAsync(It.IsAny<string?>()))
                .ReturnsAsync(totalCount);
        }

        public static void SetupGetByIdAsync(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(turma);
        }

        public static void SetupCreateAsync(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.Setup(s => s.CreateAsync(It.IsAny<TurmaEntity>()))
                .ReturnsAsync(turma);
        }

        public static void SetupUpdateAsync(this Mock<ITurmaService> mock, TurmaEntity turma)
        {
            mock.Setup(s => s.UpdateAsync(It.IsAny<TurmaEntity>()))
                .ReturnsAsync(turma);
        }

        public static void SetupDeleteAsync(this Mock<ITurmaService> mock)
        {
            mock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);
        }

        public static void SetupMapToEntity(this Mock<AutoMapper.IMapper> mock, TurmaEntity entity)
        {
            mock.Setup(m => m.Map<TurmaEntity>(It.IsAny<object>()))
                .Returns(entity);
        }

        public static void SetupMapToQueryDTO(this Mock<AutoMapper.IMapper> mock, TurmaQueryDTO dto)
        {
            mock.Setup(m => m.Map<TurmaQueryDTO>(It.IsAny<TurmaEntity>()))
                .Returns(dto);
        }

        public static void SetupMapToQueryDTOs(this Mock<AutoMapper.IMapper> mock, IEnumerable<TurmaQueryDTO> dtos)
        {
            mock.Setup(m => m.Map<IEnumerable<TurmaQueryDTO>>(It.IsAny<IEnumerable<TurmaEntity>>()))
                .Returns(dtos);
        }
    }
} 