class Program
{
    static Menu menu = new Menu();

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
            Console.WriteLine("+--------------------------------+");
            Console.Write("Please select an option (1/2/3/4/5): ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    RestaurantMap.show();
                    Reserve.Reservation();
                    break;
                case "2":
                    showMenu();
                    break;
                case "3":
                    restInfo();
                    break;
                case "4":
                    AdminManager.();
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

    static void restInfo()
    {
        Console.WriteLine("Jake’s restaurant information:");
        Console.WriteLine("Location: Wijnhaven 107, 3011 WN in Rotterdam");
        Console.WriteLine("Phone: (123) 456-7890");
        Console.WriteLine("\t\t\t\tEmail: jakes@example.com");
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
}
