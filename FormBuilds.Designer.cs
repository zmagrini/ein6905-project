
namespace WindowsFormsApp1
{
    partial class FormBuilds
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadTimeLabel = new System.Windows.Forms.Label();
            this.loadTimeTextbox = new System.Windows.Forms.TextBox();
            this.nextBuildButton = new System.Windows.Forms.Button();
            this.buildInfo = new System.Windows.Forms.Label();
            this.workShiftsBox = new System.Windows.Forms.TextBox();
            this.workShiftsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.priorityLabel = new System.Windows.Forms.Label();
            this.priorityButton = new System.Windows.Forms.RadioButton();
            this.priorityNo = new System.Windows.Forms.RadioButton();
            this.buildNameBox = new System.Windows.Forms.TextBox();
            this.buildNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadTimeLabel
            // 
            this.loadTimeLabel.AutoSize = true;
            this.loadTimeLabel.Location = new System.Drawing.Point(299, 110);
            this.loadTimeLabel.Name = "loadTimeLabel";
            this.loadTimeLabel.Size = new System.Drawing.Size(187, 15);
            this.loadTimeLabel.TabIndex = 0;
            this.loadTimeLabel.Text = "Approximate Load time (in shifts):";
            // 
            // loadTimeTextbox
            // 
            this.loadTimeTextbox.Location = new System.Drawing.Point(333, 128);
            this.loadTimeTextbox.Name = "loadTimeTextbox";
            this.loadTimeTextbox.Size = new System.Drawing.Size(100, 23);
            this.loadTimeTextbox.TabIndex = 1;
            this.loadTimeTextbox.TextChanged += new System.EventHandler(this.buildTimeTextbox_TextChanged);
            // 
            // nextBuildButton
            // 
            this.nextBuildButton.Location = new System.Drawing.Point(342, 455);
            this.nextBuildButton.Name = "nextBuildButton";
            this.nextBuildButton.Size = new System.Drawing.Size(75, 23);
            this.nextBuildButton.TabIndex = 2;
            this.nextBuildButton.Text = "Next >";
            this.nextBuildButton.UseVisualStyleBackColor = true;
            this.nextBuildButton.Click += new System.EventHandler(this.nextBuildButton_Click);
            // 
            // buildInfo
            // 
            this.buildInfo.AutoSize = true;
            this.buildInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buildInfo.Location = new System.Drawing.Point(297, 23);
            this.buildInfo.Name = "buildInfo";
            this.buildInfo.Size = new System.Drawing.Size(207, 25);
            this.buildInfo.TabIndex = 3;
            this.buildInfo.Text = "BUILD "+ (FormPreliminaryInfo.currentBuild + 1) + " INFORMATION";
            // 
            // workShiftsBox
            // 
            this.workShiftsBox.Location = new System.Drawing.Point(333, 178);
            this.workShiftsBox.Name = "workShiftsBox";
            this.workShiftsBox.Size = new System.Drawing.Size(100, 23);
            this.workShiftsBox.TabIndex = 5;
            // 
            // workShiftsLabel
            // 
            this.workShiftsLabel.AutoSize = true;
            this.workShiftsLabel.Location = new System.Drawing.Point(348, 160);
            this.workShiftsLabel.Name = "workShiftsLabel";
            this.workShiftsLabel.Size = new System.Drawing.Size(70, 15);
            this.workShiftsLabel.TabIndex = 4;
            this.workShiftsLabel.Text = "Work Shifts:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Available Labs:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Lab 1",
            "Lab 2",
            "Lab 3",
            "Lab 4",
            "Lab 5",
            "Lab 6",
            "Lab 7",
            "All Labs"});
            this.checkedListBox1.Location = new System.Drawing.Point(348, 234);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(70, 146);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // priorityLabel
            // 
            this.priorityLabel.AutoSize = true;
            this.priorityLabel.Location = new System.Drawing.Point(358, 395);
            this.priorityLabel.Name = "priorityLabel";
            this.priorityLabel.Size = new System.Drawing.Size(50, 15);
            this.priorityLabel.TabIndex = 8;
            this.priorityLabel.Text = "Priority?";
            // 
            // priorityButton
            // 
            this.priorityButton.AutoSize = true;
            this.priorityButton.Location = new System.Drawing.Point(334, 413);
            this.priorityButton.Name = "priorityButton";
            this.priorityButton.Size = new System.Drawing.Size(42, 19);
            this.priorityButton.TabIndex = 10;
            this.priorityButton.TabStop = true;
            this.priorityButton.Text = "Yes";
            this.priorityButton.UseVisualStyleBackColor = true;
            // 
            // priorityNo
            // 
            this.priorityNo.AutoSize = true;
            this.priorityNo.Location = new System.Drawing.Point(393, 413);
            this.priorityNo.Name = "priorityNo";
            this.priorityNo.Size = new System.Drawing.Size(41, 19);
            this.priorityNo.TabIndex = 11;
            this.priorityNo.TabStop = true;
            this.priorityNo.Text = "No";
            this.priorityNo.UseVisualStyleBackColor = true;
            this.priorityNo.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // buildNameBox
            // 
            this.buildNameBox.Location = new System.Drawing.Point(333, 82);
            this.buildNameBox.Name = "buildNameBox";
            this.buildNameBox.Size = new System.Drawing.Size(100, 23);
            this.buildNameBox.TabIndex = 13;
            this.buildNameBox.TextChanged += new System.EventHandler(this.buildNameBox_TextChanged);
            // 
            // buildNameLabel
            // 
            this.buildNameLabel.AutoSize = true;
            this.buildNameLabel.Location = new System.Drawing.Point(346, 64);
            this.buildNameLabel.Name = "buildNameLabel";
            this.buildNameLabel.Size = new System.Drawing.Size(72, 15);
            this.buildNameLabel.TabIndex = 12;
            this.buildNameLabel.Text = "Build Name:";
            // 
            // FormBuilds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 525);
            this.Controls.Add(this.buildNameBox);
            this.Controls.Add(this.buildNameLabel);
            this.Controls.Add(this.priorityNo);
            this.Controls.Add(this.priorityButton);
            this.Controls.Add(this.priorityLabel);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.workShiftsBox);
            this.Controls.Add(this.workShiftsLabel);
            this.Controls.Add(this.buildInfo);
            this.Controls.Add(this.nextBuildButton);
            this.Controls.Add(this.loadTimeTextbox);
            this.Controls.Add(this.loadTimeLabel);
            this.Name = "FormBuilds";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Information";
            this.Load += new System.EventHandler(this.FormBuilds_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loadTimeLabel;
        private System.Windows.Forms.TextBox loadTimeTextbox;
        private System.Windows.Forms.Button nextBuildButton;
        private System.Windows.Forms.Label buildInfo;
        private System.Windows.Forms.TextBox workShiftsBox;
        private System.Windows.Forms.Label workShiftsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label priorityLabel;
        private System.Windows.Forms.RadioButton priorityButton;
        private System.Windows.Forms.RadioButton priorityNo;
        private System.Windows.Forms.TextBox buildNameBox;
        private System.Windows.Forms.Label buildNameLabel;
    }
}