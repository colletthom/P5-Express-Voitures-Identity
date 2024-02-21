using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.Models;

namespace P5_Express_Voitures_Identity.Controllers
{
    [Authorize]
    public class MargesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MargesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get
        public IActionResult Index()
        {
            var objetmarge = _context.Marges.OrderByDescending(m => m.Id).FirstOrDefault();
            return View(objetmarge);
        }

        [HttpPost]
        public IActionResult Index(Marge marge)
        {
            _context.Add(marge);
            _context.SaveChanges();
            return RedirectToAction("Index", "voitures");
        }

    }

}
