using Microsoft.VisualBasic;
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
        Console.WriteLine("| 8. Admin Menu                  |");
        Console.WriteLine("+--------------------------------+");
    }

    public override bool UserInput(User currentUser, List<Table> tables)
    {
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("+--------------------------------+");
                    Console.WriteLine("| 1. Remove Reservation          |");
                    Console.WriteLine("| 2. Change Reservation Time     |");
                    Console.WriteLine("+--------------------------------+");
                    string reservationInput = Console.ReadLine();
                    switch (reservationInput)
                    {
                        case "1":
                            Information.DisplayMap();
                            bool Incomplete = true;
                            while (Incomplete)
                            {
                                Console.Write("Enter date (yyyy-MM-dd): ");
                                string dateString = Console.ReadLine();
                                // Get the time from the user
                                Console.Write("Enter time (HH:mm): ");
                                string timeString = Console.ReadLine();
                                // Parse date and time strings
                                if (DateTime.TryParseExact(dateString + " " + timeString, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDateTime))
                                {
                                    Reserve.ShowReservationsForDay(selectedDateTime);

                                    //Console.WriteLine("DateTime using DateTime.TryParseExact: " + selectedDateTime);
                                    Tuple<DateTime, DateTime> reservation_time = new Tuple<DateTime, DateTime>(selectedDateTime, selectedDateTime.AddHours(2));
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
                    Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
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
                    Order superadminorder = new Order();
                    superadminorder.PlaceOrder(currentUser);
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "7":
                    if (!currentUser.IsAdmin && !currentUser.IsSuperAdmin)
                    {
                        Order superadminOrder = new Order();
                        superadminOrder.DisplayOrderedItems(currentUser);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please select a valid option.");
                    }
                    return true;
                case "8":
                    bool adminMenu = true;
                    while (adminMenu)
                    {
                        AdminMenu();
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
            Console.WriteLine("|         Welcome Admin          |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| Please select an option:       |");
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
                Console.Clear();
                Console.WriteLine("Invalid input. Please select a valid option.");
                return true;
        }
    }
}
