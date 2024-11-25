namespace Application.CustomExceptions
{
    public class ConflictException(string message) : Exception(message)
    {
    }
}
