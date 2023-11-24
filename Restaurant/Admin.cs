using Newtonsoft.Json;

public class Admin : User
{
    [JsonProperty("IsAdmin", Order = 4)]
    public bool IsAdmin { get; set; }
    public Admin(string username, string email, string password)
        : base(username, email, password)
    {
        IsAdmin = true;
    }

    public override void UserMenu()
    {
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("|                                |");
        Console.WriteLine("|  Welcome to Jake’s restaurant! |");
        Console.WriteLine("|                                |");
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| Options:                       |");
        Console.WriteLine("| 1. Make Reservation            |");
        Console.WriteLine("| 2. Menu                        |");
        Console.WriteLine("| 3. Restaurant Informatio       |");
        Console.WriteLine("| 4. Logout                      |");
        Console.WriteLine("| 5. Exit                        |");
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| 6. Admin Menu                  |");
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("Please select an option (1/2/3/4/5/6):");
    }

    public override bool UserInput(User currentUser, List<Table> tables)
    {
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Information.DisplayMap();
                    Reserve.MakingReservation(currentUser.Name, currentUser.Email, tables);
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

    protected virtual void AdminMenu()
    {
        bool adminMenu = true;
        while (adminMenu)
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|  Welcome Admin                 |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| Options:                       |");
            Console.WriteLine("| 1. Add item to Menu            |");
            Console.WriteLine("| 2. Remove item from Menu       |");
            Console.WriteLine("| 3. Return to User Menu         |");
            Console.WriteLine("+--------------------------------+");
            Console.Write("Please select an option (1/2/3): ");
            adminMenu = AdminInput();
        }
    }

    protected virtual bool AdminInput()
    {
        string adminInput = Console.ReadLine();
        switch (adminInput)
        {
            case "1":
                Menu.Add_Item_Menu();
                Console.ReadKey();
                Console.Clear();
                return true;
            case "2":
                Menu.Remove_Item_Menu();
                Console.ReadKey();
                Console.Clear();
                return true;
            case "3":
                return false;  // Exiting the AdminMenu loop
            default:
                Console.WriteLine("Invalid input. Please select a valid option.");
                Console.ReadKey();
                Console.Clear();
                return true;
        }
    }
}
