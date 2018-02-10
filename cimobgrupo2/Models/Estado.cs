using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar um estado de candidatura</summary>
    /// <remarks>Possui as propriedades necessárias para tal (id do estado e nome)</remarks> 
    public class Estado
    {
        /// <summary>Propriedade correspondente ao id do estado</summary>
        public int EstadoId { get; set; }

        /// <summary>Propriedade correspondente ao nome do estado</summary>
        public string Nome { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public Estado()
        {

        }
    }
}
