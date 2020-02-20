using Microsoft.AspNetCore.Mvc;

namespace dotNetCore31.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok("Get All Customers");
        }
    }
}