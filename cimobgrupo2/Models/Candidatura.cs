using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Candidatura
    {
        public int CandidaturaId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public int ProgramaId { get; set; }
        public int EscolaParceiraId { get; set; }
        public int CursoId { get; set; }
        public int EstadoId { get; set; } 
        public string Motivo { get; set; }
        [Display(Name = "Escola Parceira")]
        public virtual EscolaParceira EscolaParceira { get; set; }
        [Display(Name = "Programa")]
        public virtual Programa Programa { get; set; }
        [Display(Name = "Curso")]
        public virtual Curso Curso { get; set; }
        [Display(Name = "Candidato")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }

        public Candidatura()
        {

        }
    }
}
