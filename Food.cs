public class Food
{
    public string Name;
    public double Price;
    public bool Vegan;

    public Food(string name, double price, bool vegan = false)
    {
        Name = name;
        Price = price;
        Vegan = vegan;
    }

    public string Display()
    {
        string Display_price = Price.ToString("F2");
        return $"{Name} {Display_price}";
    }
}