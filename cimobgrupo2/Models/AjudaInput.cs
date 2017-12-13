using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class AjudaInput
    {
        public int AjudaInputId { get; set; }
        public String Controller { get; set; }
        public String Action { get; set; }
        public String InputId { get; set; }
        public String Texto { get; set; }

        public AjudaInput()
        {

        }

        public AjudaInput(String Controller, String Action, String InputId, String Texto)
        {
            this.Controller = Controller;
            this.Action = Action;
            this.InputId = InputId;
            this.Texto = Texto;
        }
    }
}
