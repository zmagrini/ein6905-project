using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class UpdatedBuilds : Form
    {
        FormPreliminaryInfo frm;
        public static List<object> swBuildData = new List<object>();
        public static List<List<object>> allSoftwareBuilds = new List<List<object>>();
        public static int totalWorkTimePerBuild;
        public UpdatedBuilds(FormPreliminaryInfo fr)
        {
            InitializeComponent();
            frm = fr;
        }

        private void UpdatedBuilds_Load(object sender, EventArgs e)
        {
            
            List<GroupBox> buildGroups = new List<GroupBox>()
            {
                build1Group,
                build2Group,
                build3Group,
                build4Group,
                build5Group,
                build6Group,
                build7Group,
                build8Group,
                build9Group,
                build10Group,
                build11Group,
                build12Group,
                build13Group,
                build14Group,
                build15Group,
                build16Group,
                build17Group,
                build18Group,
                build19Group,
                build20Group
            };
            for (int i = 0; i < FormPreliminaryInfo.numBuilds; i++)
            {
                buildGroups[i].Enabled = true;
            }
        }

        private void generateScheduleButton_Click(object sender, EventArgs e)
        {
            List<TextBox> buildNames = new List<TextBox>()
            {
                build1NameBox,
                buildName2Box,
                buildName3Box,
                buildName4Box,
                buildName5Box,
                buildName6Box,
                buildName7Box,
                buildName8Box,
                buildName9Box,
                buildName10Box,
                buildName11Box,
                buildName12Box,
                buildName13Box,
                buildName14Box,
                buildName15Box,
                buildName16Box,
                buildName17Box,
                buildName18Box,
                buildName19Box,
                buildName20Box
            };
            List<NumericUpDown> loadShifts = new List<NumericUpDown>()
            {
                loadShifts1UpDown,
                loadShifts2UpDown,
                loadShifts3UpDown,
                loadShifts4UpDown,
                loadShifts5UpDown,
                loadShifts6UpDown,
                loadShifts7UpDown,
                loadShifts8UpDown,
                loadShifts9UpDown,
                loadShifts10UpDown,
                loadShifts11UpDown,
                loadShifts12UpDown,
                loadShifts13UpDown,
                loadShifts14UpDown,
                loadShifts15UpDown,
                loadShifts16UpDown,
                loadShifts17UpDown,
                loadShifts18UpDown,
                loadShifts19UpDown,
                loadShifts20UpDown
            };
            List<NumericUpDown> workShifts = new List<NumericUpDown>()
            {
                workShifts1UpDown,
                workShifts2UpDown,
                workShifts3UpDown,
                workShifts4UpDown,
                workShifts5UpDown,
                workShifts6UpDown,
                workShifts7UpDown,
                workShifts8UpDown,
                workShifts9UpDown,
                workShifts10UpDown,
                workShifts11UpDown,
                workShifts12UpDown,
                workShifts13UpDown,
                workShifts14UpDown,
                workShifts15UpDown,
                workShifts16UpDown,
                workShifts17UpDown,
                workShifts18UpDown,
                workShifts19UpDown,
                workShifts20UpDown
            };
            List<CheckedListBox> labsChosen = new List<CheckedListBox>()
            {
                build1LabsBox,
                build2LabsBox,
                build3LabsBox,
                build4LabsBox,
                build5LabsBox,
                build6LabsBox,
                build7LabsBox,
                build8LabsBox,
                build9LabsBox,
                build10LabsBox,
                build11LabsBox,
                build12LabsBox,
                build13LabsBox,
                build14LabsBox,
                build15LabsBox,
                build16LabsBox,
                build17LabsBox,
                build18LabsBox,
                build19LabsBox,
                build20LabsBox
            };
            List<RadioButton> priorityButtons = new List<RadioButton>()
            {
                yesButton1,
                yesButton2,
                yesButton3,
                yesButton4,
                yesButton5,
                yesButton6,
                yesButton7,
                yesButton8,
                yesButton9,
                yesButton10,
                yesButton11,
                yesButton12,
                yesButton13,
                yesButton14,
                yesButton15,
                yesButton16,
                yesButton17,
                yesButton18,
                yesButton19,
                yesButton20
            };
            List<RadioButton> noButtons = new List<RadioButton>()
            {
                noButton1,
                noButton2,
                noButton3,
                noButton4,
                noButton5,
                noButton6,
                noButton7,
                noButton8,
                noButton9,
                noButton10,
                noButton11,
                noButton12,
                noButton13,
                noButton14,
                noButton15,
                noButton16,
                noButton17,
                noButton18,
                noButton19,
                noButton20
            };
            for (int i = 0; i < FormPreliminaryInfo.numBuilds; i++)
            {
                if (buildNames[i].Text == "")
                {
                    MessageBox.Show("Error! Please input build names for each build. Enter the required data correctly to continue!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (loadShifts[i].Value < 0)
                {
                    MessageBox.Show("Error! Load shifts cannot be a negative number!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (workShifts[i].Value < 1)
                {
                    MessageBox.Show("Error! Work shifts cannot be 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (labsChosen[i].CheckedItems.Count < 1)
                {
                    MessageBox.Show("Error! Lab requirements must be selected for each build!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (labsChosen[i].CheckedItems.Count > 1 && labsChosen[i].SelectedItems.Contains("All Labs"))
                {
                    MessageBox.Show("Error! If 'All Labs' is selected, no other labs may be chosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (priorityButtons[i].Checked == false && noButtons[i].Checked == false)
                {
                    MessageBox.Show("Error! Must choose whether each lab is a priority or not!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            for (int i = 0; i < FormPreliminaryInfo.numBuilds; i++)
            {
                swBuildData.Add(buildNames[i].Text);
                swBuildData.Add((int)loadShifts[i].Value);
                swBuildData.Add((int)workShifts[i].Value);
                totalWorkTimePerBuild = + ((int)loadShifts[i].Value + (int)workShifts[i].Value);
                foreach (string item in labsChosen[i].CheckedItems)
                    swBuildData.Add(item);
                if (priorityButtons[i].Checked == true)
                    swBuildData.Add("Yes");
                else
                    swBuildData.Add("No");
                List<object> copy = new List<object>(swBuildData);
                allSoftwareBuilds.Add(copy);
                swBuildData.Clear();
            }
            this.Hide();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm.Show();
        }

        private void UpdatedBuilds_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void loadShifts1Label_Click(object sender, EventArgs e)
        {

        }
    }
}
