using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopcornBackend.DTO;
using PopcornBackend.Models;
using System;

namespace PopcornBackend.Services
{
    public class MovieService : IMovieService
    {
        private readonly MajorProjectDbContext _context;

        //some changes on movie service
        public MovieService(MajorProjectDbContext context)
        {
            _context = context;
        }
        public Boolean AddToFavoritesMovies(UserMovie FavMovie)
        {
            if(FavMovie == null)
            {
                return false;
            }
            else
            {
                _context.Add(FavMovie);
                _context.SaveChanges();
                return true;
            }
        }

        public Boolean ExistingUserFavMovie(UserMovie FavMovie)
        {
            UserMovie? ExistingMovie = _context.UserMovies.Where(um => um.MovieId == FavMovie.MovieId && um.UserId == FavMovie.UserId).FirstOrDefault();
            if(ExistingMovie != null)
            {
                return true;
            }
            return false;
        }

        public Boolean DecreaseLike(int MovieId)
        {
            Movie? currentMovie = _context.Movies.Where(movie => movie.MovieId == MovieId).FirstOrDefault();

            if (currentMovie != null)
            {
                if(currentMovie.Likes <= 0)
                {
                    currentMovie.Likes = 0;
                    return true;
                }
                currentMovie.Likes -= 1;
                _context.Update(currentMovie);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteMovie(int id)
        {
            var movie = _context.Movies.Where(movie => movie.MovieId == id).FirstOrDefault();
            if (movie == null)
            {
                return false;
            }
            else
            {
                _context.Remove(movie);
                _context.SaveChanges();
                return true;
            }

        }

        public IEnumerable<MovieDto> GetFavMovies(int id)
        {
            List<MovieDto> FavMovies = new List<MovieDto>();
            List<Movie> AllMovies = _context.Movies.ToList<Movie>();
            List<UserMovie> UserFavMovies = _context.UserMovies.Where(um => um.UserId == id).ToList();
            List<int> FavMovieId = new List<int> ();
            List<MediaCategory> categories = _context.MediaCategories.ToList();

            foreach(UserMovie userFav in UserFavMovies)
            {
                FavMovieId.Add(userFav.MovieId);
            }

            foreach(int movId in FavMovieId)
            {
                var movie = AllMovies.Where(mov => mov.MovieId == movId).FirstOrDefault();
                MovieDto movieObj = new MovieDto();
                movieObj.MovieId = movie.MovieId;
                movieObj.MovieName = movie.MovieName;
                movieObj.MoviePath = movie.MoviePath;
                movieObj.MoviePoster = movie.MoviePoster;
                movieObj.MovieDescription = movie.MovieDescription;
                movieObj.Likes = movie.Likes;
                movieObj.CategoryId = movie.CategoryId;
                movieObj.UserId = movie.UserId;
                movieObj.CategoryName = categories.Where(mediaCategory => mediaCategory.CategoryId == movie.CategoryId).FirstOrDefault().CategoryName;
                FavMovies.Add(movieObj);
            }

            return FavMovies;
        }


        public IEnumerable<MovieDto> GetSearchedMovies(string searchKey)
        {
            List<MovieDto> searchedMovies = new List<MovieDto>();
            List<Movie> AllMovies = _context.Movies.Where(movie => movie.MovieName.ToLower().Contains(searchKey.ToLower())).ToList<Movie>();

            //List<MediaCategory> categories = _context.MediaCategories.Where(cat => cat.CategoryName.ToLower().Contains(searchKey.ToLower())).ToList();

            foreach (var movie in AllMovies)
            {
                MovieDto movieObj = new MovieDto();
                movieObj.MovieId = movie.MovieId;
                movieObj.MovieName = movie.MovieName;
                movieObj.MoviePath = movie.MoviePath;
                movieObj.MoviePoster = movie.MoviePoster;
                movieObj.MovieDescription = movie.MovieDescription;
                movieObj.Likes = movie.Likes;
                movieObj.CategoryId = movie.CategoryId;
                movieObj.UserId = movie.UserId;
                searchedMovies.Add(movieObj);
            }
            return searchedMovies;
        }


        public MovieDto GetMovie(int id)
        {

            Movie mov =  _context.Movies.Where(movie => movie.MovieId == id).FirstOrDefault();
            List<MediaCategory> categories = _context.MediaCategories.ToList();

            MovieDto movieDtoObj = new MovieDto();

            movieDtoObj.MovieId = mov.MovieId;
            movieDtoObj.MovieName = mov.MovieName;
            movieDtoObj.MoviePath = mov.MoviePath;
            movieDtoObj.MoviePoster = mov.MoviePoster;
            movieDtoObj.CategoryId = mov.CategoryId;
            movieDtoObj.UserId = mov.UserId;
            movieDtoObj.Likes = mov.Likes;
            movieDtoObj.MovieDescription = mov.MovieDescription;
            movieDtoObj.CategoryName = categories.Where(mediaCategory => mediaCategory.CategoryId == mov.CategoryId).FirstOrDefault().CategoryName;
            return movieDtoObj;
            
        }


        public IEnumerable<MovieDto> GetMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();

            foreach(Movie mov in _context.Movies)
            {
                MovieDto movieDtoObj = new MovieDto();

                movieDtoObj.MovieId = mov.MovieId;
                movieDtoObj.MovieName = mov.MovieName;
                movieDtoObj.MoviePath = mov.MoviePath;
                movieDtoObj.MoviePoster = mov.MoviePoster;
                movieDtoObj.CategoryId= mov.CategoryId;
                movieDtoObj.UserId = mov.UserId;
                movieDtoObj.Likes = mov.Likes;
                movieDtoObj.MovieDescription = mov.MovieDescription;

                movies.Add(movieDtoObj);
            }
            return movies;
        }

        public IEnumerable<MovieDto> GetClientsMovies(int clientId)
        {
            List<MovieDto> movies = new List<MovieDto>();

            foreach (Movie mov in _context.Movies.Where(mov => mov.UserId == clientId))
            {
                MovieDto movieDtoObj = new MovieDto();

                movieDtoObj.MovieId = mov.MovieId;
                movieDtoObj.MovieName = mov.MovieName;
                movieDtoObj.MoviePath = mov.MoviePath;
                movieDtoObj.MoviePoster = mov.MoviePoster;
                movieDtoObj.CategoryId = mov.CategoryId;
                movieDtoObj.UserId = mov.UserId;
                movieDtoObj.Likes = mov.Likes;
                movieDtoObj.MovieDescription = mov.MovieDescription;

                movies.Add(movieDtoObj);
            }
            return movies;
        }


        public Boolean IncreaseLike(int MovieId)
        {
            Movie currentMovie = _context.Movies.Where(movie => movie.MovieId == MovieId).FirstOrDefault();

            if (currentMovie != null)
            {
                currentMovie.Likes += 1;
                _context.Update(currentMovie);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean PostMovie(Movie movie)
        {
            if(movie == null)
            {
                return false;
            }
            else
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return true;
            }
        }

        public Boolean PutMovie(int id, MovieDto movieDto)
        {
            var movie = _context.Movies.Where(mov => mov.MovieId == id).FirstOrDefault();

            
            if (movie != null)
            {
                movie.MovieName = movieDto.MovieName;
                movie.MoviePath = movieDto.MoviePath;
                movie.MoviePoster = movieDto.MoviePoster;
                movie.MovieDescription = movieDto.MovieDescription;
                movie.UserId = movieDto.UserId;
                movie.CategoryId = movieDto.CategoryId;
                _context.Update(movie);
                //_context.Entry(movie).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveFromFavorites(UserMovie FavMovie)
        {
            UserMovie? ExistingMovie = _context.UserMovies.Where(um => um.MovieId == FavMovie.MovieId && um.UserId == FavMovie.UserId).FirstOrDefault();
            if (ExistingMovie != null)
            {
                _context.Remove(ExistingMovie);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
