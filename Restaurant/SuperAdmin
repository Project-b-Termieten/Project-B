
using Newtonsoft.Json;

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
                    Reserve.MakingReservation(currentUser.Name, currentUser.Email, tables, currentUser.Time); //26 oktober ->currentUser.Time);
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
            Console.WriteLine("| 5. Upgrade User to Admin       |");
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
                UpgradeUser();
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

    private void UpgradeUser()
    {
        User UserForUpgrade = null;
        Login userlogin = new Login();
        UserForUpgrade = userlogin.PromptForLogin();
        if (UserForUpgrade is null)
            return;
        Admin newAdmin = new Admin(UserForUpgrade.Name, UserForUpgrade.Email, UserForUpgrade.Password);
        string filePath = "C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\User_info.json";
        string json = File.ReadAllText(filePath);
        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
        foreach (User user in users)
        {
            if (UserForUpgrade.Email == user.Email)
            {
                users.Remove(user);
                break;
            }
        }
        users.Add(newAdmin);
        string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine($"User {newAdmin.Name} has been promoted to Admin!");
    }
}
