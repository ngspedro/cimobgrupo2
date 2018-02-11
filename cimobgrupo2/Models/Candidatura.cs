using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar uma candidatura no sistema</summary>
    /// <remarks>Possui as propriedades necessárias para uma candidatura (programa, estado, etc.)</remarks> 
    public class Candidatura
    {
        /// <summary>Propriedade correspondente ao id da candidatura</summary>
        public int CandidaturaId { get; set; }

        /// <summary>Propriedade correspondente ao id do candidato associado à candidatura</summary>
        [ForeignKey("User")]
        public string UserId { get; set; }

        /// <summary>Propriedade correspondente ao id do programa associado à candidatura</summary>
        public int? ProgramaId { get; set; }

        /// <summary>Propriedade correspondente ao id da escola associada à candidatura</summary>
        public int? EscolaParceiraId { get; set; }

        /// <summary>Propriedade correspondente ao id do curso associado à candidatura</summary>
        public int? CursoId { get; set; }

        /// <summary>Propriedade correspondente ao id do estado da candidatura</summary>
        public int EstadoId { get; set; }

        /// <summary>Propriedade correspondente ao motivo de rejeição da candidatura</summary>
        public string Motivo { get; set; }

        /// <summary>Propriedade virtual correspondente ao objeto da escola associada à candidatura</summary>
        [Display(Name = "Escola Parceira")]
        public virtual EscolaParceira EscolaParceira { get; set; }

        /// <summary>Propriedade virtual correspondente ao objeto do programa associado à candidatura</summary>
        [Display(Name = "Programa")]
        public virtual Programa Programa { get; set; }

        /// <summary>Propriedade virtual correspondente ao objeto do curso associada à candidatura</summary>
        [Display(Name = "Curso")]
        public virtual Curso Curso { get; set; }

        /// <summary>Propriedade virtual correspondente ao objeto do candidato</summary>
        [Display(Name = "Candidato")]
        public virtual ApplicationUser User { get; set; }

        /// <summary>Propriedade virtual correspondente ao objeto do estado da candidatura</summary>
        [Display(Name = "Estado")]
        public virtual Estado Estado { get; set; }

        /// <summary>Propriedade virtual correspondente à lista de entrevistas associadas à candidatura</summary>
        public virtual ICollection<Entrevista> Entrevistas { get; set; } = new List<Entrevista>();

        /// <summary>Construtor sem parametros</summary>
        public Candidatura()
        {

        }
    }
}
