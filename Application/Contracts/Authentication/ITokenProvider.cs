using Domain.Users;

namespace Application.Contracts.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}