public class Reservation
{
    public string Name { get; }
    public string Email { get; }
    public int Amount { get; } = 0;
    public Table Table { get; } = null;

    public Reservation(string name, string email, int amount, Table table)
    {
        Name = name;
        Email = email;        
        Amount = amount;
        Table = table;
    }
}
