public class Drink : MenuItem
{
    public Drink(string name, double price) : base(name, price)
    {
    }

    public override string Display()
    {
        string DisplayPrice = Price.ToString("F2");
        return $"{Name} {DisplayPrice}";
    }
}
