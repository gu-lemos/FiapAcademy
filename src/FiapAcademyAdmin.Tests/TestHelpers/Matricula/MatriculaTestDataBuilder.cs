using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Tests.TestHelpers.Matricula
{
    public static class MatriculaTestDataBuilder
    {
        public static MatriculaEntity MatriculaEntity(int alunoId = 1, int turmaId = 1)
        {
            return new MatriculaEntity(alunoId, turmaId);
        }

        public static List<MatriculaEntity> MatriculaEntities(int count = 3, int alunoId = 1, int turmaIdStart = 1)
        {
            var list = new List<MatriculaEntity>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new MatriculaEntity(alunoId, turmaIdStart + i));
            }
            return list;
        }
    }
} 