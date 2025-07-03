using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatGPTIntegration.Data;
using ChatGPTIntegration.Models;

namespace ChatGPTIntegration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(AppDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all orders");
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _logger.LogInformation("Creating new order for CustomerId {CustomerId}, ProductId {ProductId}",
                order.CustomerId, order.ProductId);

            // Optional: Validate customer and product exist
            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == order.CustomerId);
            var productExists = await _context.Products.AnyAsync(p => p.ProductId == order.ProductId);

            if (!customerExists || !productExists)
            {
                _logger.LogWarning("Customer or Product not found for Order");
                return BadRequest("Invalid CustomerId or ProductId");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching order with ID: {OrderId}", id);
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                _logger.LogWarning("Order not found: {OrderId}", id);
                return NotFound();
            }
            return Ok(order);
        }
    }
}
