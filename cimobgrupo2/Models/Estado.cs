using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Candidatura> Candidaturas { get; } = new List<Candidatura>();
        public virtual ICollection<Entrevista> Entrevistas { get; } = new List<Entrevista>();

        public Estado()
        {

        }
    }
}
