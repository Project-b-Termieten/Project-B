public class Person
{
    string Name { get; set; }
    string Email { get; set; }
    string Password { get; set; }

    public Person(string name,string email,string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

}
