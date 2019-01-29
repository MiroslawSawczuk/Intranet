namespace Intranet.Authentication.Tokens
{
    public interface ITokenUser
    {
        string Id { get; }
        string Email { get; }
    }
}
