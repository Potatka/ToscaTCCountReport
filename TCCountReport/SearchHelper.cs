using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaTCCountReport
{
    public class SearchHelper
    {
        private List<TCObject> tCObjects = new List<TCObject>();
        public List<TestCase> tcs = new List<TestCase>();
        public List<TcLogDataCollection> testCaseDataCollections = new List<TcLogDataCollection>();
        


        public Dictionary<string, int> SearchForTcLogs(TCAddOnTaskContext context, TCProject project, List<string> badgeIdList,MonthSelectorHelper monthSelectorHelper)
        {
            Dictionary<string, int> badgeIdCountMap = new Dictionary<string, int>();          
           

            string remoteELSearch = string.Format("=>SUBPARTS:TestCase[(CreatedAt=~\"^{0}/\")" +
                "AND(CreatedAt=~\"2023\")]", monthSelectorHelper.GetUserSelectedMonthforReport(context));


            tCObjects = project.Search(remoteELSearch);

            if (tCObjects.Count != 0)
            {
                foreach (TCObject obj in tCObjects)
                {
                    TestCase tc = obj as TestCase; if (tc != null)
                    {
                        tcs.Add(tc);
                        TcLogDataCollection testCaseData = new TcLogDataCollection(tc);

                        testCaseDataCollections.Add(testCaseData);
                    }

                }

                foreach (string badgeID in badgeIdList)
                {
                    int testCaseCount = 0;
                    foreach (TcLogDataCollection testCase in testCaseDataCollections)
                    {

                        if (testCase.CreatedBy.ToLower() == (badgeID.ToLower()))
                        {
                            testCaseCount++;
                        }
                        
                    }
                    badgeIdCountMap.Add(badgeID, testCaseCount);

                }
            }
            else
            {
                context.ShowErrorMessage("Error", "No Test Cases found!");
            }
            tCObjects.Clear();

            return badgeIdCountMap;
        }
    }
}
