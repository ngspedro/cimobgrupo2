﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cimobgrupo2.Models;
using Microsoft.AspNetCore.Authorization;
using cimobgrupo2.Data;
using Microsoft.Extensions.FileProviders;

namespace cimobgrupo2.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context, IFileProvider fileProvider) : base(context, fileProvider, "Home")
        {

        }
        public IActionResult Index()
        {
            return View(ProperView("Index"));
        }

    }
}
