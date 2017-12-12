using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    public class DeleteAccountViewModel
    {
        [Required(ErrorMessage = "Tem que introduzir a sua password atual para apagar a conta!")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Atual:")]
        public String Password { get; set; }
    }
}
