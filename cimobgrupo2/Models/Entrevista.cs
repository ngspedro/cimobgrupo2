using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models

{
    public enum Avalicaco
    {
        Realizada, Pendente
    }
    public class Entrevista
    {

        public int EntrevistaId { get; set; }
        [Display(Name = "Data de Entrevista")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEntrevista { get; set; }
        [Display(Name = "Avaliação")]
        [Required(ErrorMessage = "{0} é obrigatorio")]
        public Avalicaco Avaliacao { get; set; } // tipo de enum 
        public string Local { get; set; }
        public string Hora { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Display(Name = "Nome")]
        public virtual ApplicationUser User { get; set; }

        public Entrevista()
        {
        }
    }

}

