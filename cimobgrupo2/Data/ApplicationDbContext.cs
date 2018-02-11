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
    /// <summary>Classe para representar o dbcontext</summary>
    /// <remarks>Possui as listas para preencher com o conteudo da bd</remarks> 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>Propriedade correspondente à lista de ajudas presentes na bd</summary>
        public DbSet<Ajuda> Ajudas { get; set; }

        /// <summary>Propriedade correspondente à lista de erros presentes na bd</summary>
        public DbSet<Erro> Erros { get; set; }

        /// <summary>Propriedade correspondente à lista de programas presentes na bd</summary>
        public DbSet<Programa> Programas { get; set; }

        /// <summary>Propriedade correspondente à lista de escolas parceiras presentes na bd</summary>
        public DbSet<EscolaParceira> EscolasParceiras { get; set; }

        /// <summary>Propriedade correspondente à lista de cursos presentes na bd</summary>
        public DbSet<Curso> Cursos { get; set; }

        /// <summary>Propriedade correspondente à lista de candidaturas presentes na bd</summary>
        public DbSet<Candidatura> Candidaturas { get; set; }

        /// <summary>Propriedade correspondente à lista de estados presentes na bd</summary>
        public DbSet<Estado> Estados { get; set; }

        /// <summary>Propriedade correspondente à lista de entrevistas presentes na bd</summary>
        public DbSet<Entrevista> Entrevistas { get; set;}

        /// <summary>Construtor com parametros para criaçao do dbcontext</summary>
        /// <param name="options">Opções para o context</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>Redefinição do OnModelCreating</summary>
        /// <param name="builder">ModelBuilder</param>
        /// <remarks>Acrescenta modificações necessárias ao modelo de dados</remarks>
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
            builder.Entity<Candidatura>()
                .HasMany<Entrevista>(c => c.Entrevistas)
                .WithOne(e => e.Candidatura)
                .HasForeignKey(e => e.CandidaturaId);
        }
    }
}
