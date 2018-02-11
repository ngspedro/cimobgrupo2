using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    /// <summary>Viewmodel para alteração de detalhes de conta</summary>
    /// <remarks>Possui as propriedades necessárias para tal (nome, data de nascimento, etc.)</remarks>
    public class ChangeDetailsViewModel
    {
        /// <summary>Propriedade correspondente ao nome</summary>
        [Required(ErrorMessage = "Nome obrigatório.")]
        [Display(Name = "Nome:")]
        public String Nome { get; set; }

        /// <summary>Propriedade correspondente à data de nascimento</summary>
        [Required(ErrorMessage = "Data de Nascimento obrigatória.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", ErrorMessage = "Data de Nascimento inválida.")]
        [Maiores17(ErrorMessage = "Só aceitamos registos a maiores de 17 anos.")]
        [Display(Name = "Data de Nascimento:")]
        public string DataNascimento { get; set; }

        /// <summary>Propriedade correspondente ao email</summary>
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        /// <summary>Propriedade correspondente ao contato</summary>
        [Required(ErrorMessage = "Contacto obrigatório.")]
        [RegularExpression(@"^9[1236][0-9]{7}$|^2[3-9][1-9][0-9]{6}$|^2[12][0-9]{7}$", ErrorMessage = "Contacto inválido.")]
        [Display(Name = "Contato:")]
        public string Contato { get; set; }
    }
}
