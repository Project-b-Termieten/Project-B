using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Signup
{
    public bool ValidateName(string name)
    {
        // Name should contain letters, numbers, and spaces only
        return !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[A-Za-z0-9 ]+$");
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
            Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            email = Console.ReadLine();
            Console.WriteLine("Please enter your password: (At least 8 characters)");
            password = Console.ReadLine();

            bool valid = true;

            if (!ValidateName(name))
            {
                Console.WriteLine("Invalid name format. Name should contain letters, numbers, and spaces only.");
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
                // Valid input, proceed with signup
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

                // Ask the user to confirm the entered information
                Console.WriteLine("Please review your information:");
                Console.WriteLine($"Name: {newUser.Name}");
                Console.WriteLine($"Email: {newUser.Email}");
                Console.WriteLine($"Password {newUser.Password}");
                Console.WriteLine("Is the information correct? (Y/N)");

                string confirmation = Console.ReadLine()?.Trim().ToUpper();

                if (!string.IsNullOrEmpty(confirmation) && confirmation == "Y")
                {
                    // Write to JSON file only if information is confirmed
                    string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                    File.WriteAllText(filePath, jsonString);

                    Console.WriteLine("Signup successful! User information has been saved!.");
                    return newUser;
                }
                else if (!string.IsNullOrEmpty(confirmation) && confirmation == "N")
                {
                    Console.WriteLine("Please enter your information again.");
                }
            }

            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}
