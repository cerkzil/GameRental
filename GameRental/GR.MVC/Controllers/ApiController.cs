using GR.Domains;
using GR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GR.MVC.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IGameService _gameService;

        public ApiController(IOrderService orderService, IGameService gameService)
        {
            _orderService = orderService;
            _gameService = gameService;
        }
        // GET: api/
        public IActionResult Index()
        {
            return View();
        }
        // GET: api/orders/
        [Route("orders")]
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderService.GetAllOrdersAsync();
        }
        // GET: api/games/
        [Route("games")]
        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _gameService.GetAllGamesAsync();
        }
    }
}
