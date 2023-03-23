using Domain.Commons;

namespace Domain.Entities;
public class Product : Auditable
{
    public string Name { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
