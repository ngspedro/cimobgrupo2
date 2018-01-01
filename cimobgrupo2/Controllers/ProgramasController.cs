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
        private ApplicationDbContext _context;
        private readonly List<Ajuda> _ajudas;
        private readonly List<Erro> _erros;
        private List<Programa> _programas;

        public ProgramasController(ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult Adicionar()
        {
            return View(ProperView("Adicionar"));
        }

        [HttpPost]
        public IActionResult Adicionar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Programa);
                _context.SaveChanges();
                SetSuccessMessage("Programa adicionado.");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            return View(Programa);
        }

        public IActionResult Editar(int? Id)
        {
  
            return View(ProperView("Editar"), _context.Programas.SingleOrDefault(p => p.ProgramaId == Id));
        }

        [HttpPost]
        public IActionResult Editar([Bind("ProgramaId,Nome,Duracao,Descricao")] Programa Programa)
        {
            if (ModelState.IsValid)
            {
                Programa atual = _context.Programas.SingleOrDefault(p => p.ProgramaId == Programa.ProgramaId);
                atual.Nome = Programa.Nome;
                atual.Duracao = Programa.Duracao;
                atual.Descricao = Programa.Descricao;
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
            SetSuccessMessage("Programa removido.");
            return RedirectToAction(nameof(Index));
        }

        public String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB"))
                return "~/Views/Programas/Cimob/" + viewName + ".cshtml";

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