using FluentValidation;

namespace SupWarden.Ressource.Ressources.VaultRessources.Create;

public class CreateVaultValidation : AbstractValidator<CreateVaultRessource>
{
    public CreateVaultValidation()
    {
        RuleFor(x => x.Label).NotNull().MaximumLength(50).NotEmpty().WithMessage("The Label cannot be empty.");
    }
}