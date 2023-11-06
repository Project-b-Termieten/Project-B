using Newtonsoft.Json;

public static class Reserve
{
    public static void Reservation(List<Table> tables)
    {
        Console.WriteLine("For how many people would you like to make a reservation?");
        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
        // hier kiezen welke tafel, check al geserveerd of niet genoeg plaatsen // if  ReadFromJsonFile() is null => is niet geserveerd

        // foreach table in table list => if gekozen tafel == tafelID => Account  = UserAccount.Name & Amount = groupAmount=> Table.WriteToJson()
    }

    public static List<Table> ReadFromJsonFile()
    {
        string filePath = "Reservations.json";
        string jsonData = File.ReadAllText(filePath);
        List<Table> objects = JsonConvert.DeserializeObject<List<Table>>(jsonData);
        return objects;
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
