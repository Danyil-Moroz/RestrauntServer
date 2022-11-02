namespace RestrauntServer.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RestrauntServer.Helpers;
    using RestrauntServer.Services;
    using System.Threading.Tasks;

    [Route("AdmminPanel")]
    public class AdminController : Controller
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        [Route("OrderDetails")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersInfo()
        {
            var result = JsonHelper.ConvertToJsonString(await _service.GetAllOrders());
            if (string.IsNullOrWhiteSpace(result.Content))
            {
                result.StatusCode = StatusCodes.Status204NoContent;
            }
            else
            {
                result.StatusCode = StatusCodes.Status200OK;
            }
            return result;
        }

        [Route("PushToNextStatus")]
        [HttpPost]
        public async Task<IActionResult> GetAllOrdersInfo([FromBody] int orderId)
        {
            var result = new ContentResult();
            if (await _service.PushToNextStatus(orderId))
            {
                result.StatusCode = StatusCodes.Status200OK;
                result.Content = "Status successfuly pushed to next status";
            }
            else
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Content = "Cannot find order or order at final status";
            }
            return result;
        }




    }
}
