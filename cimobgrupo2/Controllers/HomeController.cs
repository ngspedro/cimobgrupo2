using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using Microsoft.AspNetCore.Authorization;
using cimobgrupo2.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;

namespace cimobgrupo2.Controllers
{
    /// <summary>Controlador para a home</summary>
    /// <remarks>Extende de BaseController</remarks>
    [Authorize]
    public class HomeController : BaseController
    {
        /// <summary>Construtor com parametros - HomeController</summary>
        /// <param name="context">Context da Bd</param>
        /// <param name="fileProvider">File Provider</param>
        public HomeController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Home")
        {

        }

        /// <summary>Action que prepara e mostra o index</summary>
        /// <returns>View adequada</returns>
        public IActionResult Index()
        {
            if (User.IsInRole("CIMOB"))
            {
                return View(ProperView("Index"), BuildViewModelChart());
            }
            return View(ProperView("Index"));
        }

        /// <summary>Método para gerar o viewmodel com a informação para o gráfico presente no index do CIMOB</summary>
        /// <returns>Viewmodel com os dados</returns>
        private ChartViewModel BuildViewModelChart()
        {

            List<string> Programas = new List<String>();
            List<int> Totais = new List<int>();
            List<double> Aceites = new List<double>();
            List<double> Recusadas = new List<double>();
            List<double> Pendentes = new List<double>();

            List<Programa> lista = _context.Programas.Include(p => p.Candidaturas).OrderByDescending(p => p.Candidaturas.Count).Take(5).ToList();
            foreach (Programa p in lista)
            {
                if (p.Candidaturas.Count > 0)
                {
                    Programas.Add(p.Nome);
                    Totais.Add(p.Candidaturas.Count);

                    double countAceites = p.Candidaturas.Where(c => c.EstadoId == 2).ToList().Count;
                    double countRecusadas = p.Candidaturas.Where(c => c.EstadoId == 3).ToList().Count;
                    double countPendentes = p.Candidaturas.Where(c => c.EstadoId == 1).ToList().Count;
                    if (countAceites == 0)
                        countAceites = 0.05;

                    if (countRecusadas == 0)
                        countRecusadas = 0.05;

                    if (countPendentes == 0)
                        countPendentes = 0.05;

                    Aceites.Add(countAceites);
                    Recusadas.Add(countRecusadas);
                    Pendentes.Add(countPendentes);
                }
            }

            ChartViewModel chartVm = new ChartViewModel()
            {
                Titulo = "Programas mais Populares",
                Programas = Programas,
                Totais = Totais,
                Aceites = Aceites,
                Recusadas = Recusadas,
                Pendentes = Pendentes,
                Candidaturas = _context.Candidaturas.Include(c => c.Entrevistas).Include(c => c.Curso).Include(c => c.Programa).Include(c => c.EscolaParceira).Include(c => c.User).Include(c => c.Estado).OrderByDescending(c => c.CandidaturaId).Take(6).ToList()
            };

            return chartVm;
        }

    }
}
