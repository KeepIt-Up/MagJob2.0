using Organizations.API.Models;

public class InvitationResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public InvitationStatus Status { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationId { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
}