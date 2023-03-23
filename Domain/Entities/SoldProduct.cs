using Domain.Commons;

namespace Domain.Entities;
public class SoldProduct : Auditable
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime SoldAt { get; set; } = DateTime.UtcNow;
}
