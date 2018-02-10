using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cimobgrupo2.Extensions
{
    /// <summary>Extensão para auxilio em operações com ficheiros</summary>
    /// <remarks>Possui métodos para obtenção de nome de ficheiro, etc.</remarks> 
    public static class IFormFileExtensions
    {
        /// <summary>Método para obter o nome de um ficheiro</summary>
        /// <param name="file">Ficheiro cujo nome se pretende obter</param>
        /// <returns>String com o nome do ficheiro</returns>
        public static string GetFilename(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');
        }

        /// <summary>Método para obter a stream de um ficheiro</summary>
        /// <param name="file">Ficheiro cuja stream se pretende obter</param>
        /// <returns>MemoryStream do ficheiro</returns>
        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        /// <summary>Método para obter a stream de um ficheiro em formato de array</summary>
        /// <param name="file">Ficheiro cuja stream se pretende obter</param>
        /// <returns>MemoryStream do ficheiro em formato array</returns>
        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }
    }
}
