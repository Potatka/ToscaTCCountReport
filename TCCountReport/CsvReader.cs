
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
            List<string> nameList = new List<string>();
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Assuming the CSV has headers, use ReadHeader to skip them
                csv.Read();

                while (csv.Read())
                {   string personName = csv.GetField<string>(0);
                    string personId = csv.GetField<string>(1); // Index 1 corresponds to the second column (0-based)
                    if (personId != "") { idList.Add(personId); }
                    if (personName != "") { nameList.Add(personName); }
                }
            }


            return idList;
        }
        public Dictionary<string, string> ReadCsvAndGetIDNameMap()
        {
            Dictionary<string, string> idNameMap = new Dictionary<string, string>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Assuming the CSV has headers, use ReadHeader to skip them
                csv.Read();

                while (csv.Read())
                {
                    string personName = csv.GetField<string>(0);
                    string personId = csv.GetField<string>(1); // Index 1 corresponds to the second column (0-based)
                    if (!string.IsNullOrEmpty(personId) && !string.IsNullOrEmpty(personName))
                    {
                        idNameMap[personId] = personName;
                    }
                }
            }

            return idNameMap;
        }



    }
}
