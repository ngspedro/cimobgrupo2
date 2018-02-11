using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar uma associação entre Programa e Escola Parceira</summary>
    /// <remarks>Possui as propriedades necessárias para tal associação (id do programa, id da escola, etc)</remarks> 
    public class ProgramaEscolaParceira
    {
        /// <summary>Propriedade correspondente ao id do programa</summary>
        public int ProgramaId { get; set; }

        /// <summary>Propriedade correspondente ao objeto do programa</summary>
        public Programa Programa { get; set; }

        /// <summary>Propriedade correspondente ao id da escola parceira</summary>
        public int EscolaParceiraId { get; set; }

        /// <summary>Propriedade correspondente ao objeto da escola parceira</summary>
        public EscolaParceira EscolaParceira { get; set; }

        /// <summary>Propriedade correspondente ao número de vagas</summary>
        public int NumeroVagas { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public ProgramaEscolaParceira()
        {

        }
    }
}
