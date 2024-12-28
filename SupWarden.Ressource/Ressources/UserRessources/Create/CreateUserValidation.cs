using FluentValidation;

namespace SupWarden.Ressource.Ressources.UserRessources.Create;

public class CreateUserValidation : AbstractValidator<CreateUserRessource>
{
    public CreateUserValidation()
    {
        RuleFor(x => x.FirstName).NotNull().MaximumLength(50).NotEmpty().WithMessage("The FirstName cannot be empty.");
        RuleFor(x => x.LastName).NotNull().MaximumLength(50).NotEmpty().WithMessage("The LastName cannot be empty.");
        RuleFor(x => x.Email).NotNull().WithMessage("The Email field is required.")
           .NotEmpty().WithMessage("The Email cannot be empty.")
           .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.UserName)
            .NotNull().WithMessage("The UserName field is required.")
            .NotEmpty().WithMessage("The UserName cannot be empty.");
        RuleFor(x => x.Password).NotNull().MinimumLength(10).WithMessage("The Password Must Have 10 Chars Minimum")
            .NotEmpty().WithMessage("The Password cannot be empty.");
    }
}