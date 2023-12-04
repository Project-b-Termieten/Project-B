using Newtonsoft.Json;
using System.Globalization;


public class User
{
    public string Name { get; set; }
    public string Email { get; set; }  // Add a public setter
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
    [JsonIgnore]

    public bool HasReserved = false;
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
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("Please pick an option (1/2/3/4/5):");
    }

    public virtual bool UserInput(User currentUser, List<Table> tables)
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
                            // Get the date from the user
                            Console.Write("Enter date (yyyy-MM-dd): ");
                            string dateString = Console.ReadLine();

                            // Get the time from the user
                            Console.Write("Enter time (HH:mm): ");
                            string timeString = Console.ReadLine();

                            // Parse date and time strings
                            if (DateTime.TryParseExact(dateString + " " + timeString, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDateTime))
                            {
                                // Display the selected date and time
                                Console.WriteLine($"Selected Date and Time: {selectedDateTime}");
                                Tuple<DateTime, DateTime> reservation_time = new Tuple<DateTime, DateTime>(selectedDateTime, selectedDateTime.AddHours(1));
                                Reserve.MakingReservation(currentUser.Name, currentUser.Email, tables, reservation_time);
                                Incomplete = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date or time format.");
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
                Reserve.CancelReservation(currentUser);
                Environment.Exit(0);
                return true;
            default:
                Console.WriteLine("Invalid input. Please select a valid option.");
                return true;
        }
    }
}
