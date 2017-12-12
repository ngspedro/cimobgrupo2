using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password Atual obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Atual:")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nova Password obrigatória")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Password:")]
        [Compare("NewPassword", ErrorMessage = "As passwords não coincidem!")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
