using GS_STB.Class_Modules;
using GS_STB.Forms_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB
{
    public partial class WorkForm : Form
    {
        private BaseClass BaseC;     
        string UserName { get; set; }
        List<object> ListLoggin = new List<object>();
        string CellClick;

        readonly List<string> ListInfoGrid = new List<string>() { "Название приложения", "Название станции","Линия","Номер Лота","Полное имя лота","Модель" };


        public WorkForm(BaseClass BC,int LOTID)
        {
            InitializeComponent();
            
            BaseC = BC; //Приведение к типу
            BaseC.LOTID = LOTID;
            this.Text = BaseC.GetType().Name; //Заголовок формы называется именем модулем
            GetLoggin();//Настройка форма ввода пароля           
            Times.Enabled = true; LBPrintSN.Text = DateTime.Now.ToString("dd.MM.yyyy"); //Настройка времени
            Controllabel.Text = "";            
            //Реализация загрузки определенного класса

            CloseApp.Click += (a, e) =>
            {
                var Result = MessageBox.Show("Уверены, что хотите выйти?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                    Application.Exit();
            };

            BackButton.Click += (a, e) => 
            {
                var Result = MessageBox.Show("Уверены, что хотите вернуться в меню настройки?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                    Close();
            };          

            BTPrint.Click += (a, e) =>
             {
                 if (ListPrinter.SelectedIndex == -1)
                    { MessageBox.Show("Принтер не выбран"); return; }

                 BaseC.printName = ListPrinter.SelectedItem.ToString();
                 PrintLBName.Text = "Текущий принтер \n" + BaseC.printName;
                 ListPrinter.ClearSelected();                 
             };


            ClearBT.Click += (a, e) =>
            {
                //BaseC.cts.Cancel();
                SerialTextBox.Enabled = true;
                SerialTextBox.Clear();
                SerialTextBox.Select();
                BT_Disassebly.Enabled = false;
            };

            TB_RFIDIn.KeyDown += (a, e) => //Событие при вводе логина
            {
                if (e.KeyCode == Keys.Enter)
                    
                    if (GetLoginData()) //Метод, которы проверяет логин и добавляет в ArrayList данные о пользователе    
                    {
                        TB_RFIDIn.Clear(); TB_RFIDIn.Select();
                    }           
            };

            SerialTextBox.KeyDown += (a, e) => //Событие скнирование номера
            {
                if (e.KeyCode == Keys.Enter)
                {                 
                    BaseC.KeyDownMethod();

                    if (BaseC.GetType() == typeof(UploadStation))
                        return;

                    if (BaseC.GetType() == typeof(Desassembly_STB))
                        return;   

                    SerialTextBox.Clear(); SerialTextBox.Select();
                }
            };

            //Сохранение координат
            BT_SevePrintSettings.Click += (a, e) => 
            {

                if (BaseC.CheckPathPrinterSettings())
                    BaseC.CreatePathPrinter();
                
                    SaveSettingPrint();
                
            };

            BT_PrinterSettings.Click += (a, e) => 
            {
                var conf = new ConfimUser();
                var r = conf.ShowDialog();
                if (r != DialogResult.OK)
                {
                    return;
                }

                GB_PrinterSettings.Visible = true; GBNew.Visible = true;           
            };

            BT_ClosePrintSet.Click += (a, e) => 
            { 
                GB_PrinterSettings.Visible = false;  SaveSettingBT.Text = ""; SerialTextBox.Select(); GBNew.Visible = false;

            };

            CB_ErrorCode.TextChanged += (a, e) => 
            {
                CB_ErrorCode.MaxLength = 2;
                if (CB_ErrorCode.Text.Length == 2)
                    BT_Disassebly.Select();
            };

            BT_Disassebly.Click += (a, e) => 
            {               
                if (string.IsNullOrEmpty(CB_ErrorCode.Text))
                { MessageBox.Show("Укажите код отказа"); CB_ErrorCode.Select(); return; }

                if (CheckErrocode(CB_ErrorCode.Text))
                { MessageBox.Show("Не корректный код отказа"); CB_ErrorCode.Select(); return; }
               
                var ErrorCodeID = new Desassembly_STB().GetErrorCodeID(CB_ErrorCode.Text);
                var idApp = new Desassembly_STB().IDApp;

                if (SerialTextBox.TextLength == 21)
                {
                    if (BaseC.LOTID == 188)
                    {
                        var _result = BaseC.CheckDBDesPacking();
                        if (_result != 0)
                        {
                            MessageBox.Show($"Обнаружено, что по номеру платы '{BaseC.PCBID}' приемник упакован! \n {_result}"); CB_ErrorCode.Select(); return;
                        }

                        if (!BaseC.GetSerialUpload())
                        { 
                            MessageBox.Show($"По номеру платы '{BaseC.PCBID}' приемник не найден в FAS_Upload! \n {_result}"); CB_ErrorCode.Select(); return;
                        }
                    }                  

                    BaseC.WriteToDBDesis(ErrorCodeID);
                    BaseC.AddLogDesis(idApp);
                }
                else
                {
                    var serial = int.Parse(SerialTextBox.Text.Substring(15));
                    var smID = GetSmartCardID(serial);                    
                    var casid = GetCASID(serial);
                    BaseC.WriteToDBDesis(serial, SerialTextBox.Text, ErrorCodeID);
                    BaseC.UpdateToDBDesis(serial);
                    BaseC.DeleteToDBWeight(serial);
                    BaseC.DeleteToDBDesis(serial);
                    BaseC.DeleteToUpload(serial);
                    BaseC.AddLogDesis(serial, idApp, smID, SerialTextBox.Text, casid);
                }
                byte Result;
                if (CB_ErrorCode.Text == "TT")               
                    Result = 2;
                else
                    Result = 3;

                BaseC.AddCt_OperLog(34, Result);   
                BaseC.ShiftCounter += 1;
                BaseC.LotCounter += 1;
                BaseC.ShiftCounterUpdate();
                BaseC.LotCounterUpdate();
                Label_ShiftCounter.Text = BaseC.ShiftCounter.ToString();
                LB_LOTCounter.Text = BaseC.LotCounter.ToString();                
                DG_UpLog.Rows.Add(int.Parse(Label_ShiftCounter.Text), SerialTextBox.Text, CB_ErrorCode.Text, DateTime.UtcNow.AddHours(2));
                DG_UpLog.Sort(DG_UpLog.Columns[0], System.ComponentModel.ListSortDirection.Descending);                
                CB_ErrorCode.Text = "";
                BT_Disassebly.Enabled = false;                
                BaseC.LabelStatus(Controllabel, $"Серийный номер { SerialTextBox.Text } \n ОТКРЕПЛЁН УСПЕШНО", Color.Green);
                SerialTextBox.Enabled = true;
                SerialTextBox.Clear();
                SerialTextBox.Select();

            };
        }

        string GetCASID(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Upload.Where(c => c.SerialNumber == serial).Select(c => c.CASID).FirstOrDefault();
            }
            
        }

        long GetSmartCardID(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Upload.Where(c => c.SerialNumber == serial).Select(c => c.SmartCardID).FirstOrDefault();
            }

        }

        string GetFullSTBSN(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Start.Where(c => c.SerialNumber == serial).Select(c => c.FullSTBSN).FirstOrDefault();
            }

        }
        void SaveSettingPrint()
        {
            foreach (var item in new List<GroupBox>() { SNPRINT,IDPrint })
            {
                if (!item.Visible)               
                    continue;
                
                foreach (var n in item.Controls.OfType<NumericUpDown>())
                {
                    var line = Directory.GetFiles(@"C:\PrinterSettings").Where(c => c.Contains(n.Name)).FirstOrDefault();
                    if (string.IsNullOrEmpty(line))
                        break;

                    var line2 = line.Substring(0, 23) + n.Value.ToString() + ".txt";

                    File.Move(line, line2);
                    SaveSettingBT.Text = "Сохранено!";
                }
            }
        }

        bool CheckErrocode(string Err)
        {
            //if (CB_ErrorCode.DataSource.GetType().)          
            //    return false;

            foreach (var item in CB_ErrorCode.Items)
                if (item.ToString().Contains(Err))
                    return false;
            return true;

            //if (CB_ErrorCode.Items.Cast<List<string>>().Select(c => c.Contains(Err)).FirstOrDefault())
            //    return false;
            //return true;

        }
        void GetWorkForm()// Инициализация компонентов рабочей формы
        {
            GB_Work.Visible = true;
            GB_Work.Location = new Point(12, 12);
            GB_Work.Size = new Size(1400, 800);
            GetGridSetting(); //Настройка лога Грида, который внизу от ввода серийного номера
            BaseC.control = this;
            BaseC.LoadWorkForm();
            SetInfoGrid();            
            SerialTextBox.Select();
        }

        bool GetLoginData() //Проверка логина и получение данных о пользователе
        {
            if (GetUser())   //Проверка правильности пароля         
                return true;
            
            GetWorkForm();
            return false;
        }

        bool GetUser()
        {
            using (FASEntities FAS = new FASEntities())
            {
                if (TB_RFIDIn.Text == "1" )
                {
                    return true;
                }
                UserName = FAS.FAS_Users.Where(c => c.RFID == TB_RFIDIn.Text && c.IsActiv == true).Select(c => c.UserName).FirstOrDefault();
                if (string.IsNullOrEmpty(UserName))
                    return true;
                BaseC.UserID = FAS.FAS_Users.Where(c => c.RFID == TB_RFIDIn.Text && c.IsActiv == true).Select(c => c.UserID).FirstOrDefault();
                //BaseC.ArrayList.Add(Name);
                return false;
            }
        }
        void GetLoggin() //Настройка форма ввода пароля
        {
            GB_UserData.Visible = true;
            GB_UserData.Location = new Point(12,12);
            GB_UserData.Size = new Size(429, 177);
            TB_RFIDIn.Select();
        }
        void SetInfoGrid()
        {
            if (BaseC.GetType() == typeof(FAS_END))
            { ListInfoGrid.AddRange(new List<string>() {"Литера - ЛитерИндекс","Кол-во в групповой","Кол-во в паллете" }); }
                      

            GridInfo.RowCount = ListInfoGrid.Count + 1;
            for (int i = 0; i < GridInfo.RowCount; i++)
            {
                if (GridInfo.RowCount - i == 1)
                {
                    GridInfo[0, i].Value = "Имя пользователя";
                    GridInfo[1, i].Value = UserName;
                    break;
                }
                GridInfo[0, i].Value = ListInfoGrid[i];
                GridInfo[1, i].Value = BaseC.ArrayList[i];
            }
        }

        void GetGridSetting()
        {
            DG_UpLog.ColumnCount = BaseC.ListHeader.Count;
            for (int i = 0; i < BaseC.ListHeader.Count; i++)            
                DG_UpLog.Columns[i].HeaderText = BaseC.ListHeader[i];           
        }

        private void Times_Tick(object sender, EventArgs e)                
        {
            CurrrentTimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }       

        private void SaveClick_Click(object sender, EventArgs e)
        {
            if (CheckModelSettingDelay())           
                UpdateSettingDelay();
            else            
                AddSettingDelay();

            label17.Text = "Сохранено";
            label17.BackColor = Color.Green;
        }

        bool CheckModelSettingDelay()
        {
            using (var fas = new FASEntities())
            {
                var result = fas.FAS_UploadSetting.Where(c => fas.FAS_GS_LOTs.FirstOrDefault(b => b.LOTID == BaseC.LOTID).ModelID == c.ModelID).Select(c => c.ID == c.ID).FirstOrDefault();
                return result;
            }
        }

        void AddSettingDelay()
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < GridDelaySetting.RowCount; i++)
                {
                    var del = new FAS_UploadSetting()
                    {
                        Num = byte.Parse(GridDelaySetting[1, i].Value.ToString()),
                        Name_Stage = GridDelaySetting[0, i].Value.ToString(),
                        DelaySetting = short.Parse(GridDelaySetting[2, i].Value.ToString()),
                        CheckStage = bool.Parse(GridDelaySetting[3, i].Value.ToString()),
                        ModelID = fas.FAS_GS_LOTs.FirstOrDefault(b => b.LOTID == BaseC.LOTID).ModelID

                    };

                    fas.FAS_UploadSetting.Add(del);
                    fas.SaveChanges();
                }
               
            }
        }

        void UpdateSettingDelay()
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < GridDelaySetting.RowCount; i++)
                {
                    var Set = fas.FAS_UploadSetting.Where(c => fas.FAS_GS_LOTs.FirstOrDefault(b => b.LOTID == BaseC.LOTID).ModelID == c.ModelID & c.Num == i);
                    Set.FirstOrDefault().Num = byte.Parse(GridDelaySetting[1, i].Value.ToString());
                    Set.FirstOrDefault().Name_Stage = GridDelaySetting[0, i].Value.ToString();
                    Set.FirstOrDefault().DelaySetting = short.Parse(GridDelaySetting[2, i].Value.ToString());
                    Set.FirstOrDefault().CheckStage = bool.Parse(GridDelaySetting[3, i].Value.ToString());

                    fas.SaveChanges();
                }
            }
        }
        private void PrintCheckSN_Click(object sender, EventArgs e)
        {
            var Confim = new ConfimUser();
            var Result = Confim.ShowDialog();

            if (Result == DialogResult.Cancel)
                PrintCheckSN.Checked = !PrintCheckSN.Checked;
        }

        private void WorkForm_Load(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                this.Text =  "Work Form" + "Verison Product - " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                this.Visible = true;
            }
        }

        private void GridDelaySetting_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int result;
            if (e.ColumnIndex == 1)
            {
                if (!int.TryParse(GridDelaySetting[e.ColumnIndex,e.RowIndex].Value.ToString(),out result)) {
                    MessageBox.Show("Можно вставить только число"); GridDelaySetting[e.ColumnIndex, e.RowIndex].Value = CellClick; return;
                }

                int max;
                using (var fas = new FASEntities())
                {
                    max = fas.FAS_UploadSetting.Select(c => c.Num).Max();
                }

                if (result <= 0)
                {
                    MessageBox.Show($"Число {result} не может быть меньшн чем 0"); GridDelaySetting[e.ColumnIndex, e.RowIndex].Value = CellClick; return;
                }

                if (result > max) {
                    MessageBox.Show($"Число {result} не может быть больше чем {max}"); GridDelaySetting[e.ColumnIndex, e.RowIndex].Value = CellClick; return;
                }

                for (int i = 0; i < GridDelaySetting.RowCount; i++)
                {
                    if (i == e.RowIndex)                   
                        continue;
                   
                    if (result.ToString() == GridDelaySetting[1,i].Value.ToString())
                    {
                        GridDelaySetting[1, i].Value = CellClick;  break;
                    }
                }
                this.BeginInvoke(new MethodInvoker(() => {
                    GridDelaySetting.Sort(GridDelaySetting.Columns[1], ListSortDirection.Ascending);
                }));
               

            }

            if (e.ColumnIndex == 2)
            {
                if (!int.TryParse(GridDelaySetting[e.ColumnIndex, e.RowIndex].Value.ToString(), out result))
                {
                    MessageBox.Show("Можно вставить только число"); GridDelaySetting[e.ColumnIndex, e.RowIndex].Value = CellClick; return;
                }
            }
        }

        private void GridDelaySetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == 3)           
                return;
           
            CellClick = GridDelaySetting[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        private void GridDelaySetting_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            var myColumn = 1;//номер колонки с нестандартной сортировкой
            if (e.Column.Index == myColumn)
            {
                var s1 = e.CellValue1.ToString();
                var s2 = e.CellValue2.ToString();
                e.SortResult = ToInt(s1).CompareTo(ToInt(s2));

                e.Handled = true;
            }
           
        }

        private ulong ToInt(string s)
        {
            var num = 0ul;
            foreach (Match m in Regex.Matches(s, @"\d+"))
                num = num * 1000 + ulong.Parse(m.Value);

            return num;
        }
    }
}
