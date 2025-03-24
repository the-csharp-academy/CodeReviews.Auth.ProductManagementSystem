namespace ProductManagementSystem.iamnikitakostin.Models;

public class UserForView
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string SelectedRole {  get; set; }
}
