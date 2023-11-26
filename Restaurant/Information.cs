using Newtonsoft.Json;

static class Information
{
    public static void DisplayMap()
    {
        Console.WriteLine(
        $@"Map of the restaurant:

1-8: tables for 2
9-11: tables for 4
13-16: tables for 6

+----+----+----+----+----+----+----+----+----+----+----+----+----+----+----+
|_________bar_____________|  |                                   |   wc    |
+  __ __ __ __ __ __ __ __ __|                                   |_________+
|                                                                          |
+          _____              ______               ____           ____     +
|     |   /     \   |     |  /      \  |       |  /    \ |    |  /    \  | |
+      |  | 16  |  |       | |  15  | |           | 14 |         | 13 |    +
|     |   \_____/   |     |  \______/  |       |  \____/ |    |  \____/  | |
+           ____                    ____                    ____           +
|       |  /    \  |            |  /    \  |            |  /    \  |       |
+          |  9 |                  | 10 |                  | 12 |          +
|       |  \____/  |            |  \____/  |            |  \____/  |       |
+                                   ____                                   +
|          | [4] |              |  /    \  |              | [8] |          |
+                                  | 11 |                                  +
|   | [1] |       | [3] |       |  \____/  |       | [5] |      | [7] |    |
+                                                                          +
|          | [2] |                                        | [6] |          |
+----+----+----+----+----+----+----/    \----+----+----+----+----+----+----+
                                  [Entree]
");
        Console.WriteLine("Tables that are already reserved:\n");
        PrintReservedSeats();
        Console.WriteLine();
    }

    public static void ContactInfo()
    {
        Console.WriteLine("+----------------------------------------------+");
        Console.WriteLine("| Jake’s restaurant information:               |");
        Console.WriteLine("| Location: Wijnhaven 107, 3011 WN in Rotterdam|");
        Console.WriteLine("| Phone: (123) 456-7890                        |");
        Console.WriteLine("| Email: jakes@example.com                     |");
        Console.WriteLine("+----------------------------------------------+");
    }


    
    public static void PrintReservedSeats()
    {
        string filePath = "C:\\Users\\aidan\\OneDrive\\Documenten\\c# docs\\Restaurantapp\\Restaurantapp\\Reservation.json";
        string json = File.ReadAllText(filePath);

        List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);

       /* foreach (var reservation in reservations)
        {
            Console.WriteLine($"Table : {reservation.Table.TableID} | From: {reservation.Time.Item1} Till: {reservation.Time.Item2}");
        }*/

        foreach (var reservation in reservations)
        {
            Console.WriteLine($"Table {reservation.Table.TableID}: Reserved from {reservation.Time.Item1.ToString("MMMM dd, yyyy HH:mm")} to {reservation.Time.Item2.ToString("MMMM dd, yyyy HH:mm")}");
        }


    }
}
