using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class EscolaParceiraCurso
    {
        public int EscolaParceiraId { get; set; }
        public EscolaParceira EscolaParceira { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public EscolaParceiraCurso()
        {

        }
    }
}
