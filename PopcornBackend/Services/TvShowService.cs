using Microsoft.IdentityModel.Tokens;
using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly MajorProjectDbContext _context;
        private readonly List<MediaCategory> _categories;
        public TvShowService(MajorProjectDbContext context) 
        {
            this._context = context;
            this._categories = context.MediaCategories.ToList();

        }
        public List<TvShowDto> GetTvShows()
        {
            List<TvShowDto> tvShowDtos = new();
            if (this._context!=null)
            {
                foreach(TvShow tvShow in this._context.TvShows) 
                {
                    TvShowDto tvShowDto = new()
                    {
                        TvShowId = tvShow.ShowId,
                        TvShowName = tvShow.ShowName,
                        TvShowDescription = tvShow.ShowDescription,
                        TvShowPoster = tvShow.ShowPoster,
                        TvShowPath = tvShow.ShowPath,
                        Likes = tvShow.Likes,
                        CategoryId = tvShow.CategoryId,
                        CategoryName = _categories.FirstOrDefault(c => c.CategoryId == tvShow.CategoryId)?.CategoryName,
                        UserId = tvShow.UserId
                    };
                    tvShowDto.CategoryId = tvShow.CategoryId;
                    tvShowDtos.Add(tvShowDto);
                }
            }
            return tvShowDtos;
        }

        public TvShowDto GetTvShow(long id)
        {
            var tvShow = _context.TvShows.Where(t => t.ShowId == id).FirstOrDefault();
            TvShowDto tvShowDto = new();
            if (tvShow != null)
            {
                    tvShowDto.TvShowId = tvShow.ShowId;
                    tvShowDto.TvShowName = tvShow.ShowName;
                    tvShowDto.TvShowDescription = tvShow.ShowDescription;
                    tvShowDto.TvShowPoster = tvShow.ShowPoster;
                    tvShowDto.TvShowPath = tvShow.ShowPath;
                    tvShowDto.Likes = tvShow.Likes;
                    tvShowDto.CategoryId = tvShow.CategoryId;
                    tvShowDto.CategoryName = _categories.FirstOrDefault(c => c.CategoryId == tvShow.CategoryId)?.CategoryName;
                    tvShowDto.UserId = tvShow?.UserId ?? 0;
                tvShowDto.CategoryId = tvShow?.CategoryId ?? 0;

            }
            return tvShowDto;
        }

        public bool AddTvShow(TvShowDto tvShowDto)
        {
            if(tvShowDto!=null)
            {
                TvShow tvshow = new()
                {
                   
                    ShowName = tvShowDto.TvShowName,
                    ShowDescription = tvShowDto.TvShowDescription,
                    ShowPath = tvShowDto.TvShowPath,
                    ShowPoster = tvShowDto.TvShowPoster,
                    CategoryId = tvShowDto.CategoryId,
                    UserId = tvShowDto.UserId
                };

                _context.TvShows.Add(tvshow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateTvShow(int id,TvShowDto tvShowDto)
        {
            var tvShow = _context.TvShows.Where(t => t.ShowId == id).FirstOrDefault();

            if (tvShow != null && tvShowDto!=null)
            {
                tvShow.ShowName = tvShowDto.TvShowName;
                tvShow.ShowDescription = tvShowDto.TvShowDescription;
                tvShow.ShowPath = tvShowDto.TvShowPath;
                tvShow.ShowPoster = tvShowDto.TvShowPoster;
                tvShow.CategoryId = tvShowDto.CategoryId;
                tvShow.UserId = tvShowDto.UserId;

                _context.Update(tvShow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteTvShow(long id)
        {
            var tvshow = _context.TvShows.FirstOrDefault(t=>t.ShowId==id);
            if(tvshow!=null)
            {
                _context.TvShows.Remove(tvshow);
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public List<TvShowDto> GetFavTvShows(int id)
        {
            List<TvShow>  AllTvShows= _context.TvShows.ToList();
            List<UserTvShow> FavTvShows = _context.UserTvShows.Where(t=>t.UserId==id).ToList();
            List<int> FavTvShowId = new();
            List<TvShowDto> UserfavTvShow = new();
            if(FavTvShows!=null)
            {
                foreach (UserTvShow userfav in FavTvShows)
                {
                    FavTvShowId.Add(userfav.TvShowId);
                }

                foreach (int tvshowid in FavTvShowId)
                {
                    TvShow? tvShow = AllTvShows.FirstOrDefault(t => t.ShowId == tvshowid);
                    
                    TvShowDto tvShowDto = new()
                    {
                        TvShowId = tvShow?.ShowId ?? 0,
                        TvShowName = tvShow?.ShowName,
                        TvShowDescription = tvShow?.ShowDescription,
                        TvShowPoster = tvShow?.ShowPoster,
                        TvShowPath = tvShow?.ShowPath,
                        Likes = tvShow?.Likes,
                        CategoryId = tvShow?.CategoryId ?? 0,
                        CategoryName = _categories.FirstOrDefault(c => c.CategoryId == tvShow?.CategoryId)?.CategoryName
                    };
                    UserfavTvShow.Add(tvShowDto);
                }
            }
            return UserfavTvShow;
        }

        public IEnumerable<TvShowDto> GetSearchedTvShows(string searchKey)
        {
            List<TvShowDto> searchedTvShow = new();
            List<TvShow> AllTvShows = _context.TvShows.Where(show => show.ShowName.ToLower().Contains(searchKey.ToLower())).ToList();

            foreach (var tvShow in AllTvShows)
            {
                TvShowDto tvShowDto = new()
                {
                    TvShowId = tvShow.ShowId,
                    TvShowName = tvShow.ShowName,
                    TvShowDescription = tvShow.ShowDescription,
                    TvShowPoster = tvShow.ShowPoster,
                    TvShowPath = tvShow.ShowPath,
                    Likes = tvShow?.Likes ?? 0,
                    CategoryId = tvShow?.CategoryId ?? 0
                };
                searchedTvShow.Add(tvShowDto);
            }
            return searchedTvShow;
        }

        public bool AddToFav(UserTvShow userFavTvShow)
        {
            if(userFavTvShow != null)
            {
                if(ExistingFav(userFavTvShow)) 
                { 
                    return false;
                }
                _context.UserTvShows.Add(userFavTvShow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool ExistingFav(UserTvShow userfav)
        {
            UserTvShow? ExistingFavTvShow = _context.UserTvShows.FirstOrDefault(tvshow=>tvshow.UserId==userfav.UserId && tvshow.TvShowId==userfav.TvShowId);
            if (ExistingFavTvShow != null)
            {
                return true;
            }
            return false;
        }

       public bool RemoveFromFav(UserTvShow userFavTvShow)
       {
            UserTvShow? ExistingFavTvShow = _context.UserTvShows.FirstOrDefault(tvshow => tvshow.UserId == userFavTvShow.UserId && tvshow.TvShowId == userFavTvShow.TvShowId);
            if(ExistingFavTvShow != null)
            {
                _context.UserTvShows.Remove(ExistingFavTvShow);
                _context.SaveChanges();
                return true;
            }
            return false;
       }

        public List<TvShowDto> GetTVShowsOfClient(int userId)
        {
           List<TvShow> shows = _context.TvShows.Where(t=>t.UserId== userId).ToList();
            List<TvShowDto> tvShowDtos = new();

            if (shows.Count>0)
            {
               
                foreach (TvShow tvShow in shows)
                {
                    TvShowDto tvShowDto = new()
                    {
                        TvShowId = tvShow.ShowId,
                        TvShowName = tvShow.ShowName,
                        TvShowDescription = tvShow.ShowDescription,
                        TvShowPoster = tvShow.ShowPoster,
                        TvShowPath = tvShow.ShowPath,
                        Likes = tvShow.Likes,
                        CategoryName = _categories.FirstOrDefault(c => c.CategoryId == tvShow.CategoryId)?.CategoryName,
                        UserId = tvShow?.UserId ?? 0,
                        CategoryId = tvShow?.CategoryId ?? 0
                    };
                    tvShowDtos.Add(tvShowDto);
                }
            }
            return tvShowDtos;

        }

        public bool IncreaseLike(int MovieId)
        {
            TvShow? tvShow = _context.TvShows.FirstOrDefault(t => t.ShowId == MovieId);
            if(tvShow!=null)
            {
                tvShow.Likes += 1;
                _context.TvShows.Update(tvShow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DecreaseLike(int MovieId)
        {
            TvShow? tvShow = _context.TvShows.FirstOrDefault(t => t.ShowId == MovieId);
            if (tvShow != null)
            {
                tvShow.Likes -= 1;
                _context.TvShows.Update(tvShow);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
