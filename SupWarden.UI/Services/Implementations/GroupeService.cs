using Newtonsoft.Json;
using SupWarden.Dto.GroupDtos;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Group;
using System.Text;

namespace SupWarden.UI.Services.Implementations;


public class GroupeService : IGroupeService
{
    private readonly ApiSecurity _apiSecurity;

    public GroupeService(ApiSecurity apiSecurity)
    {
        this._apiSecurity = apiSecurity;
    }

   
   

    public async Task<IEnumerable<GroupDto>?> GetAllGroupesAsync()
    {
        var response = await _apiSecurity.Http.GetAsync("group/GetAll");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<GroupDto>>();
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task<GroupDto?> GetGroupByIdAsync(string id)
    {
        var response = await _apiSecurity.Http.GetAsync($"Group/GetById?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GroupDto>();
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<ReturnCreatedGroupeVM?> CreateGroupeAsync(CreateGroupeVM groupeVM)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(groupeVM), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PostAsync("Group/Create", jsonContent);
       
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReturnCreatedGroupeVM?>();
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }


    public async Task<bool> DeleteGroupeAsync(string groupId)
    {

        var response = await _apiSecurity.Http.DeleteAsync($"Group/Delete?id={groupId}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> UpdateGroupeAsync(UpdateGroupeVM groupeVM)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(groupeVM), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PutAsync("Group/Update", jsonContent);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

    }

    public async Task<bool> AssignAsync(AssignGroupeVM request)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PostAsync("Group/Assign", jsonContent);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

    }
}
