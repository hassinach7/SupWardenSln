using FluentValidation;

namespace SupWarden.Ressource.Ressources.GroupRessources.Update;

public class UpdateGroupValidation : AbstractValidator<UpdateGroupRessource>
{
    public UpdateGroupValidation()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50).NotEmpty().WithMessage("The Name cannot be empty.");
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("The Id cannot be empty and not be null.");
    }
}