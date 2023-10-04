using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Packaging.Signing;
using PopcornBackend.DTO;
using PopcornBackend.Models;
using PopcornBackend.Services;
using System.Data;

namespace PopcornBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly ITvShowService _TvShowService;
        private readonly ILogger<TvShowsController> _logger;
        public TvShowsController(ITvShowService tvShowService, ILogger<TvShowsController> logger)
        {
            this._TvShowService = tvShowService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("/getAllTvShows")]
        public ActionResult<List<TvShowDto>> GetAllTvShows() 
        {
            var tvShows = _TvShowService.GetTvShows();
            if (tvShows.Count>0)
            {
                _logger.LogInformation("sending tv show data");
                return tvShows;
            }
            _logger.LogInformation("NO TVSHOWS FOUND");
            return Ok("NO TVSHOWS FOUND");
        }

        [Authorize(Roles = "Client,User,Admin")]
        [HttpGet("/getTvShow/{id}")]
        public ActionResult<TvShowDto> GetTvShow(long id) 
        {
            var tvShowById = _TvShowService.GetTvShow(id);
            if (tvShowById.TvShowId != 0)
            {
                _logger.LogInformation($"sending tv show with id : {tvShowById.TvShowId}");
                return tvShowById;
            }
            _logger.LogInformation($"NO TVSHOW FOUND");
            return Ok("NO TVSHOW FOUND");

        }

        [Authorize(Roles = "Client")]
        [HttpGet("/getAllClientTvShows/{id}")]
        public ActionResult<List<TvShowDto>> GetTVShowsOfClient(int id)
        {
            var tvShowOfClient = _TvShowService.GetTVShowsOfClient(id);
            if (tvShowOfClient.Count > 0)
            {
                _logger.LogInformation($"sending tv show added by client having client id : {id}");
                return tvShowOfClient;
            }
            _logger.LogInformation($"no tv show added by client having client id : {id} found");

            return Ok("NO TVSHOW FOUND");
        }

        [Authorize(Roles = "Client")]
        [HttpPost("/addTvShow")]
        public IActionResult AddTvShow(TvShowDto tvShowDto)
        {
            if(_TvShowService.AddTvShow(tvShowDto))
            {
                _logger.LogInformation($"tv show added");
                return Ok(new {Message="Tv Show added successfully"});
            }
            _logger.LogInformation($"Cannot Add Tv Show");
            return Ok("Cannot Add Tv Show");
        }

        [Authorize(Roles = "Client")]
        [HttpPut("/updateTvShow/{id}")]
        public IActionResult UpdateTvShow(int id , TvShowDto tvShow)
        {
            var UpdateTvShow = _TvShowService.UpdateTvShow(id, tvShow);

            if(UpdateTvShow)
            {
                _logger.LogInformation($"tv show updated successfully");
                return Ok(new { Message = "Tv Show updated successfully" });
            }

            _logger.LogInformation($"TV SHOW NOT FOUND");
            return Ok("TV SHOW NOT FOUND");
        }

        [Authorize(Roles = "Client")]
        [HttpDelete("/deleteTvShow/{id}")]
        public IActionResult DeleteTvShow(long id)
        {
            if(_TvShowService.DeleteTvShow(id))
            {
                _logger.LogInformation($"Tv Show Deleted successfully");
                return Ok(new { Message = "Tv Show Deleted successfully" });
            }
            _logger.LogInformation($"Tv Show not found");
            return Ok("NOT FOUND");
        }

        [Authorize(Roles = "User,Client,Admin")]
        [HttpGet("/getUserFav/{id}")]
        public ActionResult<List<TvShowDto>> GetFavTvShows(int id) 
        {
            var UserFavTvShows = _TvShowService.GetFavTvShows(id);
            if(UserFavTvShows.Count>0)
            {
                _logger.LogInformation($"favorites of user with id : {id}");
                return Ok(UserFavTvShows);
            }
            _logger.LogInformation($"favorites of user with id : {id} not found");
            //return Ok(new {message="NO FAVOURITES ADDED"});
            return Ok(UserFavTvShows);
        }

        
        [HttpGet("/GetSearchedTvShows/{searchKey}")]
        public IEnumerable<TvShowDto> GetSearchedTvShows(string searchKey)
        {
            _logger.LogInformation($"searched tv show returned");
            return _TvShowService.GetSearchedTvShows(searchKey);
        }

        [Authorize(Roles = "User,Client,Admin")]
        [HttpPost("/addtoFavTvShow")]
        public ActionResult AddToFav(FavTvShowDto favTvShowDto) 
        { 
            UserTvShow userTvShow = new UserTvShow();
            userTvShow.UserId = favTvShowDto.UserId;
            userTvShow.TvShowId = favTvShowDto.TvShowId;
            var x = _TvShowService.AddToFav(userTvShow);
            if(x)
            {
                _logger.LogInformation($"tv show added to favorites");
                return Ok(new { status = 200, isSuccess = true, message = "ADDED" });
            }
            _logger.LogInformation($"tv show is already in user favorites");
            return Ok(new {status=200,isSuccess = true , message = "ALREADY ADDED"});
        
        }

        [Authorize(Roles = "User,Client,Admin")]
        [HttpDelete("/removeFromFav")]
        public ActionResult RemoveFromFav(int userId, int tvShowId)
        {
            FavTvShowDto favTvShowDto = new FavTvShowDto();
            favTvShowDto.TvShowId = tvShowId;
            favTvShowDto.UserId = userId;   

            UserTvShow userTvShow = new UserTvShow();
            userTvShow.UserId = favTvShowDto.UserId;
            userTvShow.TvShowId = favTvShowDto.TvShowId;
            var x = _TvShowService.RemoveFromFav(userTvShow);
            if (x)
            {
                _logger.LogInformation($"tv show is removed from user favortites");
                return Ok("Removed From Favourite");
            }
            _logger.LogWarning($"tv show not found in favorites of user");
            return Ok("Cannot remove from fav");
        }

        [AllowAnonymous]
        [HttpPatch("/increaseLike/{id}")]
        public ActionResult IncreaseLike(int id) 
        { 
            if(_TvShowService.IncreaseLike(id))
            {
                _logger.LogInformation($"tv show like incerased");
                return Ok("Increased Like");
            }
            
            return Ok("NO TV SHOW FOUND");
        
        }

        [AllowAnonymous]
        [HttpPatch("/decreaseLike/{id}")]
        public ActionResult DecreaseLike(int id)
        {
            if (_TvShowService.DecreaseLike(id))
            {
                _logger.LogInformation($"tv show like decreased");
                return Ok("Decreased Like");
            }
                
            return Ok("NO TV SHOW FOUND");
        }

    }
}
