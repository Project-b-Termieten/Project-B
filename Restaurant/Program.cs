using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

public static class Program
{
    public static User currentUser = null;
    public static List<Table> tables = new List<Table>()
    {
        new Table(1, 2), new Table(2, 2), new Table(3, 2), new Table(4, 2), new Table(5, 2), new Table(6, 2), new Table(7, 2), new Table(16, 6),
        new Table(8, 2), new Table(9, 4), new Table(10, 4), new Table(11, 4),new Table(12, 4), new Table(13, 4), new Table(14, 4), new Table(15, 6)
    };

    public static void Main()
    {
/*        string filePath = "C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\User_info.json";

        // Read existing user data from the file
        List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filePath), new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        // Add the hardcoded superadmin to the list
        SuperAdmin superAdmin = new SuperAdmin("superadmin", "superadmin@example.com", "superadmin");
        users.Add(superAdmin);

        // Serialize the updated list of users with type information
        string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        // Write the updated user data back to the file
        File.WriteAllText(filePath, jsonString);*/
        while (true)
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|  Welcome to Jake’s restaurant! |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| Options:                       |");
            Console.WriteLine("| 1. Login or Signup             |");
            Console.WriteLine("| 2. Menu                        |");
            Console.WriteLine("| 3. Restaurant Information      |");
            Console.WriteLine("| 4. Exit                        |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("Please pick an option (1/2/3/4):  ");
            string userinput = Console.ReadLine();
            switch (userinput)
            {
                case "1":
                    Login_or_Signup();
                    Console.WriteLine($"{currentUser.GetType()}");
                    bool Loggedin = true;
                    while (Loggedin)
                    {
                        currentUser.UserMenu();
                        if (currentUser is Admin)
                            Console.WriteLine("Please select an option (1/2/3/4/5/6):");
                        else
                            Console.WriteLine("Please select an option (1/2/3/4/5/6):");
                        if (currentUser.UserInput(currentUser, tables) is false)
                            Loggedin = false;
                    }
                    break;
                case "2":
                    Menu.Display_menu();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    Information.ContactInfo();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    Console.WriteLine("Exiting the app. Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break;
            }
        }
    }

    public static void Login_or_Signup()
    {
        Console.WriteLine("Do you want to LOGIN(1) or Sign up(2) ");
        string user_answer = Console.ReadLine().ToLower();

        if (user_answer == "1")
        {
            Login userLogin = new Login();
            // this causes error because the PromptForLogin method does not return a User.
            currentUser = userLogin.PromptForLogin();
            if (currentUser == null)
            {
                Console.WriteLine("Please try again or sign up.");
            }

            string email = currentUser.Email;

            Console.WriteLine("Logging in with email: " + email);
        }
        else if (user_answer == "2")
        {
            Signup usersignup = new Signup();
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: (Atleast 8 characters)");
            string password = Console.ReadLine();

            // Call the SignUp method to validate and save user information
            currentUser = usersignup.SignUp(name, email, password, false);
        }
        Console.ReadKey();
        Console.Clear();
    }
    //CODE BELOW TO HARDCODE A SUPERADMIN
    //==================================

    /*string filePath = "C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\User_info.json";

// Read existing user data from the file
List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filePath), new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All
});

// Add the hardcoded superadmin to the list
SuperAdmin superAdmin = new SuperAdmin("superadmin", "superadmin@example.com", "superadmin");
users.Add(superAdmin);

// Serialize the updated list of users with type information
string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All
});

// Write the updated user data back to the file
//File.WriteAllText(filePath, jsonString);*/
}
