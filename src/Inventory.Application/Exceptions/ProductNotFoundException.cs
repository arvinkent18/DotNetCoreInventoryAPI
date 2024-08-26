namespace Inventory.Application.Exceptions;
public class ProductNotFoundException : Exception
{
    private const string DefaultMessage = "Product not found.";

    public ProductNotFoundException(string message = DefaultMessage) : base(message)
    {
    }
}

