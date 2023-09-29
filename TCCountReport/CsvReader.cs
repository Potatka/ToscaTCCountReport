
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace ToscaTCCountReport
{
    public class CsvReaderHelper

    {
        string filePath;
        public CsvReaderHelper(string filepath) {
            this.filePath = filepath;
        }
        public List<string> ReadCsvAndGetIDs()
        {
            List<string> idList = new List<string>();
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Assuming the CSV has headers, use ReadHeader to skip them
                csv.Read();

                while (csv.Read())
                {   
                    string personId = csv.GetField<string>(1); // Index 1 corresponds to the second column (0-based)
                    if (personId != "") { idList.Add(personId); }
                }
            }
            return idList;
        }
    }
}
