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
        public Candidatura()
        {
        }
        [Key]
        public int CandidaturaId { get; set; }
        public int ProgramaId { get; set; }
        public Programa Programa { get; set; }
        public int EscolaParceiraId { get; set; }
        public EscolaParceira EscolaParceira { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
