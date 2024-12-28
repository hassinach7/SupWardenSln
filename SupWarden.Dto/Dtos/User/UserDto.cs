namespace SupWarden.Dto.Dtos.User;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PinCode { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int PhoneNumber { get; set; } 
}