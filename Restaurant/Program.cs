class Program
{
    public static Menu menu = new Menu();
    public static List<Table> tables = new List<Table>()
    {
        new Table(1, 2), new Table(2, 2), new Table(3, 2), new Table(4, 2), new Table(5, 2), new Table(6, 2), new Table(7, 2), new Table(16, 6),
        new Table(8, 2), new Table(9, 4), new Table(10, 4), new Table(11, 4),new Table(12, 4), new Table(13, 4), new Table(14, 4), new Table(15, 6)
    };

    static void Main(string[] args)
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
            Console.WriteLine("| 6. Admin menu                  |");
            Console.WriteLine("+--------------------------------+");
            Console.Write("Please select an option (1/2/3/4/5): ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    RestaurantMap.DisplayMap();
                    Reserve.Reservation(tables);
                    break;
                case "2":
                    showMenu();
                    break;
                case "3":
                    restInfo();
                    break;
                case "4":
                    login_or_Signup();
                    break;
                case "5":
                    ExitGame();
                    break;
                case "6":
                    AdminMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break;
            }
        }
    }
    static void login_or_Signup()
    {
        Console.WriteLine("Do you want to LOGIN or Sign up ");
        string user_answer = Console.ReadLine().ToLower();

        if (user_answer == "login")
        {
            Login userLogin = new Login();
            userLogin.PromptForLogin();

            string email = userLogin.Email;
            string password = userLogin.Password;

            Console.WriteLine("Logging in with email: " + email);
        }
        else if (user_answer == "sign up")
        {
            Signup usersignup = new Signup();
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();

            // Call the SignUp method to validate and save user information
            usersignup.SignUp(name, email, password);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select 'Login' or 'Sign up'.");
        }
    }

    static void restInfo()
    {
        Console.WriteLine("Jake’s restaurant information:");
        Console.WriteLine("Location: Wijnhaven 107, 3011 WN in Rotterdam");
        Console.WriteLine("Phone: (123) 456-7890");
        Console.WriteLine("Email: jakes@example.com");
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
                bool Dish_vegan;
                Console.WriteLine("What is the name of the dish?");
                string Dish_name = Console.ReadLine();
                Console.WriteLine("What is the price of the dish?");
                double Dish_price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Is the dish vegan?");
                string action = Console.ReadLine();
                if (action == "yes")
                {
                    Dish_vegan = true;
                }
                else
                {
                    Dish_vegan = false;
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
            // Check if User is a admin (Incomplete)
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
            Console.WriteLine("| 5. Exit                        |");
            Console.WriteLine("+--------------------------------+");
            Console.Write("Please select an option (1/2/3/4/5): ");
            string AdminInput = Console.ReadLine();
            switch (AdminInput)
            {
                case "1":
                    Add_Item_Menu();
                    break;
                case "2":
                    Remove_Item_Menu();
                    break;
                case "3":
                    // Check if user is a Superadmin (incomplete)
                    if (true)
                    {
                        // create Admin
                        break;
                    }
                    Console.WriteLine("User does not have the privalages to proceed.");
                    break;
                case "5":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break;
            }
        }
    }
}

