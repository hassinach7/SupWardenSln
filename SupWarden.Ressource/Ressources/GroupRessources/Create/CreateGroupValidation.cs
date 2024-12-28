using FluentValidation;


namespace SupWarden.Ressource.Ressources.GroupRessources.Create;

public class CreateGroupValidation : AbstractValidator<CreateGroupRessource>
{
    public CreateGroupValidation()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50).NotEmpty().WithMessage("The Name cannot be empty.");
    }
}