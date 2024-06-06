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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BoekenController(ManyToManyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                var newBoek = new Boek
                {
                    Titel = viewModel.Boek.Titel,
                    AuteurId = viewModel.SelectedAuteurId,
                    IsAvailable = viewModel.Boek.IsAvailable,
                    IsNewRelease = viewModel.Boek.IsNewRelease,
                    IsBestSeller = viewModel.Boek.IsBestSeller,
                    BindingType = viewModel.Boek.BindingType,
                    Afbeeldingpad = afbeeldingpad,
                };

                _context.Boeken.Add(newBoek);
                await _context.SaveChangesAsync();

                if (viewModel.SelectedGenres != null)
                {
					foreach (var genreId in viewModel.SelectedGenres)
					{
                        var boekGenres = new BoekGenre
                        {
                            BoekId = newBoek.BoekId,
                            GenreId = genreId,
                        };
						_context.BoekGenres.Add(boekGenres);
					}
                    await _context.SaveChangesAsync();
				}
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
			if (id == null)
			{
				return NotFound();
			}
            var boek = await _context.Boeken
                .Include(b => b.BoekGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.BoekId == id);

            if (boek == null)
            {
				return NotFound();
            }

			var viewModel = new EditBoekViewModel
			{
				BoekId = boek.BoekId,
                Titel = boek.Titel,
				SelectedAuteurId = boek.AuteurId,
                SelectedGenres = boek.BoekGenres.Select(bg => bg.GenreId).ToList(),
                IsAvailable = boek.IsAvailable,
                IsNewRelease = boek.IsNewRelease,
                IsBestSeller = boek.IsBestSeller,
                BindingType = boek.BindingType,
                Auteurs = await _context.Auteurs.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),
                AfbeeldingPad = boek.Afbeeldingpad
			};

            return View(viewModel);
        }
		

        private async Task<string> UploadFile(IFormFile afbeelding)
        {
            if (afbeelding == null || afbeelding.Length == 0)
            {
                return null;
            }

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");    //ne pas oublier l'injection de dépendance au début de cette classe
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