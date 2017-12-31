using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Curso
    {
        public int CursoId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        public String Nome { get; set; }

        public virtual ICollection<EscolaParceiraCurso> EscolasParceiras { get; } = new List<EscolaParceiraCurso>();

        public Curso()
        {

        }
    }
}
