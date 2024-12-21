

namespace Organizations.Infrastructure.Persistence.Repository;

internal class MemberRepository : BaseRepository<Member>, IMemberRepository
{

    public MemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public async Task ArchiveMemberAsync(Guid id)
    // {
    //     await _context.Members.Where(m => m.ID == id).ExecuteUpdateAsync(m => m.SetProperty(m => m.Archived, true));
    // }
}