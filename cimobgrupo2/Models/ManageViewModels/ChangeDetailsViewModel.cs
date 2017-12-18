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
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "Data de Nascimento inválida.")]
        [Display(Name = "Data de Nascimento:")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contacto obrigatório.")]
        [RegularExpression(@"^9[1236][0-9]{7}$|^2[3-9][1-9][0-9]{6}$|^2[12][0-9]{7}$", ErrorMessage = "Contacto inválido.")]
        [Display(Name = "Contato:")]
        public string Contato { get; set; }
    }
}
