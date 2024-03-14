using Domain.Common;

namespace Domain.ProductAggregate;

public class ProductInventory : ValueObject
{
    private ProductInventory() { }
    public int Quantity { get; private set; }
    
    
    public static ProductInventory CreateProductInventory(int quantity)
    {
        return new ProductInventory
        {
            Quantity = quantity
        };
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void RemoveProductInventory(int quantity = 1)
    {
        Quantity -= quantity;
    }
    
    
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Quantity;
    }
}