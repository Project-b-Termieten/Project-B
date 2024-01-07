public class Order
{
    /*private List<Food> orderedFoods;
    private List<Drink> orderedDrinks;
    private double totalPrice;

    public Order()
    {
        orderedFoods = new List<Food>();
        orderedDrinks = new List<Drink>();
        totalPrice = 0;
    }*/

    public void PlaceOrder(User cuser)
    {
        Console.WriteLine("Ordering Food:");
        Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
        OrderFood(cuser);
        Console.ReadLine();
        Console.WriteLine("\nOrdering Drink:");
        Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
        OrderDrink(cuser);
        cuser.totalPrice += (cuser.orderedFoods.Sum(f => f.Price) + cuser.orderedDrinks.Sum(d => d.Price));

        DisplayOrderedItems(cuser);
    }


    private void OrderFood(User cuser)
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
                    cuser.orderedFoods.Add(food);
                    Console.WriteLine($"You have ordered: {food.Display()}");
                }
                else
                {
                    Console.WriteLine($"Food item '{foodName}' not found.");
                }
            }
        } while (foodName.ToLower() != "done");
    }

    private void OrderDrink(User cuser)
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
                    cuser.orderedDrinks.Add(drink);
                    Console.WriteLine($"You have ordered: {drink.Display()}");
                }
                else
                {
                    Console.WriteLine($"Drink item '{drinkName}' not found.");
                }
            }
        } while (drinkName.ToLower() != "done");
    }


    public void DisplayOrderedItems(User cuser)
    {
        if (cuser.orderedFoods == null && cuser.orderedDrinks == null)
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|     You haven't ordered yet    |");
            Console.WriteLine("|                                |");
            Console.WriteLine("+--------------------------------+");

        }
        else
        {
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|                                |");
            Console.WriteLine("|  Welcome to Jake’s restaurant! |");
            Console.WriteLine("|                                |");
            Console.WriteLine("|          Ordered Items         |");
            Console.WriteLine($"| For {cuser.Name,-26} |");
            Console.WriteLine("|--------------------------------|");

            foreach (var food in cuser.orderedFoods)
            {
                Console.WriteLine($"| Food: {food.Display(),-24} |");
            }

            foreach (var drink in cuser.orderedDrinks)
            {
                Console.WriteLine($"| Drink: {drink.Display(),-23} |");
            }

            Console.WriteLine("+--------------------------------+");
            Console.WriteLine($"| Total Price: {cuser.totalPrice,-17:F2} |");
            Console.WriteLine("+--------------------------------+");

            Console.Write("Do you want to order more items? (Y/N): ");
            string response = Console.ReadLine().ToUpper();

            if (response == "Y")
            {
                PlaceOrder(cuser);
            }
            else
            {
                Console.WriteLine("Thank you for your order!");
            }
        }
    }
}
