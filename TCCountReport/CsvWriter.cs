using System;
using System.Collections.Generic;
using System.IO;
using Tricentis.TCAddOns;

namespace ToscaTCCountReport
{
    public class CsvWriter
    {
        readonly DateTime now = DateTime.Now;
        public CsvWriter()
        {

        }
        public void WriteToCsv(List<TcLogDataCollection> testCaseLog, TCAddOnTaskContext context, string documentsPath)
        {
            string timestamp = now.ToString("yyyyMMdd");
            string csvFileName = "ExecutionLogReport_" + timestamp + ".csv";
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

                    confirmDelete = (deleteDecision == MsgBoxResult.Yes) ? confirmDelete = true : confirmDelete = false;

                    if (!confirmDelete) { context.ShowMessageBox("Warning", "Attention,the report was not created!"); }
                }

                if (confirmDelete)
                {
                    File.Delete(filePath);
                }
            }
            
            //S
            foreach (TcLogDataCollection tcInfo in testCaseLog)
            {
                if (!File.Exists(filePath))
                {
                    var csvString = string.Format("{0},{1},{2}{3}",
                                                  "DisplayName", "CreatedByUser","CreatedTime", Environment.NewLine);
                    File.WriteAllText(filePath, csvString);
                }

                var csvData = string.Format("{0},{1},{2}{3}",
                                            tcInfo.DisplayedName, tcInfo.CreatedBy, tcInfo.CreatedAt,                                            
                                             Environment.NewLine);
                File.AppendAllText(filePath, csvData);
            }

            isWriterFinished = true;

            if (isWriterFinished == true && confirmDelete == true)
            {
                context.ShowMessageBox("Warning", String.Format("Report created in the following location: {0}"
                        , Environment.NewLine + Environment.NewLine + filePath));
            }
        }

    }
}
