using FluentValidation;

namespace SupWarden.Ressource.Ressources.ShareRessources.Delete;

public class DeleteShareValidation : AbstractValidator<DeleteShareResource>
{
    public DeleteShareValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.VaultId).NotEmpty().WithMessage("VaultId is required.");
    }
}