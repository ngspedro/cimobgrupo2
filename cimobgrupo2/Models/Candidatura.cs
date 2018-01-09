using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public enum TipoEstado
    {
        APROVADA, ADIADA, POR_AVALIAR, NAO_APROVADA
    }


    public class Candidatura
    {
        public int CandidaturaId { get; set; }
        [Display(Name = "Nome do Candidato")]
        public string Nome { get; set; }
        public int ProgramaId { get; set; }
        public Programa Programa { get; set; }
        [Display(Name = "Escola Destino")]
        public int EscolaParceiraId { get; set; }
        public EscolaParceira EscolaParceira { get; set; }
        [Display(Name ="Curso Escolhido")]
        [ForeignKey("CursoId")]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        //[ForeignKey("EstadoId")]
        //public int EstadoId { get; set; }
        public TipoEstado Estado { get; set; }
       
            
        public ICollection<Entrevista> Entrevistas { get; set; }= new List<Entrevista>();
        public Candidatura()
        {
        }
        public Candidatura(string nome, Programa programa, EscolaParceira escolaParceira, Curso curso)
        {
            this.Nome = nome;
            this.Programa = programa;
            this.EscolaParceira = escolaParceira;
            this.Curso = curso;
        }

    }
}
