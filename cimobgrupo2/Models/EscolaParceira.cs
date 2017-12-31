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
        public string Nome { get; set; }

        [Display(Name="País")]
        public String Pais { get; set; }
        public String Localidade { get; set; }

        public virtual ICollection<ProgramaEscolaParceira> Programas { get; } = new List<ProgramaEscolaParceira>();
        public virtual ICollection<EscolaParceiraCurso> Cursos { get; } = new List<EscolaParceiraCurso>();
        public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();
        public EscolaParceira()
        {

        }
    }
}
