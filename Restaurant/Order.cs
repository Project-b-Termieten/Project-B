using Newtonsoft.Json;

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
        //Menu.Display_menu(Menu.ActiveFoodMenu, Menu.ActiveDrinkMenu);
        OrderDrink(cuser);
        cuser.totalPrice += (cuser.orderedFoods.Sum(f => f.Price) + cuser.orderedDrinks.Sum(d => d.Price));
        DisplayOrderedItems(cuser);
    }


    private void OrderFood(User cuser)
    {
        if (DoesUserExist(cuser.Email) == false)
        {
            CreateInitialOrder(cuser);
        }

        Console.WriteLine("Enter the name of the food you want to order (or type 'done' to finish ordering):");
        string foodName;
        do
        {
            foodName = Console.ReadLine();
            if (foodName.ToLower() != "done")
            {
                Food food = Menu.FindItemByName<Food>(foodName, Menu.ActiveFoodMenu);
                if (food != null)
                {
                    cuser.orderedFoods.Add(food);
                    Console.WriteLine($"You have ordered: {food.Display()}");

                    string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

                    // add code that adds an empty orderedfood lists, drink list and total price to the json

                    
                    AddFoodToOrder(cuser.Email, food);

                }
                else
                {
                    Console.WriteLine($"Food item '{foodName}' not found.");
                }
            }
        } while (foodName.ToLower() != "done");
    }

        private void AddFoodToOrder(string userEmail, Food newFoodItem)
        {
            string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json";

            try
            {
                string json = File.ReadAllText(filePath);

                List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

                // Find the user's order by email
                OrderInformation userOrder = orders.FirstOrDefault(order => order.Email == userEmail);

                if (userOrder != null)
                {
                    // Add the new food item to the user's OrderedFoods array
                    userOrder.OrderedFoods.Add(newFoodItem);
                    userOrder.totalPrice += newFoodItem.Price;

                    // Serialize the updated orders list and overwrite the JSON file
                    string updatedJson = JsonConvert.SerializeObject(orders, Formatting.Indented);
                    File.WriteAllText(filePath, updatedJson);

                    //Console.WriteLine("Food item added to the order successfully.");
                }
                else
                {

                    Console.WriteLine("User not found or order information not available.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Order information file not found.");
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Error reading JSON file.");
            }
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
                Drink drink = Menu.FindItemByName<Drink>(drinkName, Menu.ActiveDrinkMenu);
                if (drink != null)
                {
                    cuser.orderedDrinks.Add(drink);
                    Console.WriteLine($"You have ordered: {drink.Display()}");

                    string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

                    if (cuser.orderedFoods == null || cuser.orderedDrinks == null)
                    {
                        CreateInitialOrder(cuser);
                    }
                    AddDrinkToOrder(cuser.Email, drink);
                }
                else
                {
                    Console.WriteLine($"Drink item '{drinkName}' not found.");
                }
            }
        } while (drinkName.ToLower() != "done");
    }

    private void AddDrinkToOrder(string userEmail, Drink newDrinkItem)
    {
        string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

        try
        {
            string json = File.ReadAllText(filePath);

            List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

            // Find the user's order by email
            OrderInformation userOrder = orders.FirstOrDefault(order => order.Email == userEmail);

            if (userOrder != null)
            {
                // Add the new drink item to the user's OrderedDrinks array
                userOrder.OrderedDrinks.Add(newDrinkItem);
                userOrder.totalPrice += newDrinkItem.Price;

                // Serialize the updated orders list and overwrite the JSON file
                string updatedJson = JsonConvert.SerializeObject(orders, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);

                Console.WriteLine("Drink item added to the order successfully.");
            }
            else
            {
                Console.WriteLine("User not found or order information not available.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Order information file not found.");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("Error reading JSON file.");
        }
    }


    /*public void awd(User cuser)
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
    }*/

    public void DisplayOrderedItems(User cuser)
    {
        try
        {
            string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

            string json = File.ReadAllText(filePath);

            List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

            var userOrders = orders.FirstOrDefault(order => order.Email == cuser.Email);

            if (userOrders == null || (userOrders.OrderedFoods == null && userOrders.OrderedDrinks == null))
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

                foreach (var food in userOrders.OrderedFoods)
                {
                    Console.WriteLine($"| Food: {food.Display(),-24} |");
                }

                foreach (var drink in userOrders.OrderedDrinks)
                {
                    Console.WriteLine($"| Drink: {drink.Display(),-23} |");
                }


                Console.WriteLine("+--------------------------------+");
                Console.WriteLine($"| Total Price: {userOrders.totalPrice,-17:F2} |");
                Console.WriteLine("+--------------------------------+");
            }

            Console.WriteLine("| 1. Add more items              |");
            Console.WriteLine("| 2. Start over                  |");
            Console.WriteLine("| (or press Enter to return)     |");
            Console.WriteLine("+--------------------------------+");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        PlaceOrder(cuser);
                            break;
                    }
                case "2": { ClearOrderedItemsByEmail(cuser.Email); break; }
                case "3": { break; }
                default:
                    Console.WriteLine("Invalid input. Please select a valid option.");
                    break;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Order information file not found.");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("Error reading JSON file.");
        }
    }

    private void CreateInitialOrder(User cuser)
    {
        string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

        OrderInformation newOrder = new OrderInformation
        {
            Email = cuser.Email,
            OrderedFoods = new List<Food>(),
            OrderedDrinks = new List<Drink>(),
            totalPrice = 0
        };

        try
        {
            // Serialize the new order and write it to the JSON file
            string json = JsonConvert.SerializeObject(new List<OrderInformation> { newOrder }, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating initial order: {ex.Message}");
        }
    }

    private void ClearOrderedItemsByEmail(string userEmail)
    {
        string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json"; // Replace with your JSON file path

        try
        {
            string json = File.ReadAllText(filePath);

            List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

            // Find the user's order by email
            OrderInformation userOrder = orders.FirstOrDefault(order => order.Email == userEmail);

            if (userOrder != null)
            {
                // Clear the user's OrderedFoods and OrderedDrinks lists
                userOrder.OrderedFoods.Clear();
                userOrder.OrderedDrinks.Clear();
                userOrder.totalPrice = 0; // Reset the total price to zero

                // Serialize the updated orders list and overwrite the JSON file
                string updatedJson = JsonConvert.SerializeObject(orders, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);

                Console.WriteLine("Ordered items cleared successfully.");
            }
            else
            {
                Console.WriteLine("User not found or order information not available.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Order information file not found.");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("Error reading JSON file.");
        }
    }


    private bool DoesUserExist(string userEmail)
    {
        string filePath = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Orders.json";

        try
        {
            string json = File.ReadAllText(filePath);

            List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

            // Check if the user's order exists by email
            bool userExists = orders.Any(order => order.Email == userEmail);

            return userExists;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Order information file not found.");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("Error reading JSON file.");
        }

        return false;
    }

    private bool DoesUserExist(string userEmail, string file_path)
    {
        
        try
        {
            string json = File.ReadAllText(file_path);

            List<OrderInformation> orders = JsonConvert.DeserializeObject<List<OrderInformation>>(json);

            // Check if the user's order exists by email
            bool userExists = orders.Any(order => order.Email == userEmail);

            return userExists;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Order information file not found.");
        }
        catch (JsonReaderException)
        {
            Console.WriteLine("Error reading JSON file.");
        }

        return false;
    }

}
