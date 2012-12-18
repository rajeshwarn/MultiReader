namespace MultiReader
{
    partial class Reader
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.translatorLabel = new System.Windows.Forms.Label();
            this.translatorTextBox = new System.Windows.Forms.TextBox();
            this.publisherTextBox = new System.Windows.Forms.TextBox();
            this.publisherLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.datePubTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.fileNameLabel2 = new System.Windows.Forms.Label();
            this.fileFormatLabel = new System.Windows.Forms.Label();
            this.fileFormatLabel2 = new System.Windows.Forms.Label();
            this.fileSizeLabel = new System.Windows.Forms.Label();
            this.fileSizeLabel2 = new System.Windows.Forms.Label();
            this.creationTimeLabel = new System.Windows.Forms.Label();
            this.lastAccessTimeLabel = new System.Windows.Forms.Label();
            this.lastWriteTimeLabel = new System.Windows.Forms.Label();
            this.lastTimeWriteLabel2 = new System.Windows.Forms.Label();
            this.lastAccessTimeLabel2 = new System.Windows.Forms.Label();
            this.creationTimeLabel2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Metadata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(902, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // openFD
            // 
            this.openFD.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 514);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(511, 574);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(533, 28);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(41, 13);
            this.authorLabel.TabIndex = 4;
            this.authorLabel.Text = "Author:";
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(536, 45);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(100, 20);
            this.authorTextBox.TabIndex = 5;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(536, 73);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "Title:";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(536, 89);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(100, 20);
            this.titleTextBox.TabIndex = 7;
            // 
            // translatorLabel
            // 
            this.translatorLabel.AutoSize = true;
            this.translatorLabel.Location = new System.Drawing.Point(536, 116);
            this.translatorLabel.Name = "translatorLabel";
            this.translatorLabel.Size = new System.Drawing.Size(57, 13);
            this.translatorLabel.TabIndex = 8;
            this.translatorLabel.Text = "Translator:";
            // 
            // translatorTextBox
            // 
            this.translatorTextBox.Location = new System.Drawing.Point(536, 132);
            this.translatorTextBox.Name = "translatorTextBox";
            this.translatorTextBox.Size = new System.Drawing.Size(100, 20);
            this.translatorTextBox.TabIndex = 9;
            // 
            // publisherTextBox
            // 
            this.publisherTextBox.Location = new System.Drawing.Point(536, 171);
            this.publisherTextBox.Name = "publisherTextBox";
            this.publisherTextBox.Size = new System.Drawing.Size(100, 20);
            this.publisherTextBox.TabIndex = 10;
            // 
            // publisherLabel
            // 
            this.publisherLabel.AutoSize = true;
            this.publisherLabel.Location = new System.Drawing.Point(536, 155);
            this.publisherLabel.Name = "publisherLabel";
            this.publisherLabel.Size = new System.Drawing.Size(50, 13);
            this.publisherLabel.TabIndex = 11;
            this.publisherLabel.Text = "Publisher";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(536, 198);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(69, 13);
            this.dateLabel.TabIndex = 12;
            this.dateLabel.Text = "Date of pub.:";
            // 
            // datePubTextBox
            // 
            this.datePubTextBox.Location = new System.Drawing.Point(536, 214);
            this.datePubTextBox.Name = "datePubTextBox";
            this.datePubTextBox.Size = new System.Drawing.Size(100, 20);
            this.datePubTextBox.TabIndex = 13;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(536, 237);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(49, 13);
            this.idLabel.TabIndex = 14;
            this.idLabel.Text = "Book ID:";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(536, 254);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 15;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(678, 28);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(55, 13);
            this.fileNameLabel.TabIndex = 16;
            this.fileNameLabel.Text = "File name:";
            // 
            // fileNameLabel2
            // 
            this.fileNameLabel2.AutoSize = true;
            this.fileNameLabel2.Location = new System.Drawing.Point(678, 45);
            this.fileNameLabel2.Name = "fileNameLabel2";
            this.fileNameLabel2.Size = new System.Drawing.Size(0, 13);
            this.fileNameLabel2.TabIndex = 17;
            this.fileNameLabel2.Click += new System.EventHandler(this.fileNameLabel2_Click);
            // 
            // fileFormatLabel
            // 
            this.fileFormatLabel.AutoSize = true;
            this.fileFormatLabel.Location = new System.Drawing.Point(678, 73);
            this.fileFormatLabel.Name = "fileFormatLabel";
            this.fileFormatLabel.Size = new System.Drawing.Size(58, 13);
            this.fileFormatLabel.TabIndex = 18;
            this.fileFormatLabel.Text = "File format:";
            // 
            // fileFormatLabel2
            // 
            this.fileFormatLabel2.AutoSize = true;
            this.fileFormatLabel2.Location = new System.Drawing.Point(678, 96);
            this.fileFormatLabel2.Name = "fileFormatLabel2";
            this.fileFormatLabel2.Size = new System.Drawing.Size(0, 13);
            this.fileFormatLabel2.TabIndex = 19;
            this.fileFormatLabel2.Click += new System.EventHandler(this.fileFormatLabel2_Click);
            // 
            // fileSizeLabel
            // 
            this.fileSizeLabel.AutoSize = true;
            this.fileSizeLabel.Location = new System.Drawing.Point(678, 116);
            this.fileSizeLabel.Name = "fileSizeLabel";
            this.fileSizeLabel.Size = new System.Drawing.Size(63, 13);
            this.fileSizeLabel.TabIndex = 20;
            this.fileSizeLabel.Text = "File size [B]:";
            // 
            // fileSizeLabel2
            // 
            this.fileSizeLabel2.AutoSize = true;
            this.fileSizeLabel2.Location = new System.Drawing.Point(678, 139);
            this.fileSizeLabel2.Name = "fileSizeLabel2";
            this.fileSizeLabel2.Size = new System.Drawing.Size(0, 13);
            this.fileSizeLabel2.TabIndex = 21;
            this.fileSizeLabel2.Click += new System.EventHandler(this.fileSizeLabel2_Click);
            // 
            // creationTimeLabel
            // 
            this.creationTimeLabel.AutoSize = true;
            this.creationTimeLabel.Location = new System.Drawing.Point(678, 155);
            this.creationTimeLabel.Name = "creationTimeLabel";
            this.creationTimeLabel.Size = new System.Drawing.Size(71, 13);
            this.creationTimeLabel.TabIndex = 22;
            this.creationTimeLabel.Text = "Creation time:";
            // 
            // lastAccessTimeLabel
            // 
            this.lastAccessTimeLabel.AutoSize = true;
            this.lastAccessTimeLabel.Location = new System.Drawing.Point(678, 198);
            this.lastAccessTimeLabel.Name = "lastAccessTimeLabel";
            this.lastAccessTimeLabel.Size = new System.Drawing.Size(67, 13);
            this.lastAccessTimeLabel.TabIndex = 23;
            this.lastAccessTimeLabel.Text = "Last access:";
            // 
            // lastWriteTimeLabel
            // 
            this.lastWriteTimeLabel.AutoSize = true;
            this.lastWriteTimeLabel.Location = new System.Drawing.Point(678, 237);
            this.lastWriteTimeLabel.Name = "lastWriteTimeLabel";
            this.lastWriteTimeLabel.Size = new System.Drawing.Size(55, 13);
            this.lastWriteTimeLabel.TabIndex = 24;
            this.lastWriteTimeLabel.Text = "Last write:";
            // 
            // lastTimeWriteLabel2
            // 
            this.lastTimeWriteLabel2.AutoSize = true;
            this.lastTimeWriteLabel2.Location = new System.Drawing.Point(678, 261);
            this.lastTimeWriteLabel2.Name = "lastTimeWriteLabel2";
            this.lastTimeWriteLabel2.Size = new System.Drawing.Size(0, 13);
            this.lastTimeWriteLabel2.TabIndex = 25;
            this.lastTimeWriteLabel2.Click += new System.EventHandler(this.lastTimeWriteLabel2_Click);
            // 
            // lastAccessTimeLabel2
            // 
            this.lastAccessTimeLabel2.AutoSize = true;
            this.lastAccessTimeLabel2.Location = new System.Drawing.Point(678, 221);
            this.lastAccessTimeLabel2.Name = "lastAccessTimeLabel2";
            this.lastAccessTimeLabel2.Size = new System.Drawing.Size(0, 13);
            this.lastAccessTimeLabel2.TabIndex = 26;
            this.lastAccessTimeLabel2.Click += new System.EventHandler(this.lastAccessTimeLabel2_Click);
            // 
            // creationTimeLabel2
            // 
            this.creationTimeLabel2.AutoSize = true;
            this.creationTimeLabel2.Location = new System.Drawing.Point(678, 178);
            this.creationTimeLabel2.Name = "creationTimeLabel2";
            this.creationTimeLabel2.Size = new System.Drawing.Size(0, 13);
            this.creationTimeLabel2.TabIndex = 27;
            this.creationTimeLabel2.Click += new System.EventHandler(this.creationTimeLabel2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.AcceptsTab = true;
            this.richTextBox2.Location = new System.Drawing.Point(539, 517);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(351, 84);
            this.richTextBox2.TabIndex = 28;
            this.richTextBox2.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Metadata,
            this.Data});
            this.dataGridView1.Location = new System.Drawing.Point(539, 280);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(351, 231);
            this.dataGridView1.TabIndex = 29;
            // 
            // Metadata
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Metadata.DefaultCellStyle = dataGridViewCellStyle1;
            this.Metadata.HeaderText = "Metdadata";
            this.Metadata.Name = "Metadata";
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            // 
            // Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 613);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.creationTimeLabel2);
            this.Controls.Add(this.lastAccessTimeLabel2);
            this.Controls.Add(this.lastTimeWriteLabel2);
            this.Controls.Add(this.lastWriteTimeLabel);
            this.Controls.Add(this.lastAccessTimeLabel);
            this.Controls.Add(this.creationTimeLabel);
            this.Controls.Add(this.fileSizeLabel2);
            this.Controls.Add(this.fileSizeLabel);
            this.Controls.Add(this.fileFormatLabel2);
            this.Controls.Add(this.fileFormatLabel);
            this.Controls.Add(this.fileNameLabel2);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.datePubTextBox);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.publisherLabel);
            this.Controls.Add(this.publisherTextBox);
            this.Controls.Add(this.translatorTextBox);
            this.Controls.Add(this.translatorLabel);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Reader";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Reader_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFD;
        private System.Windows.Forms.SaveFileDialog saveFD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label translatorLabel;
        private System.Windows.Forms.TextBox translatorTextBox;
        private System.Windows.Forms.TextBox publisherTextBox;
        private System.Windows.Forms.Label publisherLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TextBox datePubTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label fileNameLabel2;
        private System.Windows.Forms.Label fileFormatLabel;
        private System.Windows.Forms.Label fileFormatLabel2;
        private System.Windows.Forms.Label fileSizeLabel;
        private System.Windows.Forms.Label fileSizeLabel2;
        private System.Windows.Forms.Label creationTimeLabel;
        private System.Windows.Forms.Label lastAccessTimeLabel;
        private System.Windows.Forms.Label lastWriteTimeLabel;
        private System.Windows.Forms.Label lastTimeWriteLabel2;
        private System.Windows.Forms.Label lastAccessTimeLabel2;
        private System.Windows.Forms.Label creationTimeLabel2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Metadata;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;

    }
}

