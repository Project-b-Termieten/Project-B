using Newtonsoft.Json;

public static class Reserve
{
    public static void MakingReservation(string name, string email, List<Table> tables)
    {
        Console.WriteLine("For how many people would you like to make a reservation?");
        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        List<Reservation> reservations = ReadFromJsonFile();
        if (reservations == null)
        {
            reservations = new List<Reservation>();
        }
        int TableID = ValidateTable(groupAmount, reservations, tables);
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

    public static int ValidateTable(int groupAmount, List<Reservation> reservations, List<Table> tables)
    {
        Console.WriteLine("Which table would you like to reserve?");
        int tableNum;

        while (true)
        {
            Console.WriteLine("Please enter a number between 1 and 16:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out tableNum) && tableNum >= 1 && tableNum <= 16)
            {
                bool isTableAvailable = true;

                if (reservations != null)
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
        string filePath = "Reservations.json";
        string jsonData = File.ReadAllText(filePath);
        List<Reservation> objects = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
        return objects;
    }

    public static void WriteToJsonFile(List<Reservation> reservations)
    {
        string filePath = "Reservations.json";
        string jsonString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }


    /*public static int[] tables = { 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 6, 6 };
    public static List<int> reservedList = new List<int>();
    public static int barChairs = 8;

    public static void Reservation()
    {
        Console.WriteLine("For how many people would you like to make a reservation?");
        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }

        Console.WriteLine("Is this reservation for the bar? (yes/no)");
        string isBarReservation;
        while (true)
        {
            isBarReservation = Console.ReadLine().ToLower();
            if (isBarReservation == "yes" || isBarReservation == "no")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }
        if (isBarReservation == "yes")
        {
            if (barChairs >= groupAmount)
            {
                Console.WriteLine($"You have reserved for {groupAmount} people at the bar.");
                barChairs -= groupAmount;
                return;
            }
            else
            {
                Console.WriteLine($"There are only {barChairs} chairs left at the bar. Please pick another option.");
                isBarReservation = "no";
            }
        }

        if (isBarReservation == "no")
        {
            while (true)
            {
                Console.WriteLine("What table would you like to make a reservation for?");
                int TableNum;
                while (!int.TryParse(Console.ReadLine(), out TableNum) || TableNum < 1 || TableNum > tables.Length)
                {
                    Console.WriteLine("Invalid table number. Please pick between the tables on the map.");
                }
                if (!reservedList.Contains(TableNum) && tables[TableNum - 1] >= groupAmount)
                {
                    Console.WriteLine($"You have reserved for {groupAmount} people at table {TableNum}.");
                    reservedList.Add(TableNum);
                    return;
                }
                else if (reservedList.Contains(TableNum))
                {
                    Console.WriteLine("This table has already been reserved. Please pick another table.");
                }
                else
                {
                    Console.WriteLine($"There are only {tables[TableNum - 1]} chairs at this table. Please pick another table.");
                }
            }
        }
    }*/
}
