using AutoMapper;
using InventoryApi.DTOs;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Create a new order
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);

            try
            {
                var created = await _repo.AddAsync(order);
                var orderToReturn = _mapper.Map<OrderDto>(created);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderToReturn.OrderId }, orderToReturn);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Get order by Id
        /// </summary>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return NotFound();
            var result = _mapper.Map<OrderDto>(order);
            return Ok(result);
        }


        /// <summary>
        /// Get all orders
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _repo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(result);
        }
    }
}
