using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using cimobgrupo2.Models;
using cimobgrupo2.Models.FilesViewModels;
using cimobgrupo2.Extensions;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para ficheiros</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class FileController : Controller
    {
        /// <summary>Atributo para o File Provider</summary>
        private readonly IFileProvider fileProvider;

        /// <summary>Construtor com parametros - FileController</summary>
        /// <param name="fileProvider">File Provider</param>
        public FileController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        /// <summary>Método responsável por dar upload num ficheiro</summary>
        /// <param name="path">Caminho para onde o ficheiro será carregado</param>
        /// <param name="file">Ficheiro a ser carregado</param>
        /// <returns>True se foi carregado com sucesso e falso caso contrário</returns>
        public async Task<bool> UploadFile(string path, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            Directory.CreateDirectory(path);

            path = Path.Combine(path, file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        /// <summary>Método responsável por obter um ficheiro, num determinado caminho</summary>
        /// <param name="path">Caminho onde o ficheiro está presente</param>
        /// <param name="fileName">Nome do ficheiro a ser extraído</param>
        /// <returns>FileDetails do ficheiro, se existe</returns>
        public FileDetails GetFile(string path, string fileName)
        {
            var model = new FileDetails();
            foreach (var item in this.fileProvider.GetDirectoryContents(path))
            {
                if (item.Name == fileName)
                {
                    model.Name = fileName;
                    model.Path = item.PhysicalPath;
                    return model;
                }
            }
            return null;
        }

        /// <summary>Método responsável por obter uma lista de ficheiros presentes num diretório</summary>
        /// <param name="path">Caminho de onde queremos a lista de ficheiros</param>
        /// <param name="nameException">Nome de um ficheiro que não se quer na lista</param>
        /// <returns>Lista dos vários ficheiros presentes nesse caminho (Lista de FileDetails)</returns>
        public List<FileDetails> GetFiles(string path, string nameException = null)
        {
            var model = new List<FileDetails>();
            foreach (var item in this.fileProvider.GetDirectoryContents(path))
            {
                if (item.Name != nameException)
                {
                    model.Add(
                   new FileDetails { Name = item.Name, Path = item.PhysicalPath });
                }
               
            }
            return model;
        }

        /// <summary>Action responsável pelo download de um ficheiro</summary>
        /// <param name="tipo">Tipo de ficheiro (se pertence a uma candidatura ou programa)</param>
        /// <param name="id">Id do programa/candidatura a que pertence</param>
        /// <param name="nome">Nome do ficheiro</param>
        /// <returns>Download do ficheiro para o pc</returns>
        public async Task<IActionResult> Download(string tipo, string id,  string nome)
        {
            if (tipo == null || nome == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", tipo, id, nome);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        /// <summary>Action responsável pela eliminação de um ficheiro</summary>
        /// <param name="path">Caminho (pasta onde o ficheiro está presente)</param>
        /// <param name="filename">Nome do ficheiro que se pretende eliminar</param>
        /// <returns>Redirecciona para a action Files</returns>
        public IActionResult Delete(string path, string filename)
        {
            if (filename == null)
                return Content("filename not present");


            path = Path.Combine(path, filename);

            if ((System.IO.File.Exists(path)))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Files");
        }

        /// <summary>Método para obter o tipo de conteudo (extensao) de um ficheiro</summary>
        /// <param name="path">Caminho do ficheiro</param>
        /// <returns>Extensão do ficheiro</returns>
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        /// <summary>Método para obter uma lista dos tipos de ficheiro permitidos</summary>
        /// <returns>Lista de tipos</returns>
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}