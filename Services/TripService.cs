using CLIappDT.Helpers;
using CLIappDT.Mappers;
using CLIappDT.Models;
using CsvHelper;
using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CLIappDT.Services;

class TripService
{
    private readonly ApplicationContext _context;
    public TripService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task ImportTripsDataCSV(Stream csvFile, bool? continueWithExceptions = false)
    {
        // insert your path
        string duplicatesCsvFile = "{your_path_to_duplicates.csv}";

        using var duplicatesWriter = new StreamWriter(duplicatesCsvFile);
        using var duplicatesCsv = new CsvWriter(duplicatesWriter, CultureInfo.InvariantCulture);

        using var reader = new StreamReader(csvFile);
        using var csv = CsvIOFactory.GetTripsReader(reader, out var exceptions, true, delimiter: ",");

        var uniqueCombinations = new HashSet<Tuple<DateTime, DateTime, int>>();
        csv.Context.RegisterClassMap<TripCSVMapper>();
        List<Trip> trips = new List<Trip>();

        while (await csv.ReadAsync())
        {
            try
            {
                var record = csv.GetRecord<Trip>();
                CheckHelper.CheckNull(record, "CSV file have not records");

                var combination = new Tuple<DateTime, DateTime, int>(record.PickUpDateTime, record.DropOffDateTime, record.PassangerCount);

                if (!uniqueCombinations.Contains(combination))
                {
                    uniqueCombinations.Add(combination);
                    trips.Add(record);
                }
                else
                {
                    duplicatesCsv.WriteRecord(record);
                }
                
            }
            catch { }
        }

        if (exceptions.Any() && !continueWithExceptions != null)
        {
            var sb = new StringBuilder();
            sb.AppendJoin(", ", exceptions);
            throw new Exception(
                $"CSV doesn't contain required fields or have bad data in '{sb}'");
        }
        await _context.Trips.AddRangeAsync(trips);
        await _context.SaveChangesAsync();
    }

    public List<Trip> GetTripsByLocation(int locationId)
    {
        var trips = _context.Trips.FromSqlRaw("EXEC FindTripsByLocation {0}", locationId).ToList();
        return trips;
    }

    public List<Trip> GetLongestTripsByTime()
    {
        var trips = _context.Trips.FromSqlRaw("EXEC FindLongestTripsByTime").ToList();
        return trips;
    }

    public List<Trip> GetLongestTripsByDistance()
    {
        var trips = _context.Trips.FromSqlRaw("EXEC FindLongestTripsByDistance").ToList();
        return trips;
    }

    public List<Trip> GetTopLocationTripWithMaxTipAmount()
    {
        var trip = _context.Trips.FromSqlRaw("EXEC FindLocationWithMaxTipAmount").ToList();
        return trip;
    }
}
