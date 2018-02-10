using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Services
{
    /// <summary>Classe que define as credenciais de acesso à conta SendGrid para envio de emails</summary>
    public class AuthMessageSenderOptions
    {
        /// <summary>Propriedade correspondente ao username da conta</summary>
        public string SendGridUser { get; set; }

        /// <summary>Propriedade correspondente à key de envio de emails da conta</summary>
        public string SendGridKey { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public AuthMessageSenderOptions()
        {
            SendGridUser = "danielcosteira";
            SendGridKey = "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";
        }
    }
}
