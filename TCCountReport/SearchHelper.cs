using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaTCCountReport
{
    public class SearchHelper
    {
        private List<TCObject> tCObjects = new List<TCObject>();
        public List<ExecutionLog> executionLogs = new List<ExecutionLog>();
        public List<ELogDataCollection> executionLogDataCollections = new List<ELogDataCollection>();


        public List<ELogDataCollection> SearchForExecutionLogs(TCAddOnTaskContext context, TCProject project)
        {
            string remoteELSearch = string.Format("=>SUBPARTS:ExecutionLog[(Version==\"_<Version not set>\")" +
                "AND(SynchronizationPolicy==\"CustomizableDefaultIsOff\") " +
                "AND (NodePath=?\"/BCBSM Applications/Member Portal/3. Accepted/DX Execution\")]");

            //string basicELSearch = "=>SUBPARTS:ExecutionList->ActualExecutionLog";

            tCObjects = project.Search(remoteELSearch);

            if (tCObjects.Count != 0)
            {
                foreach (TCObject obj in tCObjects)
                {
                    ExecutionLog executionLog = obj as ExecutionLog; if (executionLog != null)
                    {
                        executionLogs.Add(executionLog);
                        ELogDataCollection eLogData = new ELogDataCollection(executionLog);

                        //sort thru execution logs and add them to reporting list for later
                        /*if (eLogData.createdBy == "Unknown" && eLogData.duration == "0" && eLogData.createdAt == "Unknown"
                             && eLogData.modifiedAt == "Unknown" && eLogData.modifiedAt == "Unknown")
                        {
                            executionLogDataCollections.Add(eLogData);
                        }*/
                        executionLogDataCollections.Add(eLogData);
                    }

                }


            }
            else
            {
                context.ShowErrorMessage("Error", "No Execution Lists found!");
            }
            tCObjects.Clear();

            return executionLogDataCollections;
        }
    }
}
