using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Formats.Asn1;

namespace CLIappDT.Mappers;

internal class CsvIOFactory
{
    public static CsvReader GetTripsReader(StreamReader streamReader, out List<string> fieldExeptions, bool autoDetectDelimiters = false, string delimiter = ";")
    {
        List<string> exeptions = new List<string>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter != ";" ? delimiter : ";",
            DetectDelimiter = autoDetectDelimiters,
            HeaderValidated = null,
            MissingFieldFound = ex =>
            {
                if (ex.HeaderNames != null)
                {
                    foreach (var nameEx in ex.HeaderNames)
                    {
                        exeptions.Add(nameEx);
                    }
                }
            }
        };

        fieldExeptions = exeptions;

        return new CsvReader(streamReader, config);

    }
}
