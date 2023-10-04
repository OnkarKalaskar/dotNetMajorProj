using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using PopcornBackend.DTO;
using PopcornBackend.Models;
using PopcornBackend.Services;

namespace PopcornBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movieServ, ILogger<MoviesController> logger)
        {
            movieService = movieServ;
            this._logger = logger;
        }

        // GET: api/Movies
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            if (movieService.GetMovies() != null)
            {
                _logger.LogInformation("sending movies data");
                return movieService.GetMovies();
            }
            else
            {
                _logger.LogInformation("no data found");
                return null;
            } 
        }

        [Authorize(Roles = "Client")]
        [HttpGet("ClientsMovies/{clientId}")]
        public IEnumerable<MovieDto> GetClientsMovies(int clientId)
        {
            if (movieService.GetClientsMovies(clientId) != null)
            {
                _logger.LogInformation("sending movies of client data");
                return movieService.GetClientsMovies(clientId);
            }
            else
            {
                _logger.LogInformation($"no movies with client id : {clientId} found");
                return null;
            }
        }

        [Authorize(Roles = "Client,User,Admin")]
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = movieService.GetMovie(id);

            if (movie == null)
            {
                _logger.LogInformation($"movies with id : {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"movies with id : {id} found");
            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Client")]
        [HttpPut("{id}")]
        public IActionResult PutMovie(int id, MovieDto movieDto)
        {

            if (movieService.PutMovie(id, movieDto))
            {
                _logger.LogInformation($"movies with id : {id} updated successfully");
                return Ok(new { Message = "data updated" });
            }
            else
            {
                _logger.LogInformation($"movies with id : {id} not found");
                return BadRequest();
            }
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Client")]
        [HttpPost]
        public ActionResult<Movie> PostMovie(MovieDto movie)
        {
            Movie mov = new Movie();
            mov.MovieName = movie.MovieName;
            mov.MoviePath = movie.MoviePath;
            mov.MoviePoster = movie.MoviePoster;
            mov.MovieDescription = movie.MovieDescription;
            mov.Likes = movie.Likes;
            mov.CategoryId = movie.CategoryId;
            mov.UserId = movie.UserId;

            Boolean ans = movieService.PostMovie(mov);
          if (!ans)
          {
                _logger.LogWarning($"no database found");
                return Problem("Entity set 'MajorProjectDbContext.Movies'  is null.");
          }

            _logger.LogWarning($"movie added");
            return Ok( new {Message="Movie added successfully"});
        }

        // DELETE: api/Movies/5
        [Authorize(Roles = "Client")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var ans = movieService.DeleteMovie(id);
            if(!ans)
            {
                _logger.LogWarning($"movie with id: {id} not found");
                return NotFound();
            }
            else
            {
                _logger.LogWarning($"movie with id: {id} deleted");
                return Ok(new { Message ="Movie deleted successfully" });
            }
        }



        //add user fav movies to favMovies
        //POST : api/Movies/AddFavMovie
        [Authorize(Roles = "User,Admin,Client")]
        [HttpPost("AddFavMovie")]
        public ActionResult AddToFavoritesMovies(FavMovieDto favMovieDto)
        {
            UserMovie favMovie = new UserMovie();
            favMovie.MovieId = favMovieDto.MovieId;
            favMovie.UserId = favMovieDto.UserId;

            if (!movieService.ExistingUserFavMovie(favMovie) && favMovie != null)
            {

                movieService.AddToFavoritesMovies(favMovie);
                return Ok(new { StatusCode = 200 , Message= "Added to favorites" });
            }   
            else if(movieService.ExistingUserFavMovie(favMovie))
            {
                return Ok(new { StatusCode = 200 , Message = "Already in user favorites" });
            }
            return BadRequest();
        }

        [Authorize(Roles = "User,Admin,Client")]
        [HttpDelete("RemoveFromFav")]
        public ActionResult RemoveFromFavorites(int movieId, int userId)
        {
            FavMovieDto FavMovieDto = new FavMovieDto();
            FavMovieDto.MovieId = movieId;
            FavMovieDto.UserId = userId;
            UserMovie FavMovie = new UserMovie();
            FavMovie.MovieId = FavMovieDto.MovieId;
            FavMovie.UserId = FavMovieDto.UserId;
            var ans = movieService.RemoveFromFavorites(FavMovie);
            if(ans)
            {
                return Ok(new { StatusCode = 200, Message = "Removed From Favorites" });
            }
            else
            {
                return BadRequest();
            }
        }

        //get user favorites from fav movies
        //GET : api/Movies/GetFavMovies
        [Authorize(Roles = "User,Admin,Client")]
        [HttpGet("GetFavMovies/{id}")]
        public IEnumerable<MovieDto> GetFavMovies(int id)
        { 
            return movieService.GetFavMovies(id);
        }

        [HttpGet("GetSearchedMovies/{searchKey}")]
        public IEnumerable<MovieDto> GetSearchedMovies(string searchKey)
        {
            return movieService.GetSearchedMovies(searchKey);
        }


        //increase Likes
        [AllowAnonymous]
        [HttpPatch("IncreaseLike")]
        public ActionResult IncreaseLike(int MovieId)
        {
           var ans = movieService.IncreaseLike(MovieId);
            if(ans)
            {
                return Ok("Like incresed");
            }
            return BadRequest();
        }
        
        //decrease like
        [AllowAnonymous]
        [HttpPatch("DecreaseLike")]
        public ActionResult DecreaseLike(int MovieId)
        {
            var ans = movieService.DecreaseLike(MovieId);
            if (ans)
            {
                return Ok("Like Decreased");
            }
            return BadRequest();
            
        }
    }
}
