namespace Organizations.Application.Features.Roles.Get;

public sealed class GetRoleValidator : AbstractValidator<GetRoleRequest>
{
    public GetRoleValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty()
            .WithMessage("ID is required");
    }
}