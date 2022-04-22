namespace GS_STB.Forms_Modules
{
    partial class SetRangeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridRange = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SNNum = new System.Windows.Forms.NumericUpDown();
            this.DateRange = new System.Windows.Forms.DateTimePicker();
            this.GRNotUsed = new System.Windows.Forms.GroupBox();
            this.GridExcelReport = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BreakRangedGrid = new System.Windows.Forms.DataGridView();
            this.LiterIndex = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LoadExcel = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNNum)).BeginInit();
            this.GRNotUsed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridExcelReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BreakRangedGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiterIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // GridRange
            // 
            this.GridRange.AllowUserToResizeColumns = false;
            this.GridRange.AllowUserToResizeRows = false;
            this.GridRange.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridRange.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridRange.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRange.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridRange.Location = new System.Drawing.Point(912, 20);
            this.GridRange.Name = "GridRange";
            this.GridRange.ReadOnly = true;
            this.GridRange.RowHeadersVisible = false;
            this.GridRange.Size = new System.Drawing.Size(264, 128);
            this.GridRange.TabIndex = 147;
            this.GridRange.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 148;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gray;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(423, 598);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(365, 48);
            this.button2.TabIndex = 150;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 598);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(405, 48);
            this.button1.TabIndex = 149;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SNNum
            // 
            this.SNNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SNNum.Location = new System.Drawing.Point(142, 19);
            this.SNNum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.SNNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SNNum.Name = "SNNum";
            this.SNNum.Size = new System.Drawing.Size(182, 44);
            this.SNNum.TabIndex = 151;
            this.SNNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DateRange
            // 
            this.DateRange.CustomFormat = "dd.MM.yyyy";
            this.DateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.DateRange.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateRange.Location = new System.Drawing.Point(142, 69);
            this.DateRange.Name = "DateRange";
            this.DateRange.Size = new System.Drawing.Size(318, 44);
            this.DateRange.TabIndex = 152;
            this.DateRange.Value = new System.DateTime(2021, 1, 11, 13, 45, 47, 0);
            // 
            // GRNotUsed
            // 
            this.GRNotUsed.Controls.Add(this.LoadExcel);
            this.GRNotUsed.Controls.Add(this.label4);
            this.GRNotUsed.Controls.Add(this.label3);
            this.GRNotUsed.Controls.Add(this.label2);
            this.GRNotUsed.Controls.Add(this.LiterIndex);
            this.GRNotUsed.Controls.Add(this.SNNum);
            this.GRNotUsed.Controls.Add(this.DateRange);
            this.GRNotUsed.Location = new System.Drawing.Point(615, 244);
            this.GRNotUsed.Name = "GRNotUsed";
            this.GRNotUsed.Size = new System.Drawing.Size(470, 212);
            this.GRNotUsed.TabIndex = 153;
            this.GRNotUsed.TabStop = false;
            this.GRNotUsed.Visible = false;
            // 
            // GridExcelReport
            // 
            this.GridExcelReport.AllowUserToResizeColumns = false;
            this.GridExcelReport.AllowUserToResizeRows = false;
            this.GridExcelReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridExcelReport.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridExcelReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridExcelReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridExcelReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridExcelReport.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridExcelReport.Location = new System.Drawing.Point(287, 462);
            this.GridExcelReport.Name = "GridExcelReport";
            this.GridExcelReport.ReadOnly = true;
            this.GridExcelReport.RowHeadersVisible = false;
            this.GridExcelReport.Size = new System.Drawing.Size(448, 130);
            this.GridExcelReport.TabIndex = 154;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "SerialNumber";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "RangeDate";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "FullSTBSN";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // BreakRangedGrid
            // 
            this.BreakRangedGrid.AllowUserToAddRows = false;
            this.BreakRangedGrid.AllowUserToResizeColumns = false;
            this.BreakRangedGrid.AllowUserToResizeRows = false;
            this.BreakRangedGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BreakRangedGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BreakRangedGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.BreakRangedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BreakRangedGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.BreakRangedGrid.Location = new System.Drawing.Point(17, 464);
            this.BreakRangedGrid.Name = "BreakRangedGrid";
            this.BreakRangedGrid.ReadOnly = true;
            this.BreakRangedGrid.RowHeadersVisible = false;
            this.BreakRangedGrid.Size = new System.Drawing.Size(264, 128);
            this.BreakRangedGrid.TabIndex = 155;
            // 
            // LiterIndex
            // 
            this.LiterIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LiterIndex.Location = new System.Drawing.Point(142, 119);
            this.LiterIndex.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.LiterIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LiterIndex.Name = "LiterIndex";
            this.LiterIndex.Size = new System.Drawing.Size(182, 44);
            this.LiterIndex.TabIndex = 153;
            this.LiterIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 39);
            this.label2.TabIndex = 154;
            this.label2.Text = "Индекс";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 39);
            this.label3.TabIndex = 155;
            this.label3.Text = "Дата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 39);
            this.label4.TabIndex = 156;
            this.label4.Text = "Кол-во";
            // 
            // LoadExcel
            // 
            this.LoadExcel.AutoSize = true;
            this.LoadExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadExcel.Location = new System.Drawing.Point(13, 169);
            this.LoadExcel.Name = "LoadExcel";
            this.LoadExcel.Size = new System.Drawing.Size(204, 33);
            this.LoadExcel.TabIndex = 157;
            this.LoadExcel.Text = "Выгрузка Excel";
            this.LoadExcel.UseVisualStyleBackColor = true;
            // 
            // SetRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1188, 658);
            this.Controls.Add(this.BreakRangedGrid);
            this.Controls.Add(this.GridExcelReport);
            this.Controls.Add(this.GRNotUsed);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GridRange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SetRangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetRangeForm";
            this.Load += new System.EventHandler(this.SetRangeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNNum)).EndInit();
            this.GRNotUsed.ResumeLayout(false);
            this.GRNotUsed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridExcelReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BreakRangedGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiterIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown SNNum;
        private System.Windows.Forms.DateTimePicker DateRange;
        private System.Windows.Forms.GroupBox GRNotUsed;
        private System.Windows.Forms.DataGridView GridExcelReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridView BreakRangedGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown LiterIndex;
        private System.Windows.Forms.CheckBox LoadExcel;
    }
}