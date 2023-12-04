
using Newtonsoft.Json;
using System.Globalization;

public class SuperAdmin : Admin
{
    [JsonProperty("IsSuperAdmin", Order = 5)]
    public bool IsSuperAdmin { get; set; }


    public SuperAdmin(string username, string email, string password)
        : base(username, email, password)
    {
        IsAdmin = true;
        IsSuperAdmin = true;
    }

    public override void UserMenu()
    {
        base.UserMenu();
    }

    public override bool UserInput(User currentUser, List<Table> tables)
    {
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Information.DisplayMap();
                    bool Incomplete = true;
                    while (Incomplete)
                    {
                        Console.Write("Enter date and time (yyyy-MM-dd HH:mm) or exit: ");
                        userInput = Console.ReadLine() + ":00";
                        string format = "yyyy-MM-dd HH:mm:ss";
                        if (DateTime.TryParseExact(userInput, format, null, DateTimeStyles.None, out DateTime result))
                        {
                            Console.WriteLine("DateTime using DateTime.TryParseExact: " + result);
                            Tuple<DateTime, DateTime> reservation_time = new Tuple<DateTime, DateTime>(result, result.AddHours(1));
                            Reserve.MakingReservation(currentUser.Name, currentUser.Email, tables, reservation_time);
                            Incomplete = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format");
                        }
                    }
                    HasReserved = true;
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "2":
                    Menu.Display_menu();
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "3":
                    Information.ContactInfo();
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "4":
                    Console.WriteLine("Log out successful.");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                case "5":
                    Console.WriteLine("Exiting the app. Goodbye!");
                    Environment.Exit(0);
                    return true;
                case "6":
                    AdminMenu();
                    return true;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    return true;
            }
        }
    }

    protected override void AdminMenu()
    {
        bool AdminMenu = true;
        while (AdminMenu)
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|  Welcome Admin                 |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| Options:                       |");
            Console.WriteLine("| 1. Add item to Menu            |");
            Console.WriteLine("| 2. Remove item from Menu       |");
            Console.WriteLine("| 3. return                      |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| 4. Create Admin User           |");
            Console.WriteLine("| 5. Delete Admin User           |");
            Console.WriteLine("+--------------------------------+");
            Console.Write("Please select an option (1/2/3/4/5):  ");
            AdminMenu = AdminInput();
        }
    }

    protected override bool AdminInput()
    {
        string AdminInput = Console.ReadLine();
        switch (AdminInput)
        {
            case "3":
                return false;
            case "4":
                CreateAdmin();
                return true;
            case "5":
                DeleteAdmin();
                return true;
            default:
                base.AdminInput();
                return true;
        }
    }

    private void CreateAdmin()
    {
        Signup usersignup = new Signup();
        Console.WriteLine("What is the name of the Admin?");
        string Name = Console.ReadLine();
        Console.WriteLine("What is the Email of the admin?");
        string Email = Console.ReadLine();
        Console.WriteLine("What is the password of the admin");
        string Password = Console.ReadLine();
        usersignup.SignUp(Name, Email, Password, true);
        Console.ReadKey();
        Console.Clear();
    }

    private void DeleteAdmin()
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
        Console.WriteLine("What is the Email of the admin you wish to delete?");
        string Email = Console.ReadLine();
        foreach (User user in users)
        {
            if (user.Email == Email)
            {
                users.Remove(user);
                Console.WriteLine("TEST");
                break;
            }  
        }
        string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Admin has been successfully deleted.");
        Console.ReadKey();
        Console.Clear();
    }
}
