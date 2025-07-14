using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using Moq;

namespace FiapAcademyAdmin.Tests.TestHelpers.Aluno
{
    public static class AlunoMockExtensions
    {
        public static void SetupGetAllAsync(this Mock<IAlunoService> mock, IEnumerable<AlunoEntity> alunos)
        {
            mock.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>()))
                .ReturnsAsync(alunos);
        }

        public static void SetupGetTotalCountAsync(this Mock<IAlunoService> mock, int totalCount)
        {
            mock.Setup(s => s.GetTotalCountAsync(It.IsAny<string?>()))
                .ReturnsAsync(totalCount);
        }

        public static void SetupGetByIdAsync(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(aluno);
        }

        public static void SetupCreateAsync(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.Setup(s => s.CreateAsync(It.IsAny<AlunoEntity>()))
                .ReturnsAsync(aluno);
        }

        public static void SetupUpdateAsync(this Mock<IAlunoService> mock, AlunoEntity aluno)
        {
            mock.Setup(s => s.UpdateAsync(It.IsAny<AlunoEntity>()))
                .ReturnsAsync(aluno);
        }

        public static void SetupDeleteAsync(this Mock<IAlunoService> mock, bool result = true)
        {
            mock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(result);
        }

        public static void SetupMapToEntity(this Mock<AutoMapper.IMapper> mock, AlunoEntity entity)
        {
            mock.Setup(m => m.Map<AlunoEntity>(It.IsAny<object>()))
                .Returns(entity);
        }

        public static void SetupMapToQueryDTO(this Mock<AutoMapper.IMapper> mock, AlunoQueryDTO dto)
        {
            mock.Setup(m => m.Map<AlunoQueryDTO>(It.IsAny<AlunoEntity>()))
                .Returns(dto);
        }

        public static void SetupMapToQueryDTOs(this Mock<AutoMapper.IMapper> mock, IEnumerable<AlunoQueryDTO> dtos)
        {
            mock.Setup(m => m.Map<IEnumerable<AlunoQueryDTO>>(It.IsAny<IEnumerable<AlunoEntity>>()))
                .Returns(dtos);
        }
    }
} 