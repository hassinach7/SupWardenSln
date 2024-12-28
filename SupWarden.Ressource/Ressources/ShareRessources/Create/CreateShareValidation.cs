
using FluentValidation;
using SupWarden.Ressource.Ressources.ShareRessources.Create;

namespace SupWarden.API.Validators;

public class CreateShareValidation : AbstractValidator<CreateShareResource>
{
    public CreateShareValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.VaultId).NotEmpty().WithMessage("VaultId is required.");

        RuleFor(x => x.Permission).IsInEnum().WithMessage("Permission must be a valid enum value.");
    }
}