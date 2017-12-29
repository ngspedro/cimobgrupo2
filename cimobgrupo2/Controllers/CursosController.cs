using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;

namespace cimobgrupo2.Controllers
{
    public class CursosController : Controller
    {
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private List<Curso> _cursos;

        public CursosController(ApplicationDbContext context)
        {
            _ajudas = context.Ajudas.Where(ai => ai.Controller == "Cursos").ToList();
            _erros = context.Erros.ToList();
            _cursos = context.Cursos.ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _cursos);
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _cursos.Find(c => c.CursoId == id));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Cursos/Cimob/" + viewName + ".cshtml";

            return viewName;
        }
    }
}