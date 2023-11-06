public class Drink
{
    public string Name;
    public double Price;

    public Drink(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public string Display()
    {
        string Display_price = Price.ToString("F2");
        return $"{Name} {Display_price}";
    }
}