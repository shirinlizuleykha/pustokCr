
using PustokCrBusiness.Services.Abstracts;
using PustokCrCore.Models;

namespace PustokCr.ViewServices
{
    public class LayoutService
    {
        private readonly IGenreService _genreService;

        public LayoutService(IGenreService genreService)
        {
            _genreService = genreService;
        }


        public List<Genre> GetGenres()
        {
            return _genreService.GetAllGenres();
        }
    }
}
