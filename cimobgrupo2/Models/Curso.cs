using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public String Nome { get; set; }

        public virtual ICollection<EscolaParceiraCurso> EscolasParceiras { get; } = new List<EscolaParceiraCurso>();
        public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();
        public Curso()
        {

        }
    }
}
