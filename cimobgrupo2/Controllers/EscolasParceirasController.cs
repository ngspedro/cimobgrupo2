using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using cimobgrupo2.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Extensions.FileProviders;

namespace cimobgrupo2.Controllers
{
    public class EscolasParceirasController : BaseController
    {
        private List<EscolaParceira> _escolasParceiras;

        public EscolasParceirasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "EscolasParceiras")
        {
            _escolasParceiras = context.EscolasParceiras.Include(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
        }

        public IActionResult Index()
        {
            return View(ProperView("Index"), _escolasParceiras);
        }

        public IActionResult Detalhes(int? id)
        {
            EscolaParceira escola = _escolasParceiras.Find(e => e.EscolaParceiraId == id);
            if (escola != null)
                return View(ProperView("Detalhes"), escola);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Adicionar()
        {
            FillCountryList();
            return View(ProperView("Adicionar"));
        }

        [HttpPost]
        public IActionResult Adicionar([Bind("EscolaParceiraId,Nome,Pais,Localidade")] EscolaParceira EscolaParceira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(EscolaParceira);
                _context.SaveChanges();
                SetSuccessMessage("Escola Parceira adicionada.");
                return RedirectToAction(nameof(Index));
            }
            SetErrorMessage("003");
            return View(EscolaParceira);
        }

        public IActionResult Editar(int? Id)
        {
            EscolaParceira EscolaParceira = _context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == Id);
            FillCountryList();
            ViewBag.CursosAssociar = _context.Cursos.Where(c => c.EscolasParceiras.Where(e => e.EscolaParceira == EscolaParceira).Count() == 0);
            return View(ProperView("Editar"), EscolaParceira);
        }

        [HttpPost]
        public IActionResult Editar([Bind("EscolaParceiraId,Nome,Pais,Localidade")] EscolaParceira EscolaParceira)
        {
            if (ModelState.IsValid)
            {
                EscolaParceira atual = _context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == EscolaParceira.EscolaParceiraId);
                atual.Nome = EscolaParceira.Nome;
                atual.Pais = EscolaParceira.Pais;
                atual.Localidade = EscolaParceira.Localidade;
                _context.SaveChanges();
                SetSuccessMessage("Escola Parceira editada.");
                return RedirectToAction(nameof(Index));
            }
     
            SetErrorMessage("003");
            return View(EscolaParceira);
        }

        public IActionResult RemoverModal(int? Id)
        {
            return PartialView(ProperView("RemoverModal"), _context.EscolasParceiras.SingleOrDefault(c => c.EscolaParceiraId == Id));
        }

        public IActionResult Remover(int EscolaParceiraId)
        {
            _context.EscolasParceiras.Remove(_context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == EscolaParceiraId));
            _context.SaveChanges();
            SetSuccessMessage("Escola parceira removida.");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoverAssociacoes(int EscolaParceiraId, int[] cursosRemover)
        {
            if (cursosRemover.Count() > 0)
            {
                EscolaParceira Escola = _context.EscolasParceiras.SingleOrDefault(c => c.EscolaParceiraId == EscolaParceiraId);
                foreach (int i in cursosRemover)
                {
                    Escola.Cursos.Remove(Escola.Cursos.SingleOrDefault(c => c.EscolaParceiraId == EscolaParceiraId && c.CursoId == i));

                }
                _context.SaveChanges();
                SetSuccessMessage(cursosRemover.Count() + " associações removidas.");
            }
            return RedirectToAction(nameof(Editar),  new { Id = EscolaParceiraId });
        }


        public IActionResult AssociarCursos(int EscolaParceiraId, int[] cursosAssociar)
        {
            if (cursosAssociar.Count() > 0)
            {
                EscolaParceira Escola = _context.EscolasParceiras.SingleOrDefault(c => c.EscolaParceiraId == EscolaParceiraId);
                foreach (int i in cursosAssociar)
                {
                    _context.Add(new EscolaParceiraCurso()
                    {
                        EscolaParceira = Escola,
                        Curso = _context.Cursos.SingleOrDefault(c => c.CursoId == i)
                    });
                }
                _context.SaveChanges();
                SetSuccessMessage("Cursos associados.");
            }
            
            return RedirectToAction(nameof(Editar), new { Id = EscolaParceiraId });
        }

        private void FillCountryList()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.DisplayName)))
                {
                    CountryList.Add(R.DisplayName);
                }
            }

            CountryList.Sort();
            ViewBag.CountryList = CountryList;
        }
    }
}