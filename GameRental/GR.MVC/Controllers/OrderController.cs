using GR.Domains;
using GR.EF;
using GR.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using GR.Domains.Enum;
using System.Threading.Tasks;
using System.Linq;
using GR.Services.Interfaces;

namespace GR.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;
        private readonly IOrderService _service;
        private readonly IGameService _gameService;

        public OrderController(ILogger<OrderController> logger, UserManager<AppUser> userManager, Context context, IOrderService service, IGameService gameService)
        {
            _logger = logger;
            _context = context;
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
            if (id == null)
            {
                return NotFound();
            }

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
            return View(model);
        }

        // GET: Order/Edit/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == id);

            return View(new OrderViewModel{
                OrderId = order.OrderId, 
                OrderStatus = order.OrderStatus});
        }

        // POST: Order/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("OrderId, OrderStatus")] OrderViewModel model)
        {
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == model.OrderId);

            order.OrderStatus = model.OrderStatus;

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAll));
        }

        // GET: Order/Cancel/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Cancel(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == id);

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
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == model.OrderId);

            order.OrderStatus = OrderStatus.Canceled;

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Delete/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == id);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAll));
        }
    }
}