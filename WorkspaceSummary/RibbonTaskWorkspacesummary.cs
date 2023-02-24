using System.Collections.Generic;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace InsoftExecutionLogReport
{
    // TCAddOnMenuItem: This class represents a menu item in the menu for an Addon
    class GenerateExecutionLogReport : TCAddOnMenuItem
    {
        // Unique ID Property for MenuItem
        public override string ID => "Generate Report";

        // Text Property for MenuItem
        public override string MenuText => "Execution Report";

        //Get the project
        readonly TCProject project = TCAddOn.ActiveWorkspace.GetTCProject();    
         
       
        // Override this method to implement the logic for the execution
        public override void Execute(TCAddOnTaskContext context)
        {
                     
            SearchHelper searchHelper = new SearchHelper();
            CsvWriter csvWriter= new CsvWriter();
            string documentsPath = context.GetFolderPath("Select save location");
            List<ELogDataCollection> eLogDataCollections= searchHelper.SearchForExecutionLogs(context,project);
            csvWriter.WriteToCsv(eLogDataCollections, context, documentsPath);            


            context.ShowMessageBox("", "Task finished!");

            
        }
    }
}

