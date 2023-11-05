
using System.Text.Json;
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

    public void PromptForLogin()
    {
        Console.Write("Enter your email: ");
        Email = Console.ReadLine();

        // Validate the entered email
        if (!ValidateEmail(Email))
        {
            Console.WriteLine("Invalid email format. Please enter a valid email address.");
            return; // Return immediately if the email is invalid.
        }

        Console.Write("Enter your password: ");
        Password = Console.ReadLine();

        // Load user information from the JSON file
        string filePath = "C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\User_info.json";

        if (File.Exists(filePath))
        {
            // Deserialize the JSON data to a List of User objects
            List<User> users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(filePath));

            // Find the user with the entered email
            User storedUser = users.FirstOrDefault(user => user.Email == Email);

            if (storedUser != null && Password == storedUser.Password)
            {
                Console.WriteLine("Welcome!");
            }
            else
            {
                Console.WriteLine("Login failed. Incorrect email or password.");
            }
        }
        else
        {
            Console.WriteLine("User data file not found. Please sign up first.");
        }
    }
}