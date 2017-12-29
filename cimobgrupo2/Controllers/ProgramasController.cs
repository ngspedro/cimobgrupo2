using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;

namespace cimobgrupo2.Controllers
{
    public class ProgramasController : Controller
    {
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private List<Programa> _programas;

        public ProgramasController(ApplicationDbContext context)
        {
            _ajudas = context.Ajudas.Where(ai => ai.Controller == "Programas").ToList();
            _erros = context.Erros.ToList();
            _programas = context.Programas.Include(e => e.EscolasParceiras).ThenInclude(e => e.EscolaParceira)
                .ThenInclude(e => e.Cursos).ThenInclude(e => e.Curso)
                .Include(e => e.Ficheiros).ThenInclude(e => e.Ficheiro).ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _programas);
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _programas.Find(p => p.ProgramaId == id));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Programas/Cimob/" + viewName + ".cshtml";

            return viewName;
        }
    }
}