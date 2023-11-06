using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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

    public User SignUp(string name, string email, string password)
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
                    users = JsonSerializer.Deserialize<List<User>>(existingData);
                }
                // Create a new User object for the new use
                var newUser = new User(name, email, password);
                // Add the new user to the list of existing users
                users.Add(newUser);
                string jsonString = JsonSerializer.Serialize(users);

                // Write the JSON data back to the file, overwriting the existing data
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine("Signup successful! User information saved to User_info.json.");
                return newUser;
            }
            else
            {
                Console.WriteLine("Signup failed. Please check the error messages for details.");
                Console.WriteLine("Please try again.");
                Console.WriteLine("Please enter your name: ");
                name = Console.ReadLine();
                Console.WriteLine("Please enter your email: ");
                email = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                password = Console.ReadLine();
            }
        }
    }

}
