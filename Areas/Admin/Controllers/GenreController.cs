using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PustokCrBusiness.Exceptions;
using PustokCrBusiness.Services.Abstracts;
using PustokCrCore.Models;


namespace PustokCr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            var genres = _genreService.GetAllGenres();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _genreService.AddGenreAsync(genre);
            }
            catch (DuplicateGenreException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existGenre = _genreService.GetGenre(x => x.Id == id);
            if (existGenre == null) return NotFound();
            return View(existGenre);
        }

        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                _genreService.UpdateGenre(genre.Id, genre);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (DuplicateGenreException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existGenre = _genreService.GetGenre(x => x.Id == id);
            if (existGenre == null) return NotFound();
            return View(existGenre);
        }






        [HttpPost]
        public IActionResult DeleteGenre(int id)
        {
            try
            {
                _genreService.DeleteGenre(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
