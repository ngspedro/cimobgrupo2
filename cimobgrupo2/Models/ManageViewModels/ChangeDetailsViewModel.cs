using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    public class ChangeDetailsViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        [Display(Name = "Nome:")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento obrigatória.")]
        [Display(Name = "Data de Nascimento:")]
        public String DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email:")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Contato obrigatório.")]
        [Display(Name = "Contato:")]
        public String Contato { get; set; }
    }
}
