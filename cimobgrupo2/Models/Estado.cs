using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    
    public class Estado
    {

        [Key]
        public int EstadoId { get; set; }
        public string Descricao { get; set; }
        public String  Nome { get; set; }
        //public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();
    }
}
