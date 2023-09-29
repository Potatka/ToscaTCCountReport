using System;
using System.Collections.Generic;
using System.IO;
using Tricentis.TCAddOns;

namespace ToscaTCCountReport
{
    public class CsvWriter
    {
        readonly DateTime now = DateTime.Now;
        string filePath;

        public CsvWriter()
        {
            
        }

        public void WriteToCsv(Dictionary<string, int> badgeIdCountMap, TCAddOnTaskContext context, string documentsPath)
        {
            string timestamp = now.ToString("yyyyMMdd");
            string csvFileName = "TestCaseCountReport" + timestamp + ".csv";
            string filePath = Path.Combine(documentsPath, csvFileName);
            bool confirmDelete = false;
            bool isWriterFinished;

            if (File.Exists(filePath))
            {
                // Check if context object is not null
                if (!confirmDelete)
                {
                    // Display a yes/no message box using the context object
                    MsgBoxResult deleteDecision = context.ShowMessageBox_Yes_No("Warning", String.Format("Report already exists in the following location: {0}"
                        , Environment.NewLine + Environment.NewLine + filePath) + Environment.NewLine + "Do you want to overwrite it?");

                    confirmDelete = (deleteDecision == MsgBoxResult.Yes);

                    if (!confirmDelete)
                    {
                        context.ShowMessageBox("Warning", "Attention, the report was not created!");
                        return;
                    }
                }

                if (confirmDelete)
                {
                    File.Delete(filePath);
                }
            }

            foreach (var kvp in badgeIdCountMap)
            {
                string badgeID = kvp.Key;
                int count = kvp.Value;

                if (!File.Exists(filePath))
                {
                    var csvString = string.Format("{0},{1}{2}",
                                                  "Badge ID", "Created TC Count", Environment.NewLine);
                    File.WriteAllText(filePath, csvString);
                }

                var csvData = string.Format("{0},{1},{2}",
                                               badgeID, count, Environment.NewLine);
                File.AppendAllText(filePath, csvData);
            }

            isWriterFinished = true;

            if (isWriterFinished)
            {
                context.ShowMessageBox("Warning", String.Format("Report created in the following location: {0}"
                        , Environment.NewLine + Environment.NewLine + filePath));
            }
        }
    }
}
