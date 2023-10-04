using System;
using System.Collections.Generic;
using System.Data;
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
    public class MediaCategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<MediaCategoriesController> _logger;
        public MediaCategoriesController(ICategoryService categoryService, ILogger<MediaCategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: api/MediaCategories
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<CategoryDto> GetMediaCategories()
        {
            _logger.LogInformation("Categories sent");
          return _categoryService.GetCategories();
        }  
    }
}
