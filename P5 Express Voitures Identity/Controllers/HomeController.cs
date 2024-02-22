using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.Models;
using P5_Express_Voitures_Identity.ViewModels;
using System;
using System.Diagnostics;

namespace P5_Express_Voitures_Identity.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Voiture> voitures = await _context.Voitures
                .Include(v => v.Reparations)
                .ToListAsync();

            List<VoitureVM> voitureVMs = voitures.Select(v => new VoitureVM(v, _context)).ToList();

            foreach (var vm in voitureVMs)
            {
                vm.CalculTotalReparation();
                vm.CalculStatutVoiture();

                if (vm.Voiture.PrixVente == null)
                {
                    vm.Voiture.PrixVente = vm.CalculPrixVente();
                }

            }


            return View(voitureVMs);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
