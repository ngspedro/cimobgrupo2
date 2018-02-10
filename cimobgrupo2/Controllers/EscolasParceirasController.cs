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
    /// <summary>Controlador para escolas parceiras</summary>
    /// <remarks>Extende de BaseController</remarks>
    public class EscolasParceirasController : BaseController
    {
        /// <summary>Atributo para a lista de escolas parceiras</summary>
        private List<EscolaParceira> _escolasParceiras;

        /// <summary>Construtor com parametros - EscolasParceirasController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public EscolasParceirasController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "EscolasParceiras")
        {
            _escolasParceiras = context.EscolasParceiras.Include(e => e.Cursos).ThenInclude(e => e.Curso).ToList();
        }

        /// <summary>Action que prepara e mostra o index</summary>
        /// <returns>Retorna a view</returns>
        public IActionResult Index()
        {
            return View(ProperView("Index"), _escolasParceiras);
        }

        /// <summary>Action responsável por preparar e mostrar a página de detalhes de uma escola parceira</summary>
        /// <param name="id">id da escola cujos detalhes se pretendem visualizar</param>
        /// <returns>Caso a escola seja válido, retorna a view de detalhes da mesma. Caso contrário redirecciona para o index</returns>
        public IActionResult Detalhes(int? id)
        {
            EscolaParceira escola = _escolasParceiras.Find(e => e.EscolaParceiraId == id);
            if (escola != null)
                return View(ProperView("Detalhes"), escola);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por preparar e exibir a página de criação de escola parceira</summary>
        /// <returns>View com o formulário de criação</returns>
        public IActionResult Adicionar()
        {
            FillCountryList();
            return View(ProperView("Adicionar"));
        }

        /// <summary>Action que trata da criação de uma escola parceira e da sua adição à bd</summary>
        /// <param name="EscolaParceira">Bind dos campos do formulário preenchidos pelo utilizador, num objeto EscolaParceira</param>
        /// <returns>Redireciona para a acção Index, se adicionada com sucesso.</returns>
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

        /// <summary>Action responsável por preparar e exibir a página de edição de escola parceira</summary>
        /// <param name="Id">Id da escola parceira a editar</param>
        /// <returns>View com o formulário de edição</returns>
        public IActionResult Editar(int? Id)
        {
            EscolaParceira EscolaParceira = _context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == Id);
            FillCountryList();
            ViewBag.CursosAssociar = _context.Cursos.Where(c => c.EscolasParceiras.Where(e => e.EscolaParceira == EscolaParceira).Count() == 0);
            return View(ProperView("Editar"), EscolaParceira);
        }

        /// <summary>Action que trata da edição de uma escola parceira e da sua atualização na bd</summary>
        /// <param name="EscolaParceira">Bind dos campos do formulário preenchidos pelo utilizador, num objeto EscolaParceira</param>
        /// <returns>Redireciona para a acção Index, se editada com sucesso</returns>
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

        /// <summary>Action responsável por exibir o modal de confirmação de remoção de escola parceira</summary>
        /// <param name="Id">Id da escola a remover</param>
        /// <returns>Partialview com o devido modal</returns>
        public IActionResult RemoverModal(int? Id)
        {
            return PartialView(ProperView("RemoverModal"), _context.EscolasParceiras.SingleOrDefault(c => c.EscolaParceiraId == Id));
        }

        /// <summary>Action que trata da remoção de uma escola parceira da bd</summary>
        /// <param name="EscolaParceiraId">Id da escola parceira a remover</param>
        /// <returns>Redireciona para a acção Index</returns>
        public IActionResult Remover(int EscolaParceiraId)
        {
            _context.EscolasParceiras.Remove(_context.EscolasParceiras.SingleOrDefault(e => e.EscolaParceiraId == EscolaParceiraId));
            _context.SaveChanges();
            SetSuccessMessage("Escola parceira removida.");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>Action responsável por desassociar cursos de uma determinada escola</summary>
        /// <param name="EscolaParceiraId">Id da escola parceira</param>
        /// <param name="cursosRemover">Lista com ids dos cursos a desassociar</param>
        /// <returns>Redireciona para a action de detalhes de determinada escola parceira</returns>
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

        /// <summary>Action responsável por associar cursos a uma determinada escola parceira</summary>
        /// <param name="EscolaParceiraId">Id da escola parceira</param>
        /// <param name="cursosAssociar">Lista com ids dos cursos a associar</param>
        /// <returns>Redireciona para a action de detalhes de determinada escola parceira</returns>
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

        /// <summary>Método responsável por preencher uma lista de cursos utilizada como source para uma combobox na criação de escolas parceiras</summary>
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