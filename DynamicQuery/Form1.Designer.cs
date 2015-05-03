namespace DynamicQuery
{
    partial class QueryBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryBuilder));
            this.CustomerListBox = new System.Windows.Forms.ListBox();
            this.SampleDrugsListBox = new System.Windows.Forms.ListBox();
            this.SamplesListBox = new System.Windows.Forms.ListBox();
            this.SelectedItemsListBox = new System.Windows.Forms.ListBox();
            this.Customerlabel = new System.Windows.Forms.Label();
            this.SampleLabel = new System.Windows.Forms.Label();
            this.SelectedLabel = new System.Windows.Forms.Label();
            this.SampleDrugsLabel = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.StepOneGroupBox = new System.Windows.Forms.GroupBox();
            this.steponelabel3 = new System.Windows.Forms.Label();
            this.Steponelabel2 = new System.Windows.Forms.Label();
            this.StepOneResetButton = new System.Windows.Forms.Button();
            this.StepTwoGroupBox = new System.Windows.Forms.GroupBox();
            this.resetFilters2 = new System.Windows.Forms.Button();
            this.ResetFilters = new System.Windows.Forms.Button();
            this.StepTwoNextButton = new System.Windows.Forms.Button();
            this.StepThreeGroupBox = new System.Windows.Forms.GroupBox();
            this.countfoundlabel1 = new System.Windows.Forms.Label();
            this.Exportbutton = new System.Windows.Forms.Button();
            this.ResultsdataGridView1 = new System.Windows.Forms.DataGridView();
            this.Email = new System.Windows.Forms.Button();
            this.removeallbutton5 = new System.Windows.Forms.Button();
            this.ViewURL = new System.Windows.Forms.Button();
            this.ListboxMoveDown = new System.Windows.Forms.Button();
            this.LisboxMoveUp = new System.Windows.Forms.Button();
            this.Removebutton4 = new System.Windows.Forms.Button();
            this.Print = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.CartgroupBox = new System.Windows.Forms.GroupBox();
            this.StepSwitchbutton2 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.StepOneGroupBox.SuspendLayout();
            this.StepTwoGroupBox.SuspendLayout();
            this.StepThreeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsdataGridView1)).BeginInit();
            this.CartgroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomerListBox
            // 
            this.CustomerListBox.FormattingEnabled = true;
            this.CustomerListBox.ItemHeight = 16;
            this.CustomerListBox.Location = new System.Drawing.Point(42, 45);
            this.CustomerListBox.Name = "CustomerListBox";
            this.CustomerListBox.Size = new System.Drawing.Size(120, 148);
            this.CustomerListBox.TabIndex = 0;
            this.CustomerListBox.DoubleClick += new System.EventHandler(this.CustomerListBox_DoubleClick_1);
            // 
            // SampleDrugsListBox
            // 
            this.SampleDrugsListBox.FormattingEnabled = true;
            this.SampleDrugsListBox.ItemHeight = 16;
            this.SampleDrugsListBox.Location = new System.Drawing.Point(306, 45);
            this.SampleDrugsListBox.Name = "SampleDrugsListBox";
            this.SampleDrugsListBox.Size = new System.Drawing.Size(120, 148);
            this.SampleDrugsListBox.TabIndex = 1;
            this.SampleDrugsListBox.DoubleClick += new System.EventHandler(this.SampleDrugsListBox_SelectedIndexChanged);
            // 
            // SamplesListBox
            // 
            this.SamplesListBox.FormattingEnabled = true;
            this.SamplesListBox.ItemHeight = 16;
            this.SamplesListBox.Location = new System.Drawing.Point(172, 45);
            this.SamplesListBox.Name = "SamplesListBox";
            this.SamplesListBox.Size = new System.Drawing.Size(120, 148);
            this.SamplesListBox.TabIndex = 2;
            this.SamplesListBox.DoubleClick += new System.EventHandler(this.SamplesListBox_SelectedIndexChanged);
            // 
            // SelectedItemsListBox
            // 
            this.SelectedItemsListBox.FormattingEnabled = true;
            this.SelectedItemsListBox.ItemHeight = 16;
            this.SelectedItemsListBox.Location = new System.Drawing.Point(549, 45);
            this.SelectedItemsListBox.Name = "SelectedItemsListBox";
            this.SelectedItemsListBox.Size = new System.Drawing.Size(120, 148);
            this.SelectedItemsListBox.TabIndex = 3;
            this.SelectedItemsListBox.DoubleClick += new System.EventHandler(this.SelectedItemsListBox_DoubleClick);
            // 
            // Customerlabel
            // 
            this.Customerlabel.AutoSize = true;
            this.Customerlabel.Location = new System.Drawing.Point(39, 28);
            this.Customerlabel.Name = "Customerlabel";
            this.Customerlabel.Size = new System.Drawing.Size(123, 16);
            this.Customerlabel.TabIndex = 4;
            this.Customerlabel.Text = "Customer Fields";
            this.Customerlabel.Click += new System.EventHandler(this.Customerlabel_Click);
            // 
            // SampleLabel
            // 
            this.SampleLabel.AutoSize = true;
            this.SampleLabel.Location = new System.Drawing.Point(182, 28);
            this.SampleLabel.Name = "SampleLabel";
            this.SampleLabel.Size = new System.Drawing.Size(106, 16);
            this.SampleLabel.TabIndex = 5;
            this.SampleLabel.Text = "Sample Fields";
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Location = new System.Drawing.Point(562, 28);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(117, 16);
            this.SelectedLabel.TabIndex = 6;
            this.SelectedLabel.Text = "Selected Fields";
            // 
            // SampleDrugsLabel
            // 
            this.SampleDrugsLabel.AutoSize = true;
            this.SampleDrugsLabel.Location = new System.Drawing.Point(294, 28);
            this.SampleDrugsLabel.Name = "SampleDrugsLabel";
            this.SampleDrugsLabel.Size = new System.Drawing.Size(152, 16);
            this.SampleDrugsLabel.TabIndex = 7;
            this.SampleDrugsLabel.Text = "Sample Drugs Fields";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(12, 10);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(0, 29);
            this.HeaderLabel.TabIndex = 8;
            // 
            // StepOneGroupBox
            // 
            this.StepOneGroupBox.Controls.Add(this.steponelabel3);
            this.StepOneGroupBox.Controls.Add(this.Steponelabel2);
            this.StepOneGroupBox.Controls.Add(this.StepOneResetButton);
            this.StepOneGroupBox.Controls.Add(this.CustomerListBox);
            this.StepOneGroupBox.Controls.Add(this.SampleDrugsListBox);
            this.StepOneGroupBox.Controls.Add(this.SampleDrugsLabel);
            this.StepOneGroupBox.Controls.Add(this.SamplesListBox);
            this.StepOneGroupBox.Controls.Add(this.SelectedLabel);
            this.StepOneGroupBox.Controls.Add(this.SelectedItemsListBox);
            this.StepOneGroupBox.Controls.Add(this.SampleLabel);
            this.StepOneGroupBox.Controls.Add(this.Customerlabel);
            this.StepOneGroupBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepOneGroupBox.Location = new System.Drawing.Point(16, 48);
            this.StepOneGroupBox.Name = "StepOneGroupBox";
            this.StepOneGroupBox.Size = new System.Drawing.Size(695, 224);
            this.StepOneGroupBox.TabIndex = 10;
            this.StepOneGroupBox.TabStop = false;
            this.StepOneGroupBox.Text = "Step 1 : Choose Fields";
            // 
            // steponelabel3
            // 
            this.steponelabel3.AutoSize = true;
            this.steponelabel3.Location = new System.Drawing.Point(229, 200);
            this.steponelabel3.Name = "steponelabel3";
            this.steponelabel3.Size = new System.Drawing.Size(129, 16);
            this.steponelabel3.TabIndex = 11;
            this.steponelabel3.Text = "select / de-select";
            // 
            // Steponelabel2
            // 
            this.Steponelabel2.AutoSize = true;
            this.Steponelabel2.Location = new System.Drawing.Point(74, 200);
            this.Steponelabel2.Name = "Steponelabel2";
            this.Steponelabel2.Size = new System.Drawing.Size(159, 16);
            this.Steponelabel2.TabIndex = 10;
            this.Steponelabel2.Text = "Double click a field to";
            // 
            // StepOneResetButton
            // 
            this.StepOneResetButton.Location = new System.Drawing.Point(448, 82);
            this.StepOneResetButton.Name = "StepOneResetButton";
            this.StepOneResetButton.Size = new System.Drawing.Size(75, 74);
            this.StepOneResetButton.TabIndex = 9;
            this.StepOneResetButton.Text = "Clear Fields";
            this.StepOneResetButton.UseVisualStyleBackColor = true;
            this.StepOneResetButton.Click += new System.EventHandler(this.StepOneResetButton_Click);
            // 
            // StepTwoGroupBox
            // 
            this.StepTwoGroupBox.Controls.Add(this.resetFilters2);
            this.StepTwoGroupBox.Controls.Add(this.ResetFilters);
            this.StepTwoGroupBox.Controls.Add(this.StepTwoNextButton);
            this.StepTwoGroupBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepTwoGroupBox.Location = new System.Drawing.Point(783, 48);
            this.StepTwoGroupBox.Name = "StepTwoGroupBox";
            this.StepTwoGroupBox.Size = new System.Drawing.Size(695, 224);
            this.StepTwoGroupBox.TabIndex = 11;
            this.StepTwoGroupBox.TabStop = false;
            this.StepTwoGroupBox.Text = "Step 2 : Add Filters";
            // 
            // resetFilters2
            // 
            this.resetFilters2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetFilters2.Location = new System.Drawing.Point(463, 145);
            this.resetFilters2.Name = "resetFilters2";
            this.resetFilters2.Size = new System.Drawing.Size(99, 55);
            this.resetFilters2.TabIndex = 12;
            this.resetFilters2.Text = "Clear Filters";
            this.resetFilters2.UseVisualStyleBackColor = true;
            this.resetFilters2.Click += new System.EventHandler(this.resetFilters2_Click);
            // 
            // ResetFilters
            // 
            this.ResetFilters.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetFilters.Location = new System.Drawing.Point(463, 28);
            this.ResetFilters.Name = "ResetFilters";
            this.ResetFilters.Size = new System.Drawing.Size(99, 53);
            this.ResetFilters.TabIndex = 11;
            this.ResetFilters.Text = "Clear Values";
            this.ResetFilters.UseVisualStyleBackColor = true;
            this.ResetFilters.Click += new System.EventHandler(this.ResetFilters_Click);
            // 
            // StepTwoNextButton
            // 
            this.StepTwoNextButton.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepTwoNextButton.Location = new System.Drawing.Point(579, 28);
            this.StepTwoNextButton.Name = "StepTwoNextButton";
            this.StepTwoNextButton.Size = new System.Drawing.Size(99, 172);
            this.StepTwoNextButton.TabIndex = 10;
            this.StepTwoNextButton.Text = "Search";
            this.StepTwoNextButton.UseVisualStyleBackColor = true;
            this.StepTwoNextButton.Click += new System.EventHandler(this.StepTwoNextButton_Click);
            // 
            // StepThreeGroupBox
            // 
            this.StepThreeGroupBox.Controls.Add(this.countfoundlabel1);
            this.StepThreeGroupBox.Controls.Add(this.Exportbutton);
            this.StepThreeGroupBox.Controls.Add(this.ResultsdataGridView1);
            this.StepThreeGroupBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepThreeGroupBox.Location = new System.Drawing.Point(265, 278);
            this.StepThreeGroupBox.Name = "StepThreeGroupBox";
            this.StepThreeGroupBox.Size = new System.Drawing.Size(929, 482);
            this.StepThreeGroupBox.TabIndex = 12;
            this.StepThreeGroupBox.TabStop = false;
            this.StepThreeGroupBox.Text = "Step 3 : Results";
            // 
            // countfoundlabel1
            // 
            this.countfoundlabel1.AutoSize = true;
            this.countfoundlabel1.Location = new System.Drawing.Point(400, 462);
            this.countfoundlabel1.Name = "countfoundlabel1";
            this.countfoundlabel1.Size = new System.Drawing.Size(0, 16);
            this.countfoundlabel1.TabIndex = 5;
            // 
            // Exportbutton
            // 
            this.Exportbutton.Location = new System.Drawing.Point(764, 453);
            this.Exportbutton.Name = "Exportbutton";
            this.Exportbutton.Size = new System.Drawing.Size(154, 23);
            this.Exportbutton.TabIndex = 1;
            this.Exportbutton.Text = "Export to Excel";
            this.Exportbutton.UseVisualStyleBackColor = true;
            this.Exportbutton.Click += new System.EventHandler(this.Exportbutton_Click);
            // 
            // ResultsdataGridView1
            // 
            this.ResultsdataGridView1.AllowUserToResizeRows = false;
            this.ResultsdataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsdataGridView1.Location = new System.Drawing.Point(18, 31);
            this.ResultsdataGridView1.Name = "ResultsdataGridView1";
            this.ResultsdataGridView1.RowHeadersVisible = false;
            this.ResultsdataGridView1.Size = new System.Drawing.Size(900, 419);
            this.ResultsdataGridView1.TabIndex = 0;
            this.ResultsdataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsdataGridView1_CellDoubleClick);
            this.ResultsdataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ResultsdataGridView1_ColumnHeaderMouseDoubleClick);
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(807, 103);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(113, 60);
            this.Email.TabIndex = 30;
            this.Email.Text = "Merge and Email";
            this.Email.UseVisualStyleBackColor = true;
            this.Email.Click += new System.EventHandler(this.Email_Click);
            // 
            // removeallbutton5
            // 
            this.removeallbutton5.Location = new System.Drawing.Point(18, 122);
            this.removeallbutton5.Name = "removeallbutton5";
            this.removeallbutton5.Size = new System.Drawing.Size(75, 44);
            this.removeallbutton5.TabIndex = 29;
            this.removeallbutton5.Text = "Remove All";
            this.removeallbutton5.UseVisualStyleBackColor = true;
            this.removeallbutton5.Click += new System.EventHandler(this.removeallbutton5_Click);
            // 
            // ViewURL
            // 
            this.ViewURL.Location = new System.Drawing.Point(349, 172);
            this.ViewURL.Name = "ViewURL";
            this.ViewURL.Size = new System.Drawing.Size(97, 24);
            this.ViewURL.TabIndex = 28;
            this.ViewURL.Text = "View";
            this.ViewURL.UseVisualStyleBackColor = true;
            this.ViewURL.Click += new System.EventHandler(this.ViewURL_Click);
            // 
            // ListboxMoveDown
            // 
            this.ListboxMoveDown.Location = new System.Drawing.Point(657, 116);
            this.ListboxMoveDown.Name = "ListboxMoveDown";
            this.ListboxMoveDown.Size = new System.Drawing.Size(97, 47);
            this.ListboxMoveDown.TabIndex = 27;
            this.ListboxMoveDown.Text = "Move Down";
            this.ListboxMoveDown.UseVisualStyleBackColor = true;
            this.ListboxMoveDown.Click += new System.EventHandler(this.ListboxMoveDown_Click);
            // 
            // LisboxMoveUp
            // 
            this.LisboxMoveUp.Location = new System.Drawing.Point(657, 34);
            this.LisboxMoveUp.Name = "LisboxMoveUp";
            this.LisboxMoveUp.Size = new System.Drawing.Size(97, 47);
            this.LisboxMoveUp.TabIndex = 26;
            this.LisboxMoveUp.Text = "Move Up";
            this.LisboxMoveUp.UseVisualStyleBackColor = true;
            this.LisboxMoveUp.Click += new System.EventHandler(this.LisboxMoveUp_Click);
            // 
            // Removebutton4
            // 
            this.Removebutton4.Location = new System.Drawing.Point(18, 34);
            this.Removebutton4.Name = "Removebutton4";
            this.Removebutton4.Size = new System.Drawing.Size(75, 44);
            this.Removebutton4.TabIndex = 25;
            this.Removebutton4.Text = "Remove";
            this.Removebutton4.UseVisualStyleBackColor = true;
            this.Removebutton4.Click += new System.EventHandler(this.Removebutton4_Click);
            // 
            // Print
            // 
            this.Print.Location = new System.Drawing.Point(807, 31);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(112, 66);
            this.Print.TabIndex = 24;
            this.Print.Text = "Merge and Open";
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(99, 34);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(552, 132);
            this.listBox1.TabIndex = 22;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // CartgroupBox
            // 
            this.CartgroupBox.Controls.Add(this.listBox1);
            this.CartgroupBox.Controls.Add(this.Email);
            this.CartgroupBox.Controls.Add(this.Removebutton4);
            this.CartgroupBox.Controls.Add(this.Print);
            this.CartgroupBox.Controls.Add(this.ViewURL);
            this.CartgroupBox.Controls.Add(this.ListboxMoveDown);
            this.CartgroupBox.Controls.Add(this.removeallbutton5);
            this.CartgroupBox.Controls.Add(this.LisboxMoveUp);
            this.CartgroupBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.CartgroupBox.Location = new System.Drawing.Point(265, 766);
            this.CartgroupBox.Name = "CartgroupBox";
            this.CartgroupBox.Size = new System.Drawing.Size(929, 202);
            this.CartgroupBox.TabIndex = 31;
            this.CartgroupBox.TabStop = false;
            this.CartgroupBox.Text = "Lab Report Cart";
            // 
            // StepSwitchbutton2
            // 
            this.StepSwitchbutton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.StepSwitchbutton2.Location = new System.Drawing.Point(184, 19);
            this.StepSwitchbutton2.Name = "StepSwitchbutton2";
            this.StepSwitchbutton2.Size = new System.Drawing.Size(146, 23);
            this.StepSwitchbutton2.TabIndex = 33;
            this.StepSwitchbutton2.Text = "Step 2 : Add Filters";
            this.StepSwitchbutton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StepSwitchbutton2.UseVisualStyleBackColor = true;
            this.StepSwitchbutton2.Click += new System.EventHandler(this.StepSwitchbutton2_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(18, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "Step 1 : Choose Fields";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // QueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1558, 980);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.StepSwitchbutton2);
            this.Controls.Add(this.CartgroupBox);
            this.Controls.Add(this.StepTwoGroupBox);
            this.Controls.Add(this.StepThreeGroupBox);
            this.Controls.Add(this.StepOneGroupBox);
            this.Controls.Add(this.HeaderLabel);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab Report Search Tool";
            this.Load += new System.EventHandler(this.QueryBuilder_Load);
            this.SizeChanged += new System.EventHandler(this.QueryBuilder_SizeChanged);
            this.StepOneGroupBox.ResumeLayout(false);
            this.StepOneGroupBox.PerformLayout();
            this.StepTwoGroupBox.ResumeLayout(false);
            this.StepThreeGroupBox.ResumeLayout(false);
            this.StepThreeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsdataGridView1)).EndInit();
            this.CartgroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox CustomerListBox;
        private System.Windows.Forms.ListBox SampleDrugsListBox;
        private System.Windows.Forms.ListBox SamplesListBox;
        private System.Windows.Forms.ListBox SelectedItemsListBox;
        private System.Windows.Forms.Label Customerlabel;
        private System.Windows.Forms.Label SampleLabel;
        private System.Windows.Forms.Label SelectedLabel;
        private System.Windows.Forms.Label SampleDrugsLabel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.GroupBox StepOneGroupBox;
        private System.Windows.Forms.Button StepOneResetButton;
        private System.Windows.Forms.GroupBox StepTwoGroupBox;
        private System.Windows.Forms.Button StepTwoNextButton;
        private System.Windows.Forms.GroupBox StepThreeGroupBox;
        private System.Windows.Forms.Label steponelabel3;
        private System.Windows.Forms.Label Steponelabel2;
        private System.Windows.Forms.DataGridView ResultsdataGridView1;
        private System.Windows.Forms.Button Email;
        private System.Windows.Forms.Button removeallbutton5;
        private System.Windows.Forms.Button ViewURL;
        private System.Windows.Forms.Button ListboxMoveDown;
        private System.Windows.Forms.Button LisboxMoveUp;
        private System.Windows.Forms.Button Removebutton4;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox CartgroupBox;
        private System.Windows.Forms.Button Exportbutton;
        private System.Windows.Forms.Button ResetFilters;
        private System.Windows.Forms.Button resetFilters2;
        private System.Windows.Forms.Label countfoundlabel1;
        private System.Windows.Forms.Button StepSwitchbutton2;
        private System.Windows.Forms.Button button2;
    }
}

