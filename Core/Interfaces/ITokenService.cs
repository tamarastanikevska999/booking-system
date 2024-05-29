using Core.DomainModels;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);

    }
}
