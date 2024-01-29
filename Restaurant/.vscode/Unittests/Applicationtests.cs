using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;


namespace RestaurantAltaaf.Tests;

[TestClass]
public class Applicationtests
{
    private const string ActiveFoodMenu = @"C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\RestaurantAltaaf\\RestaurantAltaaf\\Menu_Food.json";
    private const string ActiveDrinkmenu = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\Menu_Drink.json";
    private const string UsersJson = @"C:\Users\aidan\OneDrive\Documenten\c# docs\RestaurantAltaaf\RestaurantAltaaf\User_info.json"; // Provide the correct path to your JSON file


    [TestMethod]
    public void AddItemChecker() // Testgeval ID 5
    {
        string foodName = "BurgerBurger"; 
        string saladName = "SaladSalad";

        Food expectedFood = new Food("BurgerBurger", 9.99, false);
        Food expectedSalad = new Food("SaladSalad", 7.99, true);

        Menu.AddMenuItem(expectedFood, ActiveFoodMenu);
        Menu.AddMenuItem(expectedSalad, ActiveFoodMenu);


        Food foundFood = Menu.FindItemByName<Food>(foodName, ActiveFoodMenu);
        Food foundSalad = Menu.FindItemByName<Food>(saladName, ActiveFoodMenu);


        Assert.AreEqual(expectedFood.Name, foundFood.Name);
        Assert.AreEqual(expectedSalad.Name, foundSalad.Name);

        Assert.AreEqual(expectedFood.Price, foundFood.Price);
        Assert.AreEqual(expectedSalad.Price, foundSalad.Price);

        Assert.AreEqual(expectedFood.Vegan, foundFood.Vegan);
        Assert.AreEqual(expectedSalad.Vegan, foundSalad.Vegan);

    }

    [TestMethod]
    public void MenuChecker() // Testgeval ID 6
    {
        string foodName = "Burger";
        string saladName = "Salad";

        Food expectedFood = new Food("Burger", 9.99, false);
        Food expectedSalad = new Food("Salad", 7.99, true);


        Food foundFood = Menu.FindItemByName<Food>(foodName, ActiveFoodMenu);
        Food foundSalad = Menu.FindItemByName<Food>(saladName, ActiveFoodMenu);


        Assert.IsNotNull(foundFood);
        Assert.IsNotNull(foundSalad);

        Assert.AreEqual(expectedFood.Name, foundFood.Name);
        Assert.AreEqual(expectedSalad.Name, foundSalad.Name);

        Assert.AreEqual(expectedFood.Price, foundFood.Price);
        Assert.AreEqual(expectedSalad.Price, foundSalad.Price);

        Assert.AreEqual(expectedFood.Vegan, foundFood.Vegan);
        Assert.AreEqual(expectedSalad.Vegan, foundSalad.Vegan);

    }


    [TestMethod]
    public void FindFoodByName_NonExistingFood_ReturnsNull()
    {
        string foodName = "Onzin eten";
        Food foundFood = Menu.FindItemByName<Food>(foodName, ActiveFoodMenu);
        // Assert
        Assert.IsNull(foundFood);
    }


    [TestMethod]
    public void SuperadminChecker() // Testgeval ID 7
    {
        
        string json = File.ReadAllText(UsersJson);

        List<User> users = JsonConvert.DeserializeObject<List<User>>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        bool superAdminExists = false;
        foreach (var account in users)
        {
            if (account is User superAdmin)
            {
                if (superAdmin.Name == "superadmin" &&
                    superAdmin.Email == "superadmin@example.com" &&
                    superAdmin.Password == "superadmin" &&
                    superAdmin.IsAdmin == true &&
                    superAdmin.IsSuperAdmin == true)
                {
                    superAdminExists = true;
                    break;
                }
            }
        }
        Assert.IsTrue(superAdminExists, "Superadmin account does not exist in the JSON file.");
    }
    //

    [TestMethod]
    public void Test_Email()
    {
        Signup Test = new Signup();
        // correct format
        Assert.IsTrue(Test.ValidateEmail("Correct@gmail.com"));
        // base string
        Assert.IsFalse(Test.ValidateEmail("Incorrect"));
        // @ + base string
        Assert.IsFalse(Test.ValidateEmail("@Incorrect"));
        // no .###
        Assert.IsFalse(Test.ValidateEmail("Incorrect@gmail"));
    }


    [TestMethod]
    public void Test_Password()
    {
        Signup Test = new Signup();
        string correct_password = "Correct1234";
        string incorrect_password = "false";
        string incorrect_password_2 = null;

        Assert.IsTrue(Test.ValidatePassword(correct_password));
        Assert.IsFalse(Test.ValidatePassword(incorrect_password));
        Assert.IsFalse(Test.ValidatePassword(incorrect_password_2));
    }


    [TestMethod]
    public void TestDisplayMenu()
    {

        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        Menu.Display_menu(ActiveFoodMenu, ActiveDrinkmenu);
        string result = sw.ToString();

        Assert.IsTrue(result.Contains("Drink menu:"));
        Assert.IsTrue(result.Contains("Water"));
        Assert.IsTrue(result.Contains("Drink:"));

        Assert.IsTrue(result.Contains("Food menu:"));
        Assert.IsTrue(result.Contains("Burger"));
        Assert.IsTrue(result.Contains("Dish:"));
        Console.SetOut(Console.Out);
    }


}


