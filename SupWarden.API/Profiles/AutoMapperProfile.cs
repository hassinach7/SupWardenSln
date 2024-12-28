using AutoMapper;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.GroupeAssignment;
using SupWarden.Dto.Dtos.Share;
using SupWarden.Dto.Dtos.User;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.Dto.GroupDtos;
using SupWarden.Ressource.Ressources.ElementRessources.Create;
using SupWarden.Ressource.Ressources.ElementRessources.Update;

namespace SupWarden.API.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<User, UserDto>();
        this.CreateMap<Vault, VaultDto>();
        this.CreateMap<Element, ElementDto>();
        this.CreateMap<CreateElementRessource, Element>();
        this.CreateMap<UpdateElementRessource, Element>();

        // Mapping Vault Items
        this.CreateMap<User, ShareUserDto>();
        this.CreateMap<Vault, ShareVaultDto>();
        this.CreateMap<Share, ShareDto>();

        // Mapping Group Items
        this.CreateMap<User, GroupeUserDto>();
        this.CreateMap<GroupeAssignment, GroupeGroupeAssignmentDto>();
        this.CreateMap<Group, GroupDto>();


        // Mapping GroupAssignement Items
        this.CreateMap<User, GroupeAssignmentUserDto>();
        this.CreateMap<Group, GroupeAssignmentGroupDto>();
        this.CreateMap<GroupeAssignment, GroupeAssignmentDto>();

    }
}
