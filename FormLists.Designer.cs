
namespace MyLists
{
    partial class VehicleRegistrationManager
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
            this.components = new System.ComponentModel.Container();
            this.listBoxDisplay = new System.Windows.Forms.ListBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonBinarySearch = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonLinearSearch = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonTag = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxDisplay
            // 
            this.listBoxDisplay.FormattingEnabled = true;
            this.listBoxDisplay.Location = new System.Drawing.Point(22, 54);
            this.listBoxDisplay.Name = "listBoxDisplay";
            this.listBoxDisplay.Size = new System.Drawing.Size(222, 446);
            this.listBoxDisplay.TabIndex = 0;
            this.toolTip.SetToolTip(this.listBoxDisplay, "Rego plate information will be displayed here");
            this.listBoxDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxDisplay_MouseClick);
            this.listBoxDisplay.DoubleClick += new System.EventHandler(this.ListBoxDisplay_DoubleClick);
            this.listBoxDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBoxDisplay_MouseDown);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(22, 14);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(412, 20);
            this.textBoxInput.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxInput, "Text box for inputting information to be used by the program");
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInput_KeyPress);
            // 
            // buttonBinarySearch
            // 
            this.buttonBinarySearch.Location = new System.Drawing.Point(359, 332);
            this.buttonBinarySearch.Name = "buttonBinarySearch";
            this.buttonBinarySearch.Size = new System.Drawing.Size(75, 75);
            this.buttonBinarySearch.TabIndex = 2;
            this.buttonBinarySearch.Text = "Binary Search";
            this.toolTip.SetToolTip(this.buttonBinarySearch, "Binary search for a specific rego plate");
            this.buttonBinarySearch.UseVisualStyleBackColor = true;
            this.buttonBinarySearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(265, 146);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 75);
            this.buttonEnter.TabIndex = 3;
            this.buttonEnter.Text = "Enter";
            this.toolTip.SetToolTip(this.buttonEnter, "Add a new rego plate");
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(265, 54);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 75);
            this.buttonOpen.TabIndex = 4;
            this.buttonOpen.Text = "Open";
            this.toolTip.SetToolTip(this.buttonOpen, "Load data from a pre-existing text file");
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(359, 54);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 75);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.toolTip.SetToolTip(this.buttonSave, "Save data to a text file");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(359, 146);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 75);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.toolTip.SetToolTip(this.buttonDelete, "Remove selected rego plate");
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(265, 425);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 75);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.toolTip.SetToolTip(this.buttonReset, "Clear all data fields");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonLinearSearch
            // 
            this.buttonLinearSearch.Location = new System.Drawing.Point(265, 332);
            this.buttonLinearSearch.Name = "buttonLinearSearch";
            this.buttonLinearSearch.Size = new System.Drawing.Size(75, 75);
            this.buttonLinearSearch.TabIndex = 8;
            this.buttonLinearSearch.Text = "Linear Search";
            this.toolTip.SetToolTip(this.buttonLinearSearch, "Linear search for a specific rego plate");
            this.buttonLinearSearch.UseVisualStyleBackColor = true;
            this.buttonLinearSearch.Click += new System.EventHandler(this.ButtonLinearSearch_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(265, 239);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 75);
            this.buttonEdit.TabIndex = 9;
            this.buttonEdit.Text = "Edit";
            this.toolTip.SetToolTip(this.buttonEdit, "Update an existing rego plate");
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonTag
            // 
            this.buttonTag.Location = new System.Drawing.Point(359, 239);
            this.buttonTag.Name = "buttonTag";
            this.buttonTag.Size = new System.Drawing.Size(75, 75);
            this.buttonTag.TabIndex = 10;
            this.buttonTag.Text = "Tag";
            this.toolTip.SetToolTip(this.buttonTag, "Tag or untag the selected plate with the prefix \'z\'");
            this.buttonTag.UseVisualStyleBackColor = true;
            this.buttonTag.Click += new System.EventHandler(this.ButtonTag_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(457, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(0, 17);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MyLists.Properties.Resources.Logo1;
            this.pictureBox1.Location = new System.Drawing.Point(359, 425);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // VehicleRegistrationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 551);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonTag);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonLinearSearch);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonBinarySearch);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.listBoxDisplay);
            this.Name = "VehicleRegistrationManager";
            this.Text = "Vehicle Registration Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VehicleRegistrationManager_FormClosed);
            this.Load += new System.EventHandler(this.FormLists_Load);
            this.Click += new System.EventHandler(this.VehicleRegistrationManager_Click);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDisplay;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonBinarySearch;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonLinearSearch;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonTag;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel statusStrip;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

