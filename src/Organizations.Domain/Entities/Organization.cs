using Microsoft.EntityFrameworkCore;

namespace Organizations.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Organization : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public byte[]? ProfileImage { get; set; }
    public byte[]? BannerImage { get; set; }
    public List<Invitation> Invitations { get; set; } = [];
    public List<Member> Members { get; set; } = [];
    public List<Role> Roles { get; set; } = [];

    private Organization() { }

    public static Organization Create(string name, Guid ownerId, string? description, byte[]? profileImage = null, byte[]? bannerImage = null)
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
        organization.Roles.AddRange(Role.GetDefaultRoles(organization.ID));

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

        Description = description;

        if (profileImage != null)
        {
            ProfileImage = profileImage;
        }

        if (bannerImage != null)
        {
            BannerImage = bannerImage;
        }
    }

    public Invitation InviteUser(Guid userId)
    {
        //check user is not already a member
        if (IsMember(userId))
            throw new DomainException("User with id " + userId + " is already a member of organization with id " + ID);

        //create invitation
        var invitation = Invitation.Create(ID, userId);
        Invitations.Add(invitation);

        return invitation;
    }

    public Member AddMember(Guid userId, string firstName, string lastName)
    {
        var member = Member.Create(userId, ID, firstName, lastName, Roles.First(r => r.Name == "@everyone"));
        Members.Add(member);
        return member;
    }

    public Member RemoveMember(Guid userId)
    {
        var member = Members.FirstOrDefault(m => m.UserId == userId);
        Members.Remove(member);
        return member;
    }

    public Role AddRole(string name, string? description, string? color)
    {
        var role = Role.Create(name, ID, description, color);
        Roles.Add(role);
        return role;
    }

    public Role RemoveRole(Guid roleId)
    {
        var role = Roles.FirstOrDefault(r => r.ID == roleId);
        Roles.Remove(role);
        return role;
    }

    public bool IsMember(Guid userId)
    {
        return Members.Any(m => m.UserId == userId);
    }

    public bool IsInvited(Guid userId)
    {
        return Invitations.Any(i => i.UserId == userId);
    }
}