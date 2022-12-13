using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestrauntServer.Helpers;
using RestrauntServer.Services;

namespace RestrauntServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private MenuService _menuService;
        
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        // GET: Menu
        public async Task<IActionResult> Index()
        {
            var result = JsonHelper.ConvertToJsonString(await _menuService.GetMenu());

            if (string.IsNullOrWhiteSpace(result.Content))
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<IActionResult> Details(int id)
        {
            var result = JsonHelper.ConvertToJsonString(await _menuService.GetDishDetails(id));
            if (string.IsNullOrWhiteSpace(result.Content))
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }

        [HttpGet]
        [Route("Categories")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        // GET: Menu
        public async Task<IActionResult> Categories()
        {
            var result = JsonHelper.ConvertToJsonString(await _menuService.GetCategories());

            if (string.IsNullOrWhiteSpace(result.Content))
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }

    }
}
