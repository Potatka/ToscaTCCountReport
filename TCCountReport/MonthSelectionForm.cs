using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToscaTCCountReport
{
    public class MonthSelectionForm : Form
    {
        private CheckedListBox monthListBox;
        private Button okButton;
        private Button cancelButton;

        public List<string> SelectedMonths { get; private set; }

        public MonthSelectionForm(List<string> monthList)
        {
            SelectedMonths = new List<string>();

            // Initialize form controls, add them to the form, and handle events as needed.
            // You can populate the CheckedListBox with the month names.

            monthListBox = new CheckedListBox();
            okButton = new Button();
            cancelButton = new Button();

            // Add items to monthListBox and set up event handlers for buttons.

            okButton.Text = "OK";
            okButton.Click += OkButton_Click;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;

            // Set up the form layout.

            this.Text = "Select Months";
            this.Size = new System.Drawing.Size(300, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            monthListBox.Location = new System.Drawing.Point(10, 10);
            monthListBox.Size = new System.Drawing.Size(250, 300);
            monthListBox.Items.AddRange(monthList.ToArray());

            okButton.Location = new System.Drawing.Point(10, 320);
            okButton.Size = new System.Drawing.Size(100, 30);

            cancelButton.Location = new System.Drawing.Point(140, 320);
            cancelButton.Size = new System.Drawing.Size(100, 30);
           
            monthListBox.Font = new System.Drawing.Font("Arial", 12);
            monthListBox.CheckOnClick = true;


            this.Controls.Add(monthListBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            foreach (var item in monthListBox.CheckedItems)
            {
                SelectedMonths.Add(item.ToString());
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            
            Close();
        }
    }
}
