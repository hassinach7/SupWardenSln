using FluentValidation;

namespace SupWarden.Ressource.Ressources.VaultRessources.Update;

public class UpdateVaultValidation : AbstractValidator<UpdateVaultRessource>
{
    public UpdateVaultValidation()
    {
        RuleFor(x => x.Label).NotNull().MaximumLength(50).NotEmpty().WithMessage("The Label cannot be empty.");
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("The Id cannot be empty and not be null.");
    }
}