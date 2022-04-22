
namespace GS_STB.Forms_Modules.WeightForms
{
    partial class DifferenceSetting
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
            this.Plus = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Minus = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.LBWeight = new System.Windows.Forms.Label();
            this.BTBack = new System.Windows.Forms.Button();
            this.BTContiune = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Plus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minus)).BeginInit();
            this.SuspendLayout();
            // 
            // Plus
            // 
            this.Plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Plus.Location = new System.Drawing.Point(12, 181);
            this.Plus.Name = "Plus";
            this.Plus.Size = new System.Drawing.Size(197, 116);
            this.Plus.TabIndex = 0;
            this.Plus.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(65, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 108);
            this.label1.TabIndex = 1;
            this.label1.Text = "+";
            // 
            // Minus
            // 
            this.Minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Minus.Location = new System.Drawing.Point(215, 181);
            this.Minus.Name = "Minus";
            this.Minus.Size = new System.Drawing.Size(192, 116);
            this.Minus.TabIndex = 2;
            this.Minus.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(280, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 108);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // LBWeight
            // 
            this.LBWeight.AutoSize = true;
            this.LBWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LBWeight.Location = new System.Drawing.Point(5, 9);
            this.LBWeight.Name = "LBWeight";
            this.LBWeight.Size = new System.Drawing.Size(255, 29);
            this.LBWeight.TabIndex = 4;
            this.LBWeight.Text = "Вес объекта равен = ";
            // 
            // BTBack
            // 
            this.BTBack.BackColor = System.Drawing.Color.Coral;
            this.BTBack.FlatAppearance.BorderSize = 0;
            this.BTBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTBack.Location = new System.Drawing.Point(248, 322);
            this.BTBack.Name = "BTBack";
            this.BTBack.Size = new System.Drawing.Size(159, 46);
            this.BTBack.TabIndex = 6;
            this.BTBack.Text = "Назад";
            this.BTBack.UseVisualStyleBackColor = false;
            this.BTBack.Click += new System.EventHandler(this.BTBack_Click);
            // 
            // BTContiune
            // 
            this.BTContiune.BackColor = System.Drawing.Color.GreenYellow;
            this.BTContiune.FlatAppearance.BorderSize = 0;
            this.BTContiune.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTContiune.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTContiune.Location = new System.Drawing.Point(12, 322);
            this.BTContiune.Name = "BTContiune";
            this.BTContiune.Size = new System.Drawing.Size(230, 46);
            this.BTContiune.TabIndex = 5;
            this.BTContiune.Text = "Продолжить";
            this.BTContiune.UseVisualStyleBackColor = false;
            this.BTContiune.Click += new System.EventHandler(this.BTContiune_Click);
            // 
            // DifferenceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(419, 380);
            this.Controls.Add(this.BTBack);
            this.Controls.Add(this.BTContiune);
            this.Controls.Add(this.LBWeight);
            this.Controls.Add(this.Minus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Plus);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DifferenceSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DifferenceSetting";
            ((System.ComponentModel.ISupportInitialize)(this.Plus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown Plus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Minus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LBWeight;
        private System.Windows.Forms.Button BTBack;
        private System.Windows.Forms.Button BTContiune;
    }
}