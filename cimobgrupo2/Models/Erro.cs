using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar um erro do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para um erro (id, codigo, mensagem)</remarks> 
    public class Erro
    {
        /// <summary>Propriedade correspondente ao id do erro</summary>
        public int ErroId { get; set; }
        public String Codigo { get; set; }
        public String Mensagem { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public Erro()
        {

        }

        /// <summary>Construtor com parametros para criaçao de erro</summary>
        /// <param name="Codigo">Codigo do Erro</param>
        /// <param name="Mensagem">Mensagem do Erro</param>
        public Erro(String Codigo, String Mensagem)
        {
            this.Codigo = Codigo;
            this.Mensagem = Mensagem;
        }
    }
}
