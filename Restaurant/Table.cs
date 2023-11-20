using Newtonsoft.Json;

public class Table
{
    public int TableID { get; }
    public int Capacity { get; }

    public Table(int tableID, int capacity)
    {
        TableID = tableID;
        Capacity = capacity;
    }

    /*    public bool TableAvailable()
        {
            if (Account == null)
                return true;
            else
                return false;
        }*/

    /*    public void WriteToJson()
        {
            string filePath = "Reservations.json";
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }*/
}
