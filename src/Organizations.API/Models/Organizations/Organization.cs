using Npgsql.Replication;
using Organizations.API.Common.Models;

namespace Organizations.API.Models
{
    public class Organization : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Archived { get; set; } = false;

        public string OwnerId { get; set; }

        public List<Invitation> Invitations { get; set; } = [];
        public List<Member> Members { get; set; } = [];
        public List<Role> Roles { get; set; } = [];

        public byte[]? ProfileImage { get; set; }
        public byte[]? BannerImage { get; set; }

        private Organization() { }

        public static Organization Create(string name, string ownerId, string? description, byte[]? profileImage = null, byte[]? bannerImage = null)
        {
            //create organization
            var organization = new Organization()
            {
                Name = name,
                Description = description,
                OwnerId = ownerId,
                ProfileImage = profileImage,
                BannerImage = bannerImage
            };

            //seed default roles
            organization.Roles.AddRange(Role.GetDefaultRoles(organization.Id));

            //add owner to organization
            organization.AddMember(ownerId, null, null);

            return organization;
        }

        public void Update(string name, string? description, byte[]? profileImage, byte[]? bannerImage)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }

            if (profileImage != null)
            {
                ProfileImage = profileImage;
            }

            if (bannerImage != null)
            {
                BannerImage = bannerImage;
            }
        }

        public Invitation InviteUser(string userId)
        {
            //check user is not already a member
            if (IsMember(userId))
                throw new DomainException("User with id " + userId + " is already a member of organization with id " + Id);

            //create invitation
            var invitation = Invitation.Create(Id, userId);
            Invitations.Add(invitation);

            return invitation;
        }

        public Member AddMember(string userId, string firstName, string lastName)
        {
            var member = Member.Create(userId, Id, firstName, lastName, Roles.First(r => r.Name == "@everyone"));
            Members.Add(member);
            return member;
        }

        public Member RemoveMember(string userId)
        {
            var member = Members.FirstOrDefault(m => m.UserId == userId);
            Members.Remove(member);
            return member;
        }

        public Role AddRole(string name, string? description, string? color)
        {
            var role = Role.Create(name, Id, description, color);
            Roles.Add(role);
            return role;
        }

        public Role RemoveRole(int roleId)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);
            Roles.Remove(role);
            return role;
        }

        public bool IsMember(string userId)
        {
            return Members.Any(m => m.UserId == userId);
        }

        public bool IsInvited(string userId)
        {
            return Invitations.Any(i => i.UserId == userId);
        }
    }
}