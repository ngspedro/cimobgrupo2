using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    /// <summary>Viewmodel para eliminação de conta</summary>
    /// <remarks>Possui as propriedades necessárias para tal (passwor atual)</remarks>
    public class DeleteAccountViewModel
    {
        /// <summary>Propriedade correspondente à password atual</summary>
        [Required(ErrorMessage = "Tem que introduzir a sua password atual para apagar a conta!")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Atual:")]
        public String Password { get; set; }
    }
}
