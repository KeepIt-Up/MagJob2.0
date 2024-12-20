using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Repositories;
using Organizations.API.Data;
using Organizations.API.Models;

namespace Organizations.API.Repositories;

public interface IInvitationRepository : IAsyncRepository<Invitation>
{
    Task<Invitation?> GetWithOrganizationByIdAsync(int id);
}

public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    private readonly OrganizationDbContext _context;

    public InvitationRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Invitation?> GetWithOrganizationByIdAsync(int id)
    {
        return await _context.Invitations.Include(i => i.Organization).FirstOrDefaultAsync(i => i.Id == id);
    }
}
