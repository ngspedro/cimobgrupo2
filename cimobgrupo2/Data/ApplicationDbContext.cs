using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cimobgrupo2.Models;

namespace cimobgrupo2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Ajuda> Ajudas { get; set; }
        public DbSet<Erro> Erros { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<EscolaParceira> EscolasParceiras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Entrevista> Entrevistas { get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //a forma mais simples de adicionar a tabela/ estado a base de dados
            builder.Entity<Estado>().ToTable("Estado");
            //builder.Entity<Candidatura>().ToTable("Candidaturas");
            builder.Entity<ProgramaEscolaParceira>()
                .HasKey(t => new { t.ProgramaId, t.EscolaParceiraId });
            builder.Entity<EscolaParceiraCurso>().HasKey(t => new { t.EscolaParceiraId, t.CursoId });
            //foreirgnkeys tha table Candidatura 
            builder.Entity<Programa>()
                .HasMany<Candidatura>(c => c.Candidaturas)
                .WithOne(p => p.Programa)
                .HasForeignKey(p =>p.ProgramaId);
            builder.Entity<EscolaParceira>().HasMany<Candidatura>(c => c.Candidaturas).WithOne(esp => esp.EscolaParceira)
                .HasForeignKey(esp => esp.EscolaParceiraId);

            builder.Entity<Curso>().HasMany<Candidatura>(c => c.Candidaturas).WithOne(cur => cur.Curso)
                .HasForeignKey(cur => cur.CursoId);
            //foreirgnkeys tha table Entrevista 
            builder.Entity<Candidatura>().HasMany<Entrevista>(e => e.Entrevistas).WithOne(cand => cand.Candidatura)
                .HasForeignKey(cand => cand.CandidaturaId);





        }

    }
}
