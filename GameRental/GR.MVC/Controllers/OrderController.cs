using GR.Domains;
using GR.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using GR.Domains.Enum;
using System.Threading.Tasks;
using GR.Services.Interfaces;
using System.Diagnostics;

namespace GR.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _service;
        private readonly IGameService _gameService;

        public OrderController(ILogger<OrderController> logger, UserManager<AppUser> userManager, IOrderService service, IGameService gameService)
        {
            _logger = logger;
            _service = service;
            _gameService = gameService;
            _userManager = userManager;
        }

        // GET: Order/Index/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager
               .GetUserAsync(HttpContext.User);

            return View(await _service.GetUserOrdersAsync(user));
        }

        // GET: Order/IndexAll/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAll()
        {
            return View(await _service.GetAllOrdersAsync());
        }

        // GET: Order/Create/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Create(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var game = await _gameService
                .GetGameByIdAsync((Guid)id);

            var user = await _userManager
                .GetUserAsync(HttpContext.User);

            return View(new OrderViewModel
            {
                GameId = game.Id,
                GameTitle = game.Title,
                UserId = Guid.Parse(user.Id),
                UserName = user.UserName
            });
        }

        // POST: Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameTitle,UserName,GameId,UserId")] OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service
                    .CreateOrderAsync(model.GameTitle, model.UserName, model.GameId, model.UserId);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error");
        }

        // GET: Order/Edit/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var order = await _service.GetOrderByIdAsync((Guid)id);

            if (order == null) { return RedirectToAction("Error"); }

            return View(new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus
            });
        }

        // POST: Order/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("OrderId, OrderStatus")] OrderViewModel model)
        {
            await _service.UpdateOrderStatusByIdAsync(model.OrderId, model.OrderStatus);
            return RedirectToAction(nameof(IndexAll));
        }

        // GET: Order/Cancel/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Cancel(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var order = await _service.GetOrderByIdAsync((Guid)id);

            if (order == null) { return RedirectToAction("Error"); }

            return View(new OrderViewModel
            {
                OrderId = order.OrderId,
                GameTitle = order.GameTitle
            });
        }

        // POST: Order/Cancel/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel([Bind("OrderId")] OrderViewModel model)
        {
            await _service.UpdateOrderStatusByIdAsync(model.OrderId, OrderStatus.Canceled);
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Delete/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var order = await _service.GetOrderByIdAsync((Guid)id);

            if (order == null) {return RedirectToAction("Error");}

            return View(order);
        }

        // POST: Order/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteOrderByIdAsync(id);
            return RedirectToAction(nameof(IndexAll));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}