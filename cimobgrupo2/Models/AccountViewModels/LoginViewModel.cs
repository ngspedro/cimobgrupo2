using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    /// <summary>Viewmodel para login</summary>
    /// <remarks>Possui as propriedades necessárias para tal (Username e password)</remarks>
    public class LoginViewModel
    {
        /// <summary>Propriedade correspondente ao username</summary>
        [Required(ErrorMessage = "Username obrigatório.")]
        public string Username { get; set; }

        /// <summary>Propriedade correspondente à password</summary>
        [Required(ErrorMessage = "Password obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
