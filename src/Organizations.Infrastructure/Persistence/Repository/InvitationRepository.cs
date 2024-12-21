using Organizations.Domain.Entities;

namespace Organizations.Infrastructure.Persistence.Repository;

public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    private readonly ApplicationDbContext _context;

    public InvitationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Invitation?> GetWithOrganizationByIdAsync(Guid id)
    {
        return await _context.Invitations.Include(i => i.Organization).FirstOrDefaultAsync(i => i.ID == id);
    }
}
