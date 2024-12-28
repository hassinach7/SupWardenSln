using FluentValidation;

namespace SupWarden.Ressource.Ressources.GroupeAssignmentRessources.Create;

public class CreateGroupeAssignmentValidation : AbstractValidator<CreateGroupeAssignmentRessource>
{
    public CreateGroupeAssignmentValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.GroupId).NotEmpty().WithMessage("VaultId is required.");
    }
}