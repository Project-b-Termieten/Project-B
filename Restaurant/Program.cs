using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using System;

public class Program
{
    public static User currentUser = null;
    public static Menu menu = new Menu();
    public static List<Table> tables = new List<Table>()
    {
        new Table(1, 2), new Table(2, 2), new Table(3, 2), new Table(4, 2), new Table(5, 2), new Table(6, 2), new Table(7, 2), new Table(16, 6),
        new Table(8, 2), new Table(9, 4), new Table(10, 4), new Table(11, 4),new Table(12, 4), new Table(13, 4), new Table(14, 4), new Table(15, 6)
    };

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|  Welcome to Jake’s restaurant! |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("| Options:                       |");
            Console.WriteLine("| 1. Make Reservation            |");
            Console.WriteLine("| 2. Menu                        |");
            Console.WriteLine("| 3. Location                    |");
            Console.WriteLine("| 4. login                       |");
            Console.WriteLine("| 5. Exit                        |");
            if (currentUser != null && currentUser.Admin)
            {
                Console.WriteLine("| 6. Admin Menu                  |");
            }
            Console.WriteLine("+--------------------------------+");
            if (currentUser != null && currentUser.Admin)
            {
                Console.Write("Please select an option (1/2/3/4/5/6): ");
            }
            else
            {
                Console.Write("Please select an option (1/2/3/4/5): ");
            }
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    if (currentUser is null)// currentUser is null
                    {
                        Console.WriteLine("Please log or sign up in first to make a reservation.");
                        login_or_Signup();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        RestaurantMap.DisplayMap();
                        bool Incomplete = true;
                        while (Incomplete)
                        {
                            Console.Write("Enter date and time (yyyy-MM-dd HH:mm) or exit: ");
                            userInput = Console.ReadLine() + ":00";
                            string format = "yyyy-MM-dd HH:mm:ss";

                            if (userInput.ToUpper() == "EXIT")
                            {
                                Main();
                            }

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
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;
                case "2":
                    showMenu();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    restInfo();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    login_or_Signup();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "5":
                    ExitGame();
                    break;
                case "6":
                    if (currentUser.Admin)
                    {
                        AdminMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please select a valid option.");
                        break;
                    }
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break;
            }
        }

        static void login_or_Signup()
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

                string email = userLogin.Email;
                string password = userLogin.Password;

                Console.WriteLine("Logging in with email: " + email);
            }
            else if (user_answer == "2")
            {
                Signup usersignup = new Signup();
                Console.WriteLine("Please enter your name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Please enter your email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Please enter a password that needs to be 8 characters.");
                string password = Console.ReadLine();

                // Call the SignUp method to validate and save user information
                currentUser = usersignup.SignUp(name, email, password);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 'Login' or 'Sign up'.");
            }
        }

        static void restInfo()
        {
            Console.WriteLine("+----------------------------------------------+");
            Console.WriteLine("| Jake’s restaurant information:               |");
            Console.WriteLine("| Location: Wijnhaven 107, 3011 WN in Rotterdam|");
            Console.WriteLine("| Phone: (123) 456-7890                        |");
            Console.WriteLine("| Email: jakes@example.com                     |");
            Console.WriteLine("+----------------------------------------------+");
        }

        static void showMenu()
        {
            Console.WriteLine("\nMenu:");
            menu.Display_menu();
        }

        static void ExitGame()
        {
            Console.WriteLine("Exiting the app. Goodbye!");
            Environment.Exit(0);
        }

        static void Add_Item_Menu()
        {
            Console.WriteLine("What would you like to add?\n1: Food\n2: Drink");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    bool Dish_vegan = true;
                    string action;
                    bool completed = true;
                    Console.WriteLine("What is the name of the dish?");
                    string Dish_name = Console.ReadLine();
                    Console.WriteLine("What is the price of the dish?");
                    double Dish_price = Convert.ToDouble(Console.ReadLine());

                    while (completed)
                    {
                        Console.WriteLine("Is the dish vegan? (Y/N?)");
                        action = Console.ReadLine().ToUpper();
                        if (action == "Y")
                        {
                            Dish_vegan = true;
                            completed = false;
                        }
                        else if (action == "N")
                        {
                            Dish_vegan = false;
                            completed = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }
                    }
                    Food new_dish = new Food(Dish_name, Dish_price, Dish_vegan);
                    Console.WriteLine("The item has been added to the menu.");
                    menu.Add_food(new_dish);
                    break;
                case "2":
                    Console.WriteLine("What is the name of the drink?");
                    string drink_name = Console.ReadLine();
                    Console.WriteLine("What is the price of the drink?");
                    double drink_price = Convert.ToDouble(Console.ReadLine());
                    Drink new_drink = new Drink(drink_name, drink_price);
                    menu.Add_drink(new_drink);
                    Console.WriteLine("The item has been added to the menu.");
                    break;
            }
        }
        static void Remove_Item_Menu()
        {
            Console.WriteLine("What would you like to remove?\n1: Food item\n2: Drink item");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    menu.Delete_food();
                    break;
                case "2":
                    menu.Delete_drink();
                    break;
            }
        }
        static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("+--------------------------------+");
                Console.WriteLine("|                                |");
                Console.WriteLine("|  Welcome Admin                 |");
                Console.WriteLine("|                                |");
                Console.WriteLine("+--------------------------------+");
                Console.WriteLine("| Options:                       |");
                Console.WriteLine("| 1. Add item to Menu            |");
                Console.WriteLine("| 2. Remove item from Menu       |");
                Console.WriteLine("| 3. Create Admin                |");
                Console.WriteLine("| 4.                             |");
                Console.WriteLine("| 5. return                      |");
                Console.WriteLine("+--------------------------------+");
                Console.Write("Please select an option (1/2/3/4/5): ");
                string AdminInput = Console.ReadLine();
                switch (AdminInput)
                {
                    case "1":
                        Add_Item_Menu();
                        Console.ReadKey();
                        break;
                    case "2":
                        Remove_Item_Menu();
                        Console.ReadKey();
                        break;
                    case "3":
                        // Check if user is a Superadmin
                        if (currentUser.Superadmin)
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
                            break;
                        }
                        else
                        {
                            Console.WriteLine("User does not have the privalages to proceed.");
                            Console.ReadKey();
                            break;
                        }

                    case "5":
                        Main();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please select a valid option.");
                        break;
                }
            }
        }
    }

}
