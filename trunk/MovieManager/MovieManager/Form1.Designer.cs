namespace MovieManager
{
    partial class Form1
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
            this.btnDir = new System.Windows.Forms.Button();
            this.lblDir = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgPhrases = new System.Windows.Forms.DataGridView();
            this.findDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useRegeExDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.replaceMentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fullPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.savedNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.movieBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhrases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replaceMentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(161, 31);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(75, 23);
            this.btnDir.TabIndex = 1;
            this.btnDir.Text = "Directory";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(37, 36);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(98, 13);
            this.lblDir.TabIndex = 2;
            this.lblDir.Text = "Choose A Directory";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullPathDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.savedNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.movieBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(894, 349);
            this.dataGridView1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Remove Phrases";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(0, 184);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgPhrases
            // 
            this.dgPhrases.AllowUserToOrderColumns = true;
            this.dgPhrases.AllowUserToResizeRows = false;
            this.dgPhrases.AutoGenerateColumns = false;
            this.dgPhrases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPhrases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPhrases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.findDataGridViewTextBoxColumn,
            this.replaceDataGridViewTextBoxColumn,
            this.useRegeExDataGridViewCheckBoxColumn});
            this.dgPhrases.DataSource = this.replaceMentBindingSource;
            this.dgPhrases.Location = new System.Drawing.Point(264, 57);
            this.dgPhrases.Name = "dgPhrases";
            this.dgPhrases.Size = new System.Drawing.Size(343, 150);
            this.dgPhrases.TabIndex = 10;
            this.dgPhrases.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPhrases_CellValueChanged);
            this.dgPhrases.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgPhrases_KeyUp);
            // 
            // findDataGridViewTextBoxColumn
            // 
            this.findDataGridViewTextBoxColumn.DataPropertyName = "Find";
            this.findDataGridViewTextBoxColumn.HeaderText = "Find";
            this.findDataGridViewTextBoxColumn.Name = "findDataGridViewTextBoxColumn";
            // 
            // replaceDataGridViewTextBoxColumn
            // 
            this.replaceDataGridViewTextBoxColumn.DataPropertyName = "Replace";
            this.replaceDataGridViewTextBoxColumn.HeaderText = "Replace";
            this.replaceDataGridViewTextBoxColumn.Name = "replaceDataGridViewTextBoxColumn";
            // 
            // useRegeExDataGridViewCheckBoxColumn
            // 
            this.useRegeExDataGridViewCheckBoxColumn.DataPropertyName = "UseRegeEx";
            this.useRegeExDataGridViewCheckBoxColumn.HeaderText = "UseRegeEx";
            this.useRegeExDataGridViewCheckBoxColumn.Name = "useRegeExDataGridViewCheckBoxColumn";
            // 
            // replaceMentBindingSource
            // 
            this.replaceMentBindingSource.DataSource = typeof(MovieManager.Movie.ReplaceMent);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(532, 31);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(116, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Build Replace Statemets";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fullPathDataGridViewTextBoxColumn
            // 
            this.fullPathDataGridViewTextBoxColumn.DataPropertyName = "FullPath";
            this.fullPathDataGridViewTextBoxColumn.HeaderText = "FullPath";
            this.fullPathDataGridViewTextBoxColumn.Name = "fullPathDataGridViewTextBoxColumn";
            this.fullPathDataGridViewTextBoxColumn.Width = 300;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "FileName";
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.Width = 300;
            // 
            // savedNameDataGridViewTextBoxColumn
            // 
            this.savedNameDataGridViewTextBoxColumn.DataPropertyName = "SavedName";
            this.savedNameDataGridViewTextBoxColumn.HeaderText = "SavedName";
            this.savedNameDataGridViewTextBoxColumn.Name = "savedNameDataGridViewTextBoxColumn";
            this.savedNameDataGridViewTextBoxColumn.Width = 300;
            // 
            // movieBindingSource
            // 
            this.movieBindingSource.DataSource = typeof(MovieManager.Movie);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Get Suggestions";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 562);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgPhrases);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.btnDir);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhrases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replaceMentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgPhrases;
        private System.Windows.Forms.DataGridViewTextBoxColumn findDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn replaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn useRegeExDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource replaceMentBindingSource;
        private System.Windows.Forms.BindingSource movieBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn savedNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

