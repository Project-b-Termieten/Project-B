public abstract class MenuItem
{
    public string Name;
    public double Price;

    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public abstract string Display();
}
