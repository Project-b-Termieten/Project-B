using Newtonsoft.Json;

public static class Reserve
{
    public static void CancelReservation(User currentUser)
    {
        List<Reservation> reservations = ReadFromJsonFile();

        if (reservations == null || reservations.Count == 0)
        {
            Console.WriteLine("No reservations to cancel.");
            return;
        }
        // Find the reservation to cancel
        Reservation reservationToCancel = reservations.FirstOrDefault(
        r => r.Name == currentUser.Name && r.Email == currentUser.Email);

        if (reservationToCancel == null)
        {
            Console.WriteLine("Reservation not found. No reservation was canceled.");
            return;
        }
        // Remove the reservation from the list
        reservations.Remove(reservationToCancel);
        WriteToJsonFile(reservations);

        Console.WriteLine($"Reservation at table {reservationToCancel.Table.TableID} for {reservationToCancel.Amount} people at {reservationToCancel.Time.Item1} has been canceled.");
    }
    public static void MakingReservation(string name, string email, List<Table> tables, Tuple<DateTime, DateTime> Reservation_time)
    {

        List<Reservation> reservations = ReadFromJsonFile();
        if (reservations == null)
        {
            reservations = new List<Reservation>();
        }
        int groupAmount = ValidateAmount();
        int TableID = ValidateTable(groupAmount, reservations, tables, name, email, Reservation_time);
        foreach (var table in tables)
        {
            if (table.TableID == TableID)
            {
                Reservation reservation = new(name, email, groupAmount, table);
                reservation.Time = Reservation_time;
                reservations.Add(reservation);
                WriteToJsonFile(reservations);
                Console.WriteLine($"You placed a reservation at table {reservation.Table.TableID} for {reservation.Amount}");
                break;
            }
        }
    }

    public static int ValidateAmount()
    {
        Console.WriteLine("For how many people would you like to make a reservation? (Maximum of 6 people. Call restaurant for bigger reservations)");
        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0 || groupAmount > 6)
        {
            Console.WriteLine("Invalid input. Please enter a valid number. (1 to 6)");
        }
        return groupAmount;
    }
    public static int ValidateTable(int groupAmount, List<Reservation> reservations, List<Table> tables, string name, string email, Tuple<DateTime, DateTime> Reservation_time)
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
                    MakingReservation(name, email, tables, Reservation_time);
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
                            List<Reservation> List_Of_Reservations = ReadFromJsonFile();
                            foreach (Reservation Existing_Reservation in List_Of_Reservations)
                            {
                                if (IsReservationOverlapping(Reservation_time, Existing_Reservation.Time))
                                {
                                    Console.WriteLine($"Table {tableNum} has already been reserved. Please pick another table.");
                                    isTableAvailable = false;
                                    break;
                                }
                            }


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
        string filePath = "C:\\Users\\altaa\\OneDrive\\Documents\\Restuarant pro\\Restuarant pro\\Reservation.json";
        if (!File.Exists(filePath))
        {
            return new List<Reservation>();
        }

        string jsonData = File.ReadAllText(filePath);
        List<Reservation> objects = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
        return objects;
    }

    public static void WriteToJsonFile(List<Reservation> reservations)
    {
        string filePath = @"C:\Users\altaa\OneDrive\Documents\Restuarant pro\Restuarant pro\Reservation.json";
        string jsonString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }

    private static bool IsReservationOverlapping(Tuple<DateTime, DateTime> reservationTime, Tuple<DateTime, DateTime> existingReservation)
    {
        DateTime start_time = reservationTime.Item1;
        DateTime end_time = reservationTime.Item2;
        DateTime existing_start_time = existingReservation.Item1;
        DateTime existing_end_time = existingReservation.Item2;

        // Check if the start_time or end_time of the reservationTime is within the existingReservation
        if ((existing_start_time <= end_time && end_time <= existing_end_time))
        {
            return true;
        }

        // Check if the reservationTime completely encloses the existingReservation
        if (start_time <= existing_start_time && end_time >= existing_end_time)
        {
            return true;
        }

        // Check if the existingReservation completely encloses the reservationTime
        if (existing_start_time <= start_time && existing_end_time >= end_time)
        {
            return true;
        }

        return false;
    }

    public static void ShowReservationsForDay(DateTime selectedDate)
    {
        List<Reservation> reservations = Reserve.ReadFromJsonFile();

        if (reservations != null)
        {
            Console.WriteLine($"Reservations for {selectedDate}:");
            foreach (var reservation in reservations)
            {
                if (reservation.Time.Item1.Date == selectedDate)
                {
                    Console.WriteLine($"Table ID {reservation.Table.TableID} has been Reserved at {reservation.Time.Item1}");
                }
            }
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
    }
}
