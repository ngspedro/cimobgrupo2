using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public ChangeDetailsViewModel ChangeDetails { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public DeleteAccountViewModel DeleteAccount { get; set; }
    }
}
