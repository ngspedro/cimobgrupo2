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
            SendGridUser = "ngspedro";
            SendGridKey = "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";
        }
    }
}
