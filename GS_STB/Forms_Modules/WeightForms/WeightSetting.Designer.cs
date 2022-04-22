
namespace GS_STB.Forms_Modules
{
    partial class WeightSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.BTContiune = new System.Windows.Forms.Button();
            this.BTBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BT = new System.Windows.Forms.Button();
            this.TBWeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TBPlus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TBMinus = new System.Windows.Forms.TextBox();
            this.BT_packing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 87);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поставьте коробку с содержимым\r\nна весы и нажмите кнопку \"Enter\"\r\nна клавиатуре и" +
    "ли кнопку взвесить";
            // 
            // BTContiune
            // 
            this.BTContiune.BackColor = System.Drawing.Color.GreenYellow;
            this.BTContiune.FlatAppearance.BorderSize = 0;
            this.BTContiune.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTContiune.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTContiune.Location = new System.Drawing.Point(12, 157);
            this.BTContiune.Name = "BTContiune";
            this.BTContiune.Size = new System.Drawing.Size(264, 46);
            this.BTContiune.TabIndex = 999;
            this.BTContiune.TabStop = false;
            this.BTContiune.Text = "Весы и упаковка";
            this.BTContiune.UseVisualStyleBackColor = false;
            this.BTContiune.Click += new System.EventHandler(this.BTContiune_Click);
            // 
            // BTBack
            // 
            this.BTBack.BackColor = System.Drawing.Color.Coral;
            this.BTBack.FlatAppearance.BorderSize = 0;
            this.BTBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTBack.Location = new System.Drawing.Point(574, 157);
            this.BTBack.Name = "BTBack";
            this.BTBack.Size = new System.Drawing.Size(272, 46);
            this.BTBack.TabIndex = 999;
            this.BTBack.TabStop = false;
            this.BTBack.Text = "Назад";
            this.BTBack.UseVisualStyleBackColor = false;
            this.BTBack.Click += new System.EventHandler(this.BTBack_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(482, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Вес";
            // 
            // BT
            // 
            this.BT.BackColor = System.Drawing.Color.Gold;
            this.BT.FlatAppearance.BorderSize = 0;
            this.BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT.Location = new System.Drawing.Point(475, 93);
            this.BT.Name = "BT";
            this.BT.Size = new System.Drawing.Size(371, 35);
            this.BT.TabIndex = 0;
            this.BT.Text = "Взвесить";
            this.BT.UseVisualStyleBackColor = false;
            this.BT.Click += new System.EventHandler(this.BT_Click);
            // 
            // TBWeight
            // 
            this.TBWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TBWeight.Location = new System.Drawing.Point(657, 11);
            this.TBWeight.Name = "TBWeight";
            this.TBWeight.ReadOnly = true;
            this.TBWeight.Size = new System.Drawing.Size(189, 35);
            this.TBWeight.TabIndex = 3;
            this.TBWeight.Tag = "42";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(456, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 87);
            this.label3.TabIndex = 6;
            this.label3.Text = "|\r\n|\r\n|\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(470, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 29);
            this.label4.TabIndex = 8;
            this.label4.Text = " +/- Разность";
            // 
            // TBPlus
            // 
            this.TBPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TBPlus.Location = new System.Drawing.Point(680, 52);
            this.TBPlus.Name = "TBPlus";
            this.TBPlus.ReadOnly = true;
            this.TBPlus.Size = new System.Drawing.Size(67, 35);
            this.TBPlus.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(649, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "+";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(751, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 29);
            this.label6.TabIndex = 11;
            this.label6.Text = "-";
            // 
            // TBMinus
            // 
            this.TBMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TBMinus.Location = new System.Drawing.Point(779, 52);
            this.TBMinus.Name = "TBMinus";
            this.TBMinus.ReadOnly = true;
            this.TBMinus.Size = new System.Drawing.Size(67, 35);
            this.TBMinus.TabIndex = 10;
            // 
            // BT_packing
            // 
            this.BT_packing.BackColor = System.Drawing.Color.Yellow;
            this.BT_packing.FlatAppearance.BorderSize = 0;
            this.BT_packing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_packing.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_packing.Location = new System.Drawing.Point(282, 157);
            this.BT_packing.Name = "BT_packing";
            this.BT_packing.Size = new System.Drawing.Size(286, 46);
            this.BT_packing.TabIndex = 1000;
            this.BT_packing.TabStop = false;
            this.BT_packing.Text = "Упаковка";
            this.BT_packing.UseVisualStyleBackColor = false;
            this.BT_packing.Click += new System.EventHandler(this.BT_packing_Click);
            // 
            // WeightSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(855, 217);
            this.Controls.Add(this.BT_packing);
            this.Controls.Add(this.TBMinus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TBPlus);
            this.Controls.Add(this.BT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TBWeight);
            this.Controls.Add(this.BTBack);
            this.Controls.Add(this.BTContiune);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "WeightSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка весового параметра";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WeightSetting_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTContiune;
        private System.Windows.Forms.Button BTBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BT;
        private System.Windows.Forms.TextBox TBWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TBPlus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TBMinus;
        private System.Windows.Forms.Button BT_packing;
    }
}