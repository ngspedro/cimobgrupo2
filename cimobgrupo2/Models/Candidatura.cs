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
        
        [Display(Name = "Programas")]
        public int ProgramaId { get; set; }
        [Display(Name = "Escolas Parceiras")]
        public int EscolaParceiraId { get; set; }
        [Display(Name = "Cursos")]
        public int CursoId { get; set; }


        public virtual EscolaParceira EscolaParceira { get; set; }
        public virtual Programa Programa { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Candidatura()
        {

        }
    }
}
