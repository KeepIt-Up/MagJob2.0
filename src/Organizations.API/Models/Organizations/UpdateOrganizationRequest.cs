namespace Organizations.API.Models
{
    public class UpdateOrganizationRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public byte[]? ProfileImage { get; set; }
        public byte[]? BannerImage { get; set; }
    }
}
