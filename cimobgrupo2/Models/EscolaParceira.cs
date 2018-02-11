using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar uma escola parceira do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para uma escola (nome, pais, etc.)</remarks> 
    public class EscolaParceira
    {
        /// <summary>Propriedade correspondente ao id da escola parceira</summary>
        public int EscolaParceiraId { get; set; }

        /// <summary>Propriedade correspondente ao nome da escola parceira</summary>
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Nome { get; set; }

        /// <summary>Propriedade correspondente ao país da escola parceira</summary>
        [Required(ErrorMessage = "País obrigatório.")]
        [Display(Name="País")]
        public String Pais { get; set; }

        /// <summary>Propriedade correspondente à localidade da escola parceira</summary>
        [Required(ErrorMessage = "Localidade obrigatória.")]
        public String Localidade { get; set; }

        /// <summary>Propriedade virtual correspondente à lista de programas associados à escola</summary>
        public virtual ICollection<ProgramaEscolaParceira> Programas { get; } = new List<ProgramaEscolaParceira>();

        /// <summary>Propriedade virtual correspondente à lista de cursos associados à escola</summary>
        public virtual ICollection<EscolaParceiraCurso> Cursos { get; } = new List<EscolaParceiraCurso>();
        public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();

        /// <summary>Construtor sem parametros</summary>
        public EscolaParceira()
        {

        }
    }
}
