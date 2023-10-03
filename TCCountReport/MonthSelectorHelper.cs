using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;

namespace ToscaTCCountReport
{
    public class MonthSelectorHelper
    {
        public string currentSelectedMonth { get; set; }
        public MonthSelectorHelper() {  }

        public int GetUserSelectedMonthforReport (TCAddOnTaskContext context)
        {
            List<(string MonthName, int MonthNumber)> monthList = new List<(string, int)>();
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(monthNames[i]))
                {
                    monthList.Add((monthNames[i], i + 1));
                }
            }
            List<string> monthNameList = monthList.Select(item => item.MonthName).ToList();
            string userSelectedReportMonth = context.GetStringSelection("Select report month", monthNameList);
            var selectedMonthNumeral = monthList.Find(month => month.MonthName.Equals(userSelectedReportMonth, StringComparison.OrdinalIgnoreCase));
            currentSelectedMonth = selectedMonthNumeral.MonthName;

            return selectedMonthNumeral.MonthNumber;
        }

    }
}
