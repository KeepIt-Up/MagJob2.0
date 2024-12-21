public class GetOrganizationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Archived { get; set; }
    public string OwnerId { get; set; }
    public byte[]? ProfileImage { get; set; }
    public byte[]? BannerImage { get; set; }
}