public class Order
{
    private List<Food> orderedFoods;
    private List<Drink> orderedDrinks;
    private double totalPrice;

    public Order()
    {
        orderedFoods = new List<Food>();
        orderedDrinks = new List<Drink>();
        totalPrice = 0;
    }

    public void PlaceOrder(User currentUser)
    {
        Console.WriteLine("Ordering Food:");
        Menu.Display_menu();
        OrderFood();
        Console.ReadLine();
        Console.WriteLine("\nOrdering Drink:");
        Menu.Display_menu();
        OrderDrink();
        totalPrice += (orderedFoods.Sum(f => f.Price) + orderedDrinks.Sum(d => d.Price));

        DisplayOrderedItems(currentUser);
    }


    private void OrderFood()
    {
        Console.WriteLine("Enter the name of the food you want to order (or type 'done' to finish ordering):");
        string foodName;
        do
        {
            foodName = Console.ReadLine();
            if (foodName.ToLower() != "done")
            {
                Food food = Menu.FindFoodByName(foodName);
                if (food != null)
                {
                    orderedFoods.Add(food);
                    Console.WriteLine($"You have ordered: {food.Display()}");
                }
                else
                {
                    Console.WriteLine($"Food item '{foodName}' not found.");
                }
            }
        } while (foodName.ToLower() != "done");
    }

    private void OrderDrink()
    {
        Console.WriteLine("Enter the name of the drink you want to order (or type 'done' to finish ordering):");
        string drinkName;
        do
        {
            drinkName = Console.ReadLine();
            if (drinkName.ToLower() != "done")
            {
                Drink drink = Menu.FindDrinkByName(drinkName);
                if (drink != null)
                {
                    orderedDrinks.Add(drink);
                    Console.WriteLine($"You have ordered: {drink.Display()}");
                }
                else
                {
                    Console.WriteLine($"Drink item '{drinkName}' not found.");
                }
            }
        } while (drinkName.ToLower() != "done");
    }

    private void DisplayOrderedItems(User currentUser)
    {
        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("|                                |");
        Console.WriteLine("|  Welcome to Jake’s restaurant! |");
        Console.WriteLine("|                                |");
        Console.WriteLine("|          Ordered Items         |");
        Console.WriteLine($"| For {currentUser.Name,-26} |");
        Console.WriteLine("|--------------------------------|");

        foreach (var food in orderedFoods)
        {
            Console.WriteLine($"| Food: {food.Display(),-24} |");
        }

        foreach (var drink in orderedDrinks)
        {
            Console.WriteLine($"| Drink: {drink.Display(),-23} |");
        }

        Console.WriteLine("+--------------------------------+");
        Console.WriteLine($"| Total Price: {totalPrice,-17:F2} |");
        Console.WriteLine("+--------------------------------+");

        Console.Write("Do you want to order more items? (Y/N): ");
        string response = Console.ReadLine().ToUpper();

        if (response == "Y")
        {
            PlaceOrder(currentUser);
        }
        else
        {
            Console.WriteLine("Thank you for your order!");
        }
    }
}
