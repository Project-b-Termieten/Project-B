using Newtonsoft.Json;
using System.IO;
public static class Menu
{
    public static string ActiveFoodMenu => Food_Menu_A;
    public static string ActiveDrinkMenu => Drink_Menu_A;

    public static string FutureFood => Food_Menu_B;
    public static string FutureDrink => Drink_Menu_B;

    // Paths to the menus
    public static string Food_Menu_A = @"../../../Menu_Food.json";
    public static string Drink_Menu_A = @"../../../Menu_Drink.json";

    public static string Food_Menu_B = @"../../../Menu_Food_B.json";
    public static string Drink_Menu_B = @"../../../Menu_Drink_B.json";



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


    public static void Add_Item_Menu() // admin method
    {
        bool validInput = false;

        do
        {
            Console.WriteLine("What would you like to add?\n1: Food\n2: Drink");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Food new_dish = (Food)Item_Setup(userInput);
                    AddMenuItem(new_dish, ActiveFoodMenu);
                    Console.WriteLine("The item has been added to the menu.");
                    validInput = true;
                    break;
                case "2":
                    Drink new_drink = (Drink)Item_Setup(userInput);
                    AddMenuItem(new_drink, ActiveDrinkMenu);
                    Console.WriteLine("The item has been added to the menu.");
                    validInput = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 1 for Food or 2 for Drink.");
                    break;
            }
        } while (!validInput);
    }

    public static void Add_Item_Menu(string superadmin) // super admin method
    {
        do
        {
            Console.WriteLine("What would you like to add?\n1: Food\n2: Drink");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Food new_dish = (Food)Item_Setup(userInput);
                    Console.WriteLine("To which menu would you like to add this item (1/2)");
                    Console.WriteLine("1: Active Food Menu\n2: Unactive Food Menu");

                    string inputmenu = Console.ReadLine();
                    if (inputmenu == "1")
                    {
                        AddMenuItem(new_dish, ActiveFoodMenu);
                    }
                    else if (inputmenu == "2")
                    {
                        AddMenuItem(new_dish, FutureFood);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        continue; // Go back to the beginning of the loop
                    }
                    Console.WriteLine("The item has been added to the menu.");
                    break;
                case "2":
                    Drink new_drink = (Drink)Item_Setup(userInput);
                    Console.WriteLine("To which menu would you like to add this item (1/2)?");
                    Console.WriteLine("1: Active Drink Menu\n2: Unactive Drink Menu");
                    string inputdrink = Console.ReadLine();

                    if (inputdrink == "1")
                    {
                        AddMenuItem(new_drink, ActiveDrinkMenu);
                    }
                    else if (inputdrink == "2")
                    {
                        AddMenuItem(new_drink, FutureDrink);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        continue; // Go back to the beginning of the loop
                    }

                    Console.WriteLine("The item has been added to the menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    break;
            }

            break; // Exit the loop if the input is processed successfully

        } while (true);
    }

    public static object Item_Setup(string F_D)
    {

        Console.WriteLine("What is the name of the item?");
        string Item_name = Console.ReadLine();
        double Item_price;
        while (true)
        {
            Console.WriteLine("What is the price of the item?");
            string priceInput = Console.ReadLine();

            if (double.TryParse(priceInput, out Item_price))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid price.");
            }
        }
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

    public static void Remove_Item_Menu(string superadmin) // super admin method
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

    public static void Remove_Item_Menu() // admin method
    {
        Console.WriteLine("What would you like to remove?\n1: Food item\n2: Drink item");

        string userInput = Console.ReadLine();

        bool validInput = false;

        while (!validInput)
        {
            switch (userInput)
            {
                case "1":
                    string inputFood = Console.ReadLine();
                    if (inputFood == "1")
                    {
                        Delete_Item<Food>(ActiveFoodMenu);
                        validInput = true;
                    }
                    
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    }
                    break;
                case "2":
                    string inputDrink = Console.ReadLine();
                    if (inputDrink == "1")
                    {
                        Delete_Item<Drink>(ActiveDrinkMenu);
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

    public static void Display_menu(string foodMenuPath, string drinkMenuPath)
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
        string json = File.ReadAllText(menuPath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);
        existingItems.Add(item);
        string updatedJson = JsonConvert.SerializeObject(existingItems, Formatting.Indented);
        File.WriteAllText(menuPath, updatedJson);
    }

    static public void Delete_Item<T>(string fm3nupath) where T : MenuItem
    {
        Console.WriteLine("Name of the item that you want to delete: ");
        string Item_name = Console.ReadLine();

        string json = File.ReadAllText(fm3nupath);
        List<T> existingItems = JsonConvert.DeserializeObject<List<T>>(json);

        if (typeof(T) == typeof(Food) || typeof(T) == typeof(Drink))
        {
            // vind en verwijder item
            T selectedItem = existingItems.FirstOrDefault(item => item.Name.Equals(Item_name, StringComparison.OrdinalIgnoreCase));

            if (selectedItem != null)
            {
                existingItems.Remove(selectedItem);

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

    

    public static void Switch_menus()
    {
        Console.WriteLine(
 @"+--------------------------------+
|                                |
| Succesfully activated the menu |
|                                |
| Press Enter button to return.  |
+--------------------------------+");


        // Swap the active and future food menus paths
        string tempFoodPath = Food_Menu_A;
        Food_Menu_A = Food_Menu_B;
        Food_Menu_B = tempFoodPath;

        // Swap the active and future drink menus paths
        string tempDrinkPath = Drink_Menu_A;
        Drink_Menu_A = Drink_Menu_B;
        Drink_Menu_B = tempDrinkPath;


        Console.ReadKey();
    }



}
