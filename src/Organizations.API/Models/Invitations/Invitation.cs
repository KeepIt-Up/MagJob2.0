using Organizations.API.Common.Models;

namespace Organizations.API.Models;

public class Invitation : BaseEntity<int>
{
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public string UserId { get; set; }
    public InvitationStatus Status { get; set; }

    private Invitation()
    {
        Status = InvitationStatus.Pending;
    }

    public static Invitation Create(int organizationId, string userId)
    {
        return new Invitation
        {
            OrganizationId = organizationId,
            UserId = userId
        };
    }

    public Member Accept()
    {
        if (Status != InvitationStatus.Pending)
            throw new InvalidOperationException($"Invitation with ID {Id} is not pending.");

        return Organization.AddMember(UserId, "", "");
    }

    public void Reject()
    {
        if (Status != InvitationStatus.Pending)
            throw new InvalidOperationException($"Invitation with ID {Id} is not pending.");

        Status = InvitationStatus.Rejected;
    }

}

public enum InvitationStatus
{
    Pending,
    Accepted,
    Rejected,
    Cancelled
}