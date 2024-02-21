using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using P5_Express_Voitures_Identity.Models.Service;
using P5_Express_Voitures_Identity.Data;
using P5_Express_Voitures_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using P5_Express_Voitures_Identity.ViewModels;

namespace P5_Express_Voitures_Identity.Controllers
{
    [Authorize]
    public class AnnoncesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly ImageService _imageService;

        public AnnoncesController(ApplicationDbContext context, IConfiguration configuration, IWebHostEnvironment environment, ImageService imageService)
        {
            _context = context;
            _configuration = configuration;
            _environment = environment;
            _imageService = imageService;
        }

        // GET: Annonces
        public async Task<IActionResult> Index(int idVoiture)
        {
            Annonce annonce = _context?.Annonces?.FirstOrDefault(r => r.IdVoiture == idVoiture);

            return View(annonce);
        }

        // GET: Annonces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annonce == null)
            {
                return NotFound();
            }

            return View(annonce);
        }

        // GET: Annonces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Annonces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdVoiture, TitreAnnonce,DescriptionAnnonce, Photos")] Annonce annonce, List<IFormFile> Photos, int IdVoiture)
        {
            //var annonceRecupere = await _context.Annonces.FirstOrDefaultAsync(a => a.IdVoiture == IdVoiture); //si on en récupère 1
            //var annonces = await _context.Annonces.Where(a => a.IdVoiture == IdVoiture).ToListAsync(); //si on récupère une liste

            if (ModelState.IsValid)
            {
                /*if (annonceRecupere.Photos != null)
                foreach (var photo in annonceRecupere.Photos)
                {
                    if (photo.Photos != null) ;
                    {
                        using (var stream = photo.Photos.OpenReadStream())
                        {
                            await _imageService.UploadAsync(stream);
                        }
                    }
                }*/

                if (Photos != null && Photos.Count > 0)
                {
                    foreach (var file in Photos)
                    {
                        if (file != null && file.Length > 0)
                        {
                            // créer un nouvel objet Photo et l'ajouter à la collection Photos de l'annonce
                            var photo = new Photo
                            {
                                Nom = file.FileName,
                                IdAnnonce = annonce.Id
                            };

                            if (annonce.Photos == null)
                            {
                                annonce.Photos = new List<Photo>();
                            }
                            annonce.Photos.Add(photo);

                            _context.Add(annonce);
                            await _context.SaveChangesAsync();
                            var pathService = new PathService(_configuration, _environment);
                            var filePath = pathService.GetUploadsPath(photo.Nom);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                }
                else
                {
                    // sauvegarder l'annonce dans la base de données sans photo
                    _context.Add(annonce);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(annonce);

        }


        // GET: Annonces/Edit/5
        public async Task<IActionResult> Edit(int? id, int idVoiture)
        {
            ViewData["idVoiture"] = idVoiture;

            if (id == null)
            {
                return NotFound();
            }
            
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce == null)
            {
                return NotFound();
            }
            else
            {
                annonce.Photos = _context.Photos
                    .Where(a => a.IdAnnonce == id)
                    .ToList();

                return View(annonce);
            }
        }

        // POST: Annonces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitreAnnonce,DescriptionAnnonce")] Annonce annonce, IFormFile Photos, int IdVoiture)
        {
            ViewData["idVoiture"] = IdVoiture;
            if (int.TryParse(Request.Form["idVoiture"], out int idVoiture))
                annonce.IdVoiture = idVoiture;

            if (id != annonce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photos != null && Photos.Length>0)
                    {
                        // créer un nouvel objet Photo et l'ajouter à la collection Photos de l'annonce
                        var photo = new Photo
                        {
                            Nom = Photos.FileName,
                            IdAnnonce = annonce.Id
                        };

                        await _context.SaveChangesAsync();
                        var pathService = new PathService(_configuration, _environment);
                        var filePath = pathService.GetUploadsPath(photo.Nom);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Photos.CopyToAsync(stream);
                        }

                        if (annonce.Photos == null)
                        {
                            annonce.Photos = new List<Photo>();
                        }
                        photo.LienPhoto = filePath;
                        annonce.Photos.Add(photo);
                        _context.Annonces.Update(annonce);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // sauvegarder l'annonce dans la base de données sans photo
                        _context.Add(annonce);
                        await _context.SaveChangesAsync();
                    }                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnonceExists(annonce.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new { idVoiture = annonce.IdVoiture });
            }
            return View(annonce);
        }

        // GET: Annonces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annonce == null)
            {
                return NotFound();
            }

            return View(annonce);
        }

        // POST: Annonces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnonceExists(int id)
        {
            return _context.Annonces.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AjouterPhoto(int idAnnonce, IFormFile Photos)
        {
            // récupére l'annonce correspondante à l'ID
            var annonce = await _context.Annonces.FindAsync(idAnnonce);

            if (annonce == null)
            {
                return NotFound();
            }

            // créer un nouvel objet Photo et l'ajouter à la collection Photos de l'annonce
            var photo = new Photo
            {
                Nom = Photos.FileName,
                IdAnnonce = idAnnonce
            };

            if (annonce.Photos == null)
            {
                annonce.Photos = new List<Photo>();
            }
            annonce.Photos.Add(photo);

            // sauvegarder l'annonce dans la base de données
            await _context.SaveChangesAsync();

            // télécharger le fichier dans le répertoire approprié
            var pathService = new PathService(_configuration, _environment);
            var filePath = pathService.GetUploadsPath(photo.Nom);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Photos.CopyToAsync(stream);
            }

            return RedirectToAction(nameof(Details), new { idAnnonce = idAnnonce });
        }
    }
}
