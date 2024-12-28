using FluentValidation;

namespace SupWarden.Ressource.Ressources.GroupeAssignmentRessources.Delete;

public class DeleteGroupeAssignmentValidation : AbstractValidator<DeleteGroupeAssignmentRessource>
{
    public DeleteGroupeAssignmentValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId is required.");
    }
}