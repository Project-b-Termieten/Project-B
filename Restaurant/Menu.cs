using Newtonsoft.Json;
using System.IO;
public static class Menu
{
    static private List<Food> List_of_Foods = new List<Food>();
    static private List<Drink> List_of_Drinks = new List<Drink>();

    private static string _activeFoodMenu = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Menu_Food.json"; // Default value
    private static string _activeDrinkMenu = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Menu_Drink.json"; // Default value

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


    public static void Add_Item_Menu()
    {
        Console.WriteLine("What would you like to add?\n1: Food\n2: Drink");
        string userInput = Console.ReadLine();
        switch (userInput)
        {
            case "1":
                Food new_dish = (Food)Item_Setup(userInput);
                Console.WriteLine("To which menu would you like to add this item (1/2)");
                Console.WriteLine("1: Active Food Menu\n2: Future Food Menu");

                string inputmenu = Console.ReadLine();
                if (inputmenu == "1")
                {
                    AddMenuItem(new_dish, ActiveFoodMenu);
                }
                else
                {
                    AddMenuItem(new_dish, FutureFood);
                }
                Console.WriteLine("The item has been added to the menu.");

                break;
            case "2":
                Drink new_drink = (Drink)Item_Setup(userInput);
                Console.WriteLine("To which menu would you like to add this item (1/2)?");
                Console.WriteLine("1: Active Food Menu\n2: Future Food Menu");
                string inputdrink = Console.ReadLine();

                if (inputdrink == "1")
                {
                    AddMenuItem(new_drink, ActiveDrinkMenu);
                }
                else
                {
                    AddMenuItem(new_drink, FutureDrink);
                }

                Console.WriteLine("The item has been added to the menu.");
                break;
        }
    }
    public static object Item_Setup(string F_D)
    {

        Console.WriteLine("What is the name of the item?");
        string Item_name = Console.ReadLine();
        Console.WriteLine("What is the price of the item?");
        double Item_price = Convert.ToDouble(Console.ReadLine());
        if (F_D == "1")
        {
            bool Dish_vegan = Vegan_Setup(Item_name, Item_price);
            Food new_dish = new Food(Item_name, Item_price, Dish_vegan);
            return new_dish;
        }
        Drink new_drink = new Drink(Item_name, Item_price);
        return new_drink;
    }
    public static bool Vegan_Setup(string Dish_name, double Dish_price)
    {
        Console.WriteLine("Is the dish vegan? (Y/N?)");
        string action = Console.ReadLine().ToUpper();
        if (action == "Y")
        {
            return true;
        }
        else if (action == "N")
        {
            return false;
        }
        else
        {
            Console.WriteLine("Invallid input");
            Vegan_Setup(Dish_name, Dish_price);
        }
        // C# wouldn't let it work without this return :(
        return false;
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
                    if (inputFood == "1")
                    {
                        Delete_Item<Food>(ActiveFoodMenu);
                        validInput = true;
                    }
                    else if (inputFood == "2")
                    {
                        Delete_Item<Food>(FutureFood);
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
                    if (inputDrink == "1")
                    {
                        Delete_Item<Drink>(ActiveDrinkMenu);
                        validInput = true;
                    }
                    else if (inputDrink == "2")
                    {
                        Delete_Item<Drink>(FutureDrink);
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
            string foodJson = File.ReadAllText(foodMenuPath);
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
            string drinkJson = File.ReadAllText(drinkMenuPath);
            List<Drink> existingDrinks = JsonConvert.DeserializeObject<List<Drink>>(drinkJson);
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

    static public void AddMenuItem<T>(T item, string menuPath)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText(menuPath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);
        // Add the new item to the existing list
        existingItems.Add(item);
        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingItems, Formatting.Indented);
        File.WriteAllText(menuPath, updatedJson);
    }

    static public void Delete_Item<T>(string fm3nupath) where T : MenuItem
    {
        Console.WriteLine("Name of the item that you want to delete: ");
        string Item_name = Console.ReadLine();


        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText(fm3nupath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);

        if (typeof(T) == typeof(Food) || typeof(T) == typeof(Drink))
        {
            // Find and remove the food item by its name
            T selectedItem = existingItems.FirstOrDefault(item => item.Name.Equals(Item_name, StringComparison.OrdinalIgnoreCase));

            if (selectedItem != null)
            {
                existingItems.Remove(selectedItem);

                // Serialize and write the updated list back to the JSON file
                string updatedJson = JsonConvert.SerializeObject(existingItems, Formatting.Indented);
                File.WriteAllText(fm3nupath, updatedJson);

                Console.WriteLine($"Item '{Item_name}' has been deleted from the menu.");
            }
            return;
        }
        else
        {
            Console.WriteLine($"Item '{Item_name}' was not found in the menu.");
        }

    }

    public static T FindItemByName<T>(string itemName, string fileName) where T : MenuItem
    {
        string json = File.ReadAllText(fileName);

        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);

        T selectedItem = existingItems.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

        return selectedItem;
    }

    public static void Create_new_menu()
    {
        // Basis items
        List<Food> foods = new List<Food>
        {
            new Food("Burger", 9.99, false),
            new Food("Special burger", 11.99, false),
            new Food("apple salad", 6.99, true),
        };


        Console.WriteLine("Name of the new food Menu: ");
        string foodFileName = Console.ReadLine();

        Console.WriteLine("Name of the new drink Menu: ");
        string drinkFileName = Console.ReadLine();


        string foodJson = JsonConvert.SerializeObject(foods, Formatting.Indented);
        File.WriteAllText($"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\{foodFileName}", foodJson);
        FutureFood = @"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\" + foodFileName+".json";
        // Basisitems
        List<Drink> drinks = new List<Drink>
        {
            new Drink("Water", 2.0),
            new Drink("Cola", 3.5),
            new Drink("Python juice", 4.99),
        };

        string drinkJson = JsonConvert.SerializeObject(drinks, Formatting.Indented);
        File.WriteAllText($"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\{drinkFileName}", drinkJson);
        FutureDrink = @"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\" + drinkFileName +".json";
        Console.WriteLine(
 @"+--------------------------------+
|                                |
| Succesfully created the menu   |
|                                |
| Press Enter button to return.  |
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
| Press Enter button to return.  |
+--------------------------------+");
        ActiveFoodMenu = FutureFood;
        ActiveDrinkMenu = FutureDrink;
        Console.ReadKey();
    }



}
