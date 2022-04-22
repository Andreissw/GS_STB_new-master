using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
   
    public partial class FAS_LOT_Managment : Form
    {
        public FAS_LOT_Managment()
        {
            InitializeComponent();
            ShowGR(LotsGR, true);
        }

        byte labelscenarioId;
        byte WorkScenarioId;
        short modelID;
        bool Option = false;
        string CellData;
        string _cell;
        //LotCode  FullLotCode 	Model Spec DTVS TRICOLOR Market  Ptid BOX Pallet HDCP  Cert Mac ModelCH SW SW1 Weight	Label Date	User st  end range stDate      
        List<string> ListName = new List<string>()         
        {"LotCode", "FullLotCode","LOTID", "Model", "Spec", "Manufacturer", "Operator","Market","PTID","BoxCapacity","PalletCapacity","HDCP","Cert","Mac"
         ,"ModelCheck","SWRead","SWGS1Read","WeightCheck","LabelScenario","WorkingScenario","Дата создания","Создатель Лота","Начальный диапазон","Конечный диапазон"
            ,"Работа по диапазону","Стартовая дата диапазона","Version","IsBunch(связка BOT и TOP)","ВесовойКонтроль с упаковкой"};

        List<string> ListNameAdd = new List<string>()
        {"LotCode", "FullLotCode","Model", "Spec", "Manufacturer", "Operator","Market","PTID","BoxCapacity","PalletCapacity","HDCP","Cert","Mac"
         ,"ModelCheck","SWRead","SWGS1Read","WeightCheck","WorkingScenario","LabelScenario","Version","IsBunch(связка BOT и TOP)","ВесовойКонтроль с упаковкой"};

        private void FAS_LOT_Managment_Load(object sender, EventArgs e)
        {          
           GetLot(GridLot);            
        }

        string GetPath() //Выбор пути для загрузки данных
        {
            var Folder = new FolderBrowserDialog();
            Folder.ShowDialog();

            return Folder.SelectedPath;
            //return @"C:\Users\a.volodin\Desktop\B527_HDCP_100 шт";

        }
        private void GridAddLot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GridAddLot.CommitEdit(DataGridViewDataErrorContexts.Commit); // Для того чтобы работало событие на фчек бокс в гриде
        }

        short GetLength()
        {
            var modelname = GridAddLot[1, 2].Value.ToString();
            using (var fas = new FASEntities())
            {
                var result = fas.FAS_Models.Where(c => c.ModelName == modelname).Select(c => c.lenHDCP).FirstOrDefault();
                if (result == null)
                    return 0;
                return (short)result;
            }
        }

        private void GridAddLot_CellValueChanged(object sender, DataGridViewCellEventArgs e) //Событие на  чек бокс в гриде добавление лота
        {
            if (e.ColumnIndex == 0)
                return;


            if (e.RowIndex == 10) {  //Открытие HDCP

                if (GridAddLot[1, 2].Value == null)
                {
                    MessageBox.Show("Выберите модель!");
                    return;
                }

                var length = GetLength();
                if (length == 0)
                {
                    MessageBox.Show("У модели не указана длина HDCP ключа");
                    return;
                }

                GetData(HDCPGrid);
            }
            else if (e.RowIndex == 11) //Открытие Сертификатов
                GetData(CertGrid);           

            void GetData(DataGridView grid)
            {
                if (GridAddLot.CurrentCell.Value.ToString() == "True")
                {
                    var path = GetPath(); //Выбираем путь

                    if (string.IsNullOrEmpty(path))
                    { var r = (DataGridViewCheckBoxCell)GridAddLot.CurrentCell; r.Value = false; return; }

                    var Dir = new DirectoryInfo(path);
                    foreach (var item in Dir.GetFiles()) {

                        if (item.Name.Length > 15)
                        {
                            MessageBox.Show("длина имя ключа больше 15, не пройдена валидация данных ");
                            HDCPGrid.Rows.Clear();
                            return;
                        }

                        grid.Rows.Add(item.Name, item.FullName);
                        if (grid.Name == "HDCPGrid")
                        {
                            var length = GetLength();
                           
                            if (length != File.ReadAllBytes(HDCPGrid[1, 0].Value.ToString()).Length)
                            {
                                MessageBox.Show("Длина ключа не соответствует");
                                HDCPGrid.Rows.Clear();
                                return;
                            }
                            
                           
                        }
                      
                    }
                }
                else if (GridAddLot.CurrentCell.Value.ToString() == "False")                
                    grid.RowCount = 0;                    
                
            }
        }
        private void BT_AddLot_Click(object sender, EventArgs e) //Открытие формы Добавить Лот
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};

            if (GridLot.CurrentRow.Index == -1)
                return;

            ShowGR(LotsGR, false);
            ShowGR(AddLotGR, true);
            OpenAdd();            
        }

        private void OpenAdd() //Обновление грида на добавление лота
        {
            GridAddLot.DataSource = null;
            CellData = "";
            GridAddLot.RowCount = ListNameAdd.Count;
            for (int i = 0; i < GridAddLot.RowCount; i++)
                GridAddLot[0, i].Value = ListNameAdd[i];

            var CBCEll = new DataGridViewComboBoxCell();
            CBCEll.DataSource = ModelList();
            GridAddLot[1, 2] = CBCEll;

            for (int i = 10; i <= 17; i++) { 

                GridAddLot[1, i] = new DataGridViewCheckBoxCell() { Value = false };
            }

            GridAddLot[1, 20] = new DataGridViewCheckBoxCell() { Value = false };
            GridAddLot[1, 21] = new DataGridViewCheckBoxCell() { Value = false };

            var LbWork = new DataGridViewComboBoxCell();
            LbWork.DataSource = WorkScenario();
            GridAddLot[1, 17] = LbWork;

            var LbCEll = new DataGridViewComboBoxCell();
            LbCEll.DataSource = LabelScenario();
            GridAddLot[1, 18] = LbCEll;
            GridAddLot[1, 19].Value = null; //Версия
            GridAddLot[1, 0].Value = null;
            GridAddLot[1, 1].Value = null;
            GridAddLot[1, 3].Value = null;
            GridAddLot[1, 4].Value = "DTVS";
            GridAddLot[1, 5].Value = "TRICOLOR";
            GridAddLot[1, 6].Value = null;
            GridAddLot[1, 7].Value = null;
            GridAddLot[1, 8].Value = null;
            GridAddLot[1, 9].Value = null;

            GridAddLot.Columns[0].ReadOnly = true;

            for (int i = 0; i < GridAddLot.RowCount; i++)           
                GridAddLot[1, i].ReadOnly = false;
          
        }

        void SizeLot()
        {
            var lotid = short.Parse(GridLot[6, GridLot.CurrentRow.Index].Value.ToString());
            var list = GetLotInfo(lotid);           

            for (int i = 0; i < GridAddLot.RowCount; i++)
            {               
                if (i == 10 || i == 11)
                    continue;
                GridAddLot[1, i] = new DataGridViewTextBoxCell();
                GridAddLot[1, i].ReadOnly = true;
            }

            GridAddLot[1, 0].Value = list[0];
            GridAddLot[1, 1].Value = list[1];
            GridAddLot[1, 2].Value = list[3];
            GridAddLot[1, 3].Value = list[4];
            GridAddLot[1, 4].Value = list[5];
            GridAddLot[1, 5].Value = list[6];
            GridAddLot[1, 6].Value = list[7];
            GridAddLot[1, 7].Value = list[8];
            GridAddLot[1, 8].Value = list[9];
            GridAddLot[1, 8].Value = list[9];
            GridAddLot[1, 9].Value = list[10];
            GridAddLot[1, 10].Value = list[11];
            GridAddLot[1, 11].Value = list[12];
            GridAddLot[1, 12].Value = list[13];
            GridAddLot[1, 13].Value = list[14];
            GridAddLot[1, 14].Value = list[15];
            GridAddLot[1, 15].Value = list[16];
            GridAddLot[1, 16].Value = list[17];
            GridAddLot[1, 17].Value = list[19];
            GridAddLot[1, 18].Value = list[18];
            GridAddLot[1, 19].Value = list[26];
            GridAddLot[1, 20].Value = list[27];
            GridAddLot[1, 21].Value = list[28];

         

        }

        private void OptionBT_Click(object sender, EventArgs e) //Редактирования
        {
            if (!Option)
            {
                Option = true; this.OptionBT.Image = global::GS_STB.Properties.Resources.ON;
                LBOption.Text = "Редактирование данных \n ON"; GridInfo.ReadOnly = false; UpdateBT.Visible = true; BTRange.Visible = true;

                if (GridRange.Visible)
                {
                    Create.Visible = true; RBLit.Visible = true; RBRow.Visible = true; RBRow.Checked = true;
                }
                
                //-------------------------------------------- Превращение ячейки модели в кмбобокс для редактирования Model
                string _cell = GridInfo[1, 3].Value.ToString();
                GetModelId(_cell);                
                var CBCEll = new DataGridViewComboBoxCell();
                CBCEll.DataSource = ModelList();                
                CBCEll.Value = _cell;
                GridInfo[1, 3] = CBCEll;
                //-------------------------------------------

                //------------------------------------------- Преварщение ячейки в булевые значение hdcp cert и т.д
                bool result = false;
                for (int i = 11; i <= 17; i++)
                {
                    var BoolCell = new DataGridViewCheckBoxCell();
                    //BoolCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    
                    if (GridInfo[1, i].Value != null)
                        bool.TryParse(GridInfo[1, i].Value.ToString(), out result);


                    //bool value = bool.Parse(GridInfo[1, i].Value.ToString());
                    bool value = result;
                    BoolCell.Value = value;
                    GridInfo[1, i] = BoolCell;
                }

                result = false;
                if (GridInfo[1, 27].Value != null)
                    bool.TryParse(GridInfo[1, 27].Value.ToString(), out result);

                var BoolCell2 = new DataGridViewCheckBoxCell();
                bool value2 = result;
                BoolCell2.Value = value2;
                GridInfo[1, 27] = BoolCell2;

                if (GridInfo[1, 28].Value != null)
                    bool.TryParse(GridInfo[1, 28].Value.ToString(), out result);

                var BoolCell3 = new DataGridViewCheckBoxCell();
                bool value3 = result;
                BoolCell3.Value = value3;
                GridInfo[1, 28] = BoolCell3;
                //------------------------------------------- 

                string _cellLB = GridInfo[1, 18].Value.ToString(); // LabelScenario Combobox
                if (_cellLB == " | ")               
                    _cellLB = "";
                else
                    GetlabelscenarioID(_cellLB.Substring(0, 1));

                var LbCEll = new DataGridViewComboBoxCell();
                LbCEll.DataSource = LabelScenario();
                LbCEll.DropDownWidth = 300;
                LbCEll.Value = _cellLB;
                GridInfo[1, 18] = LbCEll;

                string _cellWork = GridInfo[1, 19].Value.ToString(); // WorkScenario Combobox
                if (_cellWork == " | ")
                    _cellWork = "";
                else
                    GetWorkScenarioID(_cellWork.Substring(0, 1));

                var _cellWorkCB = new DataGridViewComboBoxCell();
                _cellWorkCB.DataSource = WorkScenario();
                _cellWorkCB.DropDownWidth = 300;
                _cellWorkCB.Value = _cellWork;
                GridInfo[1, 19] = _cellWorkCB;
            }
            else
            {
                OffInfo();               
            }
        }

        private void BT_RegisterNewLOT_Click(object sender, EventArgs e) //Сохранение нового лота
        {
            var list = new List<string>(); 
            if (NumerSN.Value == 0) //Проверка на кол-во номеров в лоте
            {
                MessageBox.Show("Количество в лоте не указано", "Ошибка с заполнением данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GridAddLot.ClearSelection();
                NumerSN.Select();
                return;
            }           

            for (int i = 0; i < GridAddLot.RowCount; i++) //Проверка на заполнение грида
            {
                if (i == 2)
                    continue;

                if (GridAddLot[1, i].Value == null)
                { MessageBox.Show("Не все поля заполнены!","Ошибка с заполнением данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridAddLot.ClearSelection(); GridAddLot[1, i].Selected = true; return; }

                list.Add(GridAddLot[1, i].Value.ToString());
            }

            if (bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
            {
                if (NumerSN.Value > HDCPGrid.RowCount)
                {
                    MessageBox.Show("Количество SN в лоте не соответсвтует Кол-ву HDCP", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true)
            {
                if (NumerSN.Value > CertGrid.RowCount)
                {
                    MessageBox.Show("Количество SN в лоте не соответсвтует Кол-ву Cert", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           

            var confirm = new ConfimUser();
            var r = confirm.ShowDialog();
            if (r != DialogResult.OK)
            {
                MessageBox.Show("Ошибка доступа или неверный пароль"); return;
            }

            var lotcode = short.Parse(GridAddLot[1, 0].Value.ToString());
            var LotID = new FASEntities().FAS_GS_LOTs.Where(c => c.LOTCode == lotcode).Select(c => c.LOTID).FirstOrDefault();
            if (LotID == 0)
            {
                 LotID = SaveGSLot(confirm.UserID);            
            
                if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true & bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
                    AddSerialNumberHDCPCert(LotID);
                else if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true & bool.Parse(GridAddLot[1, 10].Value.ToString()) == false)
                    AddSerialNumberCert(LotID);
                else if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == false & bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
                    AddSerialNumberHDCP(LotID);
                else
                    AddSerialNumber(LotID);
            }
            else
            {

                var hdcp = new FASEntities().FAS_GS_LOTs.Where(c => c.LOTID == LotID).Select(c => new { hdcp = (bool)c.IsHDCPUpload, cert = (bool)c.IsCertUpload }).FirstOrDefault();
                if (hdcp.hdcp & hdcp.cert)               
                    AddSerialNumberHDCPCert(LotID);
                else if (hdcp.hdcp & !hdcp.cert)
                     AddSerialNumberHDCP(LotID);
                else if (!hdcp.hdcp & hdcp.cert)
                    AddSerialNumberCert(LotID);
                else
                    AddSerialNumber(LotID);

            }

            OpenAdd();
            MessageBox.Show("Завершено!");       
        }

        void RangeMethod(int snIn,int lotid)
        {
            if (snIn <= 15000) //Если в лоте меньше или равно 15000 номеров
            {
                using (var fas = new FASEntities())
                {
                    var st = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
                    var end = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();

                    var range = new FAS_Fixed_RG()
                    {
                        LotID = lotid,
                        LitIndex = 1,
                        LabDate = DTPic.Value,
                        RGStart = st,
                        RGEnd = end,                        
                    };

                    var gslot = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                    gslot.FirstOrDefault().RangeStart = st;
                    gslot.FirstOrDefault().RangeEnd = end;
                    gslot.FirstOrDefault().FixedRG = true;
                    gslot.FirstOrDefault().StartDate = DTPic.Value;

                    fas.FAS_Fixed_RG.Add(range);
                    fas.SaveChanges();

                }
            }
            else //Если в лоте больше номеров
            {
                using (var fas = new FASEntities())
                {
                    var st = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
                    var end = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();

                    var count = snIn / 15000;
                    var k = st;
                    for (int i = 1; i <= count; i++)
                    {
                        st = st + 15000;

                        var _range = new FAS_Fixed_RG()
                        {
                            LotID = lotid,
                            LitIndex = (short)i,

                        };
                    }          
                }
            }
        }
   

        short SaveGSLot(int UserID) // Сохранение в таблицу GSLots
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};
            using (var fas = new FASEntities())
            {
                var _fas = new FAS_GS_LOTs()
                {
                    LOTCode = short.Parse(GridAddLot[1, 0].Value.ToString()),
                    FULL_LOT_Code = GridAddLot[1, 1].Value.ToString(),
                    ModelID = modelID,
                    Specification = GridAddLot[1, 3].Value.ToString(),
                    Manufacturer = GridAddLot[1, 4].Value.ToString(),
                    Operator = GridAddLot[1, 5].Value.ToString(),
                    MarketID = GridAddLot[1, 6].Value.ToString(),
                    PTID = GridAddLot[1, 7].Value.ToString(),
                    IsActive = true,
                    BoxCapacity = int.Parse(GridAddLot[1, 8].Value.ToString()),
                    PalletCapacity = int.Parse(GridAddLot[1, 9].Value.ToString()),
                    IsHDCPUpload = bool.Parse(GridAddLot[1, 10].Value.ToString()),
                    IsCertUpload = bool.Parse(GridAddLot[1, 11].Value.ToString()),
                    IsMACUpload = bool.Parse(GridAddLot[1, 12].Value.ToString()),
                    ModelCheck = bool.Parse(GridAddLot[1, 13].Value.ToString()),
                    SWRead = bool.Parse(GridAddLot[1, 14].Value.ToString()),
                    SWGS1Read = bool.Parse(GridAddLot[1, 15].Value.ToString()),
                    GetWeight = bool.Parse(GridAddLot[1, 16].Value.ToString()),
                    LabelScenarioID = labelscenarioId,
                    WorkingScenarioID = WorkScenarioId,
                    CreateDate = DateTime.UtcNow.AddHours(2),
                    CreateByID = (short)UserID,
                    SWVersion = GridAddLot[1, 19].Value.ToString(),
                    IsBunch = bool.Parse(GridAddLot[1, 20].Value.ToString()),
                    IsWeighingPackage = bool.Parse(GridAddLot[1, 21].Value.ToString()),
                };
                fas.FAS_GS_LOTs.Add(_fas);
                fas.SaveChanges();

                return fas.FAS_GS_LOTs.OrderByDescending(c => c.LOTID).Select(c => c.LOTID).FirstOrDefault();
            }            
        }

        void OffInfo() //Режим редактирование, выключение
        {
            Option = false; this.OptionBT.Image = global::GS_STB.Properties.Resources.OFF2;
            LBOption.Text = "Редактирование данных \n OFF"; GridInfo.ReadOnly = true; UpdateBT.Visible = false; BTRange.Visible = false;
            Create.Visible = false; RBLit.Visible = false; RBRow.Visible = false;
            string _cell = GridInfo[1, 3].Value.ToString();
            GridInfo[1, 3] = new DataGridViewTextBoxCell();
            GridInfo[1, 3].Value = _cell;

            for (int i = 11; i <= 17; i++)
            {
                var TextCell = new DataGridViewTextBoxCell();
                TextCell.Value = GridInfo[1, i].Value.ToString();
                GridInfo[1, i] = TextCell;
            }

            var TextCell2 = new DataGridViewTextBoxCell();
            TextCell2.Value = GridInfo[1, 27].Value.ToString();
            GridInfo[1, 27] = TextCell2;

            var TextCell3 = new DataGridViewTextBoxCell();
            TextCell3.Value = GridInfo[1, 28].Value.ToString();
            GridInfo[1, 28] = TextCell3;

            string _cellLB = GridInfo[1, 18].Value.ToString();
            GridInfo[1, 18] = new DataGridViewTextBoxCell();
            GridInfo[1, 18].Value = _cellLB;

            string _cellLBWork = GridInfo[1, 19].Value.ToString();
            GridInfo[1, 19] = new DataGridViewTextBoxCell();
            GridInfo[1, 19].Value = _cellLBWork;
        }

        private void GridInfo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0) //Защита от редактирования 1 столбца 
                e.Cancel = true;
        }

        private void UpdateBT_Click(object sender, EventArgs e) //Update данных
        {
            short lotid = short.Parse(GridInfo[1,2].Value.ToString());

            var confirm = new ConfimUser();
            var r = confirm.ShowDialog();
            if (r != DialogResult.OK)
            {
                MessageBox.Show("Ошибка доступа или неверный пароль"); return;
            }

            using (var FAS = new FASEntities())
            {
                try
                {              
                    var _fas = FAS.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                    _fas.FirstOrDefault().LOTCode = short.Parse(GridInfo[1, 0].Value.ToString());//LotCode
                    _fas.FirstOrDefault().FULL_LOT_Code = GridInfo[1, 1].Value.ToString(); //FullLotCode
                    _fas.FirstOrDefault().ModelID = modelID;  //Model
                    _fas.FirstOrDefault().Specification = GridInfo[1, 4].Value.ToString(); // Spec
                    _fas.FirstOrDefault().Manufacturer = GridInfo[1, 5].Value.ToString(); //DTVS
                    _fas.FirstOrDefault().Operator = GridInfo[1, 6].Value.ToString(); // TRICOLOR
                    _fas.FirstOrDefault().MarketID = GridInfo[1, 7].Value.ToString(); // Market
                    _fas.FirstOrDefault().PTID = GridInfo[1, 8].Value.ToString(); //Ptid
                    _fas.FirstOrDefault().BoxCapacity = int.Parse(GridInfo[1, 9].Value.ToString()); //BOX
                    _fas.FirstOrDefault().PalletCapacity = int.Parse(GridInfo[1, 10].Value.ToString()); //Pallet
                    _fas.FirstOrDefault().IsHDCPUpload = bool.Parse(GridInfo[1, 11].Value.ToString()); //HDCP
                    _fas.FirstOrDefault().IsCertUpload = bool.Parse(GridInfo[1, 12].Value.ToString()); //Cert
                    _fas.FirstOrDefault().IsMACUpload = bool.Parse(GridInfo[1, 13].Value.ToString()); //Mac
                    _fas.FirstOrDefault().ModelCheck = bool.Parse(GridInfo[1, 14].Value.ToString()); //ModelCH
                    _fas.FirstOrDefault().SWRead = bool.Parse(GridInfo[1, 15].Value.ToString()); //SW
                    _fas.FirstOrDefault().SWGS1Read = bool.Parse(GridInfo[1, 16].Value.ToString()); //SW1
                    _fas.FirstOrDefault().GetWeight = bool.Parse(GridInfo[1, 17].Value.ToString()); //Weight
                    _fas.FirstOrDefault().LabelScenarioID = labelscenarioId; // Label
                    _fas.FirstOrDefault().WorkingScenarioID = WorkScenarioId;
                    _fas.FirstOrDefault().CreateDate = DateTime.Parse(GridInfo[1, 20].Value.ToString()); //Date
                    _fas.FirstOrDefault().SWVersion = GridInfo[1, 26].Value.ToString();
                    _fas.FirstOrDefault().CreateByID = (short)confirm.UserID;
                    _fas.FirstOrDefault().IsBunch = bool.Parse(GridInfo[1, 27].Value.ToString());
                    _fas.FirstOrDefault().IsWeighingPackage = bool.Parse(GridInfo[1, 28].Value.ToString());
                    FAS.SaveChanges();
                }
                catch (Exception b)
                {
                    MessageBox.Show($"Не пройдена валидация данных \n ---------  \n {b.ToString()}","Валидация данных не прдйена",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Сохранено!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        private void GridInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (!Option)
                return;

            DataGridView DG = sender as DataGridView;

            if (DG.CurrentCell.ColumnIndex == 1)

                if (DG.CurrentCell.RowIndex == 3 || DG.CurrentCell.RowIndex == 18 || DG.CurrentCell.RowIndex == 19)
                {
                    DG.CurrentCell = DG[1, DG.CurrentCell.RowIndex];
                    DG.BeginEdit(false);
                    var f = DG.EditingControl as DataGridViewComboBoxEditingControl;
                    f.DroppedDown = true;
                }
            if (DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value != null)
            {

                CellData = DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value.ToString(); return;
            }

            CellData = "";
        }
        private void GridAddLot_SelectionChanged(object sender, EventArgs e) //Открытие по щелчку списка у comboBox в элементе DataGrid
        {
            DataGridView DG = sender as DataGridView;           

            if (DG.CurrentCell.ColumnIndex == 1)

                if (DG.CurrentCell.RowIndex == 2 || DG.CurrentCell.RowIndex == 17 || DG.CurrentCell.RowIndex == 18)
                {
                    DG.CurrentCell = DG[1, DG.CurrentCell.RowIndex];
                    DG.BeginEdit(false);
                    var f = DG.EditingControl as DataGridViewComboBoxEditingControl;
                    if (f == null)
                    {
                        return;
                    }

                    f.DroppedDown = true;
                }
                    if (DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value != null)
                    {   
                  
                    CellData = DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value.ToString(); return;
                    }

            CellData = "";
        }
        private void GridAddLot_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};

            DataGridView DG = sender as DataGridView;
            if (DG[e.ColumnIndex, e.RowIndex].Value == null)
                return;

            _cell = DG[e.ColumnIndex, e.RowIndex].Value.ToString(); // пишем в переменную _cell, то что мы ввели   

            if (e.RowIndex == 0) //Проверка LotCode, 1 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                CheckLength(e, 3, DG); return;
            }

            if (e.RowIndex == 1) //Проверка FullLotCode, 2 строка в гриде
            { CheckLength(e, 50, DG); return; }          

            if (e.RowIndex == 2) //Проверка Model, 4 строка в гриде            
            { GetModelId(_cell); return; }

            if (e.RowIndex == 3) //Проверка Spec, 5 строка в гриде
            { CheckLength(e, 50, DG); return; }

            if (e.RowIndex == 4) //Проверка DTVS, 6 строка в гриде
            { CheckLength(e, 5, DG); return; }

            if (e.RowIndex == 5) //Проверка TRICOLOR, 7 строка в гриде
            { CheckLength(e, 10, DG); return; }

            if (e.RowIndex == 6) //Проверка Market, 8 строка в гриде
            { CheckLength(e, 15, DG); return; }

            if (e.RowIndex == 7) //Проверка Ptid, 9 строка в гриде
            { CheckLength(e, 2, DG); return; }

            if (e.RowIndex == 8 || e.RowIndex == 9) //Проверка BOX, pallet 10,11 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                CheckLength(e, 3, DG); return;
            }

            if (e.RowIndex == 17) //Проверка LabelScenario, 19 строка в гриде
            { GetWorkScenarioID(DG[1, 17].Value.ToString().Substring(0, 1)); return; }

            if (e.RowIndex == 18) //Проверка LabelScenario, 19 строка в гриде
            { GetlabelscenarioID(DG[1, 18].Value.ToString().Substring(0, 1)); return; }
          
        }

        private void GridInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e) //Событие срабатывает, когда редактирование ячейки закончено
        {
            //LotCode[0]  FullLotCode[1] LOTID[2]	Model[3] Spec[4] DTVS[5] TRICOLOR[6] Market[7] Ptid[8]
            //BOX[9] Pallet[10] HDCP[11]  Cert[12] Mac[13] ModelCH[14] SW[15] SW1[16] Weight[17]
            //Label[18] Date[19]	User st  end range stDate    
            DataGridView DG = sender as DataGridView;
            if (DG[e.ColumnIndex, e.RowIndex].Value == null)
                return;

            _cell = DG[e.ColumnIndex, e.RowIndex].Value.ToString(); // пишем в переменную _cell, то что мы ввели   

            if (e.RowIndex == 0) //Проверка LotCode, 1 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                                
                CheckLength(e, 3, DG);  return;        
            }

            if (e.RowIndex == 1) //Проверка FullLotCode, 2 строка в гриде
            {  CheckLength(e, 50, DG); return; }

            if (e.RowIndex == 2 || e.RowIndex == 20 || e.RowIndex == 21 || e.RowIndex == 22 || e.RowIndex == 23 || e.RowIndex == 24)
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; return;}

            if (e.RowIndex == 3) //Проверка Model, 4 строка в гриде            
            { GetModelId(_cell); return; }
           
            if (e.RowIndex == 4) //Проверка Spec, 5 строка в гриде
            { CheckLength(e, 50, DG); return;}

            if (e.RowIndex == 5) //Проверка DTVS, 6 строка в гриде
            {  CheckLength(e, 5, DG); return; }

            if (e.RowIndex == 6) //Проверка TRICOLOR, 7 строка в гриде
            { CheckLength(e, 10, DG); return; }

            if (e.RowIndex == 7) //Проверка Market, 8 строка в гриде
            { CheckLength(e, 15, DG); return; }

            if (e.RowIndex == 8) //Проверка Ptid, 9 строка в гриде
            { CheckLength(e, 2, DG); return; }

            
            if (e.RowIndex == 9 || e.RowIndex == 10) //Проверка BOX, pallet 10,11 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                 CheckLength(e, 3, DG); return; }

            if (e.RowIndex == 18) //Проверка LabelScenario, 19 строка в гриде
            {
                if (DG[1, 18].Value != null)
                    if (DG[1, 18].Value.ToString().Length < 2)
                        return;

                GetlabelscenarioID(DG[1,18].Value.ToString().Substring(0,1)); return; 
            
            }

            if (e.RowIndex == 19) //Проверка LabelScenario, 19 строка в гриде
            {
                if (DG[1,19].Value != null)              
                    if (DG[1, 19].Value.ToString().Length < 2)                   
                        return;
                
                GetWorkScenarioID(DG[1, 19].Value.ToString().Substring(0, 1)); return; 
            }

            if (e.RowIndex == 20) //Проверка Date, 20 строка в гриде
            {
                if (DG.Name == "GridInfo")
                {
                    ChecDate(e, DG); return;
                }
                
            }
        }
        void ChecDate(DataGridViewCellEventArgs e, DataGridView DG)
        {
            if (!DateTime.TryParse(_cell, out DateTime k))
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M("Не верный формат вводных данных, ввести разрешено только Дату"); return; }
        }

        bool ChecInt(DataGridViewCellEventArgs e, DataGridView DG)
        {
            if (!short.TryParse(_cell, out short k))
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M("Не верный формат вводных данных, ввести разрешено только число"); return false; }
            return true;
        }

        void CheckLength(DataGridViewCellEventArgs e, int L, DataGridView DG)
        {
            if (_cell.Length > L)
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M($"Для вводных данных максимум {L} символов \nВы Ввели {_cell.Length} символа!"); return; }
            DG[e.ColumnIndex, e.RowIndex].Value = _cell;
        }

       

        private void BT_LOT_Info_Click(object sender, EventArgs e) //Информация о лоте
        {
            if (GridLot.CurrentRow.Index == -1)
                return;

            this.OptionBT.Image = global::GS_STB.Properties.Resources.OFF2;
            CellData = "";
            Option = false;
            LBOption.Text = "Редактирование данных \n OFF";
            UpdateBT.Visible = false;
            GridRange.Visible = false;
            BTRange.Visible = false;

            ShowGR(LotsGR, false);
            ShowGR(LotManagment, true);
            var lotid = short.Parse(GridLot[6, GridLot.CurrentRow.Index].Value.ToString());
            var list = GetLotInfo(lotid);
            GetLotRange(GridRange,lotid);
            GridInfo.RowCount = ListName.Count;
            GridInfo[1, 3] = new DataGridViewTextBoxCell();

            for (int i = 0; i <= GridInfo.RowCount-1; i++)
            {
                GridInfo[0, i].Value = ListName[i];
                GridInfo[1, i].Value = list[i];
            }
        }

        private void BackButtonInfoLot_Click(object sender, EventArgs e) //Возврат в лот
        {
            OffInfo();
            GetLot(GridLot);
            ShowGR(LotsGR, true);
            ShowGR(LotManagment, false);      
        }

        private void BackButtonAddLot_Click(object sender, EventArgs e)
        {
            GetLot(GridLot);
            ShowGR(LotsGR, true);
            ShowGR(AddLotGR, false);
        }

        void ShowGR(System.Windows.Forms.GroupBox GB, bool B)
        {
            GB.Visible = B;
            GB.Location = new System.Drawing.Point(10,10);
            GB.Size = new Size(1400, 689);
        }

        ArrayList GetLotInfo( short LotID)
        {
            var ArrayList = new ArrayList();
            using (FASEntities FAS = new FASEntities())
            {
               
                //var data = from lot in FAS.FAS_GS_LOTs
                //           where lot.LOTID == LotID
                //           join model in FAS.FAS_Models on lot.ModelID equals model.ModelID
                //           join lb in FAS.FAS_LabelScenario on lot.LabelScenarioID equals lb.ID
                //           join user in FAS.FAS_Users on lot.CreateByID equals user.UserID
                //           join work in FAS.FAS_WorkingScenario on lot.WorkingScenarioID equals work.ID
                //           select new
                //           {
                //               LotCode      = lot.LOTCode,
                //               FullLotCode  = lot.FULL_LOT_Code,
                //               LOTID        = lot.LOTID,
                //               Model        = model.ModelName,
                //               Spec         = lot.Specification,
                //               DTVS         = lot.Manufacturer,
                //               TRICOLOR     = lot.Operator,
                //               Market       = lot.MarketID,
                //               Ptid         = lot.PTID,
                //               BOX          = lot.BoxCapacity,
                //               Pallet       = lot.PalletCapacity,
                //               HDCP         = lot.IsHDCPUpload,
                //               Cert         = lot.IsCertUpload,
                //               Mac          = lot.IsMACUpload,
                //               ModelCH      = lot.ModelCheck,
                //               SW           = lot.SWRead,
                //               SW1          = lot.SWGS1Read,
                //               Weight       = lot.GetWeight,
                //               Label        = lb.Scenario + " | " + lb.Description,
                //               WorkScenario = work.Scenario + " | " + work.Description,
                //               Date         = lot.CreateDate,
                //               User         = user.UserName,
                //               st           = lot.RangeStart,
                //               end          = lot.RangeEnd,
                //               range        = lot.FixedRG,
                //               stDate       = lot.StartDate,
                //               Verion       = lot.SWVersion,
                //               IsBunch      = lot.IsBunch,
                //               IsWeightPacking = lot.IsWeighingPackage
                //           };

                var listdatas = FAS.FAS_GS_LOTs.Where(c => c.LOTID == LotID).Select(b => new
                {
                    LotCode = b.LOTCode,
                    FullLotCode = b.FULL_LOT_Code,
                    LOTID = b.LOTID,
                    Model = b.FAS_Models.ModelName,
                    Spec = b.Specification,
                    DTVS = b.Manufacturer,
                    TRICOLOR = b.Operator,
                    Market = b.MarketID,
                    Ptid = b.PTID,
                    BOX = b.BoxCapacity,
                    Pallet = b.PalletCapacity,
                    HDCP = b.IsHDCPUpload,
                    Cert = b.IsCertUpload,
                    Mac = b.IsMACUpload,
                    ModelCH = b.ModelCheck,
                    SW = b.SWRead,
                    SW1 = b.SWGS1Read,
                    Weight = b.GetWeight,
                    Label = b.FAS_LabelScenario.Scenario + " | " + b.FAS_LabelScenario.Description ,
                    WorkScenario = b.FAS_WorkingScenario.Scenario + " | " + b.FAS_WorkingScenario.Description,
                    Date = b.CreateDate,
                    User = b.FAS_Users.UserName,
                    st = b.RangeStart,
                    end = b.RangeEnd,
                    range = b.FixedRG,
                    stDate = b.StartDate,
                    Verion = b.SWVersion,
                    IsBunch = b.IsBunch,
                    IsWeightPacking = b.IsWeighingPackage

                }).ToList();

                try
                {
                    var report = listdatas.First().GetType().GetProperties().Select(c => c.GetValue(listdatas.First()));
                    foreach (var value in report)
                        ArrayList.Add(value);
                    return ArrayList;
                }
                catch (Exception)
                {

                    return ArrayList;
                }
            }
        }

        void GetLotRange(DataGridView Grid,int lotid)
        {
            using (FASEntities Fas = new FASEntities())
            {
                Grid.DataSource = (from fix in Fas.FAS_Fixed_RG
                                   where fix.LotID == lotid
                                  select new 
                                  { 
                                      ID = fix.id,LitIndex = fix.LitIndex, Мин_диапазон = fix.RGStart, Макс_диапазон = fix.RGEnd, ДатаЭтикетки = fix.LabDate,
                                      Кол_во_номеров = (fix.RGEnd - fix.RGStart) + 1

                                  }).ToList();
                if (Grid.RowCount != 0) 
                { 
                    Grid.Visible = true;
                    Grid.Columns[0].Visible = false;
                }
            }
        }

        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
