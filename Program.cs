using CLIappDT.Models;
using CLIappDT.Services;

Console.WriteLine("Command list:");
Console.WriteLine("GetTripsByLocation -tbl\n" +
                  "GetLongestTripsByTime -ltbt\n" +
                  "GetLongestTripsByDistance -ltbd\n" +
                  "GetTopLocationTripWithMaxTipAmount -mxt\n" +
                  "To exit the program, type 'exit'");

TripService tripService = new TripService(new ApplicationContext());
List<Trip> trips;

while (true)
{
    string command = Console.ReadLine();
    if (String.IsNullOrEmpty(command))
    {
        Console.WriteLine("ERROR: Incorrect input");
    }

    if (command.ToLower() == "-i")
    {
        string pathToFile = Console.ReadLine();
        if (!String.IsNullOrEmpty(pathToFile))
        {
            await ImportData(pathToFile);
        }
    }
    else if (command.ToLower() == "-tbl")
    {
        Console.Write("Enter location id: ");
        int locationId;
        bool res = int.TryParse(Console.ReadLine(), out locationId);
        if (res)
        {
            trips = tripService.GetTripsByLocation(locationId);
            await ShowResult(trips);
        }
        else
        {
            Console.WriteLine("ERROR: Incorrect input");
        }
    }
    else if (command.ToLower() == "-ltbt")
    {
        trips = tripService.GetLongestTripsByTime();
        await ShowResult(trips);
    }
    else if (command.ToLower() == "-ltbd")
    {
        trips = tripService.GetLongestTripsByDistance();
        await ShowResult(trips);
    }
    else if (command.ToLower() == "-mxt")
    {
        trips = tripService.GetTopLocationTripWithMaxTipAmount();
        await ShowResult(trips);
    }
    else if(command.ToLower() == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("ERROR: Incorrect command");
    }
}

Console.ReadLine();

async Task ShowResult(List<Trip> trips)
{
    await Console.Out.WriteLineAsync("<=============================================================================================>");
    await Console.Out.WriteLineAsync($"| {"Trip ID",-7} | {"PULocationID",-12} | {"DOLocationID",-12} | {"Passangers",-10} | {"Distance",-8} | {"StoreAndFwdFlag",-15} | {"FareAmount",-11} | {"TipAmount",-12} |");

    foreach (Trip trip in trips)
    {
        await Console.Out.WriteLineAsync($"| {trip.Id,-7} | {trip.PULocationID,-12} | {trip.DOLocationID,-12} | {trip.PassangerCount,-10} | {trip.TripDistance,-8} | {trip.StoreAndFwdFlag,-15} | {trip.FareAmount,-11} | {trip.TipAmount,-12} |");
    }

    await Console.Out.WriteLineAsync("<=============================================================================================>");
}


async Task ImportData(string path)
{
    
    if (File.Exists(path))
    {
        TripService tripService = new TripService(new ApplicationContext());
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            try
            {
                await tripService.ImportTripsDataCSV(stream, false);
                Console.WriteLine("Imported successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    else
    {
        Console.WriteLine("File does not exist at the specified path.");
    }
}

