using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class User : IUserOperations
{
    public string Name { get; set; }
    public string Email { get; set; }  // Add a public setter
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
    [JsonIgnore]

    public List<Food> orderedFoods = new(); [JsonIgnore]

    public List<Drink> orderedDrinks = new(); [JsonIgnore]

    public double totalPrice; [JsonIgnore]

    public bool HasReserved { get; set; } = false;
    [JsonIgnore]
    public Tuple<DateTime, DateTime> Time { get; set; }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        IsAdmin = false;
        IsSuperAdmin = false;
    }



    public virtual void UserMenu()
    {
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("|                                |");
        Console.WriteLine("|  Welcome to Jake’s restaurant! |");
        Console.WriteLine("|                                |");
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| Options:                       |");
        Console.WriteLine("| 1. Reservation                 |");
        Console.WriteLine("| 2. Menu                        |");
        Console.WriteLine("| 3. Restaurant Information      |");
        Console.WriteLine("| 4. Logout                      |");
        Console.WriteLine("| 5. Exit                        |");
        Console.WriteLine("| 6. Place Order                 |");
        Console.WriteLine("| 7. View Order                  |");
        Console.WriteLine("+--------------------------------+");
    }

    public virtual bool UserInput(User currentUser, List<Table> tables)
    {
        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                Console.Clear();
    
                Console.WriteLine("+--------------------------------+");
                Console.WriteLine("| 1. Make Reservation            |");
                Console.WriteLine("| 2. Cancel Reservation          |");
                Console.WriteLine("| 3. Change Reservation          |");
                Console.WriteLine("+--------------------------------+");

                string reservationInput = Console.ReadLine();
                switch (reservationInput)
                {
                    case "1":
                        Information.DisplayMap();
                        bool Incomplete = true;
                        while (Incomplete)
                        {
                            // Get the date from the user
                            Console.Write("Enter date (yyyy-MM-dd): ");
                            string dateString = Console.ReadLine();
                            Reserve.ShowReservationsForDay(DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture));

                            // Get the time from the user
                            Console.Write("Enter time (HH:mm): ");
                            string timeString = Console.ReadLine();

                            // Parse date and time strings
                            if (DateTime.TryParseExact(dateString + " " + timeString, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDateTime))
                            {

                                Console.WriteLine("Date: " + selectedDateTime);
                                Tuple<DateTime, DateTime> reservation_time = new Tuple<DateTime, DateTime>(selectedDateTime, selectedDateTime.AddHours(1));
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
                    case "3":
                        Reserve.ChangeReservation(currentUser);
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
                if (Menu.FutureFood == null)
                {
                    return true;
                }
                else
                {
                    Menu.Display_menu(Menu.FutureFood, Menu.FutureDrink);

                }
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
                if (!currentUser.IsAdmin && !currentUser.IsSuperAdmin)
                {
                    Order order = new Order();
                    order.PlaceOrder(currentUser);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select a valid option.");
                }
                Console.ReadKey();
                Console.Clear();
                return true;
            case "7":
                if (!currentUser.IsAdmin && !currentUser.IsSuperAdmin)
                {
                    Order order = new Order();
                    order.DisplayOrderedItems(currentUser);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select a valid option.");
                }
                return true;
            default:
                Console.WriteLine("Invalid input. Please select a valid option.");
                return true;
        }


        
    }

    public bool DatePastCheck(string dateString)
    {
        if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
        {
            if (selectedDate < DateTime.Now.Date)
            {
                //Console.WriteLine("Please enter a date in the future.");
                return false; // Date is in the past
            }
        }
        return true; // Date is in the future or properly formatted
    }
}
