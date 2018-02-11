using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Classe de validação da data de nascimento</summary>
    public class Maiores17 : ValidationAttribute, IClientModelValidator
    {
        /// <summary>Método para verificar se determinada data é válida. (se é maior de 17 anos)</summary>
        /// <param name="value">Objeto com o valor do input (neste caso é uma data)</param>
        /// <returns>True se válida e False caso contrário</returns>
        public override bool IsValid(object value)
        {
            DateTime date;
            date = new DateTime();
            date = DateTime.ParseExact(value.ToString(), "dd/mm/yyyy", null);
            if (DateTime.Today.AddYears(-17) >= date)
                return true;
            return false;
        }

        /// <summary>Método para adicionar a necessidade de validação a um context</summary>
        /// <param name="context">context ao qual se quer adicionar</param>
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-dataoffparameters", errorMessage);
        }

        /// <summary>Método auxiliar para adicionar atributo</summary>
        /// <param name="attributes">Dictionary com os atributos atuais</param>
        /// <param name="key">Key do atributo a adicionar</param>
        /// <param name="value">Valor do atributo a adicionar</param>
        /// <returns>True adicionado e False caso contrário</returns>
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
