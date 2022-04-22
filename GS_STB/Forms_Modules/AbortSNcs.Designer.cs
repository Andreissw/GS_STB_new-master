namespace GS_STB.Forms_Modules
{
    partial class AbortSNcs
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
            this.DG_LOTList = new System.Windows.Forms.DataGridView();
            this.BT_OpenWorkForm = new System.Windows.Forms.Button();
            this.SNGrid = new System.Windows.Forms.DataGridView();
            this.INFO = new System.Windows.Forms.Label();
            this.AbortBT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DG_LOTList
            // 
            this.DG_LOTList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DG_LOTList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DG_LOTList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_LOTList.Cursor = System.Windows.Forms.Cursors.Default;
            this.DG_LOTList.Location = new System.Drawing.Point(12, 12);
            this.DG_LOTList.Name = "DG_LOTList";
            this.DG_LOTList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DG_LOTList.Size = new System.Drawing.Size(790, 408);
            this.DG_LOTList.TabIndex = 5;
            // 
            // BT_OpenWorkForm
            // 
            this.BT_OpenWorkForm.FlatAppearance.BorderSize = 0;
            this.BT_OpenWorkForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_OpenWorkForm.Location = new System.Drawing.Point(12, 426);
            this.BT_OpenWorkForm.Name = "BT_OpenWorkForm";
            this.BT_OpenWorkForm.Size = new System.Drawing.Size(790, 77);
            this.BT_OpenWorkForm.TabIndex = 25;
            this.BT_OpenWorkForm.Text = "Выбрать";
            this.BT_OpenWorkForm.UseVisualStyleBackColor = true;
            this.BT_OpenWorkForm.Click += new System.EventHandler(this.BT_OpenWorkForm_Click);
            // 
            // SNGrid
            // 
            this.SNGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SNGrid.Location = new System.Drawing.Point(808, 12);
            this.SNGrid.Name = "SNGrid";
            this.SNGrid.Size = new System.Drawing.Size(699, 491);
            this.SNGrid.TabIndex = 26;
            this.SNGrid.Visible = false;
            // 
            // INFO
            // 
            this.INFO.AutoSize = true;
            this.INFO.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.INFO.Location = new System.Drawing.Point(804, 506);
            this.INFO.Name = "INFO";
            this.INFO.Size = new System.Drawing.Size(59, 20);
            this.INFO.TabIndex = 27;
            this.INFO.Text = "label1";
            this.INFO.Visible = false;
            // 
            // AbortBT
            // 
            this.AbortBT.FlatAppearance.BorderSize = 0;
            this.AbortBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AbortBT.Location = new System.Drawing.Point(1349, 510);
            this.AbortBT.Name = "AbortBT";
            this.AbortBT.Size = new System.Drawing.Size(158, 77);
            this.AbortBT.TabIndex = 28;
            this.AbortBT.Text = "Открепить";
            this.AbortBT.UseVisualStyleBackColor = true;
            this.AbortBT.Visible = false;
            this.AbortBT.Click += new System.EventHandler(this.AbortBT_Click);
            // 
            // AbortSNcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1529, 599);
            this.Controls.Add(this.AbortBT);
            this.Controls.Add(this.INFO);
            this.Controls.Add(this.SNGrid);
            this.Controls.Add(this.BT_OpenWorkForm);
            this.Controls.Add(this.DG_LOTList);
            this.Name = "AbortSNcs";
            this.Text = "AbortSNcs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AbortSNcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DG_LOTList;
        internal System.Windows.Forms.Button BT_OpenWorkForm;
        private System.Windows.Forms.DataGridView SNGrid;
        private System.Windows.Forms.Label INFO;
        internal System.Windows.Forms.Button AbortBT;
    }
}