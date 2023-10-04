using Microsoft.EntityFrameworkCore;
using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public interface ISongService
    {
        public IEnumerable<Song> GetAllSongs();

        public Song GetSongById(int id);

        public IEnumerable<Song> GetClientSongs(long clientId);

        public Boolean AddSong(SongDTO songDTO, long clientId);

        public IEnumerable<Singer> GetAllSingers();

        public int AddSinger(Singer singer);

        public Boolean DeleteClientSong(int id);

        public Boolean UpdateSong(int id, Song song);

        public Boolean AddToFavoritesSongs(UserSong favSong);

        public Boolean ExistingUserFavSong(UserSong favSong);

        public Boolean RemoveFromFavorites(UserSong favSong);

        public IEnumerable<SongDTO> GetFavSongs(int id);

        public IEnumerable<SongDTO> GetSearchedSongs(string searchKey);

        public Boolean IncreaseLike(int songId);

        public Boolean DecreaseLike(int songId);

    }
}
