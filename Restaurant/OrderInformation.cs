public class OrderInformation
{
    public string Email { get; set; }
    public List<Food> OrderedFoods { get; set; }
    public List<Drink> OrderedDrinks { get; set; }
    
    public double totalPrice { get; set; }

}
