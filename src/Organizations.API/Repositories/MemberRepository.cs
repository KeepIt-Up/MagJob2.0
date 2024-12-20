using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Repositories;
using Organizations.API.Data;
using Organizations.API.Models;

namespace Organizations.API.Repositories;

public interface IMemberRepository : IAsyncRepository<Member>
{
    Task ArchiveMemberAsync(int id);
}

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    private readonly OrganizationDbContext _context;

    public MemberRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task ArchiveMemberAsync(int id)
    {
        await _context.Members.Where(m => m.Id == id).ExecuteUpdateAsync(m => m.SetProperty(m => m.Archived, true));
    }
}