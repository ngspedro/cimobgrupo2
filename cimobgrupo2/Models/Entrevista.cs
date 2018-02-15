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
    /// <summary>Enumerado para representar o estado de uma entrevista</summary>
    public enum EstadoEntrevista
    {
        Realizada, Pendente
    }

    /// <summary>Classe para representar uma entrevista do sistema</summary>
    /// <remarks>Possui as propriedades necessárias para uma entrevista (data, hora, local, etc.)</remarks> 
    public class Entrevista
    {
        /// <summary>Propriedade correspondente ao id da entrevista</summary>
        public int EntrevistaId { get; set; }
        [Required(ErrorMessage = "Data obrigatória.")]
        /// <summary>Propriedade correspondente à data da entrevista</summary>
        public string Data { get; set; }

        /// <summary>Propriedade correspondente à hora da entrevista</summary>
        [Required(ErrorMessage = "Hora obrigatória.")]
        public string Hora { get; set; }

        /// <summary>Propriedade correspondente ao local onde será realizada a entrevista</summary>
        [Required(ErrorMessage = "Local obrigatório.")]
        public string Local { get; set; }

        /// <summary>Propriedade correspondente ao estado da entrevista</summary>
        public EstadoEntrevista Estado { get; set; }

        /// <summary>Propriedade correspondente à pontuação da entrevista, depois de realizada</summary>
        public int? Pontuacao { get; set; }

        /// <summary>Propriedade correspondente aos comentários adicionais, deopis da realização e pontuação da entrevista</summary>
        public string Comentarios { get; set; }

        /// <summary>Propriedade correspondente ao id da candidatura associada à entrevista</summary>
        public int CandidaturaId { get; set; }

        /// <summary>Propriedade virtual correspondente ao objetivo da candidatura associada à entrevista</summary>
        public virtual Candidatura Candidatura { get; set; }

        /// <summary>Construtor sem parametros</summary>
        public Entrevista()
        {

        }
    }

}

