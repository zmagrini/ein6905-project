using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class FormSchedule : Form
    {
        int labOneTime = 0, labTwoTime = 0, labThreeTime = 0, labFourTime = 0, labFiveTime = 0, labSixTime = 0, labSevenTime = 0;
        List<string> days = new List<string>();
        List<string> shifts = new List<string>();
        List<Label> label = new List<Label>();
        List<TableLayoutPanel> labs = new List<TableLayoutPanel>();
        List<object> allBuildData = new List<object>();
        Label dataBlah = new Label();
        List<object> allData = new List<object>();
        List<Label> convertToLabels = new List<Label>();
        private static List<List<object>> labOneList = new List<List<object>>();
        private static List<List<object>> labTwoList = new List<List<object>>();
        private static List<List<object>> labThreeList = new List<List<object>>();
        private static List<List<object>> labFourList = new List<List<object>>();
        private static List<List<object>> labFiveList = new List<List<object>>();


        private void FormSchedule_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private static List<List<object>> labSixList = new List<List<object>>();
        private static List<List<object>> labSevenList = new List<List<object>>();
        public FormSchedule()
        {
            InitializeComponent();
            foreach(object foo in allData)
            {
                convertToLabels.Add(new Label() { Text = foo.ToString() });
            }
           
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            days.AddRange(new List<string>
            {
                new string ("Mon"),
                new string ("Tues"),
                new string ("Wed"),
                new string ("Thurs"),
                new string ("Fri"),
                new string ("Sat"),
                new string ("Sun")
            });
            shifts.AddRange(new List<string>
            {
                new string ("Shift 1"),
                new string ("Shift 2"),
                new string ("Shift 3"),
                new string ("Shift 4")
            });
            labs.AddRange(new List<TableLayoutPanel>
            {
                labOneTable,
                labTwoTable,
                labThreeTable,
                labFourTable,
                labFiveTable,
                labSixTable,
                labSevenTable
            });

            loadShiftBox.BackColor = Color.LightCoral;
            workShiftBox.BackColor = Color.LightSkyBlue;

            //format schedule tables
            for (int i = 0; i < labs.Count; i++)
            {
                for (int k = 0; k < shifts.Count; k++)
                {
                    label.Add(new Label() { Text = shifts[k], BackColor = Color.LightBlue, Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter });
                    labs[i].Controls.Add(label[k], 0, k + 1);
                }
                label.RemoveRange(0, 4);
                for (int j = 0; j < days.Count; j++)
                {
                    label.Add(new Label() { Text = days[j], BackColor = Color.LightBlue, Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter });
                    labs[i].Controls.Add(label[j], j+1, 0);
                }
                label.RemoveRange(0, 7);
            }
            
            //get data from previous form
            labOneList = FormPreliminaryInfo.labOnePrep;
            labOneTime = FormPreliminaryInfo.totalLab1TimeUsed;
            int labOneTotal = labOneTime;
            labTwoList = FormPreliminaryInfo.labTwoPrep;
            labTwoTime = FormPreliminaryInfo.totalLab2TimeUsed;
            int labTwoTotal = labTwoTime;
            labThreeList = FormPreliminaryInfo.labThreePrep;
            labThreeTime = FormPreliminaryInfo.totalLab3TimeUsed;
            int labThreeTotal = labThreeTime;
            labFourList = FormPreliminaryInfo.labFourPrep;
            labFourTime = FormPreliminaryInfo.totalLab4TimeUsed;
            int labFourTotal = labFourTime;
            labFiveList = FormPreliminaryInfo.labFivePrep;
            labFiveTime = FormPreliminaryInfo.totalLab5TimeUsed;
            int labFiveTotal = labFiveTime;
            labSixList = FormPreliminaryInfo.labSixPrep;
            labSixTime = FormPreliminaryInfo.totalLab6TimeUsed;
            int labSixTotal = labSixTime;
            labSevenList = FormPreliminaryInfo.labSevenPrep;
            labSevenTime = FormPreliminaryInfo.totalLab7TimeUsed;
            int labSevenTotal = labSevenTime;

            /******************** LAB ONE BEGIN **********************/
            if (labOneTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labOneTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list

            for (int i = 0; i < (int)labOneList.Count; i++)
            {
                for (int j = 0; j < (int)labOneList[i].ElementAt(1); j++)
                {
                    if (labOneTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labOneList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labOneTime -=  1;
                    }
                        
                }
                for (int j = 0; j < (int)labOneList[i].ElementAt(2); j++)
                {
                    if (labOneTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labOneList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labOneTime -= 1;
                    }
                        
                }
            }
            //if the lab can eliminate all shift 1, do so
            if (labOneTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labOneTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labOneTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab2;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labOneTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labOneTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labOneTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labOneTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labOneTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab2;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[0].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }
                
                for (int column = currentColumn; column < labOneTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labOneTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab2;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labOneTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labOneTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab2;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            /****************** LAB ONE END **************************/

            /******************** LAB TWO BEGIN **********************/
            Lab2: if (labTwoTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labTwoTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labTwoList.Count; i++)
            {
                for (int j = 0; j < (int)labTwoList[i].ElementAt(1); j++)
                {
                    if (labTwoTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labTwoList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labTwoTime -= 1;
                    }  
                }
                for (int j = 0; j < (int)labTwoList[i].ElementAt(2); j++)
                {
                    if (labTwoTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labTwoList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labTwoTime -= 1;
                    }
                        

                }
            }

            //if the lab can eliminate all shift 1, do so
            if (labTwoTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labTwoTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labTwoTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab3;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labTwoTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labTwoTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labTwoTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labTwoTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labTwoTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab3;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labTwoTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labTwoTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab3;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labTwoTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labTwoTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab3;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
        /****************** LAB TWO END **************************/

        /******************** LAB THREE BEGIN **********************/
        Lab3: if (labThreeTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labThreeTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labThreeList.Count; i++)
            {
                for (int j = 0; j < (int)labThreeList[i].ElementAt(1); j++)
                {
                    if (labThreeTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labThreeList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labThreeTime -= 1;
                    }
                }
                for (int j = 0; j < (int)labThreeList[i].ElementAt(2); j++)
                {
                    if (labThreeTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labThreeList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labThreeTime -= 1;
                    }
                }
            }

            //if the lab can eliminate all shift 1, do so
            if (labThreeTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labThreeTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labThreeTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labThreeTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labThreeTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labThreeTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labThreeTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labThreeTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labThreeTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labThreeTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labThreeTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labThreeTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
        /****************** LAB THREE END **************************/

        /******************** LAB FOUR BEGIN **********************/
        Lab4: if (labFourTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labFourTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labFourList.Count; i++)
            {
                for (int j = 0; j < (int)labFourList[i].ElementAt(1); j++)
                {
                    if (labFourTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labFourList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labFourTime -= 1;
                    }
                }
                for (int j = 0; j < (int)labFourList[i].ElementAt(2); j++)
                {
                    if (labFourTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labFourList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labFourTime -= 1;
                    }
                }
            }

            //if the lab can eliminate all shift 1, do so
            if (labFourTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labFourTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labFourTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab5;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labFourTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labFourTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labFourTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labFourTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labFourTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab5;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labFourTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labFourTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab5;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labFourTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labFourTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab5;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
        /****************** LAB FOUR END **************************/

        /******************** LAB FIVE BEGIN **********************/
        Lab5: if (labFiveTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labFiveTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labFiveList.Count; i++)
            {
                for (int j = 0; j < (int)labFiveList[i].ElementAt(1); j++)
                {
                    if (labFiveTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labFiveList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labFiveTime -= 1;
                    }
                }
                for (int j = 0; j < (int)labFiveList[i].ElementAt(2); j++)
                {
                    if (labFiveTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labFiveList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labFiveTime -= 1;
                    }
                        
                }
            }

            //if the lab can eliminate all shift 1, do so
            if (labFiveTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labFiveTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labFiveTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab6;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labFiveTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labFiveTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labFiveTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labFiveTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labFiveTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab6;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labFiveTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labFiveTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab6;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labFiveTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labFiveTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab6;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
        /****************** LAB FIVE END **************************/

        /******************** LAB SIX BEGIN **********************/
        Lab6: if (labSixTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labSixTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labSixList.Count; i++)
            {
                for (int j = 0; j < (int)labSixList[i].ElementAt(1); j++)
                {
                    if (labSixTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labSixList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labSixTime -= 1;
                    }
                        
                }
                for (int j = 0; j < (int)labSixList[i].ElementAt(2); j++)
                {
                    if (labSixTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labSixList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labSixTime -= 1;
                    }
                }
            }

            //if the lab can eliminate all shift 1, do so
            if (labSixTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labSixTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labSixTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab7;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labSixTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labSixTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labSixTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labSixTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labSixTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab7;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labSixTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labSixTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab7;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labSixTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labSixTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab7;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
        /****************** LAB SIX END **************************/

        /******************** LAB SEVEN BEGIN **********************/
        Lab7: if (labSevenTime > (FormPreliminaryInfo.daysPerWeek * 4))
                labSevenTime = FormPreliminaryInfo.daysPerWeek * 4;

            //add all lab one labels to the list
            for (int i = 0; i < (int)labSevenList.Count; i++)
            {
                for (int j = 0; j < (int)labSevenList[i].ElementAt(1); j++)
                {
                    if (labSevenTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labSevenList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightCoral });
                        labSevenTime -= 1;
                    }
                        
                }
                for (int j = 0; j < (int)labSevenList[i].ElementAt(2); j++)
                {
                    if (labSevenTime > 0)
                    {
                        label.Add(new Label() { Text = (string)labSevenList[i].ElementAt(0), Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.LightSkyBlue });
                        labSevenTime -= 1;
                    }
                }
            }

             //if the lab can eliminate all shift 1, do so
            if (labSevenTotal <= (FormPreliminaryInfo.daysPerWeek * 3))
            {
                for (int column = 1; column < labSevenTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labSevenTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[0].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //else if it can eliminate some shift 1, do so
            else if (labSevenTotal < (FormPreliminaryInfo.daysPerWeek * 4) && labSevenTotal > (FormPreliminaryInfo.daysPerWeek * 3))
            {
                int shiftOneDaysToSchedule = labSevenTotal % FormPreliminaryInfo.daysPerWeek;
                int currentColumn = 0;
                for (int column = 1; column < labSevenTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labSevenTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        if (shiftOneDaysToSchedule > 0)
                        {
                            labs[1].Controls.Add(label[0], column, row);
                            label.RemoveAt(0);
                        }
                    }
                    shiftOneDaysToSchedule -= 1;
                    if (shiftOneDaysToSchedule == 0)
                    {
                        currentColumn = column + 1;
                        break;
                    }
                }

                for (int column = currentColumn; column < labSevenTable.ColumnCount; column++)
                {
                    for (int row = 2; row < labSevenTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            //if lab is full, place them
            else
            {
                for (int column = 1; column < labSevenTable.ColumnCount; column++)
                {
                    for (int row = 1; row < labSevenTable.RowCount; row++)
                    {
                        if (label.Count <= 0)
                        {
                            goto Lab4;
                        }
                        labs[1].Controls.Add(label[0], column, row);
                        label.RemoveAt(0);
                    }
                }
            }
            /****************** LAB SEVEN END **************************/


        }

        private void labOneTable_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
            if (e.Column == 0)
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
            if (FormPreliminaryInfo.daysPerWeek == 4)
            {
                if ((e.Column == 5 || e.Column == 6 || e.Column == 7) && (e.Row == 1 || e.Row == 2 || e.Row == 3 || e.Row == 4))
                    e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
            else if (FormPreliminaryInfo.daysPerWeek == 5)
            {
                if ((e.Column == 6 || e.Column == 7) && (e.Row == 1 || e.Row == 2 || e.Row == 3 || e.Row == 4))
                    e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
