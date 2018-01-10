using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models

{
    public enum Avalicaco
    {
        APROVADA, REPROVADA, NAO_AVALIADA
    }
    public class Entrevista
    {

        public int EntrevistaId { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataEntrevista { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio")]
        public string Avaliacao { get; set; }
        public int CandidaturaId { get; set; }
        public virtual Candidatura Candidatura { get; set; }
        /// <summary>
        /// Verificação da data ao inserir num determinado model.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<ValidationResult> ValidarData(ValidationContext validationContext)
        {
            DateTime expectedFormatDate;
            List<ValidationResult> res = new List<ValidationResult>();
            if (DataEntrevista < DateTime.Today)
            {
                ValidationResult mss = new ValidationResult("A data inserida deve ser superior ou igual a de hoje");
                res.Add(mss);
            }
            return res;

            if(!DateTime.TryParseExact(DataEntrevista.ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out expectedFormatDate))
            {
                ValidationResult mss = new ValidationResult("A data inserida não está no formato indicado");
                res.Add(mss);
            }

        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}

