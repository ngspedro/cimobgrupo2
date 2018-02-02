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
    public class Entrevista
    {
        public int EntrevistaId { get; set; }
        [ForeignKey("User")]
        public int EstadoId { get; set; }
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name=("Data da Entrevista"))]
        public DateTime DataEntrevista { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = ("Hora da Entrevista"))]
        public DateTime HoraEntrevista { get { return DataEntrevista;} } 
        [Display(Name ="Estado")]
        public virtual Estado Estado { get; set; }
        [Required(ErrorMessage = "{0} é obrigatorio")]
        public string Avaliacao{ get { return this.Avaliar(Estado); } }
        public int CandidaturaId { get; set; }
        public virtual Candidatura Candidatura { get; set; }
        /// <summary>
        /// Verificação de datas ao inserir num determinado model.
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

            if (!DateTime.TryParseExact(DataEntrevista.ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
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

        // para mostra o estado da avaliação 
        public  string Avaliar(Estado value)
        {
            int id = value.EstadoId;
                switch (id)
                {
                     case 1 :
                        return "Entrevista com estado Pendente";
                case 2:
                    return " Entrevista com estado aceite"; 
                    case 3:
                        return "Entrevista com estado recusado";
                case 4:
                    return "Entrevista com estado de criação, aguardando outros detalhes";
                case 5:
                    return "Entrevista Avaliada e ser finalizado o processo";
                    default:
                        return "Não Existe a entrevsita por avaliar";
                }
            }


        }
    }

