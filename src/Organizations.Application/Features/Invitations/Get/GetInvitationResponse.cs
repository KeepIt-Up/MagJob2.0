namespace Organizations.Application.Features.Invitations.Get;

public sealed class GetInvitationResponse
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public string OrganizationName { get; set; }
    public string UserName { get; set; }
    public InvitationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}