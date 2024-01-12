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
        //START VAN PROGRAMMA IS HIER \/
        Startmenu();
    }

    public static void Startmenu()
    {
        while (true)
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
+--------------------------------+
|                                |
|  Welcome to Jake’s restaurant! |
|  'A Sharp Dining Experience'   |
|                                |
+--------------------------------+");
            Console.ResetColor();

            Console.WriteLine(@"
+--------------------------------+
| Please select an option:       |
| 1. Login or Signup             |
| 2. Menu                        |
| 3. Restaurant Information      |
| 4. Exit                        |
+--------------------------------+");

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
                            Console.WriteLine("Please select an option: ");
                        else
                            Console.WriteLine("Please select an option: ");
                        if (currentUser.UserInput(currentUser, tables) is false)
                            Loggedin = false;
                    }
                    break;
                case "2":
                    Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
                    Console.ReadKey();
                    if (Menu.FutureFood == null)
                    {
                        continue;
                    }
                    else
                    {
                        Menu.Display_menu(Menu.FutureFood, Menu.FutureDrink);

                    }
                    Console.Clear();
                    break;
                case "3":
                    Information.ContactInfo();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    Console.WriteLine(@"
+--------------------------------------------------------+
| Looking forward to see you at our restaurant, dag dag!!|
+--------------------------------------------------------+");


                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Invalid input. Please choose a number between 1 and 4.");
                    Console.ResetColor();

                    break;
            }
        }
    }

    public static void Login_or_Signup()
    {
        while (true)
        {
            Console.WriteLine(@"
+--------------------------------+
| Please select an option or pr- |
| ess Enter to return.           |
| 1. Login                       |
| 2. Signup                      |
+--------------------------------+");

            string user_answer = Console.ReadLine().ToLower();

            switch (user_answer)
            {
                case "1":
                    Login userLogin = new Login();
                    currentUser = userLogin.PromptForLogin();
                    if (currentUser == null)
                    {
                        Console.WriteLine("| Please try again or sign up.   |");
                        Console.WriteLine("+--------------------------------+");
                    }
                    else
                    {
                        string email = currentUser.Email;
                        Console.WriteLine("Logging in with email: " + email);
                        return; // Exit the method if login is successful.
                    }
                    break;

                case "2":
                    Signup usersignup = new Signup();
                    currentUser = usersignup.SignUp(null, null, null, false);

                    if (currentUser == null)
                    {
                        Console.WriteLine("Invalid signup. Please try again.");
                    }
                    else
                    {
                        Console.WriteLine("+----------------------------------------+");
                        Console.WriteLine("| Your information has been saved!.      |");
                        Console.WriteLine("+----------------------------------------+");
                        Console.WriteLine(@"| Logging in with email: " + currentUser.Email.PadRight(15) + @" |
+----------------------------------------+");
                        return; // Exit the method if signup is successful.
                    }
                    break;
                case "":
                    Console.Clear();
                    Startmenu();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter either 1, 2 or press ENTER to return.");
                    break;
            }
        }

        Console.ReadKey();
        Console.Clear();
    }
}


    /*//==================================
    //CODE BELOW TO HARDCODE A SUPERADMIN
    //==================================

    string filePath = "C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\User_info.json";

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


