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
        public static int daysPerWeek = 0;
        private static int labOnePossibleTime = 0, labTwoPossibleTime = 0, labThreePossibleTime = 0, labFourPossibleTime = 0,
            labFivePossibleTime = 0, labSixPossibleTime = 0, labSevenPossibleTime = 0, labTimeLeft1 = 0, labTimeLeft2 = 0;
        public static int currentBuild = 0, totalLab1TimeUsed = 0, totalLab2TimeUsed = 0, totalLab3TimeUsed = 0, totalLab4TimeUsed = 0, totalLab5TimeUsed = 0,
            totalLab6TimeUsed = 0, totalLab7TimeUsed = 0;
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
        public static List<int> shiftsRemainingPerLab = new List<int>();
        public static List<List<object>> labOnePrep = new List<List<object>>();
        public static List<List<object>> labTwoPrep = new List<List<object>>();
        public static List<List<object>> labThreePrep = new List<List<object>>();
        public static List<List<object>> labFourPrep = new List<List<object>>();
        public static List<List<object>> labFivePrep = new List<List<object>>();
        public static List<List<object>> labSixPrep = new List<List<object>>();
        public static List<List<object>> labSevenPrep = new List<List<object>>();
        object labCheck1, labCheck2, labCheck3, labCheck4, labCheck5, labCheck6;
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

                /****************************************/
                /*ALGORITHM TO FIND SCHEDULE STARTS HERE*/
                /****************************************/

                //for loop to detect the potential amount of shifts each lab could be scheduled for
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

                //add em to the list
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
               
                /*********************************************************************************************************************/
                //                                               PRIORITY BUILDS                                                     //
                /*********************************************************************************************************************/
                

                /*******************************************************************/
                /********* CASE ONE: PRIORITY BUILD, ONLY 1 LAB AVAILABLE **********/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if (((priorityBuilds.ElementAt(i).Count - 4) == 1) && (priorityBuilds.ElementAt(i).Contains("All Labs") == false)) //if priority build has only one lab it can be worked, add it to that lab's queue to be scheduled
                    {
                        //add to lab priority list (place these first)
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                        shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                    }
                }

                /*******************************************************************/
                /*********** CASE TWO: PRIORITY BUILD, 2 LABS AVAILABLE ************/
                /*******************************************************************/
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

                /*******************************************************************/
                /********** CASE THREE: PRIORITY BUILD, 3 LABS AVAILABLE ***********/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if ((priorityBuilds.ElementAt(i).Count - 4) == 3) //if 3 labs
                    {
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab 1
                        labCheck2 = priorityBuilds[i].ElementAt(4); //available lab 2
                        labCheck3 = priorityBuilds[i].ElementAt(5); //available lab 3
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        string labNumber2 = Regex.Replace((string)labCheck2, "Lab ", string.Empty); //to string
                        string labNumber3 = Regex.Replace((string)labCheck3, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        int indexToCheck2 = int.Parse(labNumber2) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck3 = int.Parse(labNumber3) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int timeRemaining1 = shiftsRemainingPerLab[indexToCheck1]; //gives shifts remaining in this lab
                        int timeRemaining2 = shiftsRemainingPerLab[indexToCheck2]; //gives shifts remaining in next lab
                        int timeRemaining3 = shiftsRemainingPerLab[indexToCheck3]; //gives shifts remaining in next lab
                        int labWithMoreTime = MoreMath.Max(timeRemaining1, timeRemaining2, timeRemaining3);
                        if (labWithMoreTime == timeRemaining1)
                        {
                            //labCheck1
                            labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining2)
                        {
                            //labCheck2
                            labBuildOrder[(string)labCheck2].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck2] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else
                        {
                            //labCheck3
                            labBuildOrder[(string)labCheck3].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck3] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                    }
                }

                /*******************************************************************/
                /*********** CASE FOUR: PRIORITY BUILD, 4 LABS AVAILABLE ***********/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if ((priorityBuilds.ElementAt(i).Count - 4) == 4) //if 4 labs
                    {
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab 1
                        labCheck2 = priorityBuilds[i].ElementAt(4); //available lab 2
                        labCheck3 = priorityBuilds[i].ElementAt(5); //available lab 3
                        labCheck4 = priorityBuilds[i].ElementAt(6); //available lab 4
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        string labNumber2 = Regex.Replace((string)labCheck2, "Lab ", string.Empty); //to string
                        string labNumber3 = Regex.Replace((string)labCheck3, "Lab ", string.Empty); //to string
                        string labNumber4 = Regex.Replace((string)labCheck4, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        int indexToCheck2 = int.Parse(labNumber2) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck3 = int.Parse(labNumber3) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck4 = int.Parse(labNumber4) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int timeRemaining1 = shiftsRemainingPerLab[indexToCheck1]; //gives shifts remaining in this lab
                        int timeRemaining2 = shiftsRemainingPerLab[indexToCheck2]; //gives shifts remaining in next lab
                        int timeRemaining3 = shiftsRemainingPerLab[indexToCheck3]; //gives shifts remaining in next lab
                        int timeRemaining4 = shiftsRemainingPerLab[indexToCheck4]; //gives shifts remaining in next lab
                        int labWithMoreTime = MoreMath.Max(timeRemaining1, timeRemaining2, timeRemaining3, timeRemaining4);
                        if (labWithMoreTime == timeRemaining1)
                        {
                            //labCheck1
                            labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining2)
                        {
                            //labCheck2
                            labBuildOrder[(string)labCheck2].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck2] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining3)
                        {
                            //labCheck3
                            labBuildOrder[(string)labCheck3].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck3] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else
                        {
                            //labCheck4
                            labBuildOrder[(string)labCheck4].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck4] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                    }
                }

                /*******************************************************************/
                /*********** CASE FIVE: PRIORITY BUILD, 5 LABS AVAILABLE ***********/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if ((priorityBuilds.ElementAt(i).Count - 4) == 5) //if 6 labs
                    {
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab 1
                        labCheck2 = priorityBuilds[i].ElementAt(4); //available lab 2
                        labCheck3 = priorityBuilds[i].ElementAt(5); //available lab 3
                        labCheck4 = priorityBuilds[i].ElementAt(6); //available lab 4
                        labCheck5 = priorityBuilds[i].ElementAt(7); //available lab 5
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        string labNumber2 = Regex.Replace((string)labCheck2, "Lab ", string.Empty); //to string
                        string labNumber3 = Regex.Replace((string)labCheck3, "Lab ", string.Empty); //to string
                        string labNumber4 = Regex.Replace((string)labCheck4, "Lab ", string.Empty); //to string
                        string labNumber5 = Regex.Replace((string)labCheck5, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        int indexToCheck2 = int.Parse(labNumber2) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck3 = int.Parse(labNumber3) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck4 = int.Parse(labNumber4) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck5 = int.Parse(labNumber5) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int timeRemaining1 = shiftsRemainingPerLab[indexToCheck1]; //gives shifts remaining in this lab
                        int timeRemaining2 = shiftsRemainingPerLab[indexToCheck2]; //gives shifts remaining in next lab
                        int timeRemaining3 = shiftsRemainingPerLab[indexToCheck3]; //gives shifts remaining in next lab
                        int timeRemaining4 = shiftsRemainingPerLab[indexToCheck4]; //gives shifts remaining in next lab
                        int timeRemaining5 = shiftsRemainingPerLab[indexToCheck5]; //gives shifts remaining in next lab
                        int labWithMoreTime = MoreMath.Max(timeRemaining1, timeRemaining2, timeRemaining3, timeRemaining4, timeRemaining5);
                        if (labWithMoreTime == timeRemaining1)
                        {
                            //labCheck1
                            labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining2)
                        {
                            //labCheck2
                            labBuildOrder[(string)labCheck2].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck2] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining3)
                        {
                            //labCheck3
                            labBuildOrder[(string)labCheck3].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck3] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining4)
                        {
                            //labCheck4
                            labBuildOrder[(string)labCheck4].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck4] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else
                        {
                            //labCheck5
                            labBuildOrder[(string)labCheck5].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck5] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                    }
                }

                /*******************************************************************/
                /*********** CASE SIX: PRIORITY BUILD, 6 LABS AVAILABLE ***********/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if ((priorityBuilds.ElementAt(i).Count - 4) == 6) //if 6 labs
                    {
                        labCheck1 = priorityBuilds[i].ElementAt(3); //available lab 1
                        labCheck2 = priorityBuilds[i].ElementAt(4); //available lab 2
                        labCheck3 = priorityBuilds[i].ElementAt(5); //available lab 3
                        labCheck4 = priorityBuilds[i].ElementAt(6); //available lab 4
                        labCheck5 = priorityBuilds[i].ElementAt(7); //available lab 5
                        labCheck6 = priorityBuilds[i].ElementAt(8); //available lab 6
                        string labNumber1 = Regex.Replace((string)labCheck1, "Lab ", string.Empty); //to string
                        string labNumber2 = Regex.Replace((string)labCheck2, "Lab ", string.Empty); //to string
                        string labNumber3 = Regex.Replace((string)labCheck3, "Lab ", string.Empty); //to string
                        string labNumber4 = Regex.Replace((string)labCheck4, "Lab ", string.Empty); //to string
                        string labNumber5 = Regex.Replace((string)labCheck5, "Lab ", string.Empty); //to string
                        string labNumber6 = Regex.Replace((string)labCheck6, "Lab ", string.Empty); //to string
                        int indexToCheck1 = int.Parse(labNumber1) - 1; //converts to int and gets the index of shiftsRemainingPerLab for first available lab
                        int indexToCheck2 = int.Parse(labNumber2) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck3 = int.Parse(labNumber3) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck4 = int.Parse(labNumber4) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck5 = int.Parse(labNumber5) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int indexToCheck6 = int.Parse(labNumber6) - 1; //converts to int and gets the index of shiftsRemainingPerLab for next available lab
                        int timeRemaining1 = shiftsRemainingPerLab[indexToCheck1]; //gives shifts remaining in this lab
                        int timeRemaining2 = shiftsRemainingPerLab[indexToCheck2]; //gives shifts remaining in next lab
                        int timeRemaining3 = shiftsRemainingPerLab[indexToCheck3]; //gives shifts remaining in next lab
                        int timeRemaining4 = shiftsRemainingPerLab[indexToCheck4]; //gives shifts remaining in next lab
                        int timeRemaining5 = shiftsRemainingPerLab[indexToCheck5]; //gives shifts remaining in next lab
                        int timeRemaining6 = shiftsRemainingPerLab[indexToCheck6]; //gives shifts remaining in next lab
                        int labWithMoreTime = MoreMath.Max(timeRemaining1, timeRemaining2, timeRemaining3, timeRemaining4, timeRemaining5, timeRemaining6);
                        if (labWithMoreTime == timeRemaining1)
                        {
                            //labCheck1
                            labBuildOrder[(string)labCheck1].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck1] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining2)
                        {
                            //labCheck2
                            labBuildOrder[(string)labCheck2].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck2] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining3)
                        {
                            //labCheck3
                            labBuildOrder[(string)labCheck3].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck3] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining4)
                        {
                            //labCheck4
                            labBuildOrder[(string)labCheck4].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck4] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else if (labWithMoreTime == timeRemaining5)
                        {
                            //labCheck5
                            labBuildOrder[(string)labCheck5].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck5] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                        else 
                        {
                            //labCheck6
                            labBuildOrder[(string)labCheck6].Add(priorityBuilds.ElementAt(i));
                            shiftsRemainingPerLab[indexToCheck6] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                        }
                    }
                }

                /*******************************************************************/
                /*************** CASE SEVEN: PRIORITY BUILD, ANY LAB ***************/
                /*******************************************************************/
                for (int i = 0; i < priorityBuilds.Count; i++)
                {
                    if (((priorityBuilds.ElementAt(i).Count - 4) == 1) && (priorityBuilds.ElementAt(i).Contains("All Labs"))) //if priority build has only one lab it can be worked, add it to that lab's queue to be scheduled
                    {
                        int indexOfMax = shiftsRemainingPerLab.IndexOf(shiftsRemainingPerLab.Max());
                        string labName = "Lab " + (indexOfMax + 1);
                        labBuildOrder[labName].Add(priorityBuilds.ElementAt(i));
                        shiftsRemainingPerLab[indexOfMax] -= (int)priorityBuilds[i].ElementAt(1) + (int)priorityBuilds[i].ElementAt(2);
                    }

                }

                /*********************************************************************************************************************/
                //                                             NON-PRIORITY BUILDS                                                   //
                /*********************************************************************************************************************/



                //placing labs into schedule algorithm


                /*********************************************************************************************************************/
                //                                             SCHEDULE PREPARATION                                                  //
                /*********************************************************************************************************************/
                
                // PREP BUILD 1
                for (int i = 0; i < labOneBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labOnePrep.Add((List<object>)labOneBuildOrder.ElementAt(i));
                    int count = labOnePrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labOnePrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labOnePrep.Count; i++)
                {
                    int totalTime1 = (int)labOnePrep[i].ElementAt(1) + (int)labOnePrep[i].ElementAt(2);
                    totalLab1TimeUsed += totalTime1;
                }

                // PREP BUILD 2
                for (int i = 0; i < labTwoBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labTwoPrep.Add((List<object>)labTwoBuildOrder.ElementAt(i));
                    int count = labTwoPrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labTwoPrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labTwoPrep.Count; i++)
                {
                    int totalTime2 = (int)labTwoPrep[i].ElementAt(1) + (int)labTwoPrep[i].ElementAt(2);
                    totalLab2TimeUsed += totalTime2;
                }

                // PREP BUILD 3
                for (int i = 0; i < labThreeBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labThreePrep.Add((List<object>)labThreeBuildOrder.ElementAt(i));
                    int count = labThreePrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labThreePrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labThreePrep.Count; i++)
                {
                    int totalTime3 = (int)labThreePrep[i].ElementAt(1) + (int)labThreePrep[i].ElementAt(2);
                    totalLab3TimeUsed += totalTime3;
                }

                // PREP BUILD 4
                for (int i = 0; i < labFourBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labFourPrep.Add((List<object>)labFourBuildOrder.ElementAt(i));
                    int count = labFourPrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labFourPrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labFourPrep.Count; i++)
                {
                    int totalTime4 = (int)labFourPrep[i].ElementAt(1) + (int)labFourPrep[i].ElementAt(2);
                    totalLab4TimeUsed += totalTime4;
                }

                // PREP BUILD 5
                for (int i = 0; i < labFiveBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labFivePrep.Add((List<object>)labFiveBuildOrder.ElementAt(i));
                    int count = labFivePrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labFivePrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labFivePrep.Count; i++)
                {
                    int totalTime5 = (int)labFivePrep[i].ElementAt(1) + (int)labFivePrep[i].ElementAt(2);
                    totalLab5TimeUsed += totalTime5;
                }

                // PREP BUILD 6
                for (int i = 0; i < labSixBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labSixPrep.Add((List<object>)labSixBuildOrder.ElementAt(i));
                    int count = labSixPrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labSixPrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labSixPrep.Count; i++)
                {
                    int totalTime6 = (int)labSixPrep[i].ElementAt(1) + (int)labSixPrep[i].ElementAt(2);
                    totalLab6TimeUsed += totalTime6;
                }

                // PREP BUILD 7
                for (int i = 0; i < labSevenBuildOrder.Count; i++) //finding labs for priority builds
                {
                    labSevenPrep.Add((List<object>)labSevenBuildOrder.ElementAt(i));
                    int count = labSevenPrep[i].Count();
                    for (int j = 3; j < count; j++)
                    {
                        labSevenPrep[i].RemoveAt(3);
                    }
                }
                for (int i = 0; i < labSevenPrep.Count; i++)
                {
                    int totalTime7 = (int)labSevenPrep[i].ElementAt(1) + (int)labSevenPrep[i].ElementAt(2);
                    totalLab7TimeUsed += totalTime7;
                }

                //PLACE SCHEDULE AND OPEN GRAPHIC
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
