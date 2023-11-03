using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tricentis.TCAddOns;
using System.Windows.Forms; // Add Windows Forms namespace

namespace ToscaTCCountReport
{
    public class MonthSelectorHelper
    {
        List<(string MonthName, int MonthNumber) > monthList = new List<(string, int)>();
        public string currentSelectedMonth { get; set; }
        public MonthSelectorHelper() {

            List<(string MonthName, int MonthNumber)> monthList = new List<(string, int)>();
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(monthNames[i]))
                {
                    monthList.Add((monthNames[i], i + 1));
                }
            }
        }

        public List<string> GetUserSelectedMonthforReport(TCAddOnTaskContext context)
        {
            
            List<string> monthNameList = monthList.Select(item => item.MonthName).ToList();

            // Open the custom month selection dialog
            List<string> selectedMonths = OpenMonthSelectionDialog(monthNameList);

            return selectedMonths;
        }

        private List<string> OpenMonthSelectionDialog(List<string> monthNameList)
        {
            List<string> selectedMonths = new List<string>();

            using (var form = new MonthSelectionForm(monthNameList))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    selectedMonths = form.SelectedMonths;
                }
            }

            return selectedMonths;
        }

        public string GetUsersSelectedYearForReport(TCAddOnTaskContext context)
        {
            List<string> yearList = new List<string> { "2021", "2022", "2023", "2024" };
            var selectedYearNumeral = context.GetStringSelection("Select report year:", yearList, DateTime.Now.Year.ToString());

            return selectedYearNumeral;
        }
    }

   
}
