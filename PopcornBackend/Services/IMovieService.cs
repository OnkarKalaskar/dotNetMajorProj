using Microsoft.AspNetCore.Mvc;
using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public interface IMovieService
    {
        public IEnumerable<MovieDto> GetMovies();

        public MovieDto GetMovie(int id);

        public IEnumerable<MovieDto> GetClientsMovies(int clientId);

        public Boolean PutMovie(int id, MovieDto movieDto);

        public Boolean PostMovie(Movie movie);

        public Boolean DeleteMovie(int id);

        public Boolean AddToFavoritesMovies(UserMovie FavMovie);

        public Boolean ExistingUserFavMovie(UserMovie FavMovie);

        public Boolean RemoveFromFavorites(UserMovie FavMovie);

        public IEnumerable<MovieDto> GetFavMovies(int id);

        public IEnumerable<MovieDto> GetSearchedMovies(string searchKey);

        public Boolean IncreaseLike(int MovieId);

        public Boolean DecreaseLike(int MovieId);


    }
}
