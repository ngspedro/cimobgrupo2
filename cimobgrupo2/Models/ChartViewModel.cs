using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models
{
    /// <summary>Viewmodel para apresentação de index do cimob (ultimas candidaturas e chart de programas mais populares)</summary>
    /// <remarks>Possui as propriedades necessárias para tal (candidaturas, nomes dos programas, titulo, etc.)</remarks>
    public class ChartViewModel
    {
        /// <summary>Propriedade correspondente ao titulo do grafico</summary>
        public string Titulo { get; set; }

        /// <summary>Propriedade correspondente aos programas a aparecer no gráfico</summary>
        public List<string> Programas { get; set; }

        /// <summary>Propriedade correspondente ao total de candidaturas a aparecer no gráfico</summary>
        public List<int> Totais { get; set; }

        /// <summary>Propriedade correspondente ao número de candidaturas aceites a aparecer no gráfico</summary>
        public List<double> Aceites { get; set; }

        /// <summary>Propriedade correspondente ao número de candidaturas recusadas a aparecer no gráfico</summary>
        public List<double> Recusadas { get; set; }

        /// <summary>Propriedade correspondente ao número de candidaturas pendentes a aparecer no gráfico</summary>
        public List<double> Pendentes { get; set; }

        /// <summary>Propriedade correspondente à lista das últimas candidaturas a aparecer na dashboard</summary>
        public List<Candidatura> Candidaturas { get; set; }
    }

}
