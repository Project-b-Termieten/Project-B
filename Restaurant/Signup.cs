using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class Signup
{
    public bool ValidateName(string name)
    {
        return !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[A-Za-z0-9 ]+$");
    }

    public bool ValidateEmail(string email)
    {
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
    }

    public bool ValidatePassword(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Length >= 8;
    }

    public User SignUp(bool admin)
    {
        while (true)
        {
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: (At least 8 characters)");
            string password = Console.ReadLine();
            bool valid = true; // Assume input is valid until proven otherwise

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
                // Proceed with signup
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
            else
            {
                // Prompt user to enter details again
                Console.WriteLine("Please enter your name: ");
                name = Console.ReadLine();
                Console.WriteLine("Please enter your email: ");
                email = Console.ReadLine();
                Console.WriteLine("Please enter your password: (At least 8 characters)");
                password = Console.ReadLine();

                // Check if the user wants to retry or cancel signup
                Console.WriteLine("Do you want to retry signup? (yes/no)");
                string choice = Console.ReadLine().ToLower();
                if (choice != "yes")
                {
                    Console.WriteLine("Signup cancelled.");
                    return null;
                }
            }
        }
    }
}
