public static class Reserve
{
    public static int[] tables = { 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 6, 6 };
    public static List<int> reservedList = new List<int>();
    public static int barChairs = 8;

    public static void Reservation()
    {
        Console.WriteLine("For how many people would you like to make a reservation?");
  
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
    }
}