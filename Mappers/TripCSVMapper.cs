using CLIappDT.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CLIappDT.Mappers;
public class TripCSVMapper : ClassMap<Trip>
{
    public TripCSVMapper() 
    {
        Map(m => m.PickUpDateTime).Name("tpep_pickup_datetime");
        Map(m => m.DropOffDateTime).Name("tpep_dropoff_datetime");
        Map(m => m.PassangerCount).Name("passenger_count");
        Map(m => m.TripDistance).Name("trip_distance");
        Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag");
        Map(m => m.PULocationID).Name("PULocationID");
        Map(m => m.DOLocationID).Name("DOLocationID");
        Map(m => m.FareAmount).Name("fare_amount");
        Map(m => m.TipAmount).Name("tip_amount");
    }
}
