using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Localization.Models;
using Microsoft.Extensions.Localization;

namespace Demo.Localization.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizador;

        public HomeController(IStringLocalizer<HomeController> localizador)
        {
            _localizador = localizador;
        }

        //exemplo http://localhost:49502/en-US

        public IActionResult Index()
        {
            ViewData["Conteudo"] = _localizador["Conteudo"];
            return View();
        }

        //exemplo = http://localhost:49502/en-US/Home/Privacy
        public IActionResult Privacy()
        {
            ViewData["Conteudo"] = _localizador["Conteudo"];
            return View();
        }

        //exemplo = http://localhost:49502/en-US/Home/Cadastro
        public IActionResult Cadastro()
        {
            ViewData["Conteudo"] = _localizador["Conteudo"];
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
