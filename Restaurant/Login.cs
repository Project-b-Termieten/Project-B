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

    public Login()
    {
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
            Console.WriteLine("Invalid email format. Please enter a valid email address.");
            return null;
        }

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        // Load user information from the JSON file
        string filePath = "C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\User_info.json";

        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("User data file not found. Please create an account first.");
            return null;
        }

        try
        {
            // Read all text from the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON data to a List of User objects using Newtonsoft.Json
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            // Find the user with the entered email
            User storedUser = users?.FirstOrDefault(user => user.Email == email);

            if (storedUser != null && password == storedUser.Password)
            {
                // Identify the type of the user
                if (storedUser is SuperAdmin superAdmin)
                {
                    Console.WriteLine($"Welcome, Super Admin {superAdmin.Name}!");
                }
                else if (storedUser is Admin admin)
                {
                    Console.WriteLine($"Welcome, Admin {admin.Name}!");
                }
                else if (storedUser is User regularUser)
                {
                    Console.WriteLine($"Welcome, User {regularUser.Name}!");
                }

                // Return the deserialized user with the correct type
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
