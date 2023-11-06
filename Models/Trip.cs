using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIappDT.Models;

public class Trip
{
    private string storeAndFwdFlag;
    public int Id { get; set; }
    public DateTime PickUpDateTime { get; set; }
    public DateTime DropOffDateTime { get; set; }
    public int PassangerCount { get; set; }
    public double TripDistance { get; set; }
    public string StoreAndFwdFlag
    {
        get
        {
            return storeAndFwdFlag;
        }
        set
        {
            if (value == "N")
            {
                storeAndFwdFlag = "No";
            }
            else if (value == "Y")
            {
                storeAndFwdFlag = "Yes";
            }
            else
            {
                storeAndFwdFlag = value;
            }
        }
    }
    public int PULocationID { get; set; }
    public int DOLocationID { get; set; }
    public double FareAmount { get; set; }
    public double TipAmount { get; set; }
}
