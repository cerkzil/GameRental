using GR.Domains;
using GR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GR.MVC.Controllers
{
    [Route("Api")]
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
        // GET: Api/
        public IActionResult Index()
        {
            return View();
        }
        // GET: Api/Orders/
        [Route("Orders")]
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderService.GetAllOrdersAsync();
        }
        // GET: Api/Games/
        [Route("Games")]
        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _gameService.GetAllGamesAsync();
        }
    }
}
