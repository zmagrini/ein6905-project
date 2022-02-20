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
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class FormPreliminaryInfo : Form
    {
        private List<string> priorityLabs = new List<string>();
        int totalShifts = 0, daysPerWeek = 0;
        private static int labOnePossibleTime = 0, labTwoPossibleTime = 0, labThreePossibleTime = 0, labFourPossibleTime = 0,
            labFivePossibleTime = 0, labSixPossibleTime = 0, labSevenPossibleTime = 0, labTimeLeft1 = 0, labTimeLeft2 = 0;
        public static int currentBuild = 0;
        List<int> potentialLabWorkTime = new List<int>();
        private List<List<object>> priorityBuilds = new List<List<object>>();
        List<object> lowPriorityBuilds = new List<object>();
        private static List<object> labOneBuildOrder = new List<object>();
        private static List<object> labTwoBuildOrder = new List<object>();
        private static List<object> labThreeBuildOrder = new List<object>();
        private static List<object> labFourBuildOrder = new List<object>();
        private static List<object> labFiveBuildOrder = new List<object>();
        private static List<object> labSixBuildOrder = new List<object>();
        private static List<object> labSevenBuildOrder = new List<object>();
        private static List<int> shiftsRemainingPerLab = new List<int>();
        object labCheck1, labCheck2, labCheck3, labCheck4, labCheck5, labCheck6;
        string labFound1, labFound2;
        private List<List<object>> listOfBuilds = new List<List<object>>();
        public static int numBuilds;
        List<Label> priorityLabel = new List<Label>();
        
        Dictionary<string, List<object>> labBuildOrder = new Dictionary<string, List<object>>()
                        {
                            {"Lab 1", labOneBuildOrder},
                            {"Lab 2", labTwoBuildOrder},
                            {"Lab 3", labThreeBuildOrder},
                            {"Lab 4", labFourBuildOrder},
                            {"Lab 5", labFiveBuildOrder},
                            {"Lab 6", labSixBuildOrder},
                            {"Lab 7", labSevenBuildOrder},
                        };
        
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
                shiftsRemainingPerLab.AddRange(new List<int>
                {
                    daysPerWeek * 4,
                    daysPerWeek * 4,
                    daysPerWeek * 4,
                    daysPerWeek * 4,
                    daysPerWeek * 4,
                    daysPerWeek * 4,
                    daysPerWeek * 4
                });
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
                listOfBuilds = FormBuilds.allBuilds; //list of all builds
                if ((FormBuilds.totalWorkTimePerBuild * 6) / availableWorkers.Value > 40)
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
                /*
                 1.  */
                for (int i = 0; i < listOfBuilds.Count; i++) //for each build
                {
                    //find how much work each lab could potentially have
                    if (listOfBuilds.ElementAt(i).Contains("Lab 1"))
                        labOnePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 2"))
                        labTwoPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 3"))
                        labThreePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 4"))
                        labFourPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 5"))
                        labFivePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 6"))
                        labSixPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("Lab 7"))
                        labSevenPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    if (listOfBuilds.ElementAt(i).Contains("All Labs"))
                    {
                        labOnePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labTwoPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labThreePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labFourPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labFivePossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labSixPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                        labSevenPossibleTime += ((int)listOfBuilds[i].ElementAt(1) + (int)listOfBuilds[i].ElementAt(2));
                    }
                        
                    

                    //if not priority and can go in any lab, add to low priority builds (will be added to schedule last based on current lab availability) 
                    if (listOfBuilds.ElementAt(i).Contains("All Labs") && listOfBuilds.ElementAt(i).Contains("No"))
                        lowPriorityBuilds.Add(listOfBuilds.ElementAt(i));
                    
                    //if its a priority build, add it to list of priority builds (added to schedule first ***greedy algorithm***)
                    if (listOfBuilds.ElementAt(i).Contains("Yes"))
                        priorityBuilds.Add(listOfBuilds.ElementAt(i));
                }
                potentialLabWorkTime.AddRange(new List<int>
                {
                    labOnePossibleTime,
                    labTwoPossibleTime,
                    labThreePossibleTime,
                    labFourPossibleTime,
                    labFivePossibleTime,
                    labSixPossibleTime,
                    labSevenPossibleTime
                });

                //this for loop places all priority builds with 1 available lab into labs they will be worked
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if (((priorityBuilds.ElementAt(i).Count - 4) == 1) && !priorityBuilds.ElementAt(i).Contains("All Labs")) //if priority build has only one lab it can be worked, add it to that lab's queue to be scheduled
                    {
                        //add to lab priority list (place these first)
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                        shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                    }
                }

                //this for loop is for the 2 lab case
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if ((priorityBuilds.ElementAt(i).Count - 4) == 2) //if 2 labs
                    {
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab
                        labCheck2 = priorityBuilds[i].ElementAt(4); //available lab
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        string labNumber2 = Regex.Replace((string)labCheck2, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        int indexToCheck2 = int.Parse(labNumber2) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int timeRemaining1 = shiftsRemainingPerLab[indexToCheck1]; //gives shifts remaining in this lab
                        int timeRemaining2 = shiftsRemainingPerLab[indexToCheck2]; //gives shifts remaining in next lab
                        int labWithMoreTime = Math.Max(timeRemaining1, timeRemaining2);
                        if (labWithMoreTime == timeRemaining1)
                        {
                            //labCheck1
                            labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else
                        {
                            //labCheck2
                            labBuildOrder[(string)labCheck2].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck2] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                    }
                }
                    /*else if ((priorityBuilds.ElementAt(i).Count - 4) == 3) //if 3 labs
                    {
                        //place these third
                    }
                    else if ((priorityBuilds.ElementAt(i).Count - 4) == 4) //if 4 labs
                    {
                        //place these fourth
                    }
                    else if ((priorityBuilds.ElementAt(i).Count - 4) == 5) //if 5 labs
                    {
                        //place these fifth
                    }
                    else if ((priorityBuilds.ElementAt(i).Count - 4) == 6) //if 6 labs
                    {
                        //place these sixth
                    }
                    else //any lab
                    {
                        //place 7th
                        //int minIndex = potentialLabWorkTime.IndexOf(potentialLabWorkTime.Min());

                    }*/
                

                //add the potential lab work times to a list
                

                //placing labs into schedule algorithm
                //do i sort these into lists of priorities? aka priority builds available in one lab go first. priority available in 2 labs goes next, etc.

                /*for (int i = 0; i < priorityBuilds.Count; i++) //finding labs for priority builds
                {
                    if (priorityBuilds.ElementAt(i).ToString().Contains("Lab"))
                    {
                        priorityLabs.Add()
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
