namespace Organizations.Application.Features.Organizations.Create
{
    public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
    {
        public CreateOrganizationValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
        }
    }
}