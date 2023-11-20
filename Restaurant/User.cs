using Newtonsoft.Json;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonProperty("Admin")]
    public bool Admin { get; set; }

    [JsonProperty("Superadmin")]
    public bool Superadmin { get; set; }

    // Parameterless constructor
    public User()
    {
        // Initialize default values if needed
        Admin = false;
        Superadmin = false;
    }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Admin = false;
        Superadmin = false;
    }

    public User(string name, string email, string password, bool admin)
    {
        Name = name;
        Email = email;
        Password = password;
        Admin = admin;
        Superadmin = false;
    }
}
