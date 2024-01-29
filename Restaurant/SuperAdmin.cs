
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

public class SuperAdmin : Admin, IUserOperations
{
    [JsonProperty("IsSuperAdmin", Order = 5)]
    public bool IsSuperAdmin { get; set; }

    public static string jsonReservation = @"../../../Reservation.json";
    public static string jsonUserinfo = @"../../../User_info.";

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
                        // Jerrel, dit is voor pakken van datum
                        Console.Write("Enter date (yyyy-MM-dd or press ENTER to return): ");
                        string dateString = Console.ReadLine();
                        if (dateString == "")
                        {
                            Console.Clear();
                            return true;
                        }
                        // deze voor time
                        Console.Write("Enter time (HH:mm): ");
                        string timeString = Console.ReadLine();
                        
                        if (DateTime.TryParseExact(dateString + " " + timeString, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDateTime))
                        {
                            Reserve.ShowReservationsForDay(selectedDateTime);

                            //Console.WriteLine("DateTime using DateTime.TryParseExact: " + selectedDateTime); deze line is niet meer nodig
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
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "2":
                    Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
                    //Menu.Display_menu(Menu.FutureFood, Menu.FutureDrink);

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
                        Console.Write("Please select an option: ");
                        adminMenu = AdminInput();
                    }
                    return true;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    return true;
            }
        }
    }

    protected override void AdminMenu()
    {
        base.AdminMenu();
        Console.WriteLine("| 4. Create Admin User           |");
        Console.WriteLine("| 5. Delete Admin User           |");
        Console.WriteLine("| 6. Change Reservation          |");
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| 7. Switch menus                |");
        Console.WriteLine("+--------------------------------+");
    }

    protected override bool AdminInput()
    {
        string AdminInput = Console.ReadLine();
        switch (AdminInput)
        {
            case "1":
                Menu.Add_Item_Menu("string");
                Console.ReadKey();
                Console.Clear();
                return true;
            case "2":
                Menu.Remove_Item_Menu();
                Console.ReadKey();
                Console.Clear();
                return true;
            case "3":
                Console.Clear();
                return false;
            case "4":
                CreateAdmin();
                return true;
            case "5":
                DeleteAdmin();
                return true;
            case "6":
                Change_Reservation();
                return true;
            case "7":
                Menu.Switch_menus();
                return true;
            default:
                base.AdminInput();
                return true;
        }
    }

    private void CreateAdmin()
    {
        Signup usersignup = new Signup();
        /*Console.WriteLine("What is the name of the Admin?");
        string Name = Console.ReadLine();
        Console.WriteLine("What is the Email of the admin?");
        string Email = Console.ReadLine();
        Console.WriteLine("What is the password of the admin");
        string Password = Console.ReadLine();*/
        usersignup.SignUp(true);
        Console.ReadKey();
        Console.Clear();
    }

    private void DeleteAdmin()
    {

        List<User> users = new List<User>();

        if (File.Exists(jsonReservation))
        {
            string existingData = File.ReadAllText(jsonReservation);
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
                break;
            }
        }
        string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        File.WriteAllText(jsonReservation, jsonString);
        Console.WriteLine("Admin has been successfully deleted.");
        Console.ReadKey();
        Console.Clear();
    }


    public void Change_Reservation()
    {
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| Enter Email of the reservation |");
        Console.WriteLine("+--------------------------------+");
        string Email_User = Console.ReadLine();
        if (!Check_User_Present(Email_User))
        {
            return; // exit als die user niet bestaat
        }
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| 1. Remove Reservation          |");
        Console.WriteLine("| 2. Change Reservation Time     |");
        Console.WriteLine("| 3. Exit                        |");
        Console.WriteLine("+--------------------------------+");

        while (true)
        {
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please Enter the Index of the Email,\nYou wish to remove");
                    int index = int.Parse(Console.ReadLine());
                    RemoveReservationByIndex(jsonReservation, index);
                    return; // Exit the method after removing reservation
                case "2":
                    Console.WriteLine("+--------------------------------+");
                    Console.WriteLine("| Reservation to change:         |");
                    Console.WriteLine("+--------------------------------+");
                    ShowReservationsWithEmail(jsonReservation, Email_User);
                    int index_ = int.Parse(Console.ReadLine());
                    Change_Reservation_method(jsonReservation, index_);
                    return; // Exit the method after changing reservation time
                case "3":
                    return; // Exit the method without making any changes
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break; // Keep the loop running for invalid inputs
            }
        }
    }


    bool Check_User_Present(string Email_User)
    {

        string jsonContent = System.IO.File.ReadAllText(jsonUserinfo);

        JObject jsonData = JsonConvert.DeserializeObject<JObject>(jsonContent);

        // Extract the list of users
        JArray users = jsonData["$values"] as JArray;

        // Check if the target email is present in the list
        foreach (JToken user in users)
        {
            if (user["Email"]?.ToString() == Email_User)
            {
                return true;
            }
        }
        return false;
    }

    static void ShowReservationsWithEmail(string jsonFilePath, string targetEmail)
    {
        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

        JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);

        // Printen van reserveringen voor de email
        Console.WriteLine($"Reservations for Email: {targetEmail}");
        for (int i = 0; i < reservations.Count; i++)
        {
            if (String.Equals(reservations[i]["Email"]?.ToString(), targetEmail, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Index: {i}, Email: {reservations[i]["Email"]}, Time {reservations[i]["Time"]}");
            }
        }
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| Enter index of reservation:    |");
        Console.WriteLine("+--------------------------------+");
    }

    static void RemoveReservationByIndex(string jsonFilePath, int index)
    {
        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

        JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);

        // checken van index
        if (index >= 0 && index < reservations.Count)
        {
            reservations.RemoveAt(index);

            // Lezen van json
            string updatedJsonContent = JsonConvert.SerializeObject(reservations, Formatting.Indented);

            // Updaten van de json
            System.IO.File.WriteAllText(jsonFilePath, updatedJsonContent);

            Console.WriteLine($"Reservation at index {index} removed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid index. No reservation removed.");
        }
    }

    

    static void Change_Reservation_method(string jsonFilePath, int index)
    {
        Console.Write("Enter date and time (yyyy-MM-dd HH:mm) or exit: ");
        string userInput = Console.ReadLine() + ":00";
        string format = "yyyy-MM-dd HH:mm:ss";
        if (userInput.ToUpper() == "EXIT:00")
        {
            return;
        }

        if (DateTime.TryParseExact(userInput, format, null, DateTimeStyles.None, out DateTime result))
        {
            Tuple<DateTime, DateTime> New_Reservation_Time = new Tuple<DateTime, DateTime>(result, result.AddHours(2));
            List<Reservation> List_of_Reservations = Reserve.ReadFromJsonFile();
            
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            // Json data pakken
            JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);
            if (index >= 0 && index < reservations.Count)
            {

                JToken newTimeToken = JToken.FromObject(New_Reservation_Time);
                reservations[index]["Time"] = newTimeToken;
                string updatedJsonContent = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, updatedJsonContent);
            }
            Console.Clear();
            Console.WriteLine(
@"+--------------------------------+
|                                |
| Succesfully changed the reser- |
| vation!!                       |
|                                |");
            return; // Voltooid van reservering aanpssen, terug naar super admin menu
        }
        else
        {
            Console.WriteLine("Invalid date format");
        }
    }
}
