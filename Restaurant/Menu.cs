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
        string json = File.ReadAllText("Menu_Food.json");
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
        string json_ = File.ReadAllText("Menu_Drink.json");
        List<Drink> existingdrinks = JsonConvert.DeserializeObject<List<Drink>>(json_);
        foreach (Drink drink_item in existingdrinks)
        {
            Console.WriteLine($"Dish: {drink_item.Name}, Price: {drink_item.Price}");
        }
        Console.WriteLine("-----------------------------");
    }

    public void Add_food(Food Food_item)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("Menu_Food.json");
        List<Food> existingFoods = JsonConvert.DeserializeObject<List<Food>>(json);

        // Add the new food item to the existing list
        existingFoods.Add(Food_item);

        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingFoods, Formatting.Indented);
        File.WriteAllText("Menu_Food.json", updatedJson);
    }

    public void Add_drink(Drink Drink_item)
    {
        // Deserialize the existing data from the JSON file
        string json = File.ReadAllText("Menu_Drink.json");
        List<Drink> existingdrinks = JsonConvert.DeserializeObject<List<Drink>>(json);

        // Add the new food item to the existing list
        existingdrinks.Add(Drink_item);

        // Serialize and write the updated list back to the JSON file
        string updatedJson = JsonConvert.SerializeObject(existingdrinks, Formatting.Indented);
        File.WriteAllText("Menu_Drink.json", updatedJson);
    }
}