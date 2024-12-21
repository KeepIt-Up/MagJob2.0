namespace Organizations.Application.Features.Organizations.Update;

public sealed class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationRequest>
{
    public UpdateOrganizationValidator()
    {
        RuleFor(x => x.OrganizationId)
            .NotEmpty()
            .WithMessage("Organization ID is required");
        RuleFor(x => x.Name)
            .Must(name => name.Length <= 50)
            .WithMessage("Name must be less than 50 characters");
        RuleFor(x => x.Description)
            .Must(description => description.Length <= 1000)
            .WithMessage("Description must be less than 1000 characters");
        RuleFor(x => x.ProfileImage)
            .Must(image => image.Length <= 1024 * 1024 * 5)
            .WithMessage("Profile image must be less than 5MB");
        RuleFor(x => x.BannerImage)
            .Must(image => image.Length <= 1024 * 1024 * 5)
            .WithMessage("Banner image must be less than 5MB");
    }
}