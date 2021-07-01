using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApi.Models;
using UserApi.Services.Favorites;

namespace UserApi.Controllers
{
    [Authorize]
    [Route("favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoritesService _favoritesService;

        public FavoritesController(FavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet("{id}")]
        public IActionResult GetFavorite(int id)
        {
            var response = _favoritesService.GetFavoriteById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetFavoritesByUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var favorites = _favoritesService.GetFavorites(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public IActionResult AddFavorite([FromForm] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ResponseModel response = _favoritesService.AddFavorite(userId, id);

            return Ok(response);
        }

        [HttpDelete]
        public IActionResult RemoveFavorite([FromForm] int id)
        {
            ResponseModel response = _favoritesService.RemoveFavorite(id);
            return Ok(response);
        }
    }
}