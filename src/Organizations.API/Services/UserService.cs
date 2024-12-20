using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Models;
using Organizations.API.Models;
using Organizations.API.Repositories;

public interface IUserService
{
    Task<PaginatedList<Invitation>> GetInvitationsByUserIdAsync(string userId, PaginationOptions options);
}

public class UserService : IUserService
{
    private readonly IInvitationRepository _invitationRepository;
    public UserService(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    public async Task<PaginatedList<Invitation>> GetInvitationsByUserIdAsync(string userId, PaginationOptions options)
    {
        var invitations = _invitationRepository
            .AsQueryable()
            .Where(i => i.UserId == userId);
        return await PaginatedList<Invitation>.CreateAsync(invitations, options);
    }
}
