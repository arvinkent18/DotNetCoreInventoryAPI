namespace Inventory.Application.Exceptions;
public class ProductAlreadyExistsException : Exception
{
    public ProductAlreadyExistsException()
            : base("The product already exists.")
    {
    }

    public ProductAlreadyExistsException(string message)
        : base(message)
    {
    }

    public ProductAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