LOTCode,FULL_LOT_Code,ModelName,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used,
LOTID, RangeStart,RangeEnd, FixedRG,StartDate
from FAS_GS_LOTs as gs
left join FAS_Models as m on gs.ModelID = m.ModelID
where IsActive = 1
order by LOTID desc ");
            //using (FASEntities FAS = new FASEntities())
            //{
            //    //Grid.DataSource = (from Lot in FAS.FAS_GS_LOTs
            //    //                   join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
            //    //                   where Lot.IsActive == true
            //    //                   orderby Lot.LOTID descending
            //    //                   select new
            //    //                   {
            //    //                       Lot = Lot.LOTCode,
            //    //                       Full_Lot = Lot.FULL_LOT_Code,
            //    //                       Model = model.ModelName,
            //    //                       InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       Used = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       LotID = Lot.LOTID,
            //    //                       Стартдиапазон = Lot.RangeStart,
            //    //                       Конецдиапазон = Lot.RangeEnd,
            //    //                       FixedRG = Lot.FixedRG,
            //    //                       StartDate = Lot.StartDate
            //    //                   }).ToArray();
            //}
        }

        

        void M(string line)
        {
            MessageBox.Show(line,"Не правильный формат данных",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        void GetModelId(string model)
        {
            using (var fas = new FASEntities())
            {
                modelID = fas.FAS_Models.Where(c => c.ModelName == model).Select(c => c.ModelID).FirstOrDefault();
            }
        }

        void GetlabelscenarioID(string Scenario)
        {
            using (var fas = new FASEntities())
            {
                labelscenarioId = fas.FAS_LabelScenario.Where(c => c.Scenario == Scenario).Select(c => c.ID).FirstOrDefault();
            }
        }

        void GetWorkScenarioID(string Scenario)
        {
            using (var fas = new FASEntities())
            {
                WorkScenarioId = fas.FAS_WorkingScenario.Where(c => c.Scenario == Scenario).Select(c => c.ID).FirstOrDefault();
            }
        }

        object LabelScenario()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_LabelScenario.Select(c => c.Scenario + " | " + c.Description).ToList();
            }
        }

        object WorkScenario()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_WorkingScenario.Select(c => c.Scenario + " | " + c.Description).ToList();
            }
        }

        object ModelList()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Models.Where(c=>c.ModelTypeID == 1).Select(c => c.ModelName).Distinct().ToList();
            }
        }

        void AddSerialNumber(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,
                    };

                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();
                }
            }
        }
        void AddSerialNumberHDCP(short LotID)
        {
            
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_HDCP = new FAS_HDCP()
                        {
                            HDCPName = HDCPGrid[0, i].Value.ToString(),
                            HDCPContent = File.ReadAllBytes(HDCPGrid[1, i].Value.ToString()),
                        },
                    };

                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }
        void AddSerialNumberCert(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_CERT = new FAS_CERT()
                        {
                            CERTName = CertGrid[0, i].Value.ToString(),
                            CertContent = File.ReadAllBytes(CertGrid[1, i].Value.ToString()),
                        }

                    };
                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }

        void AddSerialNumberHDCPCert(short LotID)
        {
            using (var fas = new FASEntities())
            {          

                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_HDCP = new FAS_HDCP()
                        {
                            HDCPName = HDCPGrid[0, i].Value.ToString(),
                            HDCPContent = File.ReadAllBytes(HDCPGrid[1, i].Value.ToString()),
                        },

                        FAS_CERT = new FAS_CERT()
                        {
                            CERTName = CertGrid[0, i].Value.ToString(),
                            CertContent = File.ReadAllBytes(CertGrid[1, i].Value.ToString()),
                        }
                      

                    };

                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetRangeForm SetRange = new SetRangeForm(int.Parse(GridInfo[1, 2].Value.ToString()), int.Parse(GridInfo[1,0].Value.ToString()));
            var Result = SetRange.ShowDialog();
            if (Result == DialogResult.Cancel)
                return; 

            GridRange.Visible = true;
            GetLotRange(GridRange, int.Parse(GridInfo[1, 2].Value.ToString()));

        }

        private void GridRange_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 4)            
                e.Cancel = true;
            
        }

        private void GridRange_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DateTime d = DateTime.MinValue;
            var r = DateTime.TryParse(GridRange[e.ColumnIndex,e.RowIndex].Value.ToString(),out d);
            if (!r)
            {
                MessageBox.Show("Не верный формат даты");
                return;
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (GridRange == null)
                return;

            if (GridRange.RowCount <= 0 )
                return;

            if (GridRange.CurrentCell.RowIndex ==  -1)            
                return;

            GridExcelReport.Rows.Clear();

            if (RBLit.Checked) //Строка
            {
                string index = GridRange[1, GridRange.CurrentCell.RowIndex].Value.ToString();

                for (int i = 0; i < GridRange.RowCount; i++)              
                    if (GridRange[1, i].Value.ToString() == index)
                    {
                        int st = int.Parse(GridRange[2, i].Value.ToString());
                        int end = int.Parse(GridRange[3, i].Value.ToString());
                        DateTime Date = DateTime.Parse(GridRange[4, i].Value.ToString());
                        ReportExcel(st, end, Date);
                    }                

            }
            else if (RBRow.Checked)
            {
                int st = int.Parse(GridRange[2, GridRange.CurrentCell.RowIndex].Value.ToString());
                int end = int.Parse(GridRange[3, GridRange.CurrentCell.RowIndex].Value.ToString());
                DateTime Date = DateTime.Parse(GridRange[4, GridRange.CurrentCell.RowIndex].Value.ToString());
                ReportExcel(st, end, Date);
            }

            OpenExcel();
            MessageBox.Show("Готово!");
        }

        void OpenExcel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application(); //Создаем объект Excel
            ExcelApp.Application.Workbooks.Add(Type.Missing); //Добавляет в Excel Лист
            Range cells = ExcelApp.Columns["D:D"];
            cells.NumberFormat = "@";

            for (int i = 0; i < GridExcelReport.ColumnCount; i++)        //Добавляет заголовки     
                ExcelApp.Cells[1, i + 1] = GridExcelReport.Columns[i].HeaderText;

            for (int i = 0; i < GridExcelReport.RowCount; i++) //Добавляем таблицу           
                for (int k = 0; k < GridExcelReport.ColumnCount; k++)
                    ExcelApp.Cells[i + 1, k + 1] = GridExcelReport[k, i].Value;

            ExcelApp.Visible = true;
            GC.Collect();
            ExcelApp = null;
        }

        void ReportExcel(int st, int end, DateTime d)
        {

            for (int i = st; i <= end; i++)
            {
                var date = d.ToString("ddMMyyyy");
                var FullSTBSN = GenerateFullSTBSN(i, date);
                GridExcelReport.Rows.Add(GridExcelReport.RowCount, i, d.ToString("dd.MM.yyyy"), FullSTBSN);
            }
        }

        string GenerateFullSTBSN(int serialnumber, string ProdDate) //Генерация Полного Серийного номера
        {
            string FullSTBSN;
            string LotCode = GridInfo[1, 0].Value.ToString();
            int _serNumber = serialnumber;
            var FullSTBSN_Arr = "0" + ProdDate + "01" + LotCode + _serNumber;
            var D = StringToIntArray(FullSTBSN_Arr);

            var result = (D[0] * 1 + D[1] * 2 + D[2] * 3 + D[3] * 4 + D[4] * 5 + D[5] * 6 + D[6] * 7 + D[7] * 8 + D[8] * 9 + D[9] * 10 +
            D[10] * 1 + D[11] * 2 + D[12] * 3 + D[13] * 4 + D[14] * 5 + D[15] * 6 + D[16] * 7 + D[17] * 8 + D[18] * 9 + D[19] * 10 +
            D[20] * 1 + D[21] * 2);

            var result2 = (D[0] * 3 + D[1] * 4 + D[2] * 5 + D[3] * 6 + D[4] * 7 + D[5] * 8 + D[6] * 9 + D[7] * 10 + D[8] * 1 + D[9] * 2 +
            D[10] * 3 + D[11] * 4 + D[12] * 5 + D[13] * 6 + D[14] * 7 + D[15] * 8 + D[16] * 9 + D[17] * 10 + D[18] * 1 + D[19] * 2 +
            D[20] * 3 + D[21] * 4);

            var r1 = result % 11;
            var r2 = result2 % 11;

            if (r1 == 10)
                if (r2 == 10)
                    FullSTBSN = "0" + FullSTBSN_Arr;
                else
                    FullSTBSN = r2.ToString() + FullSTBSN_Arr;
            else
                FullSTBSN = r1.ToString() + FullSTBSN_Arr;

            return FullSTBSN;


            List<int> StringToIntArray(string raw)
            {
                var d = new List<int>();

                for (int i = 0; i < raw.Length; i++)
                    d.Add(int.Parse(raw.Substring(i, 1)));

                return d;
            }
        }

        private void AddBTSerialNumber_Click(object sender, EventArgs e)
        {
            if (GridLot.CurrentRow.Index == -1)
                return;


            ShowGR(LotsGR, false);
            ShowGR(AddLotGR, true);
            OpenAdd();

            SizeLot();

        }
    }
}
