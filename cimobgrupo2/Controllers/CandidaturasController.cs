using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cimobgrupo2.Controllers
{
    public class CandidaturasController : Controller
    {
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private List<Candidatura> _candidaturas;
        private ApplicationDbContext context;

        public CandidaturasController(ApplicationDbContext context)
        {
            this.context = context;
            //vai buscar a lista de candidaturas
            _candidaturas = context.Candidaturas.Include(c => c.Curso).Include(c => c.Programa).Include(c => c.EscolaParceira).Include(c => c.User).ToList();
            
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _candidaturas);
        }

        public IActionResult Detalhes(int? id)
        {
            if(id != null)
                return View(ProperView("Detalhes"), _candidaturas.Find(c => c.UserId == id));
            else
                return View(ProperView("Detalhes"), _candidaturas);
        }

        public IActionResult Criar()
        {
            //vai buscar a lista de programas e asocia a uma ViewBag
            ViewBag.Programas = context.Programas.Select(p => new SelectListItem()
            {
                Value = p.ProgramaId.ToString(),
                Text = p.Nome
            }).ToList();
            //vai buscar a lista de escolas e asocia a uma ViewBag
            ViewBag.Escolas = context.EscolasParceiras.Select(e => new SelectListItem()
            {
                Value = e.EscolaParceiraId.ToString(),
                Text = e.Nome
            }).ToList();
            //vai buscar a lista de cursos e asocia a uma ViewBag
            ViewBag.Cursos = context.Cursos.Select(c => new SelectListItem()
            {
                Value = c.CursoId.ToString(),
                Text = c.Nome
            }).ToList();
            return View(ProperView("Criar"));
        }

        [HttpPost]
        public IActionResult Criar(Candidatura Model)
        {
            if (ModelState.IsValid)
            {
                context.Candidaturas.Add(new Candidatura
                {
                    CursoId = Model.CursoId,
                    EscolaParceiraId = Model.EscolaParceiraId,
                    ProgramaId = Model.EscolaParceiraId,
                    //UserId = model.UserId
                });
                return View(ProperView("Index"));
            }
            else
            {
                return View(ProperView("Detalhes"));
            }
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Candidaturas/" + viewName + ".cshtml";

            return viewName;
        }
    }
}