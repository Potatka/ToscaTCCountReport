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


        public Dictionary<string, int> SearchForTcLogs(TCAddOnTaskContext context, TCProject project, List<string> badgeIdList)
        {
            Dictionary<string, int> badgeIdCountMap = new Dictionary<string, int>();

            foreach (string badgeID in badgeIdList)
            {
                string remoteELSearch = string.Format("=>SUBPARTS:TestCase[(CreatedAt=~\"^8/\")" +
                    "AND(CreatedAt=~\"2023\")" +
                    "AND(CreatedBy==\"{0}\")]", badgeID);
                
                  tCObjects = project.Search(remoteELSearch);
                List<TCObject> list = new List<TCObject>();              
                
                list = project.Search(remoteELSearch);
                int count = tCObjects.Count;
                badgeIdCountMap.Add(badgeID, count);
                
                
                
            }





           /* if (tCObjects.Count != 0)
            {
                foreach (TCObject obj in tCObjects)
                {
                    TestCase tc = obj as TestCase; if (tc != null)
                    {
                        tcs.Add(tc);
                        TcLogDataCollection eLogData = new TcLogDataCollection(tc);
                        tc.AllOwnedSubItems
                        //sort thru execution logs and add them to reporting list for later
                        if (eLogData.createdBy == "Unknown" && eLogData.duration == "0" && eLogData.createdAt == "Unknown"
                             && eLogData.modifiedAt == "Unknown" && eLogData.modifiedAt == "Unknown")
                        {
                            executionLogDataCollections.Add(eLogData);
                        }
                        executionLogDataCollections.Add(eLogData);
                    }

                }


            }
            else
            {
                context.ShowErrorMessage("Error", "No Execution Lists found!");
            }
            tCObjects.Clear();*/

            return badgeIdCountMap;
        }
    }
}
