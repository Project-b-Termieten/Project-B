class Admin : User
{
    public bool Is_Admin { get; set; }
    public bool Is_Super_Admin { get; set; }
    public Admin(string name, string email, string password, bool isAdmin, bool isSuperAdmin)
    {
        Name = name;
        Email = email;
        Password = password;
        Is_Admin = isAdmin;
        Is_Super_Admin = isSuperAdmin;
    }
}