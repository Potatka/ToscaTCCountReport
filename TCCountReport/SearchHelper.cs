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
        public List<TestCase> tcs = new List<TestCase>();
        public List<TcLogDataCollection> executionLogDataCollections = new List<TcLogDataCollection>();


        public List<TcLogDataCollection> SearchForTcLogs(TCAddOnTaskContext context, TCProject project)
        {
            string remoteELSearch = string.Format("=>SUBPARTS:TestCase[(CreatedAt=~\"^9/\")" +
                "AND(CreatedAt=~\"2023\")" +
                "AND(CreatedBy==\"Admin\")]");

            //string basicELSearch = "=>SUBPARTS:ExecutionList->ActualExecutionLog";

            tCObjects = project.Search(remoteELSearch);
           

            if (tCObjects.Count != 0)
            {
                foreach (TCObject obj in tCObjects)
                {
                    TestCase tc = obj as TestCase; if (tc != null)
                    {
                        tcs.Add(tc);
                        TcLogDataCollection eLogData = new TcLogDataCollection(tc);

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
