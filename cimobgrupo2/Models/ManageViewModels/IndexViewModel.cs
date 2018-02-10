using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    /// <summary>Viewmodel para página de detalhes de conta</summary>
    /// <remarks>Possui as propriedades necessárias para tal (Viewmodel de alteração de detalhes, mudança de password e eliminação de conta)</remarks>
    public class IndexViewModel
    {
        /// <summary>Propriedade correspondente ao viewmodel de alteração de detalhes da conta</summary>
        public ChangeDetailsViewModel ChangeDetails { get; set; }

        /// <summary>Propriedade correspondente ao viewmodel de mudança de password</summary>
        public ChangePasswordViewModel ChangePassword { get; set; }

        /// <summary>Propriedade correspondente ao viewmodel de eliminação de conta</summary>
        public DeleteAccountViewModel DeleteAccount { get; set; }
    }
}
