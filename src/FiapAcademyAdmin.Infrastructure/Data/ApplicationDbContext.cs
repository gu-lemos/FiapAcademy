using Microsoft.EntityFrameworkCore;
using FiapAcademyAdmin.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace FiapAcademyAdmin.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AlunoEntity> Alunos { get; set; }
        public DbSet<TurmaEntity> Turmas { get; set; }
        public DbSet<MatriculaEntity> Matriculas { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlunoEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DataNascimento).IsRequired();
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.DataCadastro).IsRequired();
                entity.HasIndex(e => e.Cpf).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Nome);
                
                entity.HasMany(e => e.Matriculas)
                      .WithOne(m => m.Aluno)
                      .HasForeignKey(m => m.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TurmaEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.DataCadastro).IsRequired();
                entity.HasIndex(e => e.Nome);

                entity.HasMany(e => e.Matriculas)
                      .WithOne(m => m.Turma)
                      .HasForeignKey(m => m.TurmaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MatriculaEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AlunoId).IsRequired();
                entity.Property(e => e.TurmaId).IsRequired();
                entity.Property(e => e.DataMatricula).IsRequired();
                
                entity.HasIndex(e => new { e.AlunoId, e.TurmaId }).IsUnique();
                
                entity.HasOne(e => e.Aluno)
                      .WithMany(a => a.Matriculas)
                      .HasForeignKey(e => e.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Turma)
                      .WithMany(t => t.Matriculas)
                      .HasForeignKey(e => e.TurmaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.DataCadastro).IsRequired();
                entity.Property(e => e.Ativo).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Nome);
            });
        }

        public void SeedData()
        {
            if (!Turmas.Any())
            {
                var turmas = new List<TurmaEntity>
                {
                    new("Desenvolvimento Web", "Curso focado em desenvolvimento de aplicações web modernas"),
                    new("Mobile Development", "Desenvolvimento de aplicativos móveis para iOS e Android"),
                    new("Data Science", "Análise de dados e machine learning"),
                    new("Cloud Computing", "Infraestrutura e serviços em nuvem"),
                    new("Cybersecurity", "Segurança da informação e proteção de dados")
                };

                Turmas.AddRange(turmas);
                SaveChanges();
            }

            if (!Alunos.Any())
            {
                var alunos = new List<AlunoEntity>
                {
                    new("João Silva Santos", new DateTime(1995, 3, 15), "12345678901", "joao.silva@email.com", HashPassword("Senha123!")),
                    new("Maria Oliveira Costa", new DateTime(1998, 7, 22), "98765432100", "maria.oliveira@gmail.com", HashPassword("Senha123!")),
                    new("Pedro Almeida Lima", new DateTime(1993, 11, 8), "45678912300", "pedro.almeida@hotmail.com", HashPassword("Senha123!")),
                    new("Ana Beatriz Ferreira", new DateTime(2000, 1, 30), "78912345600", "ana.ferreira@yahoo.com", HashPassword("Senha123!")),
                    new("Carlos Eduardo Rodrigues", new DateTime(1997, 5, 12), "32165498700", "carlos.rodrigues@outlook.com", HashPassword("Senha123!")),
                    new("Fernanda Souza Martins", new DateTime(1994, 9, 18), "14725836900", "fernanda.martins@email.com", HashPassword("Senha123!")),
                    new("Lucas Mendes Pereira", new DateTime(1999, 12, 3), "96385274100", "lucas.mendes@gmail.com", HashPassword("Senha123!")),
                    new("Juliana Costa Silva", new DateTime(1996, 4, 25), "85296374100", "juliana.costa@hotmail.com", HashPassword("Senha123!")),
                    new("Roberto Santos Almeida", new DateTime(1992, 8, 14), "74185296300", "roberto.santos@yahoo.com", HashPassword("Senha123!")),
                    new("Patrícia Lima Oliveira", new DateTime(2001, 2, 7), "36925814700", "patricia.lima@outlook.com", HashPassword("Senha123!")),
                    new("Bruno Henrique Souza", new DateTime(1990, 6, 10), "11122233344", "bruno.souza@email.com", HashPassword("Senha123!")),
                    new("Camila Ramos Dias", new DateTime(1992, 12, 21), "55566677788", "camila.dias@gmail.com", HashPassword("Senha123!")),
                    new("Eduardo Faria Lopes", new DateTime(1998, 3, 5), "99988877766", "eduardo.lopes@hotmail.com", HashPassword("Senha123!")),
                    new("Gabriela Nunes Prado", new DateTime(1997, 8, 19), "22233344455", "gabriela.prado@yahoo.com", HashPassword("Senha123!")),
                    new("Henrique Silva Barros", new DateTime(1995, 11, 2), "33344455566", "henrique.barros@outlook.com", HashPassword("Senha123!")),
                    new("Isabela Castro Pinto", new DateTime(2002, 4, 14), "44455566677", "isabela.pinto@email.com", HashPassword("Senha123!")),
                    new("Joana D'Arc Lima", new DateTime(1993, 7, 23), "55544433322", "joana.lima@gmail.com", HashPassword("Senha123!")),
                    new("Marcelo Tavares Rocha", new DateTime(1991, 9, 30), "66677788899", "marcelo.rocha@hotmail.com", HashPassword("Senha123!")),
                    new("Natália Borges Alves", new DateTime(1996, 2, 17), "77788899900", "natalia.alves@yahoo.com", HashPassword("Senha123!")),
                    new("Otávio Ramos Pires", new DateTime(1994, 10, 8), "88899900011", "otavio.pires@outlook.com", HashPassword("Senha123!")),
                    new("Paula Regina Duarte", new DateTime(1999, 5, 27), "99900011122", "paula.duarte@email.com", HashPassword("Senha123!"))
                };

                Alunos.AddRange(alunos);
                SaveChanges();
            }

            if (!Matriculas.Any())
            {
                var matriculas = new List<MatriculaEntity>
                {
                    new(1, 1),
                    new(2, 1),
                    new(3, 2),
                    new(4, 2),
                    new(5, 3),
                    new(6, 3),
                    new(7, 4),
                    new(8, 4),
                    new(9, 5),
                    new(10, 5),
                    new(11, 1),
                    new(12, 2),
                    new(13, 3),
                    new(14, 4),
                    new(15, 5)
                };

                Matriculas.AddRange(matriculas);
                SaveChanges();
            }

            if (!Usuarios.Any())
            {
                var usuarios = new List<UsuarioEntity>
                {
                    new("Administrador", "admin@fiapacademy.com", HashPassword("123"))
                };

                Usuarios.AddRange(usuarios);
                SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 