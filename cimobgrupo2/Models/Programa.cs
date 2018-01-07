using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Programa
    {
        public int ProgramaId { get; set; }
        public String Nome { get; set; }

        [Display(Name="Descrição")]
        public String Descricao { get; set; }

        [Display(Name ="Duração")]
        public int Duracao { get; set; }

        public String Edital { get; set; }

        public virtual ICollection<ProgramaEscolaParceira> EscolasParceiras { get; } = new List<ProgramaEscolaParceira>();

        public Programa()
        {

        }
    }
}
