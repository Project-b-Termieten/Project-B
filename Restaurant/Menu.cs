using Newtonsoft.Json;
using System.IO;
public static class Menu
{
    static private List<Food> List_of_Foods = new List<Food>();
    static private List<Drink> List_of_Drinks = new List<Drink>();

    private static string _activeFoodMenu = @"C:\Users\jerre\OneDrive\Bureaublad\projectbb\projectbb\Menu_Food.json"; // Default value
    private static string _activeDrinkMenu = @"C:\Users\jerre\OneDrive\Bureaublad\projectbb\projectbb\Menu_Drink.json"; // Default value

    public static string ActiveFoodMenu
    {
        get => _activeFoodMenu;
        set => _activeFoodMenu = value;
    }

    public static string ActiveDrinkMenu
    {
        get => _activeDrinkMenu;
        set => _activeDrinkMenu = value;
    }

    public static string FutureFood { get; set; }
    public static string FutureDrink { get; set; }

    public static List<string> FoodMenus = new List<string>
    {
        ActiveFoodMenu
    };


    public static List<string> DrinkMenus = new List<string>
    {
        ActiveDrinkMenu
    };

    public static void ManageList(List<string> list, string newItem)
    {
        if (list.Count >= 2)
        {
            list.RemoveAt(0);
            string directoryPath = list[0];
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");
                    foreach (string jsonFile in jsonFiles)
                    {
                        File.Delete(jsonFile);
                        Console.WriteLine($"Deleted: {jsonFile}");
                    }
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access to the directory or file is denied.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            // Remove the oldest item if count exceeds 2
        }
        list.Add(newItem);
    }

    public static void Item_Setup()
    {
        Console.WriteLine("What would you like to add?\n1: Food\n2: Drink");
        string userInput = Console.ReadLine();
        switch (userInput)
        {
            case "1":
                bool Dish_vegan = true;
                string action = "";
                Console.WriteLine("What is the name of the dish?");
                string Dish_name = Console.ReadLine();
                Console.WriteLine("What is the price of the dish?");
                double Dish_price = Convert.ToDouble(Console.ReadLine());
                bool test = true;
                while (test)
                {
                    Console.WriteLine("Is the dish vegan? (Y/N?)");
                    action = Console.ReadLine().ToUpper();
                    if (action == "Y")
                    {
                        Dish_vegan = true;
                        test = false;
                    }
                    else if (action == "N")
                    {
                        Dish_vegan = false;
                        test = false;
                    }
                    else
                    {
                        Console.WriteLine("Invallid input");
                    }
                }
                Food new_dish = new Food(Dish_name, Dish_price, Dish_vegan);
                Console.WriteLine("To which menu would you like to add this item (1/2)");
                Console.WriteLine("1: Active Food Menu\n2: Future Food Menu");

                string inputmenu = Console.ReadLine();

                if (inputmenu == "1")
                {
                    Add_Item<Food>(new_dish, ActiveFoodMenu);
                }
                else
                {
                    Add_Item<Food>(new_dish, FutureFood);
                }

                Console.WriteLine("The item has been added to the menu.");

                break;
            case "2":
                Console.WriteLine("What is the name of the drink?");
                string drink_name = Console.ReadLine();
                Console.WriteLine("What is the price of the drink?");
                double drink_price = Convert.ToDouble(Console.ReadLine());
                Drink new_drink = new Drink(drink_name, drink_price);

                Console.WriteLine("To which menu would you like to add this item (1/2)?");
                Console.WriteLine("1: Active Food Menu\n2: Future Food Menu");
                string inputdrink = Console.ReadLine();

                if (inputdrink == "1")
                {
                    Add_Item<Drink>(new_drink, ActiveDrinkMenu);
                }
                else
                {
                    Add_Item<Drink>(new_drink, FutureDrink);
                }

                Console.WriteLine("The item has been added to the menu.");
                break;
        }
    }
    public static void Add_Item<T>(T MenuItem, string MenuPath) where T : MenuItem
    {
        string json = File.ReadAllText(MenuPath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);
        existingItems.Add(MenuItem);
        string updatedJson = JsonConvert.SerializeObject(existingItems, Formatting.Indented);
        File.WriteAllText(MenuPath, updatedJson);
    }


