using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using cimobgrupo2.Models.FilesViewModels;

namespace cimobgrupo2.Controllers
{
    public class ProgramasController : BaseController
    {
        private string BASE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "programas");

        private List<Programa> _programas;

        public ProgramasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Programas")
        {
            _programas = context.Programas.Include(e => e.EscolasParceiras).ThenInclude(e => e.EscolaParceira)
                .ThenInclude(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _programas);
        }

        public IActionResult Detalhes(int? id)
        {
            Programa programa = _programas.Find(p => p.ProgramaId == id);
            var caminho = "programas/" + id;
            ViewBag.Edital = _fileController.GetFile(caminho, programa.Edital);
            ViewBag.Documentos = _fileController.GetFiles(caminho, programa.Edital);
            return View(ProperView("Detalhes"), programa);
        }

        public IActionResult Adicionar()
        {
            return View(ProperView("Adicionar"));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Programa);
                _context.SaveChanges();

                if (file != null)
                {
                    Programa.Edital = file.GetFilename();

                    var caminho = Path.Combine(BASE_PATH, Programa.ProgramaId.ToString());
                    await _fileController.UploadFile(caminho, file);
                }

                _context.SaveChanges();
                SetSuccessMessage("Programa adicionado.");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            return View(Programa);
        }

        public IActionResult Editar(int? Id)
        {
            Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == Id);
            ViewBag.EscolasAssociar = _context.EscolasParceiras.Where(e => e.Programas.Where(p => p.Programa == Programa).Count() == 0);

            var caminho = "programas/" + Id;
            ViewBag.Edital = _fileController.GetFile(caminho, Programa.Edital);
            ViewBag.Documentos = _fileController.GetFiles(caminho, Programa.Edital);
            return View(ProperView("Editar"), Programa);
        }

        [HttpPost]
        public async Task<IActionResult> Editar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Programa atual = _context.Programas.SingleOrDefault(p => p.ProgramaId == Programa.ProgramaId);
                atual.Nome = Programa.Nome;
                atual.Duracao = Programa.Duracao;
                atual.Descricao = Programa.Descricao;


                if (file != null)
                {
                    var caminho = Path.Combine(BASE_PATH, atual.ProgramaId.ToString());
                    _fileController.Delete(caminho, atual.Edital);
                    atual.Edital = file.GetFilename();
                    await _fileController.UploadFile(caminho, file);
                }

                _context.SaveChanges();
                SetSuccessMessage("Programa editado.");
                return RedirectToAction(nameof(Index));
            }

            SetErrorMessage("003");
            return View(Programa);
        }

        public IActionResult RemoverModal(int? Id)
        {
            return PartialView(ProperView("RemoverModal"), _context.Programas.SingleOrDefault(p => p.ProgramaId == Id));
        }

        public IActionResult Remover(int ProgramaId)
        {
            _context.Programas.Remove(_context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId));
            _context.SaveChanges();

            foreach (FileDetails f in _fileController.GetFiles("programas/" + ProgramaId))
            {
                _fileController.Delete(Path.Combine(BASE_PATH, ProgramaId.ToString()), f.Name);
            }

            SetSuccessMessage("Programa removido.");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoverAssociacoes(int ProgramaId, int[] escolasRemover)
        {
            if (escolasRemover.Count() > 0)
            {
                Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId);
                foreach (int i in escolasRemover)
                {
                    Programa.EscolasParceiras.Remove(Programa.EscolasParceiras.SingleOrDefault(e => e.ProgramaId == ProgramaId && e.EscolaParceiraId == i));

                }
                _context.SaveChanges();
                SetSuccessMessage(escolasRemover.Count() + " associações removidas.");
            }
            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        public IActionResult RemoverDocumentos(int ProgramaId, string[] ficheirosRemover)
        {
            if (ficheirosRemover.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, ProgramaId.ToString());
                foreach (string s in ficheirosRemover)
                {

                    _fileController.Delete(caminho, s);

                }
                SetSuccessMessage(ficheirosRemover.Count() + " documentos removidos.");
            }
            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        [HttpPost]
        public async Task<IActionResult> AssociarDocumentos(int ProgramaId, List<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, ProgramaId.ToString());
                foreach (IFormFile f in files)
                {
                    await _fileController.UploadFile(caminho, f);
                }
                SetSuccessMessage(files.Count() + " documentos associados.");
            }

            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }

        public IActionResult AssociarCursos(int ProgramaId, int[] escolasAssociar)
        {
            if (escolasAssociar.Count() > 0)
            {
                Programa Programa = _context.Programas.SingleOrDefault(p => p.ProgramaId == ProgramaId);
                foreach (int i in escolasAssociar)
                {
                    _context.Add(new ProgramaEscolaParceira()
                    {
                        Programa = Programa,
                        EscolaParceira = _context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == i)
                    });
                }
                _context.SaveChanges();
                SetSuccessMessage("Escolas Parceiras associadas.");
            }

            return RedirectToAction(nameof(Editar), new { Id = ProgramaId });
        }
    }
}