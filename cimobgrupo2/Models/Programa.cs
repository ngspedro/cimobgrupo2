using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar um programa do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para um programa de mobilidade (id do programa, descrição, duracao do programa, etc.)</remarks> 
    public class Programa
    {
        /// <summary>Propriedade correspondente ao id do programa de mobilidade</summary>
        public int ProgramaId { get; set; }

        /// <summary>Propriedade correspondente ao nome do programa de mobilidade</summary>
        public String Nome { get; set; }

        /// <summary>Propriedade correspondente à descrição do programa de mobilidade</summary>
        [Display(Name="Descrição")]
        public String Descricao { get; set; }

        /// <summary>Propriedade correspondente à duração do programa de mobilidade</summary>
        [Display(Name ="Duração")]
        public int Duracao { get; set; }

        /// <summary>Propriedade correspondente ao nome do ficheiro correspondente ao edital do programa</summary>
        public String Edital { get; set; }

        /// <summary>Propriedade virtual correspondente à lista de escolas parceiras associados ao programa</summary>
        public virtual ICollection<ProgramaEscolaParceira> EscolasParceiras { get; } = new List<ProgramaEscolaParceira>();

        /// <summary>Propriedade virtual correspondente à lista de candidaturas feitas ao programa</summary>
        public virtual ICollection<Candidatura> Candidaturas { get;  } = new List<Candidatura>();

        /// <summary>Construtor sem parametros</summary>
        public Programa()
        {

        }
    }
}
