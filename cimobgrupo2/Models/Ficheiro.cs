using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Ficheiro
    {
        public String FicheiroId { get; set; }
        public String Nome { get; set; }
        public String Caminho { get; set; }

        public virtual ICollection<ProgramaFicheiro> Programas { get; } = new List<ProgramaFicheiro>();

        public Ficheiro()
        {

        }
    }
}
