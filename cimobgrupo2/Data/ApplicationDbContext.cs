using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cimobgrupo2.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace cimobgrupo2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ajuda> Ajudas { get; set; }
        public DbSet<Erro> Erros { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<EscolaParceira> EscolasParceiras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Entrevista> Entrevistas { get; set; } //depois deve-se atualizar a bd mais logo

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //2º metodo para desenhar 
        public class ToDoContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-cimobgrupo2-4E6CECB9-2848-47FF-A665-295AB2AF00D9;Trusted_Connection=True;MultipleActiveResultSets=true");
                return new ApplicationDbContext(builder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<ProgramaEscolaParceira>()
                .HasKey(t => new { t.ProgramaId, t.EscolaParceiraId });

            builder.Entity<EscolaParceiraCurso>()
                .HasKey(t => new { t.EscolaParceiraId, t.CursoId });
            //foreirgnkeys tha table Candidatura 
            builder.Entity<Programa>().HasMany<Candidatura>(c => c.Candidaturas)
                .WithOne(p => p.Programa).HasForeignKey(p => p.ProgramaId);

            builder.Entity<EscolaParceira>().HasMany<Candidatura>(c => c.Candidaturas).WithOne(esp => esp.EscolaParceira)
                .HasForeignKey(esp => esp.EscolaParceiraId);

            builder.Entity<Curso>().HasMany<Candidatura>(c => c.Candidaturas).WithOne(cur => cur.Curso)
                .HasForeignKey(cur => cur.CursoId);
            //foreirgnkeys tha table Entrevista from programa
            builder.Entity<Estado>().HasMany<Candidatura>(c => c.Candidaturas).WithOne(est => est.Estado)
                .HasForeignKey(est => est.EstadoId)
                .OnDelete(DeleteBehavior.Cascade); ;
            //foreirgnkeys tha table Entrevista from programa
            builder.Entity<Candidatura>().HasMany<Entrevista>(e => e.Entrevistas).WithOne(cand => cand.Candidatura)
               .HasForeignKey(cand => cand.CandidaturaId);
            //foreirgnkeys tha table Entrevista from programa

            //builder.Entity<Programa>().HasMany<Entrevista>(e => e.Entrevistas).WithOne(pro=> pro.Programa)
            //    .HasForeignKey(pro => pro.ProgramaId);
            //foreirgnkeys tha table Entrevista 
            builder.Entity<Estado>().HasMany<Entrevista>(e => e.Entrevistas).WithOne(est => est.Estado)
               .HasForeignKey(est => est.EstadoId)
               .OnDelete(DeleteBehavior.Cascade);
            //foreirgnkeys tha table Entrevista from programa



        }
    }
}
