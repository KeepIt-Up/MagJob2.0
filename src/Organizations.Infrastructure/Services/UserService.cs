// using System.Security.Claims;
// using Microsoft.EntityFrameworkCore;
// using Organizations.API.Common.Models;
// using Organizations.API.Repositories;
// using Organizations.Domain.Entities;

// public interface IUserService
// {
//     Task<PaginatedList<Invitation>> GetInvitationsByUserIdAsync(string userId, PaginationOptions options);
//     Task<User> GetUserAsync();
// }

// public class UserService : IUserService
// {
//     private readonly IInvitationRepository _invitationRepository;
//     private readonly IHttpContextAccessor _httpContextAccessor;
//     public UserService(IInvitationRepository invitationRepository, IHttpContextAccessor httpContextAccessor)
//     {
//         _invitationRepository = invitationRepository;
//         _httpContextAccessor = httpContextAccessor;
//     }

//     public async Task<PaginatedList<Invitation>> GetInvitationsByUserIdAsync(string userId, PaginationOptions options)
//     {
//         var invitations = _invitationRepository
//             .AsQueryable()
//             .Where(i => i.UserId == userId);
//         return await PaginatedList<Invitation>.CreateAsync(invitations, options);
//     }

//     public async Task<User> GetUserAsync()
//     {
//         var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         throw new NotImplementedException();
//     }
// }
