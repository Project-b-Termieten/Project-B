using Newtonsoft.Json;
using System.IO;
public static class Menu
{
    static private List<Food> List_of_Foods = new List<Food>();
    static private List<Drink> List_of_Drinks = new List<Drink>();


    public static void Add_Item_Menu()
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
                Console.WriteLine("The item has been added to the menu.");
                Add_food(new_dish);
                break;
            case "2":
                Console.WriteLine("What is the name of the drink?");
                string drink_name = Console.ReadLine();
                Console.WriteLine("What is the price of the drink?");
                double drink_price = Convert.ToDouble(Console.ReadLine());
                Drink new_drink = new Drink(drink_name, drink_price);
                Add_drink(new_drink);
                Console.WriteLine("The item has been added to the menu.");
                break;
        }
    }
    public static void Remove_Item_Menu()
    {
        Console.WriteLine("What would you like to remove?\n1: Food item\n2: Drink item");
        string userInput = Console.ReadLine();
        switch (userInput)
        {
            case "1":
                Delete_food();
                break;
            case "2":
                Delete_drink();
                break;
        }
    }
    static public void Display_menu()
    {
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Food menu:");
        Console.WriteLine("-----------------------------");
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json");
        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);
        foreach (Food food_item in existingFoods)
        {
            if (food_item.Vegan)
                Console.WriteLine($"Dish: {food_item.Name}, Price: {food_item.Price} Vegan: Yes");
            else
            {
                Console.WriteLine($"Dish: {food_item.Name}, Price: {food_item.Price}");
            }
        }
        Console.WriteLine("-----------------------------\n");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Drink menu:");
        Console.WriteLine("-----------------------------");
        string json_ = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json");
        List<Drink> existingdrinks = JsonConvert.DeserializeObject<List<Drink>>(json_);
        foreach (Drink drink_item in existingdrinks)
        {
            Console.WriteLine($"Drink: {drink_item.Name}, Price: {drink_item.Price}");
        }
        Console.WriteLine("-----------------------------");
    }

    static public void Add_food(Food Food_item)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText(@"C:\Users\altaa\OneDrive\Documents\Restuarant pro\Restuarant pro\Menu_Food.json");
        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);

        // Add the new food item to the existing list
        existingFoods.Add(Food_item);

        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingFoods, Formatting.Indented);
        File.WriteAllText(@"C:\Users\altaa\OneDrive\Documents\Restuarant pro\Restuarant pro\Menu_Food.json", updatedJson);
    }

    static public void Add_drink(Drink Drink_item)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json");
        List<Drink> existingdrinks = JsonConvert.DeserializeObject<List<Drink>>(json);

        // Add the new food item to the existing list
        existingdrinks.Add(Drink_item);

        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingdrinks, Formatting.Indented);
        File.WriteAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json", updatedJson);
    }

    static public void Delete_food()
    {
        Console.WriteLine("Name of the dish that you want to delete: ");
        string foodName = Console.ReadLine();


        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json");
        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);

        // Find and remove the food item by its name
        Food foodToRemove = existingFoods.FirstOrDefault(food => food.Name == foodName);
        if (foodToRemove != null)
        {
            existingFoods.Remove(foodToRemove);

            // Serialize and write the updated list back to the JSON file
            string updatedJson = JsonConvert.SerializeObject(existingFoods, Formatting.Indented);
            File.WriteAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json", updatedJson);

            Console.WriteLine($"Food item '{foodName}' has been deleted from the menu.");
        }
        else
        {
            Console.WriteLine($"Food item '{foodName}' was not found in the menu.");
        }
    }

    static public void Delete_drink()
    {

        Console.WriteLine("Name of the drink that you want to delete: ");

        string drinkName = Console.ReadLine();
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json");
        List<Drink> existingDrinks = JsonConvert.DeserializeObject<List<Drink>>(json);

        // Find and remove the drink item by its name
        Drink drinkToRemove = existingDrinks.FirstOrDefault(drink => drink.Name == drinkName);
        if (drinkToRemove != null)
        {
            existingDrinks.Remove(drinkToRemove);

            // Serialize and write the updated list back to the JSON file
            string updatedJson = JsonConvert.SerializeObject(existingDrinks, Formatting.Indented);
            File.WriteAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json", updatedJson);

            Console.WriteLine($"Drink item '{drinkName}' has been deleted from the menu.");
        }
        else
        {
            Console.WriteLine($"Drink item '{drinkName}' was not found in the menu.");
        }
    }
    public static Food FindFoodByName(string foodName)
    {
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json");

        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);

        Food selectedFood = existingFoods.FirstOrDefault(food => food.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase));

        return selectedFood;
    }

    public static Drink FindDrinkByName(string drinkName)
    {
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json");

        List<Drink> existingDrinks = JsonConvert.DeserializeObject<List<Drink>>(json);

        Drink selectedDrink = existingDrinks.FirstOrDefault(drink => drink.Name.Equals(drinkName, StringComparison.OrdinalIgnoreCase));

        return selectedDrink;
    }


}
