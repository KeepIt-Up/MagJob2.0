public class GetOrganizationResponse
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public byte[]? ProfileImage { get; set; }
    public byte[]? BannerImage { get; set; }
}