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
        public DbSet<Ajuda> Ajudas { get; set; }
        public DbSet<Erro> Erros { get; set; }

        public DbSet<Programa> Programas { get; set; }
        public DbSet<EscolaParceira> EscolasParceiras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProgramaEscolaParceira>()
                .HasKey(t => new { t.ProgramaId, t.EscolaParceiraId });

            builder.Entity<EscolaParceiraCurso>()
                .HasKey(t => new { t.EscolaParceiraId, t.CursoId });
        }
    }
}
