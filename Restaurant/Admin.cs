using Newtonsoft.Json;
public class Admin : User
{
    public bool Superadmin { get; set; }

    public Admin(string name, string email, string password) : base(name, email, password)
    {
        Superadmin = false;
    }
    public Admin(string name, string email, string password, bool superadmin) : base(name, email, password)
    {
        Superadmin = superadmin;
    }

}
