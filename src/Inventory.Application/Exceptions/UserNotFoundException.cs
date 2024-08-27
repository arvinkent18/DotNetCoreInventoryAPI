namespace Inventory.Application.Exceptions;
public class UserNotFoundException : Exception
{
    private const string DefaultMessage = "User not found.";

    public UserNotFoundException(string message = DefaultMessage) : base(message)
    {
    }
}
