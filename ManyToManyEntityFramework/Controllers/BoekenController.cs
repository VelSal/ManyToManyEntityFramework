using ManyToManyApp.Data;
using ManyToManyApp.Models;
using ManyToManyApp.Models.ViewModels;
using ManyToManyEntityFramework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ManyToManyApp.Controllers
{

    public class BoekenController : Controller
    {
        private readonly ManyToManyContext _context;

        public BoekenController(ManyToManyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var boeken = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.BoekGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();
            ViewBag.boekenCount = boeken.Count();
            if (boeken == null || !boeken.Any())
            {
                return NotFound();
            }

            var viewModel = boeken.Select(b => new BoekenIndexViewModel
            {
                BoekId = b.BoekId,
                Titel = b.Titel,
                AuteurNaam = b.Auteur.Naam,
                GenreNamen = b.BoekGenres.Select(bg => bg.Genre.Naam).ToList(),
                IsAvailable = b.IsAvailable,
                IsNewRelease = b.IsNewRelease,
                IsBestSeller = b.IsBestSeller,
                BindingType = b.BindingType.HasValue ? b.BindingType.Value.ToString() : "onbekend"

            }).ToList();

            return View(viewModel);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateBoekViewModel
            {
                Auteurs = await _context.Auteurs.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),
                SelectedGenres = new List<int>(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBoekViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                string? afbeeldingpad = viewModel.Afbeelding != null && viewModel.Afbeelding.Length > 0 
                    ? await UploadFile(viewModel.Afbeelding) 
                    : "/images/default.jpg"; 
            }

            return View();
        }

        private async Task<string> UploadFile(IFormFile afbeelding)
        {
            if (afbeelding == null || afbeelding.Length == 0)
            {
                return null;
            }

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + afbeelding.FileName;
            string filePath = Path.Combine(uploadPath, uniqueFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await afbeelding.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }
    }
}