    /*public static void Remove_Item_Menu()
    {
        Console.WriteLine("What would you like to remove?\n1: Food item\n2: Drink item");
        string userInput = Console.ReadLine();
        switch (userInput)
        {
            case "1":
                Console.WriteLine("From what menu would you like to remove?");
                string inputfood = Console.ReadLine();
                if (inputfood == "1")
                {
                    Delete_food(ActiveFoodMenu);
                }
                else
                {
                    Delete_food(FutureFood);
                }
                break;
            case "2":
                Console.WriteLine("From what menu would you like to remove?");
                string inputdrink = Console.ReadLine();
                if (inputdrink == "1")
                {
                    Delete_drink(ActiveDrinkMenu);
                }
                else
                {
                    Delete_drink(FutureDrink);
                }
                break;
        }
    }*/

   
    static public void Display_menu(string foodMenuPath, string drinkMenuPath)
    {
        if (foodMenuPath == null || drinkMenuPath == null)
        {
            Console.WriteLine("Menu paths are not provided.");
            return;
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine("Food menu:");
        Console.WriteLine("-----------------------------");

        try
        {
            string foodJson = File.ReadAllText("C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\Menu_Food.json");
            List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(foodJson);
            if (existingFoods != null)
            {
                foreach (Food food_item in existingFoods)
                {
                    if (food_item.Vegan)
                        Console.WriteLine($"Dish: {food_item.Name}, Price: {food_item.Price} Vegan: Yes");
                    else
                        Console.WriteLine($"Dish: {food_item.Name}, Price: {food_item.Price}");
                }
            }
            else
            {
                Console.WriteLine("No existing food items.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Food menu file not found.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Error reading food menu JSON.");
        }

        Console.WriteLine("-----------------------------\n");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Drink menu:");
        Console.WriteLine("-----------------------------");

        try
        {
            string drinkJson = File.ReadAllText("C:\\Users\\jerre\\OneDrive\\Bureaublad\\projectbb\\projectbb\\Menu_Drink.json");
            List <Drink> existingDrinks = JsonConvert.DeserializeObject<List<Drink>>(drinkJson);
            if (existingDrinks != null)
            {
                foreach (Drink drink_item in existingDrinks)
                {
                    Console.WriteLine($"Drink: {drink_item.Name}, Price: {drink_item.Price}");
                }
            }
            else
            {
                Console.WriteLine("No existing drink items.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Drink menu file not found.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Error reading drink menu JSON.");
        }

        Console.WriteLine("-----------------------------");
    }


    public static void Remove_Item_Menu()
    {
        Console.WriteLine("What would you like to remove?\n1: Food item\n2: Drink item");
        string userInput = Console.ReadLine();

        bool validInput = false;

        while (!validInput)
        {
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("From which menu would you like to remove?\n1: Active Food Menu\n2: Future Food Menu");
                    string inputFood = Console.ReadLine();
                    Console.WriteLine("Name of the item that you want to delete: ");
                    string itemName = Console.ReadLine();
                    if (inputFood == "1")
                    {
                        Remove_Item<Food>(ActiveFoodMenu, itemName);
                        validInput = true;
                    }
                    else if (inputFood == "2")
                    {
                        Remove_Item<Food>(FutureFood, itemName);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    }
                    break;

                case "2":
                    Console.WriteLine("From which menu would you like to remove?\n1: Active Drink Menu\n2: Future Drink Menu");
                    string inputDrink = Console.ReadLine();
                    Console.WriteLine("Name of the drink that you want to delete: ");
                    string drinkName = Console.ReadLine();
                    if (inputDrink == "1")
                    {
                        Remove_Item<Drink>(ActiveDrinkMenu, drinkName);
                        validInput = true;
                    }
                    else if (inputDrink == "2")
                    {
                        Remove_Item<Drink>(FutureDrink, drinkName);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please enter 1 or 2.");
                    userInput = Console.ReadLine();
                    break;
            }
        }
    }

    public static void Remove_Item<T>(string menuPath, string itemName) where T : MenuItem
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText(menuPath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);

        // Find and remove the item by its name
        T itemToRemove = existingItems.FirstOrDefault(item => item.Name == itemName);
        if (itemToRemove != null)
        {
            existingItems.Remove(itemToRemove);

            // Serialize and write the updated list back to the JSON file
            string updatedJson = JsonConvert.SerializeObject(existingItems, Formatting.Indented);
            File.WriteAllText(menuPath, updatedJson);

            Console.WriteLine($"{typeof(T).Name} item '{itemName}' has been deleted from the menu.");
        }
        else
        {
            Console.WriteLine($"{typeof(T).Name} item '{itemName}' was not found in the menu.");
        }
    }

    public static T FindItemByName<T>(string itemName) where T : MenuItem
    {
        string json;

        if (typeof(T) == typeof(Food))
        {
            json = File.ReadAllText(ActiveFoodMenu);
        }
        else if (typeof(T) == typeof(Drink))
        {
            json = File.ReadAllText(ActiveDrinkMenu);
        }
        else
        {
            throw new InvalidOperationException("Unsupported item type");
        }

        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);

        T selectedItem = existingItems.FirstOrDefault(item =>
            item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

        return selectedItem;
    }


    public static void Create_new_menu()
    {
        // Lijst voor het menu
        List<Food> foods = new List<Food>
        {
            new Food("Burger", 9.99, false),
            new Food("Fishtick skin", 9.99, false),
            new Food("One piece of eggplant", 15.0, true)
        };


        Console.WriteLine("Name of the new food Menu: ");
        string foodFileName = Console.ReadLine();

        Console.WriteLine("Name of the new drink Menu: ");
        string drinkFileName = Console.ReadLine();


        string foodJson = JsonConvert.SerializeObject(foods, Formatting.Indented);
        File.WriteAllText($"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\{foodFileName}", foodJson);
        FutureFood = @"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\" + foodFileName;
        // Basisitems, deze zijn in elk menu aanwezig
        List<Drink> drinks = new List<Drink>
        {
            new Drink("Water", 2.0),
            new Drink("Gert's Biertje", 3.5),
            new Drink("Python juice", 3.0),
        };

        string drinkJson = JsonConvert.SerializeObject(drinks, Formatting.Indented);
        File.WriteAllText($"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\{drinkFileName}", drinkJson);
        FutureDrink = @"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\" + drinkFileName;
        Console.WriteLine(
 @"+--------------------------------+
|                                |
| Succesfully created the menu   |
|                                |
|                                |
| (Press any button to retur)    |
+--------------------------------+");

        ManageList(FoodMenus, FutureFood);
        ManageList(DrinkMenus, FutureDrink);
        Console.ReadKey();


    }

    public static void Activate_Menus()
    {
        Console.WriteLine(
 @"+--------------------------------+
|                                |
| Succesfully activated the menu |
|                                |
|                                |
| (Press any button to return)   |
+--------------------------------+");
        ActiveFoodMenu = FutureFood;
        ActiveDrinkMenu = FutureDrink;
        Console.ReadKey();
    }



}
