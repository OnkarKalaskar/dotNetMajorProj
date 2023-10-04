using PopcornBackend.DTO;
using PopcornBackend.Models;
using System.Collections;

namespace PopcornBackend.Services
{
    public interface ITvShowService
    {
        public List<TvShowDto> GetTvShows();

        public TvShowDto GetTvShow(long id);

        public bool AddTvShow(TvShowDto tvShowDto);

        public bool UpdateTvShow(int id,TvShowDto tvShowDto);

        public bool DeleteTvShow(long id);

        public List<TvShowDto> GetFavTvShows(int id);

        public IEnumerable<TvShowDto> GetSearchedTvShows(string searchKey);

        public bool AddToFav(UserTvShow userFavTvShow);

        public bool RemoveFromFav(UserTvShow userFavTvShow);
        public List<TvShowDto> GetTVShowsOfClient(int userId);
        public bool IncreaseLike(int MovieId);
        public bool DecreaseLike(int MovieId);
        
    }
}
