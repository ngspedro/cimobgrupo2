using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class ProgramaEscolaParceira
    {
        public int ProgramaId { get; set; }
        public Programa Programa { get; set; }

        public int EscolaParceiraId { get; set; }
        public EscolaParceira EscolaParceira { get; set; }

        public int NumeroVagas { get; set; }

        public ProgramaEscolaParceira()
        {

        }
    }
}
