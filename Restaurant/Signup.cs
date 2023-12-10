using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class Signup
{
    public bool ValidateName(string name)
    {
        // Name should contain letters and spaces only
        return !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[A-Za-z ]+$");
    }

    public bool ValidateEmail(string email)
    {
        // Email should be in a valid format
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
    }

    public bool ValidatePassword(string password)
    {
        // Password should have at least 8 characters
        return !string.IsNullOrEmpty(password) && password.Length >= 8;
    }

    public User SignUp(string name, string email, string password, bool admin)
    {
        while (true)
        {
            bool valid = true;

            if (!ValidateName(name))
            {
                Console.WriteLine("Invalid name format. Name should contain letters and spaces only.");
                valid = false;
            }

            if (!ValidateEmail(email))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                valid = false;
            }

            if (!ValidatePassword(password))
            {
                Console.WriteLine("Invalid password format. Password should have at least 8 characters.");
                valid = false;
            }

            if (valid)
            {
                string filePath = @"../../../User_info.json";

                List<User> users = new List<User>();

                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                    users = JsonConvert.DeserializeObject<List<User>>(existingData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                }

                User newUser;

                if (admin)
                {
                    newUser = new Admin(name, email, password);
                }
                else
                {
                    newUser = new User(name, email, password);
                }

                users.Add(newUser);

                string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                File.WriteAllText(filePath, jsonString);

                Console.WriteLine("Signup successful! User information saved to User_info.json.");
                return newUser;
            }
        }
    }
}
