using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace cimobgrupo2.Controllers
{
    public class CandidaturasController : Controller
    {
        private string BASE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "candidaturas");

        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private FileController _fileController;
        private List<Candidatura> _candidaturas;
        private ApplicationDbContext _context;

        public CandidaturasController(ApplicationDbContext context, IFileProvider fileProvider)
        {
            _fileController = new FileController(fileProvider);
            this._context = context;
            //vai buscar a lista de candidaturas
            _candidaturas = context.Candidaturas.Include(c => c.Curso).Include(c => c.Programa).Include(c => c.EscolaParceira).Include(c => c.User).ToList();

        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _candidaturas);
        }

        public IActionResult Criar()
        {
            //vai buscar a lista de programas e asocia a uma ViewBag
            ViewBag.Programas = _context.Programas.Select(p => new SelectListItem()
            {
                Value = p.ProgramaId.ToString(),
                Text = p.Nome
            }).ToList();
            //vai buscar a lista de escolas e asocia a uma ViewBag
            ViewBag.Escolas = _context.EscolasParceiras.Select(e => new SelectListItem()
            {
                Value = e.EscolaParceiraId.ToString(),
                Text = e.Nome
            }).ToList();
            //vai buscar a lista de cursos e asocia a uma ViewBag
            ViewBag.Cursos = _context.Cursos.Select(c => new SelectListItem()
            {
                Value = c.CursoId.ToString(),
                Text = c.Nome
            }).ToList();

            var caminho = "candidaturas/" + this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Documentos = _fileController.GetFiles(caminho);

            return View(ProperView("Criar"));
        }

        [HttpPost]
        public IActionResult Criar([Bind("ProgramaId,EscolaParceiraId,CursoId")] Candidatura Candidatura)
        {
            if (ModelState.IsValid)
            {
                Candidatura.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(Candidatura);
                _context.SaveChanges();
                SetSuccessMessage("Programa adicionado.");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            return View(Candidatura);
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _candidaturas.Find(p => p.CandidaturaId == id));
        }

        [HttpPost]
        public async Task<IActionResult> AssociarDocumentos(List<IFormFile> files)
        {
            if (files.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (IFormFile f in files)
                {
                    await _fileController.UploadFile(caminho, f);
                }
                SetSuccessMessage(files.Count() + " documentos adicionados.");
            }

            return RedirectToAction(nameof(Criar));
        }

        public IActionResult RemoverDocumentos(string[] ficheiros)
        {
            if (ficheiros.Count() > 0)
            {
                var caminho = Path.Combine(BASE_PATH, User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (string s in ficheiros)
                {

                    _fileController.Delete(caminho, s);

                }
                SetSuccessMessage(ficheiros.Count() + " documentos removidos.");
            }
            return RedirectToAction(nameof(Criar));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Candidaturas/" + viewName + ".cshtml";

            return viewName;
        }

        private void SetSuccessMessage(String Message)
        {
            TempData["Success"] = Message;
        }

        private void SetErrorMessage(String Code)
        {
            var Erro = _erros.SingleOrDefault(e => e.Codigo == Code);
            TempData["Error_Code"] = Erro.Codigo;
            TempData["Error_Message"] = Erro.Mensagem;
        }
    }
}