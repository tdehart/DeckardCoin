namespace AHBot
{
    partial class AHBotForm
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
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.processIdsInput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.searchLowerBoundInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchUpperBoundInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.itemFilesInput = new System.Windows.Forms.ComboBox();
            this.testModeCheckbox = new System.Windows.Forms.CheckBox();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.classSelectionInput = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numCharsInput = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.sResultsDelayTextbox = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.buyoutVarianceInput = new System.Windows.Forms.TextBox();
            this.logTabControl = new System.Windows.Forms.TabControl();
            this.activityPage = new System.Windows.Forms.TabPage();
            this.activityLogBox = new System.Windows.Forms.ListBox();
            this.boughtItemsPage = new System.Windows.Forms.TabPage();
            this.boughtItemsBox = new System.Windows.Forms.ListBox();
            this.itemFileRefreshButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pidRefreshButton = new System.Windows.Forms.Button();
            this.safeModeCheckbox = new System.Windows.Forms.CheckBox();
            this.logTabControl.SuspendLayout();
            this.activityPage.SuspendLayout();
            this.boughtItemsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(10, 166);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(85, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(244, 166);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(85, 23);
            this.closeButton.TabIndex = 14;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // processIdsInput
            // 
            this.processIdsInput.FormattingEnabled = true;
            this.processIdsInput.Location = new System.Drawing.Point(101, 7);
            this.processIdsInput.Name = "processIdsInput";
            this.processIdsInput.Size = new System.Drawing.Size(104, 21);
            this.processIdsInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Diablo 3 process:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search every";
            // 
            // searchLowerBoundInput
            // 
            this.searchLowerBoundInput.Location = new System.Drawing.Point(82, 64);
            this.searchLowerBoundInput.Name = "searchLowerBoundInput";
            this.searchLowerBoundInput.Size = new System.Drawing.Size(40, 20);
            this.searchLowerBoundInput.TabIndex = 2;
            this.searchLowerBoundInput.Text = "800";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "to";
            // 
            // searchUpperBoundInput
            // 
            this.searchUpperBoundInput.Location = new System.Drawing.Point(144, 64);
            this.searchUpperBoundInput.Name = "searchUpperBoundInput";
            this.searchUpperBoundInput.Size = new System.Drawing.Size(40, 20);
            this.searchUpperBoundInput.TabIndex = 3;
            this.searchUpperBoundInput.Text = "1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "milliseconds";
            // 
            // itemFilesInput
            // 
            this.itemFilesInput.FormattingEnabled = true;
            this.itemFilesInput.Location = new System.Drawing.Point(58, 36);
            this.itemFilesInput.Name = "itemFilesInput";
            this.itemFilesInput.Size = new System.Drawing.Size(100, 21);
            this.itemFilesInput.TabIndex = 8;
            // 
            // testModeCheckbox
            // 
            this.testModeCheckbox.AutoSize = true;
            this.testModeCheckbox.Location = new System.Drawing.Point(155, 142);
            this.testModeCheckbox.Name = "testModeCheckbox";
            this.testModeCheckbox.Size = new System.Drawing.Size(76, 17);
            this.testModeCheckbox.TabIndex = 9;
            this.testModeCheckbox.Text = "Test mode";
            this.testModeCheckbox.UseVisualStyleBackColor = true;
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(96, 89);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(35, 20);
            this.delayTextBox.TabIndex = 10;
            this.delayTextBox.Text = "20";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Click delay (ms):";
            // 
            // classSelectionInput
            // 
            this.classSelectionInput.FormattingEnabled = true;
            this.classSelectionInput.Items.AddRange(new object[] {
            "Barbarian",
            "Demon Hunter",
            "Monk",
            "Witch Doctor",
            "Wizard"});
            this.classSelectionInput.Location = new System.Drawing.Point(119, 114);
            this.classSelectionInput.Name = "classSelectionInput";
            this.classSelectionInput.Size = new System.Drawing.Size(112, 21);
            this.classSelectionInput.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Default search class:";
            // 
            // numCharsInput
            // 
            this.numCharsInput.Location = new System.Drawing.Point(298, 114);
            this.numCharsInput.Name = "numCharsInput";
            this.numCharsInput.Size = new System.Drawing.Size(32, 20);
            this.numCharsInput.TabIndex = 7;
            this.numCharsInput.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(239, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "# of chars:";
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(277, 10);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(60, 13);
            this.versionLabel.TabIndex = 18;
            this.versionLabel.Text = "version";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(142, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Search results delay (ms):";
            // 
            // sResultsDelayTextbox
            // 
            this.sResultsDelayTextbox.Location = new System.Drawing.Point(270, 89);
            this.sResultsDelayTextbox.Name = "sResultsDelayTextbox";
            this.sResultsDelayTextbox.Size = new System.Drawing.Size(35, 20);
            this.sResultsDelayTextbox.TabIndex = 5;
            this.sResultsDelayTextbox.Text = "500";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(103, 166);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(85, 23);
            this.stopButton.TabIndex = 13;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Buyout variance:";
            // 
            // buyoutVarianceInput
            // 
            this.buyoutVarianceInput.Location = new System.Drawing.Point(100, 139);
            this.buyoutVarianceInput.Name = "buyoutVarianceInput";
            this.buyoutVarianceInput.Size = new System.Drawing.Size(40, 20);
            this.buyoutVarianceInput.TabIndex = 11;
            this.buyoutVarianceInput.Text = "1000";
            // 
            // logTabControl
            // 
            this.logTabControl.Controls.Add(this.activityPage);
            this.logTabControl.Controls.Add(this.boughtItemsPage);
            this.logTabControl.Location = new System.Drawing.Point(343, 5);
            this.logTabControl.Name = "logTabControl";
            this.logTabControl.SelectedIndex = 0;
            this.logTabControl.Size = new System.Drawing.Size(318, 190);
            this.logTabControl.TabIndex = 28;
            // 
            // activityPage
            // 
            this.activityPage.Controls.Add(this.activityLogBox);
            this.activityPage.Location = new System.Drawing.Point(4, 22);
            this.activityPage.Name = "activityPage";
            this.activityPage.Padding = new System.Windows.Forms.Padding(3);
            this.activityPage.Size = new System.Drawing.Size(310, 164);
            this.activityPage.TabIndex = 0;
            this.activityPage.Text = "Activity Log";
            this.activityPage.UseVisualStyleBackColor = true;
            // 
            // activityLogBox
            // 
            this.activityLogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.activityLogBox.FormattingEnabled = true;
            this.activityLogBox.Location = new System.Drawing.Point(0, 2);
            this.activityLogBox.Name = "activityLogBox";
            this.activityLogBox.Size = new System.Drawing.Size(310, 156);
            this.activityLogBox.TabIndex = 27;
            // 
            // boughtItemsPage
            // 
            this.boughtItemsPage.Controls.Add(this.boughtItemsBox);
            this.boughtItemsPage.Location = new System.Drawing.Point(4, 22);
            this.boughtItemsPage.Name = "boughtItemsPage";
            this.boughtItemsPage.Padding = new System.Windows.Forms.Padding(3);
            this.boughtItemsPage.Size = new System.Drawing.Size(310, 164);
            this.boughtItemsPage.TabIndex = 1;
            this.boughtItemsPage.Text = "Bought Items";
            this.boughtItemsPage.UseVisualStyleBackColor = true;
            // 
            // boughtItemsBox
            // 
            this.boughtItemsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.boughtItemsBox.FormattingEnabled = true;
            this.boughtItemsBox.Location = new System.Drawing.Point(0, 2);
            this.boughtItemsBox.Name = "boughtItemsBox";
            this.boughtItemsBox.Size = new System.Drawing.Size(310, 156);
            this.boughtItemsBox.TabIndex = 27;
            // 
            // itemFileRefreshButton
            // 
            this.itemFileRefreshButton.Location = new System.Drawing.Point(164, 34);
            this.itemFileRefreshButton.Name = "itemFileRefreshButton";
            this.itemFileRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.itemFileRefreshButton.TabIndex = 30;
            this.itemFileRefreshButton.Text = "Refresh";
            this.itemFileRefreshButton.UseVisualStyleBackColor = true;
            this.itemFileRefreshButton.Click += new System.EventHandler(this.itemConfigRefreshButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Item file:";
            // 
            // pidRefreshButton
            // 
            this.pidRefreshButton.Location = new System.Drawing.Point(211, 5);
            this.pidRefreshButton.Name = "pidRefreshButton";
            this.pidRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.pidRefreshButton.TabIndex = 31;
            this.pidRefreshButton.Text = "Refresh";
            this.pidRefreshButton.UseVisualStyleBackColor = true;
            this.pidRefreshButton.Click += new System.EventHandler(this.pidRefreshButton_Click);
            // 
            // safeModeCheckbox
            // 
            this.safeModeCheckbox.AutoSize = true;
            this.safeModeCheckbox.Location = new System.Drawing.Point(237, 142);
            this.safeModeCheckbox.Name = "safeModeCheckbox";
            this.safeModeCheckbox.Size = new System.Drawing.Size(77, 17);
            this.safeModeCheckbox.TabIndex = 32;
            this.safeModeCheckbox.Text = "Safe mode";
            this.safeModeCheckbox.UseVisualStyleBackColor = true;
            // 
            // AHBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 196);
            this.Controls.Add(this.safeModeCheckbox);
            this.Controls.Add(this.pidRefreshButton);
            this.Controls.Add(this.itemFileRefreshButton);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.logTabControl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buyoutVarianceInput);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.sResultsDelayTextbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numCharsInput);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.classSelectionInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.testModeCheckbox);
            this.Controls.Add(this.itemFilesInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.searchUpperBoundInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchLowerBoundInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processIdsInput);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startButton);
            this.Name = "AHBotForm";
            this.Text = "Auction Buddy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AHBotForm_FormClosed_1);
            this.Load += new System.EventHandler(this.AHBotForm_Load);
            this.logTabControl.ResumeLayout(false);
            this.activityPage.ResumeLayout(false);
            this.boughtItemsPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox processIdsInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchLowerBoundInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchUpperBoundInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox itemFilesInput;
        private System.Windows.Forms.CheckBox testModeCheckbox;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox classSelectionInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox numCharsInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox sResultsDelayTextbox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox buyoutVarianceInput;
        private System.Windows.Forms.TabControl logTabControl;
        private System.Windows.Forms.TabPage activityPage;
        private System.Windows.Forms.ListBox activityLogBox;
        private System.Windows.Forms.TabPage boughtItemsPage;
        private System.Windows.Forms.ListBox boughtItemsBox;
        private System.Windows.Forms.Button itemFileRefreshButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button pidRefreshButton;
        private System.Windows.Forms.CheckBox safeModeCheckbox;

    }
}