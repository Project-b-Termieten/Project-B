using Newtonsoft.Json;

public class Table
{
    public int TableNum;
    public int Capacity;
    // public Account = null; Persoon assignen aan tafel
    // public int Amount = null; Aantal personen aan tafel

    public Table(int tablenum, int capacity)
    {
        TableNum = tablenum;
        Capacity = capacity;
    }

    public bool TableAvailable()
    {
        if (Account == null)
            return true;
        else
            return false;
    }

    public void WriteToJson()
    {
        string filePath = "Reservations.json";
        string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }

    public static List<Table> ReadFromJsonFile()
    {
        string filePath = "Reservations.json";
        string jsonData = File.ReadAllText(filePath);
        List<Table> objects = JsonConvert.DeserializeObject<List<Table>>(jsonData);
        return objects;
    }
}