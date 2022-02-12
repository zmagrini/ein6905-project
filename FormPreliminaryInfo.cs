using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using FastMember;
using System.Xml;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using Microsoft.VisualBasic.FileIO;

namespace WindowsFormsApp1
{
    public partial class FormPreliminaryInfo : Form
    {
        //private static List<object> dataDump = new List<object>();
        private List<object> _scheduleData;
        int totalShifts = 0, daysPerWeek = 0;
        List<int> buildIndices = new List<int>();
        
        private List<List<object>> listOfBuilds = new List<List<object>>();
        public static int numBuilds;
        List<Label> priorityLabel = new List<Label>();
        
        //
        public static int currentBuild = 0;
        //private string buildName;
        private int totalWork;
        public FormPreliminaryInfo()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (numBuildsBox.SelectedItem == null || labsOpenBox.SelectedItem == null)
            {
                MessageBox.Show("Error! Must fill all data for the week.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                numBuilds = int.Parse(numBuildsBox.SelectedItem.ToString());
                if (labsOpenBox.Text == "Monday-Thursday")
                {
                    daysPerWeek = 4;
                }
                else if (labsOpenBox.Text == "Monday-Friday")
                {
                    daysPerWeek = 5;
                }
                else
                {
                    daysPerWeek = 7;
                }
                totalShifts = daysPerWeek * 4;
                this.Hide();
                for (currentBuild = 0; currentBuild < numBuilds; currentBuild++)
                {
                    
                    try
                    {
                        using (FormBuilds buildPopup = new FormBuilds())
                        {
                            buildPopup.ShowDialog();
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    } 
                }
                listOfBuilds = FormBuilds.allBuilds;
                if ((totalWork * 6) / availableWorkers.Value > 40)
                {
                    DialogResult answer = MessageBox.Show("Not enough workers to cover all shifts. Allow overtime?", "Overtime Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (answer == DialogResult.No)
                    {
                        MessageBox.Show("Optimal schedule cannot be found. More workers are needed to cover all shifts.", "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                 
                }
                //TODO: ALGORITHM TO FIND OPTIMAL SCHEDULE HERE//
                //greedy algorithm
                /*for (int i = 0; i < listOfBuilds.Count; i++)
                {
                    if (listOfBuilds.ElementAt(i).Contains("Yes"))
                    {
                        foreach (List<object> build in listOfBuilds.ElementAt(i)) //for each build in listOfBuilds that is priority
                        {
                            var stringList = build.OfType<string>();
                            priorityLabel.Add(new Label() { Text = stringList.ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter });//create a label of build name + L
                            //create a label of build name + W
                        }
                    }
                }*/
                MessageBox.Show("Optimal schedule found. Schedule created!", "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                FormSchedule createdSchedule = new FormSchedule();
                createdSchedule.ShowDialog();
            }
        }
        public bool Export<T>(List<T> list, string file, string sheetName)
        {
            bool exported = false;
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);
                workbook.SaveAs(file);
                exported = true;
            }
                return exported;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        
       /*List<object> addList ()
        {
            foreach (object scheduleData in FormBuilds.buildData)
            {

            }
        }*/

    }
}
