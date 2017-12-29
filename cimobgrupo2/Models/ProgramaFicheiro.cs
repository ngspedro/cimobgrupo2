using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class ProgramaFicheiro
    {
        public int ProgramaFicheiroId { get; set; }

        public int ProgramaId { get; set; }
        public Programa Programa { get; set; }

        public int FicheiroId { get; set; }
        public Ficheiro Ficheiro { get; set; }

        public ProgramaFicheiro()
        {

        }
    }
}
