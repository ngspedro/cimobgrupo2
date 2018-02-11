using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace cimobgrupo2.Models
{
    /// <summary>Classe para representar um utilizador do sistema</summary>
    /// <remarks>Acrescenta propriedades uteis (neste caso) à classe IdentityUser</remarks> 
    public class ApplicationUser : IdentityUser
    {
        /// <summary>Propriedade correspondente ao nome do utilizador</summary>
        public String Nome { get; set; }

        /// <summary>Propriedade correspondente à data de nascimento do utilizador</summary>
        public String DataNascimento { get; set; }

        /// <summary>Propriedade correspondente ao contacto do utilizador</summary>
        public String Contato { get; set; }

        /// <summary>Propriedade correspondente à password hashed para acesso no wpf</summary>
        public String PasswordHashAux { get; set; }
    }
}
