using Newtonsoft.Json;

public static class Reserve
{
    public static void MakingReservation(string name, string email, List<Table> tables)
    {
        
        List<Reservation> reservations = ReadFromJsonFile();
        if (reservations == null)
        {
            reservations = new List<Reservation>();
        }
        int groupAmount = ValidateAmount();
        int TableID = ValidateTable(groupAmount, reservations, tables, name, email);
        foreach (var table in tables)
        {
            if (table.TableID == TableID)
            {
                Reservation reservation = new(name, email, groupAmount, table);
                reservations.Add(reservation);
                WriteToJsonFile(reservations);
                Console.WriteLine($"You places a reservation at table {reservation.Table.TableID} for {reservation.Amount}");
                break;
            }
        }
    }

    public static int ValidateAmount()
    {
        Console.WriteLine("For how many people would you like to make a reservation?");
        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        return groupAmount;
    }
    public static int ValidateTable(int groupAmount, List<Reservation> reservations, List<Table> tables, string name, string email)
    {
        Console.WriteLine("Which table would you like to reserve?");
        int tableNum;

        while (true)
        {
            Console.WriteLine("Please enter a number between 1 and 16 (or 0 to go back):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out tableNum) && tableNum >= 0 && tableNum <= 16)
            {
                bool isTableAvailable = true;

                if (tableNum == 0)
                    MakingReservation(name, email, tables);
                else if (reservations != null)
                {
                    foreach (var reservation in reservations)
                    {
                        Table myTable = null;
                        foreach (Table table in tables)
                        {
                            if (table.TableID == tableNum)
                                myTable = table;
                        }
                        if (reservation.Table.TableID == tableNum)
                        {
                            Console.WriteLine($"Table {tableNum} has already been reserved. Please pick another table.");
                            isTableAvailable = false;
                            break;
                        }
                        else if (myTable.Capacity < groupAmount)
                        {
                            Console.WriteLine($"This table has less than {groupAmount} seats. Please pick another table. {myTable.Capacity}");
                            isTableAvailable = false;
                            break;
                        }
                    }
                }

                if (isTableAvailable)
                {
                    break; // Valid table number, exit the loop
                }
            }
            else
            {
                Console.WriteLine($"Invalid input: '{input}'. Please enter a valid number between 1 and 16.");
            }
        }

        return tableNum;
    }
    public static List<Reservation> ReadFromJsonFile()
    {
        string filePath = @"../../../Reservations.json";
        string jsonData = File.ReadAllText(filePath);
        List<Reservation> objects = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
        return objects;
    }

    public static void WriteToJsonFile(List<Reservation> reservations)
    {
        string filePath = @"../../../Reservations.json";
        string jsonString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }
}
