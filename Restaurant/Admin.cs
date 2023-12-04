using Newtonsoft.Json;
using System.Globalization;

public class Admin : User, IUserOperations
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
        base.UserMenu();
        Console.WriteLine("| 6. Admin Menu                  |");
        Console.WriteLine("+--------------------------------+");
    }

    public override bool UserInput(User currentUser, List<Table> tables)
    {
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("(1.) Make Reservation");
                    Console.WriteLine("(2.) Cancel Reservation");
                    string reservationInput = Console.ReadLine();
                    switch (reservationInput)
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
                            break;
                        case "2":
                            Reserve.CancelReservation(currentUser);
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please select a valid option.");
                            break;
                    }
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
                    bool adminMenu = true;
                    while (adminMenu)
                    {
                        AdminMenu();
                        Console.Write("Please select an option (1/2/3): ");
                        adminMenu = AdminInput();
                    }
                    return true;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    return true;
            }
        }
    }

    protected virtual void AdminMenu()
    {
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
