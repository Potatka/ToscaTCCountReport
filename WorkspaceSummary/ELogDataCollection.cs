using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAPIObjects.Objects;

namespace InsoftExecutionLogReport
{
    public class ELogDataCollection
    {
        //Sorting values
        public string DisplayedName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedAt { get; set; }
        public string Duration { get; set; }
        //Printable values
        public string NumberOfEntries { get; set; }
        public string NumberOfTestCases { get; set; }
        public string NumberOfTestCasesFailed { get; set; }
        public string NumberOfTestCasesPassed { get; set; }
        public string NumberOfTestCasesNotExecuted { get; set; }
        public string NumberOfTestCasesWithUnknownState { get; set; }


        public ELogDataCollection(ExecutionLog el)
        {
            //Get the sorting criteria for printing to report
            DisplayedName = el.DisplayedName;
            CreatedBy = el.GetAttributeValue("CreatedBy");
            CreatedAt = el.GetAttributeValue("CreatedAt");
            ModifiedBy = el.GetAttributeValue("ModifiedBy");
            ModifiedAt = el.GetAttributeValue("ModifiedAt");
            Duration = el.GetAttributeValue("Duration");

            //Get the values to be written to csv report
            NumberOfTestCases = el.GetAttributeValue("NumberOfTestCases");
            NumberOfTestCasesFailed = el.GetAttributeValue("NumberOfTestCasesFailed");
            NumberOfTestCasesPassed = el.GetAttributeValue("NumberOfTestCasesPassed");
            NumberOfTestCasesNotExecuted = el.GetAttributeValue("NumberOfTestCasesNotExecuted");
            NumberOfTestCasesWithUnknownState = el.GetAttributeValue("NumberOfTestCasesWithUnknownState");


        }

    }
}
