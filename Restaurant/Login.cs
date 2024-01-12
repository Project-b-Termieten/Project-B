using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class Login
{
    public string Email { get; set; }
    public string Password { get; set; }

    public Login(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public bool ValidateEmail(string email)
    {
        // Email should be in a valid format
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
    }

    public User PromptForLogin()
    {
        Console.Write("Enter your email: ");
        string email = Console.ReadLine();

        // Validate the entered email
        if (!ValidateEmail(email))
        {
            Console.WriteLine(@"
+--------------------------------+
| Invalid Email adress, make su- |
| re it conains an '@'.          |
+--------------------------------+");
            return null;
        }

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        // Load user information from the JSON file
        string filePath = "C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\User_info.json";

        // File error handling hier beneden
        if (!File.Exists(filePath))
        {
            Console.WriteLine("User data file not found. Please create an account first.");
            return null;
        }

        try
        {
            string json = File.ReadAllText(filePath);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            // Vinden van gebruiker op email adres
            User storedUser = users?.FirstOrDefault(user => user.Email == email);

            if (storedUser != null && password == storedUser.Password)
            {
                // Identificeren van type account
                if (storedUser is SuperAdmin superAdmin)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, Super Admin {superAdmin.Name}!");
                }
                else if (storedUser is Admin admin)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, Admin {admin.Name}!");
                }
                else if (storedUser is User regularUser)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, User {regularUser.Name}!");
                }

                return storedUser;
            }
            else
            {
                Console.WriteLine("Login failed. Incorrect email or password.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading or deserializing user data: {ex.Message}");
            return null;
        }
    }
}
