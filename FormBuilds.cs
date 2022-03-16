using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormBuilds : Form
    {
         
        List<string> availableLabs = new List<string>();
        public static List<List<object>> allBuilds = new List<List<object>>();
        public static string buildName;
        public static int totalWorkTimePerBuild;
        public static List<object> buildData = new List<object>();
        int check;

        public FormBuilds()
        {
            
            InitializeComponent();
            buildNameBox.Select();
        }
        
        private void buildTimeTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void nextBuildButton_Click(object sender, EventArgs e)
        {

            if (loadTimeTextbox.Text == "")
            {
                MessageBox.Show("Error! Please enter the required data to continue!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.TryParse(loadTimeTextbox.Text, out check) == false)
            {
                MessageBox.Show("Error! Work shifts must contain only numeric values (1-30)!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(loadTimeTextbox.Text) < 0 || int.Parse(loadTimeTextbox.Text) > 20)
            {
                MessageBox.Show("Error! Load shifts must contain a valid response (0-20)!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (workShiftsBox.Text == "")
            {
                MessageBox.Show("Error! Please enter the required data to continue!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.TryParse(workShiftsBox.Text, out check) == false)
            { 
                MessageBox.Show("Error! Work shifts must contain only numeric values (1-30)!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(workShiftsBox.Text) < 1 || int.Parse(workShiftsBox.Text) > 30)
            {
                MessageBox.Show("Error! Work shifts must contain a valid response (1-30)!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (priorityButton.Checked == false && priorityNo.Checked == false)
            {
                MessageBox.Show("Error! Please choose whether this build is a priority build.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (buildNameBox.Text == null)
            {
                MessageBox.Show("Error! Please input build name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Error! A lab must be selected.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkedListBox1.CheckedItems.Count > 1 && checkedListBox1.SelectedItems.Contains("All Labs"))
            {
                MessageBox.Show("Error! 'All Labs' cannot be selected with other options.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (priorityButton.Checked == false && priorityNo.Checked == false)
            {
                MessageBox.Show("Error! Please choose whether this build is a priority or not.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //ALL DATA IS GONE AFTER EACH BUILD

            buildData.Add(this.buildNameBox.Text); 
            buildData.Add(int.Parse(this.loadTimeTextbox.Text));
            buildData.Add(int.Parse(this.workShiftsBox.Text));
            totalWorkTimePerBuild =+ int.Parse(this.loadTimeTextbox.Text) + int.Parse(workShiftsBox.Text);
            foreach (string item in this.checkedListBox1.CheckedItems)
            {
                buildData.Add(item);
            }
            if (priorityButton.Checked == true)
                buildData.Add("Yes");
            else
                buildData.Add("No");
            List<object> copy = new List<object>(buildData);
            allBuilds.Add(copy);
            buildData.Clear();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormBuilds_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buildNameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
