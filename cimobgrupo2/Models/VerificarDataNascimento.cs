using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    public class Maiores17 : ValidationAttribute, IClientModelValidator
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

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-dataoffparameters", errorMessage);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
