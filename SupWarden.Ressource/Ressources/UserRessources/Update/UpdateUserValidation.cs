using FluentValidation;

namespace SupWarden.Ressource.Ressources.UserRessources.Update
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserRessource>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
