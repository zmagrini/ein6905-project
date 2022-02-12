
namespace WindowsFormsApp1
{
    partial class FormPreliminaryInfo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.numBuildsLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.numBuildsBox = new System.Windows.Forms.ComboBox();
            this.labsOpenBox = new System.Windows.Forms.ComboBox();
            this.labsOpenLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.availableWorkers = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.availableWorkers)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(215, 31);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(278, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "F35 Lab Schedule Optimization";
            // 
            // numBuildsLabel
            // 
            this.numBuildsLabel.AutoSize = true;
            this.numBuildsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numBuildsLabel.Location = new System.Drawing.Point(255, 83);
            this.numBuildsLabel.Name = "numBuildsLabel";
            this.numBuildsLabel.Size = new System.Drawing.Size(198, 21);
            this.numBuildsLabel.TabIndex = 1;
            this.numBuildsLabel.Text = "Number of Builds for Week";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(322, 320);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(68, 27);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // numBuildsBox
            // 
            this.numBuildsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numBuildsBox.FormattingEnabled = true;
            this.numBuildsBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.numBuildsBox.Location = new System.Drawing.Point(288, 107);
            this.numBuildsBox.Name = "numBuildsBox";
            this.numBuildsBox.Size = new System.Drawing.Size(132, 23);
            this.numBuildsBox.TabIndex = 3;
            // 
            // labsOpenBox
            // 
            this.labsOpenBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.labsOpenBox.FormattingEnabled = true;
            this.labsOpenBox.Items.AddRange(new object[] {
            "Monday-Thursday",
            "Monday-Friday",
            "Everyday"});
            this.labsOpenBox.Location = new System.Drawing.Point(286, 172);
            this.labsOpenBox.Name = "labsOpenBox";
            this.labsOpenBox.Size = new System.Drawing.Size(132, 23);
            this.labsOpenBox.TabIndex = 5;
            // 
            // labsOpenLabel
            // 
            this.labsOpenLabel.AutoSize = true;
            this.labsOpenLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labsOpenLabel.Location = new System.Drawing.Point(270, 148);
            this.labsOpenLabel.Name = "labsOpenLabel";
            this.labsOpenLabel.Size = new System.Drawing.Size(172, 21);
            this.labsOpenLabel.TabIndex = 4;
            this.labsOpenLabel.Text = "Labs Open (days/week)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(285, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Available Workers";
            // 
            // availableWorkers
            // 
            this.availableWorkers.Location = new System.Drawing.Point(294, 238);
            this.availableWorkers.Name = "availableWorkers";
            this.availableWorkers.Size = new System.Drawing.Size(120, 23);
            this.availableWorkers.TabIndex = 7;
            // 
            // FormPreliminaryInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 368);
            this.Controls.Add(this.availableWorkers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labsOpenBox);
            this.Controls.Add(this.labsOpenLabel);
            this.Controls.Add(this.numBuildsBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.numBuildsLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "FormPreliminaryInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Information";
            ((System.ComponentModel.ISupportInitialize)(this.availableWorkers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label numBuildsLabel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.ComboBox numBuildsBox;
        private System.Windows.Forms.ComboBox labsOpenBox;
        private System.Windows.Forms.Label labsOpenLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown availableWorkers;
    }
}

