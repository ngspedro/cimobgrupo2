using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Data;
using cimobgrupo2.Models;
using Microsoft.Extensions.FileProviders;

namespace cimobgrupo2.Controllers
{
    public class BaseController : Controller
    {
        protected string _controllerName;

        protected ApplicationDbContext _context;
        protected FileController _fileController;

        protected readonly List<Ajuda> _ajudas;
        protected readonly List<Erro> _erros;

        public BaseController(ApplicationDbContext context, IFileProvider fileProvider, string controllerName)
        {
            _fileController = new FileController(fileProvider);
            _context = context;
            _ajudas = context.Ajudas.Where(ai => ai.Controller == controllerName).ToList();
            _erros = context.Erros.ToList();
            _controllerName = controllerName;
        }

        protected String ProperView(String viewName)
        {
            if (User.IsInRole("CIMOB") || User.IsInRole("Admin"))
                return "~/Views/" + _controllerName + "/Cimob/" + viewName + ".cshtml";

            return viewName;
        }

        protected void SetErrorMessage(String Code)
        {
            var Erro = _erros.SingleOrDefault(e => e.Codigo == Code);
            TempData["Error_Code"] = Erro.Codigo;
            TempData["Error_Message"] = Erro.Mensagem;
        }

        protected void SetSuccessMessage(String Message)
        {
            TempData["Success"] = Message;
        }
    }
}