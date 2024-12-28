using SupWarden.Dto.Dtos.Element;
using SupWarden.UI.ViewModels.Element;
using SupWarden.UI.ViewModels.Vault;


namespace SupWarden.UI.Services.Contracts
{
    public interface IElementService
    {
        Task<IEnumerable<ElementDto>?> GetElementsAsync();
        Task<ReturnCreatedElementVM?> CreateElementAsync(CreateElementVaultVM elementVM);
        Task<ReturnUpdatedElementVM?> UpdateElementAsync(UpdateElementVM elementVM);
        Task<bool> DeleteElementAsync(string elementId);
        ValueTask<ElementDto> GetElementByIdAsync(string id);
    }
}
