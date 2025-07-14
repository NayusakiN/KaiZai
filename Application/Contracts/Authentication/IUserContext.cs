namespace Application.Contracts.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
}