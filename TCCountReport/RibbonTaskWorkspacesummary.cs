using System.Collections.Generic;
using ToscaTCCountReport;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaTCCountReport
{
    // TCAddOnMenuItem: This class represents a menu item in the menu for an Addon
    class GenerateExecutionLogReport : TCAddOnMenuItem
    {
        // Unique ID Property for MenuItem
        public override string ID => "Generate Report";

        // Text Property for MenuItem
        public override string MenuText => "TestCaseByUser Report";

        //Get the project
        readonly TCProject project = TCAddOn.ActiveWorkspace.GetTCProject();  
        
         
       
        // Override this method to implement the logic for the execution
        public override void Execute(TCAddOnTaskContext context) 
        {                     
            SearchHelper searchHelper = new SearchHelper();
            CsvWriter csvWriter= new CsvWriter();
            MonthSelectorHelper monthSelectorHelper = new MonthSelectorHelper();
            CsvReaderHelper csvReader = new CsvReaderHelper(context.GetFilePath("Select input CSV file"));               
            string documentsPath = context.GetFolderPath("Select save location");
            Dictionary<string,int> eLogDataCollections= searchHelper.SearchForTcLogs(context,project,csvReader.ReadCsvAndGetIDs(), monthSelectorHelper);
            csvWriter.WriteToCsv(eLogDataCollections, context, documentsPath,monthSelectorHelper.currentSelectedMonth,csvReader.ReadCsvAndGetIDNameMap());            


            context.ShowMessageBox("", "Task finished!");

            
        }
    }
}

