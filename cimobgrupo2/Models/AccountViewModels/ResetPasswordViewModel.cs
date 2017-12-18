﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Nova Password obrigatória.")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Password:")]
        [Compare("Password", ErrorMessage = "As passwords não coincidem!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
