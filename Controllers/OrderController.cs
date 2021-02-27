using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models;
using TestWebApi.Services;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IPostOrderService _postOrderService;

        public OrderController(IPostOrderService postOrderService)
        {
            _postOrderService = postOrderService;
        }

        [HttpPost]
        [Route("talabat")]
        public async Task<ActionResult> PostTalabatOrderModel()
        {
            bool success = await _postOrderService.Post(SystemType.Talabat, Request.Body);
            if(success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("zomato")]
        public async Task<ActionResult<OrderModel>> PostZomatoOrderModel()
        {
            bool success = await _postOrderService.Post(SystemType.Zomato, Request.Body);
            if(success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("uber")]
        public async Task<ActionResult<OrderModel>> PostUberOrderModel()
        {
            bool success = await _postOrderService.Post(SystemType.Uber, Request.Body);
            if(success)
                return Ok();
            else
                return BadRequest();
        }
    }
}
