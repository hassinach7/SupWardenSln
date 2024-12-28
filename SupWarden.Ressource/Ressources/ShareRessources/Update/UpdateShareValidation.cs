using FluentValidation;
using SupWarden.Ressource.Ressources.ShareRessources.Update;

namespace SupWarden.API.Validators;

public class UpdateShareValidation : AbstractValidator<UpdateShareResource>
{
    public UpdateShareValidation()
    {
        RuleFor(x => x.Permission).IsInEnum().WithMessage("Permission must be a valid enum value.");
        RuleFor(x => x.VaultId).NotEmpty().WithMessage("VaultId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("VaultId is required.");
    }
}
