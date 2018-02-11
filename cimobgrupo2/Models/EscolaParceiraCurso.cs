using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar uma associação entre Escola Parceira e Curso</summary>
    /// <remarks>Possui as propriedades necessárias para tal associação (id da escola, id do curso, etc)</remarks> 
    public class EscolaParceiraCurso
    {
        /// <summary>Propriedade correspondente ao id da escola parceira a associar</summary>
        public int EscolaParceiraId { get; set; }

        /// <summary>Propriedade correspondente ao objeto da escola parceira a associar</summary>
        public EscolaParceira EscolaParceira { get; set; }

        /// <summary>Propriedade correspondente ao id do curso a associar</summary>
        public int CursoId { get; set; }

        /// <summary>Propriedade correspondente ao objeto do curso a associar</summary>
        public Curso Curso { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public EscolaParceiraCurso()
        {

        }
    }
}
