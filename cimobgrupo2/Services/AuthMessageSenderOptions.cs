using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }

        public AuthMessageSenderOptions()
        {
            SendGridUser = "ngspedro";
            SendGridKey = "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";
        }
    }
}
