namespace GS_STB.Forms_Modules
{
    partial class FixedRange
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LBText = new System.Windows.Forms.Label();
            this.GridRange = new System.Windows.Forms.DataGridView();
            this.BTOK = new System.Windows.Forms.Button();
            this.BTCancel = new System.Windows.Forms.Button();
            this.BTNoTRange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).BeginInit();
            this.SuspendLayout();
            // 
            // LBText
            // 
            this.LBText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBText.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LBText.Location = new System.Drawing.Point(12, 9);
            this.LBText.Name = "LBText";
            this.LBText.Size = new System.Drawing.Size(1079, 105);
            this.LBText.TabIndex = 0;
            this.LBText.Text = "В лоте присутствует разбивка серийных номеров на диапазоны, выберите нужный вам д" +
    "иапозон";
            this.LBText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GridRange
            // 
            this.GridRange.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridRange.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.GridRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRange.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridRange.GridColor = System.Drawing.SystemColors.Control;
            this.GridRange.Location = new System.Drawing.Point(12, 117);
            this.GridRange.Name = "GridRange";
            this.GridRange.RowHeadersVisible = false;
            this.GridRange.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridRange.Size = new System.Drawing.Size(1079, 321);
            this.GridRange.TabIndex = 1;
            // 
            // BTOK
            // 
            this.BTOK.BackColor = System.Drawing.Color.LimeGreen;
            this.BTOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTOK.Location = new System.Drawing.Point(12, 444);
            this.BTOK.Name = "BTOK";
            this.BTOK.Size = new System.Drawing.Size(411, 48);
            this.BTOK.TabIndex = 2;
            this.BTOK.Text = "ОК";
            this.BTOK.UseVisualStyleBackColor = false;
            this.BTOK.Click += new System.EventHandler(this.BTOK_Click);
            // 
            // BTCancel
            // 
            this.BTCancel.BackColor = System.Drawing.Color.Gray;
            this.BTCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTCancel.Location = new System.Drawing.Point(429, 444);
            this.BTCancel.Name = "BTCancel";
            this.BTCancel.Size = new System.Drawing.Size(360, 48);
            this.BTCancel.TabIndex = 3;
            this.BTCancel.Text = "Отмена";
            this.BTCancel.UseVisualStyleBackColor = false;
            this.BTCancel.Click += new System.EventHandler(this.BTCancel_Click);
            // 
            // BTNoTRange
            // 
            this.BTNoTRange.BackColor = System.Drawing.Color.DarkOrange;
            this.BTNoTRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTNoTRange.Location = new System.Drawing.Point(795, 444);
            this.BTNoTRange.Name = "BTNoTRange";
            this.BTNoTRange.Size = new System.Drawing.Size(296, 48);
            this.BTNoTRange.TabIndex = 4;
            this.BTNoTRange.Text = "Без диапазона";
            this.BTNoTRange.UseVisualStyleBackColor = false;
            this.BTNoTRange.Click += new System.EventHandler(this.BTNoRange_Click);
            // 
            // FixedRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 495);
            this.ControlBox = false;
            this.Controls.Add(this.BTNoTRange);
            this.Controls.Add(this.BTCancel);
            this.Controls.Add(this.BTOK);
            this.Controls.Add(this.GridRange);
            this.Controls.Add(this.LBText);
            this.Name = "FixedRange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FixedRange";
            this.Load += new System.EventHandler(this.FixedRange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBText;
        private System.Windows.Forms.DataGridView GridRange;
        private System.Windows.Forms.Button BTOK;
        private System.Windows.Forms.Button BTCancel;
        private System.Windows.Forms.Button BTNoTRange;
    }
}