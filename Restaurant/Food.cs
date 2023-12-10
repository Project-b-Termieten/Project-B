public class Food : MenuItem
{
    public bool Vegan;

    public Food(string name, double price, bool vegan = false) : base(name, price)
    {
        Vegan = vegan;
    }

    public override string Display()
    {
        string DisplayPrice = Price.ToString("F2");
        string veganInfo = Vegan ? " (Vegan)" : "";
        return $"{Name} {DisplayPrice}{veganInfo}";
    }
}
