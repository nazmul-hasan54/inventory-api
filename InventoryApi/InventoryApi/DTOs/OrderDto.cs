using InventoryApi.Entities;

namespace InventoryApi.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
