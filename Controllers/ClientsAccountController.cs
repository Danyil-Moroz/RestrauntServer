namespace RestrauntServer.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RestrauntServer.Data;
    using RestrauntServer.Models;
    using RestrauntServer.Services;
    using RestrauntServer.Helpers;
    using Microsoft.AspNetCore.Http;
    using System;

    [Route("")]
    public class ClientsAccountController : Controller
    {
        private readonly CleintAccountService _service;

        public ClientsAccountController(CleintAccountService service)
        {
            _service = service;
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] Client client)
        {
            var result = JsonHelper.ConvertToJsonString(await _service.GetCLient(client.Email, client.Password));
            if (string.IsNullOrWhiteSpace(result.Content))
            {
                result.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                result.StatusCode = StatusCodes.Status200OK;
            }

            return result;
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] Client client)
        {
            var result = new ContentResult();
            try
            {
                if (client != null)
                {
                    if (await _service.CreateClient(client)) {

                        result.StatusCode = StatusCodes.Status200OK;

                    }
                    else
                    {
                        result.StatusCode = StatusCodes.Status400BadRequest;
                        result.Content = "User with same Email already registered";
                    }
                   
                }
                else
                {
                    result.StatusCode  = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
            }

            return result;

        }

    }
}
