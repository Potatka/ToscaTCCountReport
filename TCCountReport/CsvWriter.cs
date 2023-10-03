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

        public void WriteToCsv(Dictionary<string, int> badgeIdCountMap, TCAddOnTaskContext context, string documentsPath, string selectedMonth,Dictionary<string, string> resourceNameMap)
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

            foreach (string personId in badgeIdCountMap.Keys)
            {
                // Check if the personId exists in resourceNameMap
                if (resourceNameMap.ContainsKey(personId))
                {
                    // Get the value from resourceNameMap using the personId key
                    string resourceName = resourceNameMap[personId];
                    
                }
                else
                {
                    Console.WriteLine($"Person ID: {personId} does not have a matching resource.");
                }
            }

            foreach (var kvp in badgeIdCountMap)
            {
                string badgeID = kvp.Key;
                int count = kvp.Value;
                string resourceName;
                if (resourceNameMap.ContainsKey(badgeID))
                {
                    // Get the value from resourceNameMap using the personId key
                     resourceName = resourceNameMap[badgeID];

                }
                else
                {
                    resourceName = "Resource name not found for Badge ID";
                }
                if (!File.Exists(filePath))
                {
                    var csvString = string.Format("{0},{1},{2},{3}{4}",
                                                  "Resource Name","Badge ID", "Created TC Count", "Selected Month", Environment.NewLine);
                    File.WriteAllText(filePath, csvString);
                }

                var csvData = string.Format("{0},{1},{2},{3},{4}",
                                               resourceName, badgeID, count, selectedMonth, Environment.NewLine);
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
