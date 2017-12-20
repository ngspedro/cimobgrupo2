using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Maiores17 : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date;
            date = new DateTime();
            date = DateTime.ParseExact(value.ToString(), "dd/mm/yyyy", null);
            if (DateTime.Today.AddYears(-17) >= date)
                return true;
            return false;
        }
    }
}
