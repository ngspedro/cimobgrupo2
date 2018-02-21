using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimobgrupo2.Models.FilesViewModels
{
    /// <summary>Viewmodel para apresentação de ficheiros</summary>
    /// <remarks>Possui as propriedades necessárias para tal (nome do ficheiro e caminho)</remarks> 
    public class FileDetails
    {
        /// <summary>Propriedade correspondente ao nome do ficheiro</summary>
        public string Name { get; set; }
        /// <summary>Propriedade correspondente ao caminho do ficheiro</summary>
        public string Path { get; set; }
    }
}
