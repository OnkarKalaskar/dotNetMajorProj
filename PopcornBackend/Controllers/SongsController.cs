using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopcornBackend.DTO;
using PopcornBackend.Models;
using PopcornBackend.Services;

namespace PopcornBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService songService;
        private readonly ILogger<SongsController> _logger;

        public SongsController(ISongService songService, ILogger<SongsController> logger)
        {
            this.songService = songService;
            this._logger = logger;
        }

        // POST: api/Songs/AddSong/5
        [Authorize(Roles ="Client")]
        [HttpPost("AddSong/{clientId}")]
        public async Task<ActionResult<Song>> PostSong(SongDTO songDTO, long clientId)
        {
            if (songService.AddSong(songDTO, clientId))
            {
                _logger.LogInformation("Song added successfully");
                return CreatedAtAction("GetSong", new { id = songDTO.SongId }, songDTO);
            }
            _logger.LogInformation("Song did not added successfully");
            return Problem("Entity set 'MajorProjectDbContext.Songs'  is null.");
            
        }


        // POST: api/Songs/Singer
        [Authorize(Roles = "Client")]
        [HttpPost("Singer/")]
        public async Task<ActionResult<Singer>> PostSinger(Singer singer)
        {
            int singerId = songService.AddSinger(singer);
            if (singerId != 0)
            {
                _logger.LogInformation("singer added successfully");
                return Ok(singerId);
            }
            _logger.LogInformation("Song did not added successfully");
            return Problem("Entity set 'MajorProjectDbContext.Songs'  is null.");

        }


        // GET: api/Songs
        [AllowAnonymous]
        [HttpGet("/getAllSongs")]
        public IEnumerable<Song> GetSongs()
        {

            if (songService.GetAllSongs() != null)
            {
                _logger.LogInformation("All songs sent");
                return songService.GetAllSongs();
            }
            else
            {
                _logger.LogInformation("songs not sent");
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet("/getAllSingers")]
        public IEnumerable<Singer> GetSingers()
        {

            if (songService.GetAllSingers() != null)
            {
                _logger.LogInformation("singers sent");
                return songService.GetAllSingers();
            }
            else
            {
                _logger.LogInformation("No singers data sent");
                return null;
            }
        }


        // GET: api/Songs/5
        [Authorize(Roles = "Client,User,Admin")]
        [HttpGet("{id}")]
        public ActionResult<Song> GetSong(int id)
        {

            var song = songService.GetSongById(id);

            if (song == null)
            {
                _logger.LogInformation($"song with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"song with id {id} sent");
            return song;
        }

        [Authorize(Roles = "Client")]
        [HttpGet("ClientSongs/{clientId}")]
        public IEnumerable<Song> GetClientSongs(long clientId)
        {

            if (songService.GetClientSongs(clientId) != null)
            {
                _logger.LogInformation($"songs added by client with id : {clientId} sent");
                return songService.GetClientSongs(clientId);
            }
            else
            {
                _logger.LogInformation($"songs added by client with id : {clientId} not found");
                return null;
            }
        }


        [Authorize(Roles = "Client")]
        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {

            var ans = songService.DeleteClientSong(id);
            if (!ans)
            {
                _logger.LogInformation("song is not deleted");
                return NotFound();
            }
            else
            {
                _logger.LogInformation("song deleted successfully");
                return Ok(new { Message = "Song deleted successfully" });
            }
        }

        [Authorize(Roles = "Client")]
        // PUT: api/Songs/UpdateSong/5
        [HttpPut("UpdateSong/{id}")]
        public IActionResult PutSong(int id, Song song)
        {

            if (songService.UpdateSong(id, song))
            {
                _logger.LogInformation("song updated successfully");
                return Ok(new { Message = "data updated" });
            }
            else
            {
                _logger.LogInformation("song not updated");
                return BadRequest();
            }
        }


        //POST : api/Songs/AddFavSong
        [Authorize(Roles = "Client,User,Admin")]
        [HttpPost("AddFavSong")]
        public ActionResult AddToFavoritesSongs(FavSongDTO favSongDto)
        {
            UserSong favSong = new UserSong();
            favSong.SongId = favSongDto.SongId;
            favSong.UserId = favSongDto.UserId;

            if (!songService.ExistingUserFavSong(favSong) && favSong != null)
            {
                songService.AddToFavoritesSongs(favSong);
                _logger.LogInformation($"song added to favorites of user with user id : {favSongDto.UserId}");
                return Ok(new { Message = "ADDEDTOFAV" });
            }
            else if (songService.ExistingUserFavSong(favSong))
            {
                _logger.LogInformation($"song is already in user favorites with user id : {favSongDto.UserId}");
                return Ok(new { Message = "ALREADYINFAV" });
            }
            _logger.LogWarning($"some problem with adding to favorite");
            return BadRequest();
        }

        [Authorize(Roles = "Client,User,Admin")]
        [HttpDelete("RemoveFromFavSong")]
        public ActionResult RemoveFromFavorites(int userId, int songId)
        {
            FavSongDTO favSongDto = new FavSongDTO();
            favSongDto.SongId = songId;
            favSongDto.UserId = userId;

            UserSong favSong = new UserSong();
            favSong.SongId = favSongDto.SongId;
            favSong.UserId = favSongDto.UserId;
            var ans = songService.RemoveFromFavorites(favSong);
            if (ans)
            {
                _logger.LogWarning($"song removed from favorites of user with user id : {favSongDto.UserId}");
                return Ok("Removed From Favorites");
            }
            else
            {
                _logger.LogWarning($"problem with removing from favorite");
                return BadRequest();
            }
        }

        //get user favorites from fav songs
        //GET : api/Movies/GetFavMovies
        [Authorize(Roles = "Client,User,Admin")]
        [HttpGet("GetFavSongs/{id}")]
        public IEnumerable<SongDTO> GetFavSongs(int id)
        {
            _logger.LogWarning($"favorite songs of user having user id : {id} sent");
            return songService.GetFavSongs(id);
        }

        [HttpGet("GetSearchedSongs/{searchKey}")]
        public IEnumerable<SongDTO> GetSearchedSongs(string searchKey)
        {
            _logger.LogWarning($"search result for search key : {searchKey}");
            return songService.GetSearchedSongs(searchKey);
        }


        //increase Likes

        [AllowAnonymous]
        [HttpPatch("IncreaseLike")]
        public ActionResult IncreaseLike(int songId)
        {
            var ans = songService.IncreaseLike(songId);
            if (ans)
            {
                _logger.LogWarning($"like increased for song having song id : {songId}");
                return Ok("Like incresed");
            }
            return BadRequest();
        }

        //decrease like

        [AllowAnonymous]
        [HttpPatch("DecreaseLike")]
        public ActionResult DecreaseLike(int songId)
        {
            var ans = songService.DecreaseLike(songId);
            if (ans)
            {
                _logger.LogWarning($"like decreased for song having song id : {songId}");
                return Ok("Like Decreased");
            }
            return BadRequest();

        }

    }
}