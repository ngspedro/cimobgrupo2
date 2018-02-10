using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar uma ajuda do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para uma ajuda (texto da ajuda, elemento, etc.)</remarks> 
    public class Ajuda
    {
        /// <summary>Propriedade correspondente ao id da ajuda</summary>
        public int AjudaId { get; set; }

        /// <summary>Propriedade correspondente ao controller onde a ajuda será utilizada</summary>
        public String Controller { get; set; }

        /// <summary>Propriedade correspondente à action onde a ajuda será utilizada</summary>
        public String Action { get; set; }

        /// <summary>Propriedade correspondente ao elemento onde a ajuda será mostrada</summary>
        public String Elemento { get; set; }

        /// <summary>Propriedade correspondente ao texto correspondente à ajuda</summary>
        public String Texto { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public Ajuda()
        {

        }

        /// <summary>Construtor com parametros para criaçao de ajuda</summary>
        /// <param name="Controller">Controller onde a ajuda será utilizada</param>
        /// <param name="Action">Action onde a ajuda será utilizada</param>
        /// <param name="Elemento">Elemento onde a ajuda será mostrada</param>
        /// <param name="Texto">Texto correspondente à ajuda</param>
        public Ajuda(String Controller, String Action, String Elemento, String Texto)
        {
            this.Controller = Controller;
            this.Action = Action;
            this.Elemento = Elemento;
            this.Texto = Texto;
        }
    }
}
