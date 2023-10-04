using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public class SongService : ISongService
    {
        private readonly MajorProjectDbContext _context;


        public SongService(MajorProjectDbContext context)
        {
            this._context = context;
        }

        public Boolean AddSong(SongDTO songDTO, long clientId)
        {
            if (_context.Songs == null)
            {
                return false;
            }

            Song song = new Song
            {
                SongName = songDTO.SongName,
                SongLyrics = songDTO.SongLyrics,
                SongType = songDTO.SongType,
                SongGeneration = songDTO.SongGeneration,
                SongPath = songDTO.SongPath,
                SongPoster = songDTO.SongPoster,
                SongDescription = songDTO.SongDescription,
                CategoryId = songDTO.CategoryId,
                UserId= clientId,
         
            };            

            _context.Songs.Add(song);
            _context.SaveChanges();
            
            if(songDTO.Singers.Count > 0)
            {
                foreach (var s in songDTO.Singers)
                {
                    SongSinger songSinger = new SongSinger { SongId = song.SongId, SingerId = s };
                    _context.SongSingers.Add(songSinger);
                    _context.SaveChanges();
                }
            }            

            return true;
        }

        public IEnumerable<Singer> GetAllSingers()
        {
            return _context.Singers.ToList();
        }

        public int AddSinger(Singer singer)
        {
            if (_context.Songs == null)
            {
                return 0;
            }

            var s= _context.Singers.Where(s => s.SingerName.ToLower() == singer.SingerName.ToLower()).FirstOrDefault();

            if (s == null)
            {
                _context.Singers.Add(singer);
                _context.SaveChanges();
                return singer.SingerId;
            }

            return s.SingerId;
        }


        public IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        public Song GetSongById(int id)
        {
            return _context.Songs.Where(song => song.SongId == id).FirstOrDefault();
        }

        public IEnumerable<Song> GetClientSongs(long clientId)
        {
            return _context.Songs.Where(song => song.UserId == clientId).ToList();
        }

        public Boolean DeleteClientSong(int id)
        {
            var song = _context.Songs.Where(song => song.SongId == id).FirstOrDefault();
            if (song == null)
            {
                return false;
            }
            else
            {
                _context.Remove(song);
                _context.SaveChanges();
                return true;
            }
        }

        public Boolean UpdateSong(int id, Song song)
        {
            var existingSong = _context.Songs.Where(song => song.SongId == id).FirstOrDefault();

            if (existingSong != null)
            {
                existingSong.SongName = song.SongName;
                existingSong.SongPath = song.SongPath;
                existingSong.SongPoster = song.SongPoster;
                existingSong.SongLyrics = song.SongLyrics;
                existingSong.SongDescription = song.SongDescription;
                existingSong.SongGeneration = song.SongGeneration;
                existingSong.SongType = song.SongType;
                existingSong.CategoryId = song.CategoryId;
                existingSong.Likes = song.Likes;
                existingSong.UserId = song.UserId;

                _context.Update(existingSong);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean AddToFavoritesSongs(UserSong favSong)
        {
            if (favSong == null)
            {
                return false;
            }
            else
            {
                _context.Add(favSong);
                _context.SaveChanges();
                return true;
            }
        }

        public Boolean ExistingUserFavSong(UserSong favSong)
        {
            UserSong? ExistingSong = _context.UserSongs.Where(us => us.SongId == favSong.SongId && us.UserId == favSong.UserId).FirstOrDefault();
            if (ExistingSong != null)
            {
                return true;
            }
            return false;
        }

        public Boolean RemoveFromFavorites(UserSong favSong)
        {
            UserSong? ExistingSong = _context.UserSongs.Where(us => us.SongId == favSong.SongId && us.UserId == favSong.UserId).FirstOrDefault();
            if (ExistingSong != null)
            {
                _context.Remove(ExistingSong);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<SongDTO> GetFavSongs(int id)
        {
            List<SongDTO> favSongs = new List<SongDTO>();

            List<Song> AllSongs = _context.Songs.ToList<Song>();

            List<UserSong> UserFavSongs = _context.UserSongs.Where(us => us.UserId == id).ToList();

            List<int> FavSongId = new List<int>();

            foreach (UserSong userFav in UserFavSongs)
            {
                FavSongId.Add(userFav.SongId);
            }

            foreach (int songId in FavSongId)
            {
                var song = AllSongs.Where(song => song.SongId == songId).FirstOrDefault();
                SongDTO songObj = new SongDTO();
                songObj.SongId = song.SongId;
                songObj.SongName = song.SongName;
                songObj.SongPath = song.SongPath;
                songObj.SongPoster = song.SongPoster;
                songObj.SongLyrics = song.SongLyrics;
                songObj.SongGeneration = song.SongGeneration;
                songObj.SongType = song.SongType;
                songObj.SongDescription = song.SongDescription;

                favSongs.Add(songObj);
            }
            return favSongs;
        }

        public IEnumerable<SongDTO> GetSearchedSongs(string searchKey)
        {
            List<SongDTO> searchedSongs = new List<SongDTO>();
            List<Song> AllSongs = _context.Songs.Where(song => song.SongName.ToLower().Contains(searchKey.ToLower()) || 
                                                               song.SongType.ToLower().Contains(searchKey.ToLower()) || 
                                                               song.SongLyrics.ToLower().Contains(searchKey.ToLower()) || 
                                                               song.SongGeneration.ToLower().Contains(searchKey.ToLower()) ).ToList<Song>();
            

            foreach (var song in AllSongs)
            {
                SongDTO songObj = new SongDTO();
                songObj.SongId = song.SongId;
                songObj.SongName = song.SongName;
                songObj.SongPath = song.SongPath;
                songObj.SongPoster = song.SongPoster;
                songObj.SongLyrics = song.SongLyrics;
                songObj.SongGeneration = song.SongGeneration;
                songObj.SongType = song.SongType;
                songObj.SongDescription = song.SongDescription;

                searchedSongs.Add(songObj);
            }
            return searchedSongs;
        }

        public Boolean IncreaseLike(int songId)
        {
            Song currentSong = _context.Songs.Where(song => song.SongId == songId).FirstOrDefault();

            if (currentSong != null)
            {
                currentSong.Likes += 1;
                _context.Update(currentSong);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DecreaseLike(int songId)
        {
            Song? currentSong = _context.Songs.Where(song => song.SongId == songId).FirstOrDefault();

            if (currentSong != null)
            {
                currentSong.Likes -= 1;
                _context.Update(currentSong);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}