using Organizations.Infrastructure.Common;

namespace Organizations.Infrastructure.Persistence.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<PaginatedList<Invitation, T>> GetInvitationsByUserIdAsync<T>(Guid userId, PaginationOptions paginationOptions, CancellationToken cancellationToken)
        {
            return await PaginatedList<Invitation, T>.CreateAsync(Context.Set<Invitation>().Where(i => i.UserId == userId), paginationOptions);
        }
    }
}