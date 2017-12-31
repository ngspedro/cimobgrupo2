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
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
            _ajudas = context.Ajudas.Where(ai => ai.Controller == "Cursos").ToList();
            _erros = context.Erros.ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _context.Cursos.ToList());
        }

        public IActionResult Detalhes(int? id)
        {
            return View(ProperView("Detalhes"), _context.Cursos.ToList().Find(c => c.CursoId == id));
        }

        public IActionResult NovoCursoModal()
        {
            return PartialView(ProperView("NovoCursoModal"));
        }

        public IActionResult NovoCurso([Bind("Nome")] Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Curso);
                _context.SaveChanges();
                SetSuccessMessage("Curso adicionado.");
            } else
            {
                SetErrorMessage("003");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarCursoModal(int? Id)
        {
            return PartialView(ProperView("EditarCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }

        public IActionResult EditarCurso ([Bind("CursoId,Nome")] Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _context.Cursos.SingleOrDefault(c => c.CursoId == Curso.CursoId).Nome = Curso.Nome;
                _context.SaveChanges();
                SetSuccessMessage("Curso editado.");
            } else
            {
                SetErrorMessage("003");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoverCursoModal(int? Id)
        {
            return PartialView(ProperView("RemoverCursoModal"), _context.Cursos.SingleOrDefault(c => c.CursoId == Id));
        }


        public IActionResult RemoverCurso(int CursoId)
        {
            _context.Cursos.Remove(_context.Cursos.SingleOrDefault(m => m.CursoId == CursoId));
            _context.SaveChanges();
            SetSuccessMessage("Curso removido.");
            return RedirectToAction(nameof(Index));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Cursos/Cimob/" + viewName + ".cshtml";

            return viewName;
        }

        private void SetErrorMessage(String Code)
        {
            var Erro = _erros.SingleOrDefault(e => e.Codigo == Code);
            TempData["Error_Code"] = Erro.Codigo;
            TempData["Error_Message"] = Erro.Mensagem;
        }

        private void SetSuccessMessage(String Message)
        {
            TempData["Success"] = Message;
        }
    }
}