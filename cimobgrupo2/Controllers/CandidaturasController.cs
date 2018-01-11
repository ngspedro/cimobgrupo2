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
using cimobgrupo2.Services;

namespace cimobgrupo2.Controllers
{
    public class CandidaturasController : BaseController
    {
        private string BASE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "candidaturas");

        private List<Candidatura> _candidaturas;

        private readonly IEmailSender _emailSender;

        public CandidaturasController(ApplicationDbContext context, IEmailSender emailSender, 
            IFileProvider fileProvider) : base(context, fileProvider, "Candidaturas")
        {
            //vai buscar a lista de candidaturas
            _candidaturas = context.Candidaturas.Include(c => c.Curso).Include(c => c.Programa).Include(c => c.EscolaParceira).Include(c => c.User).Include(c => c.Estado).ToList();
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            if (!User.IsInRole("Estudante"))
            {
                ViewBag.ProgramasPublicar = _context.Programas.Include(p => p.Candidaturas).Include(e => e.EscolasParceiras).ThenInclude(e => e.EscolaParceira)
                .ThenInclude(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
                return View(ProperView("Index"), _candidaturas.Where(c => c.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação")));
            }
            return RedirectToAction(nameof(Criar));
        }

        public IActionResult Criar()
        {
            if (!User.IsInRole("Estudante"))
                return RedirectToAction(nameof(Index));
            

            Candidatura aux = _candidaturas.SingleOrDefault(c => c.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (aux == null)
            {
                _context.Add(new Candidatura()
                {
                    UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Programa = null,
                    EscolaParceira = null,
                    Curso = null,
                    Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação")
                    
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Criar));
            }
            else if (aux != null && aux.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Em Criação"))
            {
                return RedirectToAction(ProperView("Detalhes"), new { id = aux.CandidaturaId });
            }

            Candidatura Candidatura = _candidaturas.SingleOrDefault(c => c.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<Programa> listaProgramas = _context.Programas.Include(p => p.EscolasParceiras).ThenInclude(p => p.EscolaParceira).ToList();
            List<EscolaParceira> listaEscolas = _context.EscolasParceiras.Include(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
            List<Curso> listaCursos = _context.Cursos.ToList();

            if (Candidatura.ProgramaId != null)
                listaEscolas = listaEscolas.Where(e => e.Programas.Where(p => p.ProgramaId == Candidatura.ProgramaId).Count() != 0).ToList();

            if (Candidatura.EscolaParceiraId != null)
                listaCursos = listaCursos.Where(e => e.EscolasParceiras.Where(p => p.EscolaParceiraId == Candidatura.EscolaParceiraId).Count() != 0).ToList();

            ViewBag.Programas = listaProgramas;
            ViewBag.Escolas = listaEscolas;
            ViewBag.Cursos = listaCursos;

            var caminho = "candidaturas/" + this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Documentos = _fileController.GetFiles(caminho);

            return View(ProperView("Criar"), Candidatura);
        }


        [HttpPost]
        public IActionResult Submeter(int CandidaturaId)
        {
            Candidatura Candidatura = _candidaturas.SingleOrDefault(c => c.CandidaturaId == CandidaturaId);
            if (Candidatura != null && Candidatura.Programa != null && Candidatura.EscolaParceira != null && Candidatura.Curso != null)
            {
                Candidatura.EstadoId = 1; //estado pendente
                _context.SaveChanges();
                SetSuccessMessage("Candidatura Submetida. Por favor aguarde pela avaliação da mesma.");
                return RedirectToAction(nameof(Detalhes), new { id = Candidatura.CandidaturaId });
            }
            
            SetErrorMessage("003");
            return RedirectToAction(nameof(Criar));
        }

        public IActionResult EscolherPrograma(int CandidaturaId, int ProgramaEscolhido)
        {
            Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
            if (c != null && ProgramaEscolhido != 0 && ProgramaEscolhido != c.ProgramaId)
            {
                c.ProgramaId = ProgramaEscolhido;
                c.EscolaParceiraId = null;
                c.CursoId = null;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        public IActionResult EscolherEscola(int CandidaturaId, int EscolaEscolhida)
        {
            Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
            if (c != null && EscolaEscolhida != 0 && EscolaEscolhida != c.EscolaParceiraId)
            {
                c.EscolaParceiraId = EscolaEscolhida;
                c.CursoId = null;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        public IActionResult EscolherCurso(int CandidaturaId, int CursoEscolhido)
        {
            if (CandidaturaId != 0 && CursoEscolhido != 0)
            {
                Candidatura c = _candidaturas.SingleOrDefault(cand => cand.CandidaturaId == CandidaturaId);
                c.CursoId = CursoEscolhido;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Criar));
        }

        public IActionResult Detalhes(int? id)
        {
            Candidatura candidatura = _candidaturas.Find(p => p.CandidaturaId == id); 
            if (candidatura != null)
            {
                if (!User.IsInRole("Estudante") || candidatura.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    var caminho = "candidaturas/" + candidatura.UserId;
                    ViewBag.Documentos = _fileController.GetFiles(caminho);
                    return View(ProperView("Detalhes"), candidatura);
                }
            }

            return RedirectToAction(nameof(Index));
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
                //SetSuccessMessage(files.Count() + " documentos adicionados.");
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
                //SetSuccessMessage(ficheiros.Count() + " documentos removidos.");
            }
            return RedirectToAction(nameof(Criar));
        }

        public IActionResult Aceitar(int? id)
        {
            Candidatura candidatura = _candidaturas.SingleOrDefault(p => p.CandidaturaId == id);
            if (candidatura != null)
            {
                Programa programa = _context.Programas.Include(p => p.EscolasParceiras).ThenInclude(p => p.EscolaParceira).SingleOrDefault(p => p.ProgramaId == candidatura.ProgramaId);
                ProgramaEscolaParceira assoc = programa.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == candidatura.EscolaParceiraId);
                if(assoc.NumeroVagas > 0)
                {
                    assoc.NumeroVagas--;
                    candidatura.Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Aceite");
                    _context.SaveChanges();
                    SetSuccessMessage("Candidatura aceite.");
                    return RedirectToAction(nameof(Detalhes), new { id = id });
                }

                SetErrorMessage("006");
               return RedirectToAction(nameof(Detalhes), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Recusar(int CandidaturaId, string motivo)
        {
            Candidatura candidatura = _candidaturas.SingleOrDefault(p => p.CandidaturaId == CandidaturaId);
            if (candidatura != null)
            {
                candidatura.Estado = _context.Estados.SingleOrDefault(e => e.Nome == "Recusada");
                candidatura.Motivo = motivo;
                _context.SaveChanges();
                SetSuccessMessage("Candidatura recusada.");
                return RedirectToAction(nameof(Detalhes), new { id = CandidaturaId });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> PublicarResultados(int[] programasPublicar)
        {
            if (programasPublicar.Count() > 0)
            {
                foreach (Candidatura c in _candidaturas)
                {
                    if (programasPublicar.Where(p => p == c.ProgramaId).Any() && c.Estado != _context.Estados.SingleOrDefault(e => e.Nome == "Pendente"))
                    {
                        await _emailSender.SendEmailCandidaturaResultado(c);
                    }
                   
                }
                SetSuccessMessage("Resultados Publicados (" + programasPublicar.Count() + " programas)");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}