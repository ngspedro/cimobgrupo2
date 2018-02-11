using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar um curso do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para um curso (id e nome)</remarks> 
    public class Curso
    {
        /// <summary>Propriedade correspondente ao id do curso</summary>
        public int CursoId { get; set; }

        /// <summary>Propriedade correspondente ao nome do curso</summary>
        [Required(ErrorMessage = "Nome obrigatório.")]
        public String Nome { get; set; }

        /// <summary>Propriedade virtual correspondente à lista de escolas parceiras associadas ao curso</summary>
        public virtual ICollection<EscolaParceiraCurso> EscolasParceiras { get; } = new List<EscolaParceiraCurso>();
        public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();

        /// <summary>Construtor sem parametros</summary>
        public Curso()
        {

        }
    }
}
