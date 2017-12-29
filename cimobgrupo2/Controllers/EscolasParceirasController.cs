using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;

namespace cimobgrupo2.Controllers
{
    public class EscolasParceirasController : Controller
    {
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private List<EscolaParceira> _escolasParceiras;

        public EscolasParceirasController(ApplicationDbContext context)
        {
            _ajudas = context.Ajudas.Where(ai => ai.Controller == "EscolasParceiras").ToList();
            _erros = context.Erros.ToList();
            _escolasParceiras = context.EscolasParceiras.Include(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _escolasParceiras);
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _escolasParceiras.Find(e => e.EscolaParceiraId == id));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/EscolasParceiras/Cimob/" + viewName + ".cshtml";

            return viewName;
        }
    }
}