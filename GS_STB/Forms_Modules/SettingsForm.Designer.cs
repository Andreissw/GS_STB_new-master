namespace GS_STB.Forms_Modules
{
    partial class SettingsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GBInfo = new System.Windows.Forms.GroupBox();
            this.BT_OpenSettings = new System.Windows.Forms.Button();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineSettings = new System.Windows.Forms.GroupBox();
            this.BTBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_SaveLine = new System.Windows.Forms.Button();
            this.CB_Line = new System.Windows.Forms.ComboBox();
            this.Fas_Start = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CheckBoxSN = new System.Windows.Forms.CheckBox();
            this.TB_LabelSNCount = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.RB_Local_DateTime = new System.Windows.Forms.RadioButton();
            this.RB_Server_Time = new System.Windows.Forms.RadioButton();
            this.UploadStationGB = new System.Windows.Forms.GroupBox();
            this.CHID = new System.Windows.Forms.CheckBox();
            this.CHSN = new System.Windows.Forms.CheckBox();
            this.CheckBoxDublicateSCID = new System.Windows.Forms.CheckBox();
            this.TB_LabelIDCount = new System.Windows.Forms.TextBox();
            this.TBCHSN = new System.Windows.Forms.TextBox();
            this.FAS_Weight = new System.Windows.Forms.GroupBox();
            this.TB_Deviation = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.TB_AutoSetSNin = new System.Windows.Forms.TextBox();
            this.SettingLot = new System.Windows.Forms.GroupBox();
            this.DG_LOTList = new System.Windows.Forms.DataGridView();
            this.BT_OpenWorkForm = new System.Windows.Forms.Button();
            this.GBInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.LineSettings.SuspendLayout();
            this.Fas_Start.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.UploadStationGB.SuspendLayout();
            this.FAS_Weight.SuspendLayout();
            this.SettingLot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).BeginInit();
            this.SuspendLayout();
            // 
            // GBInfo
            // 
            this.GBInfo.Controls.Add(this.BT_OpenSettings);
            this.GBInfo.Controls.Add(this.GridInfo);
            this.GBInfo.Location = new System.Drawing.Point(827, 16);
            this.GBInfo.Name = "GBInfo";
            this.GBInfo.Size = new System.Drawing.Size(479, 284);
            this.GBInfo.TabIndex = 1;
            this.GBInfo.TabStop = false;
            this.GBInfo.Text = "Информация о рабочей станции";
            this.GBInfo.Visible = false;
            // 
            // BT_OpenSettings
            // 
            this.BT_OpenSettings.FlatAppearance.BorderSize = 0;
            this.BT_OpenSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_OpenSettings.Image = global::GS_STB.Properties.Resources.settings__1_;
            this.BT_OpenSettings.Location = new System.Drawing.Point(6, 217);
            this.BT_OpenSettings.Name = "BT_OpenSettings";
            this.BT_OpenSettings.Size = new System.Drawing.Size(69, 63);
            this.BT_OpenSettings.TabIndex = 42;
            this.BT_OpenSettings.UseVisualStyleBackColor = true;
            this.BT_OpenSettings.Click += new System.EventHandler(this.BT_OpenSettings_Click);
            // 
            // GridInfo
            // 
            this.GridInfo.AllowUserToAddRows = false;
            this.GridInfo.AllowUserToDeleteRows = false;
            this.GridInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridInfo.ColumnHeadersVisible = false;
            this.GridInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridInfo.Location = new System.Drawing.Point(6, 19);
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.Size = new System.Drawing.Size(467, 192);
            this.GridInfo.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 5;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 5;
            // 
            // LineSettings
            // 
            this.LineSettings.Controls.Add(this.BTBack);
            this.LineSettings.Controls.Add(this.label2);
            this.LineSettings.Controls.Add(this.label1);
            this.LineSettings.Controls.Add(this.BT_SaveLine);
            this.LineSettings.Controls.Add(this.CB_Line);
            this.LineSettings.Location = new System.Drawing.Point(1323, 371);
            this.LineSettings.Name = "LineSettings";
            this.LineSettings.Size = new System.Drawing.Size(296, 202);
            this.LineSettings.TabIndex = 2;
            this.LineSettings.TabStop = false;
            this.LineSettings.Text = "Настройка Линии";
            this.LineSettings.Visible = false;
            // 
            // BTBack
            // 
            this.BTBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTBack.Location = new System.Drawing.Point(210, 91);
            this.BTBack.Name = "BTBack";
            this.BTBack.Size = new System.Drawing.Size(187, 44);
            this.BTBack.TabIndex = 22;
            this.BTBack.Text = "Назад";
            this.BTBack.UseVisualStyleBackColor = true;
            this.BTBack.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MistyRose;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(390, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Дальнейшая работа невозможна укажите рабочую линию!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Линия";
            // 
            // BT_SaveLine
            // 
            this.BT_SaveLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_SaveLine.Location = new System.Drawing.Point(9, 91);
            this.BT_SaveLine.Name = "BT_SaveLine";
            this.BT_SaveLine.Size = new System.Drawing.Size(196, 44);
            this.BT_SaveLine.TabIndex = 20;
            this.BT_SaveLine.Text = "Сохранить линию";
            this.BT_SaveLine.UseVisualStyleBackColor = true;
            // 
            // CB_Line
            // 
            this.CB_Line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Line.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Line.FormattingEnabled = true;
            this.CB_Line.Location = new System.Drawing.Point(9, 58);
            this.CB_Line.Name = "CB_Line";
            this.CB_Line.Size = new System.Drawing.Size(196, 28);
            this.CB_Line.TabIndex = 19;
            // 
            // Fas_Start
            // 
            this.Fas_Start.Controls.Add(this.groupBox3);
            this.Fas_Start.Controls.Add(this.groupBox2);
            this.Fas_Start.Location = new System.Drawing.Point(827, 306);
            this.Fas_Start.Name = "Fas_Start";
            this.Fas_Start.Size = new System.Drawing.Size(251, 214);
            this.Fas_Start.TabIndex = 3;
            this.Fas_Start.TabStop = false;
            this.Fas_Start.Text = "Fas_Start";
            this.Fas_Start.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CheckBoxSN);
            this.groupBox3.Controls.Add(this.TB_LabelSNCount);
            this.groupBox3.Location = new System.Drawing.Point(6, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 64);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Печать этикеток SN ";
            // 
            // CheckBoxSN
            // 
            this.CheckBoxSN.AutoSize = true;
            this.CheckBoxSN.Checked = true;
            this.CheckBoxSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxSN.Location = new System.Drawing.Point(11, 31);
            this.CheckBoxSN.Name = "CheckBoxSN";
            this.CheckBoxSN.Size = new System.Drawing.Size(152, 17);
            this.CheckBoxSN.TabIndex = 16;
            this.CheckBoxSN.Text = "Количество этикеток SN";
            this.CheckBoxSN.UseVisualStyleBackColor = true;
            this.CheckBoxSN.CheckedChanged += new System.EventHandler(this.CheckBoxSN_CheckedChanged);
            // 
            // TB_LabelSNCount
            // 
            this.TB_LabelSNCount.Location = new System.Drawing.Point(169, 28);
            this.TB_LabelSNCount.Name = "TB_LabelSNCount";
            this.TB_LabelSNCount.Size = new System.Drawing.Size(33, 20);
            this.TB_LabelSNCount.TabIndex = 8;
            this.TB_LabelSNCount.Text = "3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DateTimePicker);
            this.groupBox2.Controls.Add(this.Label7);
            this.groupBox2.Controls.Add(this.RB_Local_DateTime);
            this.groupBox2.Controls.Add(this.RB_Server_Time);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 120);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выбор режима даты";
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.CustomFormat = "dd.MM.yyyy";
            this.DateTimePicker.Enabled = false;
            this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker.Location = new System.Drawing.Point(9, 92);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(204, 20);
            this.DateTimePicker.TabIndex = 21;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(6, 72);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(209, 13);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "Укажите требуемую дату производства";
            // 
            // RB_Local_DateTime
            // 
            this.RB_Local_DateTime.AutoSize = true;
            this.RB_Local_DateTime.Location = new System.Drawing.Point(6, 46);
            this.RB_Local_DateTime.Name = "RB_Local_DateTime";
            this.RB_Local_DateTime.Size = new System.Drawing.Size(136, 17);
            this.RB_Local_DateTime.TabIndex = 18;
            this.RB_Local_DateTime.TabStop = true;
            this.RB_Local_DateTime.Text = "Время локально с ПК";
            this.RB_Local_DateTime.UseVisualStyleBackColor = true;
            // 
            // RB_Server_Time
            // 
            this.RB_Server_Time.AutoSize = true;
            this.RB_Server_Time.Checked = true;
            this.RB_Server_Time.Location = new System.Drawing.Point(6, 23);
            this.RB_Server_Time.Name = "RB_Server_Time";
            this.RB_Server_Time.Size = new System.Drawing.Size(62, 17);
            this.RB_Server_Time.TabIndex = 16;
            this.RB_Server_Time.TabStop = true;
            this.RB_Server_Time.Text = "Сервер";
            this.RB_Server_Time.UseVisualStyleBackColor = true;
            // 
            // UploadStationGB
            // 
            this.UploadStationGB.Controls.Add(this.CHID);
            this.UploadStationGB.Controls.Add(this.CHSN);
            this.UploadStationGB.Controls.Add(this.CheckBoxDublicateSCID);
            this.UploadStationGB.Controls.Add(this.TB_LabelIDCount);
            this.UploadStationGB.Controls.Add(this.TBCHSN);
            this.UploadStationGB.Location = new System.Drawing.Point(1094, 532);
            this.UploadStationGB.Name = "UploadStationGB";
            this.UploadStationGB.Size = new System.Drawing.Size(212, 100);
            this.UploadStationGB.TabIndex = 39;
            this.UploadStationGB.TabStop = false;
            this.UploadStationGB.Text = "UploadStation";
            this.UploadStationGB.Visible = false;
            // 
            // CHID
            // 
            this.CHID.AutoSize = true;
            this.CHID.Checked = true;
            this.CHID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHID.Location = new System.Drawing.Point(6, 43);
            this.CHID.Name = "CHID";
            this.CHID.Size = new System.Drawing.Size(148, 17);
            this.CHID.TabIndex = 24;
            this.CHID.Text = "Количество этикеток ID";
            this.CHID.UseVisualStyleBackColor = true;
            this.CHID.CheckedChanged += new System.EventHandler(this.CHID_CheckedChanged);
            // 
            // CHSN
            // 
            this.CHSN.AutoSize = true;
            this.CHSN.Location = new System.Drawing.Point(6, 19);
            this.CHSN.Name = "CHSN";
            this.CHSN.Size = new System.Drawing.Size(152, 17);
            this.CHSN.TabIndex = 25;
            this.CHSN.Text = "Количество этикеток SN";
            this.CHSN.UseVisualStyleBackColor = true;
            this.CHSN.CheckedChanged += new System.EventHandler(this.CHSN_CheckedChanged);
            // 
            // CheckBoxDublicateSCID
            // 
            this.CheckBoxDublicateSCID.AutoSize = true;
            this.CheckBoxDublicateSCID.Checked = true;
            this.CheckBoxDublicateSCID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxDublicateSCID.Location = new System.Drawing.Point(6, 67);
            this.CheckBoxDublicateSCID.Name = "CheckBoxDublicateSCID";
            this.CheckBoxDublicateSCID.Size = new System.Drawing.Size(180, 17);
            this.CheckBoxDublicateSCID.TabIndex = 26;
            this.CheckBoxDublicateSCID.Text = "Проверка уникальности SC ID";
            this.CheckBoxDublicateSCID.UseVisualStyleBackColor = true;
            // 
            // TB_LabelIDCount
            // 
            this.TB_LabelIDCount.Location = new System.Drawing.Point(160, 40);
            this.TB_LabelIDCount.Name = "TB_LabelIDCount";
            this.TB_LabelIDCount.Size = new System.Drawing.Size(33, 20);
            this.TB_LabelIDCount.TabIndex = 22;
            this.TB_LabelIDCount.Text = "2";
            // 
            // TBCHSN
            // 
            this.TBCHSN.Location = new System.Drawing.Point(160, 16);
            this.TBCHSN.Name = "TBCHSN";
            this.TBCHSN.Size = new System.Drawing.Size(33, 20);
            this.TBCHSN.TabIndex = 23;
            this.TBCHSN.Text = "0";
            // 
            // FAS_Weight
            // 
            this.FAS_Weight.Controls.Add(this.TB_Deviation);
            this.FAS_Weight.Controls.Add(this.Label16);
            this.FAS_Weight.Controls.Add(this.Label10);
            this.FAS_Weight.Controls.Add(this.TB_AutoSetSNin);
            this.FAS_Weight.Location = new System.Drawing.Point(1323, 579);
            this.FAS_Weight.Name = "FAS_Weight";
            this.FAS_Weight.Size = new System.Drawing.Size(92, 24);
            this.FAS_Weight.TabIndex = 41;
            this.FAS_Weight.TabStop = false;
            this.FAS_Weight.Text = "FAS_Weight";
            this.FAS_Weight.Visible = false;
            // 
            // TB_Deviation
            // 
            this.TB_Deviation.Location = new System.Drawing.Point(418, 49);
            this.TB_Deviation.Name = "TB_Deviation";
            this.TB_Deviation.Size = new System.Drawing.Size(114, 20);
            this.TB_Deviation.TabIndex = 49;
            this.TB_Deviation.Text = "15";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label16.Location = new System.Drawing.Point(18, 111);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(240, 20);
            this.Label16.TabIndex = 50;
            this.Label16.Text = "Окно ввода серийного номера";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label10.Location = new System.Drawing.Point(18, 28);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(579, 80);
            this.Label10.TabIndex = 48;
            this.Label10.Text = "Для начала работы выполните следующие действия:\n1. Укажите отклонение от эталонно" +
    "й массы, грамм\n2. Поместите приемник, подготовленный к упаковке, на платформу ве" +
    "сов.\n3. Отсканируйте номер приемника.";
            // 
            // TB_AutoSetSNin
            // 
            this.TB_AutoSetSNin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TB_AutoSetSNin.Location = new System.Drawing.Point(18, 139);
            this.TB_AutoSetSNin.Name = "TB_AutoSetSNin";
            this.TB_AutoSetSNin.Size = new System.Drawing.Size(525, 26);
            this.TB_AutoSetSNin.TabIndex = 47;
            this.TB_AutoSetSNin.Text = "12345678901234";
            // 
            // SettingLot
            // 
            this.SettingLot.Controls.Add(this.DG_LOTList);
            this.SettingLot.Controls.Add(this.BT_OpenWorkForm);
            this.SettingLot.Location = new System.Drawing.Point(12, 12);
            this.SettingLot.Name = "SettingLot";
            this.SettingLot.Size = new System.Drawing.Size(809, 697);
            this.SettingLot.TabIndex = 42;
            this.SettingLot.TabStop = false;
            this.SettingLot.Text = "SettingLot";
            this.SettingLot.Visible = false;
            // 
            // DG_LOTList
            // 
            this.DG_LOTList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DG_LOTList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DG_LOTList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_LOTList.Cursor = System.Windows.Forms.Cursors.Default;
            this.DG_LOTList.Location = new System.Drawing.Point(6, 19);
            this.DG_LOTList.Name = "DG_LOTList";
            this.DG_LOTList.ReadOnly = true;
            this.DG_LOTList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DG_LOTList.Size = new System.Drawing.Size(787, 572);
            this.DG_LOTList.TabIndex = 4;
            this.DG_LOTList.Visible = false;
            // 
            // BT_OpenWorkForm
            // 
            this.BT_OpenWorkForm.FlatAppearance.BorderSize = 0;
            this.BT_OpenWorkForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_OpenWorkForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_OpenWorkForm.Image = global::GS_STB.Properties.Resources.play;
            this.BT_OpenWorkForm.Location = new System.Drawing.Point(704, 597);
            this.BT_OpenWorkForm.Name = "BT_OpenWorkForm";
            this.BT_OpenWorkForm.Size = new System.Drawing.Size(89, 95);
            this.BT_OpenWorkForm.TabIndex = 24;
            this.BT_OpenWorkForm.Text = "Запуск";
            this.BT_OpenWorkForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BT_OpenWorkForm.UseVisualStyleBackColor = true;
            this.BT_OpenWorkForm.Visible = false;
            this.BT_OpenWorkForm.Click += new System.EventHandler(this.BT_OpenWorkForm_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1558, 721);
            this.Controls.Add(this.SettingLot);
            this.Controls.Add(this.FAS_Weight);
            this.Controls.Add(this.UploadStationGB);
            this.Controls.Add(this.Fas_Start);
            this.Controls.Add(this.LineSettings);
            this.Controls.Add(this.GBInfo);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GBInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.LineSettings.ResumeLayout(false);
            this.LineSettings.PerformLayout();
            this.Fas_Start.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.UploadStationGB.ResumeLayout(false);
            this.UploadStationGB.PerformLayout();
            this.FAS_Weight.ResumeLayout(false);
            this.FAS_Weight.PerformLayout();
            this.SettingLot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBInfo;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox LineSettings;
        private System.Windows.Forms.GroupBox Fas_Start;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.RadioButton RB_Server_Time;
        internal System.Windows.Forms.RadioButton RB_Local_DateTime;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox CheckBoxSN;
        internal System.Windows.Forms.TextBox TB_LabelSNCount;
        private System.Windows.Forms.GroupBox UploadStationGB;
        internal System.Windows.Forms.CheckBox CHID;
        internal System.Windows.Forms.CheckBox CHSN;
        internal System.Windows.Forms.CheckBox CheckBoxDublicateSCID;
        internal System.Windows.Forms.TextBox TB_LabelIDCount;
        internal System.Windows.Forms.TextBox TBCHSN;
        private System.Windows.Forms.GroupBox FAS_Weight;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.TextBox TB_Deviation;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.TextBox TB_AutoSetSNin;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button BT_SaveLine;
        internal System.Windows.Forms.ComboBox CB_Line;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button BT_OpenSettings;
        private System.Windows.Forms.GroupBox SettingLot;
        private System.Windows.Forms.DataGridView DG_LOTList;
        internal System.Windows.Forms.Button BT_OpenWorkForm;
        internal System.Windows.Forms.Button BTBack;
        internal System.Windows.Forms.DateTimePicker DateTimePicker;
    }
}