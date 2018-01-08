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

namespace cimobgrupo2.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileProvider fileProvider;
       

        public FileController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

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

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

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