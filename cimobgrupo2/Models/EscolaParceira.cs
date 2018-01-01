using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class EscolaParceira
    {
        public int EscolaParceiraId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "País obrigatório.")]
        [Display(Name="País")]
        public String Pais { get; set; }

        [Required(ErrorMessage = "Localidade obrigatória.")]
        public String Localidade { get; set; }

        public virtual ICollection<ProgramaEscolaParceira> Programas { get; } = new List<ProgramaEscolaParceira>();
        public virtual ICollection<EscolaParceiraCurso> Cursos { get; } = new List<EscolaParceiraCurso>();

        public EscolaParceira()
        {

        }
    }
}
