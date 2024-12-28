using FluentValidation;
using SupWarden.Ressource.Ressources.ElementRessources.Create;

namespace SupWarden.Ressource.Validators
{
    public class CreateElementValidation : AbstractValidator<CreateElementRessource>
    {
        public CreateElementValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Identifiant)
                .NotEmpty().WithMessage("Identifiant is required.")
                .MaximumLength(100).WithMessage("Identifiant cannot exceed 100 characters.");

            RuleFor(x => x.Uri)
                .NotEmpty().WithMessage("URI is required.")
                .MaximumLength(250).WithMessage("URI cannot exceed 250 characters.");

            RuleFor(x => x.Note)
                .MaximumLength(250).WithMessage("Note cannot exceed 250 characters.");

            RuleFor(x => x.VaultId).NotEmpty().NotNull().WithMessage("Vault Id is required.");
        }
    }
}
