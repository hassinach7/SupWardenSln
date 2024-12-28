using AutoMapper;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.User;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.Dto.GroupDtos;
using SupWarden.UI.ViewModels.Group;
using SupWarden.UI.ViewModels.User;
using SupWarden.UI.ViewModels.Vault;

namespace SupWarden.UI.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<ElementDto, UpdateElementVM>();
        this.CreateMap<ElementDto, DeleteElementVM>();
        this.CreateMap<VaultDto, UpdateVaultVM>();
        this.CreateMap<VaultDto, DeleteVaultVM>();
        this.CreateMap<GroupDto, DeleteGroupeVM>();
        this.CreateMap<GroupDto, UpdateGroupeVM>();
        this.CreateMap<UserDto, UpdateProfileVM> ();
        this.CreateMap<UpdateProfileVM, UserDto>();
        this.CreateMap<CreateUserVM, UserDto>();

    }
}