using Newtonsoft.Json;
using System.IO;
public class Menu
{
    private List<Food> List_of_Foods = new List<Food>();
    private List<Drink> List_of_Drinks = new List<Drink>();
    public void Display_menu()
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

    public void Add_food(Food Food_item)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json");
        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);

        // Add the new food item to the existing list
        existingFoods.Add(Food_item);

        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingFoods, Formatting.Indented);
        File.WriteAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Food.json", updatedJson);
    }

    public void Add_drink(Drink Drink_item)
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

    public void Delete_food()
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

    public void Delete_drink()
    {

        Console.WriteLine("Name of the drink that you want to delete: ");

        string drinkName = Console.ReadLine();
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Menu_Drink.json");
        List <Drink> existingDrinks = JsonConvert.DeserializeObject<List<Drink>>(json);

        // Find and remove the drink item by its name
        Drink drinkToRemove = existingDrinks.FirstOrDefault(drink => drink.Name == drinkName);
        if (drinkToRemove != null)
        {
            existingDrinks.Remove(drinkToRemove);

            // Serialize and write the updated list back to the JSON file
            string updatedJson = JsonConvert.SerializeObject(existingDrinks, Formatting.Indented);
            File.WriteAllText("Menu_Drink.json", updatedJson);

            Console.WriteLine($"Drink item '{drinkName}' has been deleted from the menu.");
        }
        else
        {
            Console.WriteLine($"Drink item '{drinkName}' was not found in the menu.");
        }
    }
}
