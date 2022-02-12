using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormSchedule : Form
    {

        List<string> days = new List<string>();
        List<string> shifts = new List<string>();
        List<Label> label = new List<Label>();
        List<TableLayoutPanel> labs = new List<TableLayoutPanel>();
        List<object> allBuildData = new List<object>();
        Label dataBlah = new Label();
        List<object> allData = new List<object>();
        List<Label> convertToLabels = new List<Label>();
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
            
            for (int i = 0; i < labs.Count; i++)
            {
                for (int k = 0; k < shifts.Count; k++)
                {
                    label.Add(new Label() { Text = shifts[k], BackColor = Color.LightBlue, Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter });
                    labs[i].Controls.Add(label[k], 0, k + 1);
                }
                label.RemoveRange(0, 4);
                for (int j = 0; j < 7; j++)
                {
                    label.Add(new Label() { Text = days[j], BackColor = Color.LightBlue, Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter });
                    labs[i].Controls.Add(label[j], j+1, 0);
                }
                label.RemoveRange(0, 7);
            }
            
        }

        private void labOneTable_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
            if (e.Column == 0)
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
