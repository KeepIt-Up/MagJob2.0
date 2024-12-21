namespace Organizations.Application.Features.Users.GetUserOrganizations;

public sealed class GetUserOrganizationsResponse
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    // Add other relevant identity properties you want to expose
}