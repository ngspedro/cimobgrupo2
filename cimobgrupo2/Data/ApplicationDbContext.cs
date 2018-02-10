using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cimobgrupo2.Models;

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

            builder.Entity<ProgramaEscolaParceira>()
                .HasKey(t => new { t.ProgramaId, t.EscolaParceiraId });

            builder.Entity<EscolaParceiraCurso>()
                .HasKey(t => new { t.EscolaParceiraId, t.CursoId });

            builder.Entity<Candidatura>()
                .HasMany<Entrevista>(c => c.Entrevistas)
                .WithOne(e => e.Candidatura)
                .HasForeignKey(e => e.CandidaturaId);
        }
    }
}
