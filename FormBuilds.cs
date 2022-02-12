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
            
            if (int.Parse(loadTimeTextbox.Text) < 1 || int.Parse(loadTimeTextbox.Text) > 20 || loadTimeTextbox.Text == null)
            {
                MessageBox.Show("Error! Build time must contain a valid response (1-20)!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (priorityButton.Checked == false && priorityNo.Checked == false)
            {
                MessageBox.Show("Error! Please choose whether this build is a priority build.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (buildNameBox.Text == null)
            {
                MessageBox.Show("Error! Please input build name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ALL DATA IS GONE AFTER EACH BUILD

            buildData.Add(this.buildNameBox.Text); 
            buildData.Add("L: " + int.Parse(this.loadTimeTextbox.Text));
            buildData.Add("W: " + int.Parse(this.workShiftsBox.Text));
            totalWorkTimePerBuild = int.Parse(this.loadTimeTextbox.Text) + int.Parse(workShiftsBox.Text);
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
    }
}
