using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Reserve
{
    public static string jsonReservation = @"../../../Reservation.json";

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
        Console.WriteLine($"You want to make a reservation for {groupAmount} people. Is this correct? (Y/N)");
        string confirmation = Console.ReadLine();

        if (confirmation.Trim().ToUpper() != "Y")
        {
            groupAmount = ValidateAmount();
        }

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
        Console.WriteLine("For how many people would you like to make a reservation?");
        Console.WriteLine("(Maximum of 6 people, call restaurant for bigger reservations)");

        int groupAmount;
        while (!int.TryParse(Console.ReadLine(), out groupAmount) || groupAmount <= 0 || groupAmount > 6)
        {
            Console.WriteLine("Invalid input. Please enter a valid number. (1 to 6)");
        }
        return groupAmount;
    }

    public static void PrintReservedSeats(DateTime targetDate)
    {
        List<Reservation> reservations = ReadFromJsonFile();

        if (reservations == null || reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            return;
        }

        Console.WriteLine($"Reserved Seats for {targetDate.ToShortDateString()}:");

        /*foreach (var reservation in reservations)
        {
            if (reservation.Time.Item1.Date == targetDate.Date)
            {
                Console.WriteLine($"Reservation for {reservation.Amount} people at Table {reservation.Table.TableID} on {reservation.Time.Item1}");
            }
        }*/

        foreach (var reservation in reservations)
        {
            var date = reservation.Time.Item1.ToString("MMMM dd yyyy");
            var startTime = reservation.Time.Item1.ToString("HH:mm");
            var endTime = reservation.Time.Item2.ToString("HH:mm");

            Console.WriteLine($"Table {reservation.Table.TableID}:\nDate: {date}\nTime: from {startTime} till {endTime}");
        }
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
        string jsonData = File.ReadAllText(jsonReservation);
        List<Reservation> objects = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
        return objects;
    }

    public static void WriteToJsonFile(List<Reservation> reservations)
    {
        string jsonString = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText(jsonReservation, jsonString);
    }

    public static bool IsReservationOverlapping(Tuple<DateTime, DateTime> reservationTime, Tuple<DateTime, DateTime> existingReservation)
    {
        DateTime start_time = reservationTime.Item1;
        DateTime end_time = reservationTime.Item2;
        DateTime existing_start_time = existingReservation.Item1;
        DateTime existing_end_time = existingReservation.Item2;

        // Check if the start_time or end_time of the reservationTime is within the existingReservation
        if ((existing_start_time <= start_time && start_time <= existing_end_time) ||
            (existing_start_time <= end_time && end_time <= existing_end_time))
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



    public static void ChangeReservation(User currentUser)
    {
        ShowReservationsWithEmail(jsonReservation, currentUser.Email);

        // Get the user input for the index
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            // Change the reservation time
            Change_Reservation_Method(jsonReservation, index);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the reservation index.");
        }
    }

    public static void ShowReservationsWithEmail(string jsonFilePath, string targetEmail)
    {
        string jsonContent = File.ReadAllText(jsonFilePath);

        JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);

        // Printen van reserveringen voor de email
        Console.WriteLine($"Reservations for {targetEmail}:");

        for (int i = 0; i < reservations.Count; i++)
        {
            if (String.Equals(reservations[i]["Email"]?.ToString(), targetEmail, StringComparison.OrdinalIgnoreCase))
            {


                Console.WriteLine($"Reservation number: {i}\n Date: {reservations[i]["Time"]}");
            }
        }

        Console.WriteLine("+--------------------------------+");
        Console.WriteLine("| Enter number of reservation:   |");
        Console.WriteLine("+--------------------------------+");
    }


    public static void ShowReservationsWithEmail(string targetEmail)
    {
        string jsonContent = File.ReadAllText(jsonReservation);

        JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);

        // Filteren van reservering op de target email
        var matchingReservations = reservations
            .Where(r => String.Equals(r["Email"]?.ToString(), targetEmail, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Printen van reserveringen en als er geen zijn, geef dat aan
        if (matchingReservations.Any())
        {
            Console.WriteLine($"Reservations for {targetEmail}:");
            for (int i = 0; i < matchingReservations.Count; i++)
            {
                Console.WriteLine($"Reservation number: {i}\n Date: {matchingReservations[i]["Time"]}");
            }
        }
        else
        {
            Console.WriteLine($"No reservations found for {targetEmail}.");
        }
    }




    static void Change_Reservation_Method(string jsonFilePath, int index)
    {
        Console.Write("Enter date and time (yyyy-MM-dd HH:mm) or exit: ");
        string userInput = Console.ReadLine() + ":00";
        string format = "yyyy-MM-dd HH:mm:ss";
        if (userInput.ToUpper() == "EXIT:00")
        {
            return;
        }

        if (DateTime.TryParseExact(userInput, format, null, DateTimeStyles.None, out DateTime result))
        {
            Tuple<DateTime, DateTime> New_Reservation_Time = new Tuple<DateTime, DateTime>(result, result.AddHours(2));
            List<Reservation> List_of_Reservations = Reserve.ReadFromJsonFile();
            
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            // Json data pakken
            JArray reservations = JsonConvert.DeserializeObject<JArray>(jsonContent);
            if (index >= 0 && index < reservations.Count)
            {

                JToken newTimeToken = JToken.FromObject(New_Reservation_Time);
                reservations[index]["Time"] = newTimeToken;
                string updatedJsonContent = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, updatedJsonContent);
            }
            Console.Clear();
            Console.WriteLine(
@"+--------------------------------+
|                                |
| Succesfully changed the reser- |
| vation!!                       |
|                                |
+--------------------------------+");
            return; // Voltooid van reservering aanpssen, terug naar super admin menu
        }
        else
        {
            Console.WriteLine("Invalid date format");
        }
    }

    
    public static void ShowReservationsForDay(DateTime selectedDate)
    {
        List<Reservation> reservations = Reserve.ReadFromJsonFile();

        if (reservations != null)
        {

            foreach (var reservation in reservations)
            {
                if (reservation.Time.Item1.Date == selectedDate)
                {
                    Console.WriteLine($"\nTable ID {reservation.Table.TableID} has been Reserved at {reservation.Time.Item1}");
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
    }


}


