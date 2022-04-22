using GS_STB.Forms_Modules;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    
    class FAS_END: BaseClass
    {
        public string Liter;        
        List<short> Arraylpac = new List<short>();      
        string FullSTBSN;
        int ShortSN;        
        byte literID;
        Label Controllabel = new Label();
        Label ControllabelFASEND = new Label();
        public FAS_END()
        {
            ListHeader = new List<string>() { "№", "Serial", "Litera","GroupBox","Pallet","ScanDate" };
            IDApp = 2;
        }

        void GetDataPac()
        {
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Grid.RowCount = 0;
            if (!CheckCounter()) //Проверка на первый запуск по лоту и линии
  /* 1 запуск*/  { AddPacCounter(); Arraylpac = GetpackingCounter(); BoxNum.Text = "1"; PalletNum.Text = "1"; NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString(); }
            else
            {
                // pac.PalletCounter[0], pac.BoxCounter[1], pac.UnitCounter[2]
                Arraylpac = GetpackingCounter();
                PalletNum.Text = Arraylpac[0].ToString();
                BoxNum.Text = Arraylpac[1].ToString();
                NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString();

                if (Arraylpac[2].ToString() != ArrayList[7].ToString())
                {
                    var grid = GetDatePac(short.Parse(PalletNum.Text), short.Parse(BoxNum.Text), literID);
                    Grid.RowCount = grid.Count();
                    for (int i = 0; i < Grid.RowCount; i++)
                    {
                        Grid[0, i].Value = grid[i].UnitNum;
                        Grid[1, i].Value = grid[i].SerialNumber;
                        Grid[2, i].Value = grid[i].Littera;
                        Grid[3, i].Value = grid[i].BoxNum;
                        Grid[4, i].Value = grid[i].PalletNum;
                        Grid[5, i].Value = grid[i].PackingDate;
                    }
                }
            }
        }
      
        public override void LoadWorkForm()
        {            
             var FUG = control.Controls.Find("FAS_EndGB", true).FirstOrDefault();            
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            GroupBox GBWeight = control.Controls.Find("GBWeight", true).OfType<GroupBox>().FirstOrDefault();
            Label MinWeightLB = control.Controls.Find("MinWeightLB", true).OfType<Label>().FirstOrDefault();
            Label MaxWeightLB = control.Controls.Find("MaxWeightLB", true).OfType<Label>().FirstOrDefault();
            Label WeightLB = control.Controls.Find("WeightLB", true).OfType<Label>().FirstOrDefault();

            if (isWeightPacking)
            {
                GBWeight.Visible = true;
                GBWeight.Location = new Point(998, 407);
                MinWeightLB.Text += $": {this.Weight - this.Min}"; MaxWeightLB.Text += $": {this.Max+this.Weight}"; WeightLB.Text += $": {this.Weight}";
            }

            Liter = GetLiter();
            literID = GetLiterID();
            ArrayList[6] = Liter + " " + LitIndex;

            FUG.Visible = true;
            FUG.Location = new Point(179, 11);
            FUG.Size = new Size(536, 209);

            GetDataPac();    

            ShiftCounterStart();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();

        }
        public override void KeyDownMethod()
        {
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            ControllabelFASEND = control.Controls.Find("FASENDLB", true).OfType<Label>().FirstOrDefault();
            int _weight = 0;

            Controllabel.Text = "";
            ControllabelFASEND.Text = "";

            if (TB.TextLength != 23)
            { LabelStatus(Controllabel, "Не верный номер", Color.Red,TB); Grid.BackgroundColor = Color.Red; return; }
            FullSTBSN = TB.Text;

            if (!int.TryParse(TB.Text.Substring(15), out int k))
            { LabelStatus(Controllabel, $"Неверный формат номера {TB.Text}", Color.Red,TB); return; }
            ShortSN = k; //Если удачно, то преобразуем ShortSN

            PCBID = GetPCBID();

            var ct_op_Result = CheckCt_OperLog();
            if (ct_op_Result != "true")
            { LabelStatus(Controllabel, $"{TB.Text}  {ct_op_Result}", Color.Red,TB); return; }

            if (CheckMethods(TB)) //Проверка Сер. Ном. в таблицах и проверка диапазона
            { Grid.BackgroundColor = Color.Red;  return; }

            
            if (!isWeightPacking) //Не Совместная упаковка с весами
            {
                if (!CheckLot()) // Временное условие
                {
                    if (CheckSN(TB))//Проверерка флажков
                    { Grid.BackgroundColor = Color.Red; return; }
                }
            }
            else
            {
                if (CheckSNWeight(TB)) //Проверерка флажков
                {
                    { Grid.BackgroundColor = Color.Red; return; }
                }

                FAS_Weight_Control Weight = new FAS_Weight_Control(Min,Max, this.Weight);
                var Result = Weight.ControlWeight();
                if (!Result)
                {
                    LabelStatus(Controllabel, Weight.Message, Color.Red,TB); Grid.BackgroundColor = Color.Red; return;
                }
                _weight = Weight.Weight;
            }           
            
            WriteToDB(_weight);
            return;
        }
        void SetSerialNumber()
        {
            using (var fas = new FASEntities())
            {
                //var _fas = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                //_fas.FirstOrDefault().IsPacked = true;
                //fas.SaveChanges();
            }
        }
        void UpdateCounter()
        {
            using (var fas = new FASEntities())
            {
                var _fas = fas.FAS_PackingCounter.Where(c => c.LineID == LineID && c.LOTID == LOTID && c.LitIndex == LitIndex) ;
                _fas.FirstOrDefault().PalletCounter = Arraylpac[0];
                _fas.FirstOrDefault().BoxCounter = Arraylpac[1];
                _fas.FirstOrDefault().UnitCounter = Arraylpac[2];
                fas.SaveChanges();
            }
        }

        public string CheckCt_OperLog()
        {
            using (var fas = new FASEntities())
            {
                var Result = fas.Ct_OperLog.OrderByDescending(c => c.StepDate).Where(c => c.PCBID == PCBID).Select(c => c.StepID).FirstOrDefault();
                var TestResult = fas.Ct_OperLog.OrderByDescending(c => c.StepDate).Where(c => c.PCBID == PCBID).Select(c => c.TestResultID).FirstOrDefault();
                
                if (Result == 4) //Ремонта
                {
                    if (TestResult == 4)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла в ремонте и имеет статус 'Подтверждение списание от ОТК'";
                    if (TestResult == 3)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла в ремонте и имеет статус Fail!";
                    if (TestResult == 2)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла в ремонте и имеет статус Pass!";
                }
                else if (Result == 31) //Scrap
                {
                    if (TestResult == 3)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПрошла Забракована в таблице системы Scrap";
                    if (TestResult == 2)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата Выведена как Pass в таблице системы Scrap";
                }
                else if (Result == 34)
                    return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла Desassembly_STB";

                return "true";

            }
        }


        void AddPackingGS()
        {
            using (var fas = new FASEntities())
            {
                var _fas = new FAS_PackingGS
                {
                    SerialNumber = ShortSN,
                    LiterID = literID,
                    LiterIndex = LitIndex,
                    PalletNum = Arraylpac[0],
                    BoxNum = Arraylpac[1],
                    LOTID = (short)LOTID,
                    UnitNum = Arraylpac[2],
                    PackingDate = DateTime.UtcNow.AddHours(2),
                    PackingByID = (short)UserID
                };

                var _fas1 = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                _fas1.FirstOrDefault().IsPacked = true;               

                fas.FAS_PackingGS.Add(_fas);
                fas.SaveChanges();
            }
        }

        bool CheckTableWeight()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_WeightStation.Where(c => c.SerialNumber == ShortSN).Select(c => c.SerialNumber == c.SerialNumber).FirstOrDefault();
            }
        }

        void AddPackingGSWeight(int Weight)
        {            
            using (var fas = new FASEntities())
            {
                FAS_WeightStation _fasW = new FAS_WeightStation();
                var _fas = new FAS_PackingGS
                {
                    SerialNumber = ShortSN,
                    LiterID = literID,
                    LiterIndex = LitIndex,
                    PalletNum = Arraylpac[0],
                    BoxNum = Arraylpac[1],
                    LOTID = (short)LOTID,
                    UnitNum = Arraylpac[2],
                    PackingDate = DateTime.UtcNow.AddHours(2),
                    PackingByID = (short)UserID
                };

                if (!CheckTableWeight())
                {
                    _fasW.SerialNumber = ShortSN;
                    _fasW.STBWeight = (short)Weight;
                    _fasW.WeightByID = (short)UserID;
                    _fasW.WeightDate = DateTime.UtcNow.AddHours(2);
                    fas.FAS_WeightStation.Add(_fasW); 
                }
                else
                {
                    var RR = fas.FAS_WeightStation.Where(c => c.SerialNumber == ShortSN);
                    RR.FirstOrDefault().WeightDate = DateTime.UtcNow.AddHours(2);
                    RR.FirstOrDefault().STBWeight = (short)Weight;
                    RR.FirstOrDefault().WeightByID = (short)UserID;
                }

                var _fas1 = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                _fas1.FirstOrDefault().IsPacked = true; _fas1.FirstOrDefault().IsWeighted = true;

                fas.FAS_PackingGS.Add(_fas);
                
                fas.SaveChanges();
            }
        }
        public override void GetComponentClass()
        {           
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
            GetLot(Grid);
        }
        void WriteToDB(int _weight)
        {   // pac.PalletCounter[0], pac.BoxCounter[1], pac.UnitCounter[2]
            Label LBPN = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();

            //Arraylpac
            //pac.PalletCounter [0], pac.BoxCounter[1], pac.UnitCounter[2]

            //если юнит каунтер = емкости коробки, то очищаем грид коробки и увеличиваем счетчик на 1
            if (Arraylpac[2].ToString() == ArrayList[7].ToString()) 
            {
                //if (Grid.RowCount  != Arraylpac[2])
                //{
                //    LabelStatus(Controllabel, $"Порядковый номер упакованного приемника {Arraylpac[2]}\nне соответствует количеству отсканированных приемников в коробке {Grid.RowCount}\n Вызовите технолога! Сбился счётчик!", Color.Red,TBSN); return;
                //}

                Grid.RowCount = 0;                
                //Если текущая коробка кратна кол-во коробок в паллете, то паллет + 1
/*PalletCouner*/Arraylpac[0] = Arraylpac[1] % int.Parse(ArrayList[8].ToString()) == 0 ? (short)(Arraylpac[0] + 1) : Arraylpac[0];
                LBPN.Text = Arraylpac[0].ToString();

 /*BoxCounter*/ Arraylpac[1] += 1;
                BoxNum.Text = Arraylpac[1].ToString();

                NextBoxNum.Text = (Arraylpac[1] + 1).ToString();
            }
            Arraylpac[2] = (short)(Grid.RowCount + 1);
            // "№", "Serial", "Litera","GroupBox","Pallet","ScanDate"    
            if (!isWeightPacking)
                AddPackingGS();
            else
                AddPackingGSWeight(_weight);
            
            
            UpdateCounter();
           
            AddCt_OperLog(35);
            AddToOperLogFASEND(FullSTBSN, PCBID);
            ShiftCounter += 1;
            LotCounter += 1;
            ShiftCounterUpdate();
            LotCounterUpdate();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();

            Grid.Rows.Add(Arraylpac[2], FullSTBSN, ArrayList[6], Arraylpac[1], Arraylpac[0], DateTime.UtcNow.AddHours(2));
            Grid.Sort(Grid.Columns[0], System.ComponentModel.ListSortDirection.Descending);

            //SetFullBox(Arraylpac[0].ToString(), Arraylpac[1].ToString(),Grid.RowCount, int.Parse(ArrayList[7].ToString()));

            Grid.BackgroundColor = Color.Green;
            LabelStatus(Controllabel, "Приемник успешно добавлен!", Color.Green);
            ControllabelFASEND.Text = "";
        }

        //NoInLot = Не найден номер в лоте(Работа без диапазона)
        //NoInRange = Не найден свободный номер в лоте (Работа без диапазона) 

        void SetFullBox(string palletNum, string BoxNum, int rowGrid, int unbox)
        {
            if (rowGrid != unbox)
                return;
            
            loadgrid.SelectString($@" use fas update  [FAS].[dbo].[FAS_PackingGS]
                                     set FullBox = 1
                                     where LOTID = {LOTID} and PalletNum = {palletNum} and BoxNum = {BoxNum} and LiterID = {literID} ");
        }

        bool CheckRange(TextBox TBSN)
        {
            Link: var R = GetlabelDate();
            
            if (R == "NoInLot")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Не найден в лоте {LotCode}", Color.Red, TBSN); return false; }

            if (R == "NoInRange")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Не найден свободный номер", Color.Red, TBSN); return false; }

            //===============================Работа с диапазоном ниже
            if (R == "NotRangeLot")            
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Нет в диапазоне лота \n от {StartRangeLot} до {EndRangeLot}", Color.Red, TBSN); return false; }

            if (R == "Abort")
            { LabelStatus(ControllabelFASEND, $"Закончились серийные номера готовые к упаковке в диапазоне Лота!", Color.Red, TBSN); return false; }

            if (R == "NotRange")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Номер не соответсвует текущему индексу литеры \n{ArrayList[6]} от {StartRange} до {EndRange} ", Color.Red, TBSN); return false; }

            if (R == "New") //Открывается новый диапазон, возвращаемся к 198 строке в метод GetlabelDate()     
            {
                GetDataPac();
                goto Link;            
            }
            return true;
        }

        bool CheckTables(TextBox TBSN) //Проверка таблиц на содержание данных
        {
            var list = CheckTableData(ShortSN); //Выгружаем три переменные Bool в массив [0] - FAS_Start; [1] - Upload; [2] - Packing

            if (list[0] == false) //Проверка FASStart
            {
                LabelStatus(Controllabel, $"{FullSTBSN} - Не найден в таблице Fas_Start", Color.Red, TBSN); return false;
            }

            if (list[1] == false) //Проверка Upload
            {
                if (!CheckLot())
                {
                    LabelStatus(Controllabel, $"{FullSTBSN} - Не найден в таблице Upload", Color.Red, TBSN); return false;
                }
              
            }

            if (list[2] == true) //Проверка Packing
            {
                var listpac = GetInfoPacing(ShortSN);
                LabelStatus(Controllabel, $" {FullSTBSN} Приемник был упакован \n Литтер {listpac[1]}, Паллет {listpac[2]}, Коробка {listpac[3]} \n номер {listpac[4]} дата упаковки {listpac[5]}, упаковщик  {listpac[6]}", Color.Red, TBSN); return false;
            }

            return true;
        }

        bool CheckMethods(TextBox TBSN) //Делаем две проверки, если какая то проверка не прошла, вылет из программы
        {
            var resultL = new List<bool>() { CheckRange(TBSN), CheckTables(TBSN)};
            foreach (var item in resultL)
            {
                if (item == false)
                    return true;
            }
            return false;
        }
       
        ArrayList GetSerialNum(int shortSN)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from S in Fas.FAS_SerialNumbers
                            join F in Fas.FAS_Start on S.SerialNumber equals F.SerialNumber
                            where LOTID == LOTID & S.SerialNumber == shortSN
                            select new { S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair, F.PCBID }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;
            }
        }

        ArrayList GetInfoPac(int serial)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from pac in Fas.FAS_PackingGS
                            join lit in Fas.FAS_Liter on pac.LiterID equals lit.ID
                            join Use in Fas.FAS_Users on pac.PackingByID equals Use.UserID
                            where pac.SerialNumber == serial
                            select new { Liter = lit.LiterName + pac.LiterIndex, pac.PalletNum, pac.BoxNum, pac.UnitNum, pac.PackingDate, Use.UserName });

                if (list.Count() == 0)
                    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;
            }
        }

        int GetPCBID()
        {
            using (var fas = new FASEntities())
            {
                var sernumber = int.Parse(FullSTBSN.Substring(15));
                return fas.FAS_Start.Where(c=>c.SerialNumber == sernumber).Select(c=>c.PCBID).FirstOrDefault();
            }
        }

        bool CheckSN(TextBox TBSN)
        {
            var list = GetSerialNum(ShortSN);
            var L = list.GetRange(0, 5).OfType<bool>().ToList();
            // S.IsUsed[0], S.IsActive[1], S.IsUploaded[2], S.IsWeighted[3], S.IsPacked[4], S.InRepair[5], F.PCBID[6]

            //used 1, active 1, uploaded 1, weighted 1, packed 0

            if (L[0] == true & L[1] == true & L[2] == true & L[3] == true & L[4] == false) //Успешно пройдены проверки 
                return false;   //TODO Изменить проверку        

            //used 1, active 1, uploaded 1, weighted 1, packed 1 
            if (L[0] == true & L[1] == true & L[2] == true & L[3] == true & L[4] == true) //Приемник уже упакован
            {
                var info = GetInfoPac(ShortSN);
                if (info.Count == 0)
                    { LabelStatus(Controllabel, "Этот номер по таблице SerialNumber упакован \n Но в таблице упаковки номера нет \n Обратитесь к технологу", Color.Red, TBSN); return true;}

                 LabelStatus(Controllabel, $"номер {info[4].ToString()} уже упакован /n Литер {info[0].ToString()}, Паллет {info[1].ToString()} /n Групповая {info[2].ToString()} /n Дата упаковки {info[5].ToString()} Упакован: {info[6].ToString()}  ", Color.Red, TBSN); return true; 
            }

            //used 1, active 1, uploaded 0, weighted 0, packed 0
            if (L[0] == true & L[1] == true & L[2] == false & L[4] == false) //Не прошит приемник
            {
                if (!CheckLot())
                {
                    LabelStatus(Controllabel, $"{ShortSN} не прошит в приемник", Color.Red, TBSN); return true;
                }
            }

            //used 1, active 1, uploaded 1, weighted 0, packed 0
            if (L[0] == true & L[1] == true & L[2] == true || L[2] == false & L[3] == false & L[4] == false) //Не прошел весовой контроль
            { //TODO Нужно будет убрать проверку на весовой контроль
                if (GetWeightCheck()) //Если проверка есть 
                {
                    LabelStatus(Controllabel, $"{ShortSN} не прошел весовой контроль", Color.Red, TBSN); return true;
                }
                return false;
            }          

            LabelStatus(Controllabel, "Произошла ошибка с проверкой SerialNumbers - обратитесь к Технологу", Color.Red, TBSN);
            return true;
        }

        bool CheckSNWeight(TextBox TBSN)
        {
            var list = GetSerialNum(ShortSN);
            var L = list.GetRange(0, 5).OfType<bool>().ToList();
            // S.IsUsed[0], S.IsActive[1], S.IsUploaded[2], S.IsWeighted[3], S.IsPacked[4], S.InRepair[5], F.PCBID[6]

            //used 1, active 1, uploaded 1, weighted 1, packed 0

            if (L[0] == true & L[1] == true & L[2] == true &  L[4] == false) //Успешно пройдены проверки 
                return false;   //TODO Изменить проверку        

            //used 1, active 1, uploaded 1, weighted 1, packed 1 
            if (L[0] == true & L[1] == true & L[2] == true &  L[4] == true) //Приемник уже упакован
            {
                var info = GetInfoPac(ShortSN);
                if (info.Count == 0)
                { LabelStatus(Controllabel, "Этот номер по таблице SerialNumber упакован \n Но в таблице упаковки номера нет \n Обратитесь к технологу", Color.Red,TBSN); return true; }

                LabelStatus(Controllabel, $"номер {info[4].ToString()} уже упакован /n Литер {info[0].ToString()}, Паллет {info[1].ToString()} /n Групповая {info[2].ToString()} /n Дата упаковки {info[5].ToString()} Упакован: {info[6].ToString()}  ", Color.Red,TBSN); return true;
            }

            //used 1, active 1, uploaded 0, weighted 0, packed 0
            if (L[0] == true & L[1] == true & L[2] == false & L[4] == false) //Не прошит приемник
            {               
                LabelStatus(Controllabel, $"{ShortSN} не прошит в приемник", Color.Red,TBSN); return true;                
            }          

            LabelStatus(Controllabel, "Произошла ошибка с проверкой SerialNumbers - обратитесь к Технологу", Color.Red,TBSN);
            return true;
        }
        List<bool> CheckTableData(int ShortSN) //Проверка флажков
        {
            using (var FAS = new FASEntities())
            {
                var List = new List<bool>();
                var list = (from ST in FAS.FAS_Start                            
                            from UP in FAS.FAS_Upload.Where(c=>c.SerialNumber ==ST.SerialNumber).DefaultIfEmpty()
                            from Pac in FAS.FAS_PackingGS.Where(c=>c.SerialNumber ==ST.SerialNumber).DefaultIfEmpty()
                            
                            where ST.SerialNumber == ShortSN
                            select new
                            {
                               ST = !ST.SerialNumber.Equals(null),
                               UP = !UP.SerialNumber.Equals(null),
                               Pac = !Pac.SerialNumber.Equals(null),                                                           
                            });
              
                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report.OfType<bool>())
                    List.Add(value);
                return List;
            }
        }

        string GetLiter()
        {
            using (var Fas = new FASEntities())
            {
                var linename = ArrayList[2].ToString();
                return Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.LiterName).FirstOrDefault();
            }
        }

        byte GetLiterID()
        {
            using (var Fas = new FASEntities())
            {
                var linename = ArrayList[2].ToString();
                return (byte)Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.ID).FirstOrDefault();
            }
        }
       List<GridInfo> GetDatePac(short palletNum, short BoxNum,byte literID)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                
                 var list = (from pac in Fas.FAS_PackingGS
                            join st in Fas.FAS_Start on pac.SerialNumber equals st.SerialNumber
                            join l in Fas.FAS_Liter on pac.LiterID equals l.ID
                            where pac.PalletNum == palletNum & pac.BoxNum == BoxNum & pac.LiterID == literID  & pac.LOTID == LOTID  & pac.LiterIndex == LitIndex orderby pac.UnitNum descending
                            select new GridInfo(){UnitNum =  pac.UnitNum, SerialNumber = st.FullSTBSN,Littera = l.LiterName + " " + pac.LiterIndex,BoxNum = pac.BoxNum, PalletNum = pac.PalletNum, PackingDate = pac.PackingDate }).ToList();
                return list;                
            }
        }

        List<short> GetpackingCounter()
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new List<short>();
                var list = (from pac in Fas.FAS_PackingCounter
                            where pac.LOTID == LOTID && pac.LineID == LineID && pac.LitIndex == LitIndex
                            select new { pac.PalletCounter, pac.BoxCounter, pac.UnitCounter}).ToList();
                
                    foreach (var item in list)
                    {
                        ArrayList.Add(item.PalletCounter); ArrayList.Add(item.BoxCounter); ArrayList.Add(item.UnitCounter); break;
                    }                   
                return ArrayList;
            }
        }

        void AddPacCounter()
        {
            using (var FAS = new FASEntities())
            {
                var Pac = new FAS_PackingCounter()
                {
                    PalletCounter = 1,
                    BoxCounter = 1,
                    UnitCounter = 1,
                    LineID = (byte)LineID,
                    LOTID = (short)LOTID,
                    LitIndex = LitIndex
                    
                };
                FAS.FAS_PackingCounter.Add(Pac);
                FAS.SaveChanges();
            }
        }

        bool CheckCounter()
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_PackingCounter.Where(c => c.LOTID == LOTID & c.LineID == LineID & c.LitIndex == LitIndex).Select(c => c.ID == c.ID).FirstOrDefault();
            }
        }
        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
LOTCode,FULL_LOT_Code,ModelName, gs.LiterIndex, BoxCapacity,PalletCapacity,LOTID,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used
,RangeStart,RangeEnd,FixedRG,StartDate
from FAS_GS_LOTs as gs
left join FAS_Models as m on gs.ModelID = m.ModelID
where IsActive = 1
order by LOTID desc ");
            #region Старый код
            //using (FASEntities FAS = new FASEntities())
            //{
            //    var list = from Lot in FAS.FAS_GS_LOTs
            //               join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID                        
            //               where Lot.IsActive == true orderby LOTID descending                     
            //               select new
            //               {
            //                   Lot = Lot.LOTCode,
            //                   Full_Lot = Lot.FULL_LOT_Code,
            //                   Model = model.ModelName,   
            //                   Lot.LiterIndex,
            //                   Lot.BoxCapacity,
            //                   Lot.PalletCapacity,
            //                   Lot.LOTID,
            //                   InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Стартдиапазон = Lot.RangeStart,
            //                   Конецдиапазон = Lot.RangeEnd,
            //                   Lot.FixedRG,
            //                   Lot.StartDate

            //               };

            //    Grid.DataSource = list.ToList();

            //}
            #endregion
        }

        string GetlabelDate()
        {
            if (DateFas_Start) // Работа по диапазону
            {
                //NotRangeLot = Не попал в общий диапазон лота
                //NotRange = Не попал в диапазон Литера
                //Abort = Закончились серийные номера во всех диапазонах
                //False = Прошло успешно!           
                //New = Открывается новый диапазон
                //NoInLot = Не найден номер в лоте(Работа без диапазона)
                //NoInRange = Не найден свободный номер в лоте (Работа без диапазона) 

                var Result = GetSerialNumberRange();

                if (Result == "NotRangeLot") //Не попал в общий диапазон лота
                    return "NotRangeLot";

                if (Result == "Abort")
                    return "Abort"; //Закончились серийные номера во всех диапазонах  

                if (Result == "NotRange") //Не попал в диапазон Литера
                    return "NotRange";         

               if (Result == "New") //Если открывается новый диапазон
                   return "New";

                return "False"; //Все успешно
            }
            else //Работа не по диапазону 
            {
                var Result = GetSerialNumberNotRange();

                if (Result == "NoInLot")
                    return "NoInLot";
                if (Result == "NoInRange")
                    return "NoInRange";

                return "False";
            }
        }
        string GetSerialNumberNotRange()
        {            
            using (FASEntities FAS = new FASEntities())
            {
                //Сначала проверяем номер в лоте
                var R = FAS.FAS_SerialNumbers.Where(C => C.SerialNumber == ShortSN & C.LOTID == LOTID & C.IsUsed == true & C.IsActive == true).Select(C => C.SerialNumber == C.SerialNumber).FirstOrDefault();
                if (!R)                
                    return "NoInLot";

                //Потом проверяем в этом лоте, чтобы номер был без диапазонным
                var Re = FAS.FAS_SerialNumbers.Where(C => C.SerialNumber == ShortSN & C.LOTID == LOTID & C.FixedID == null & C.IsUsed == true & C.IsActive == true).Select(C => C.SerialNumber == C.SerialNumber).FirstOrDefault();
                if (!Re)
                    return "NoInRange";
                return "False";     
            }
        }

        string GetSerialNumberRange()        
        {
            bool result = false;
            var GridInfo = (DataGridView)control.Controls.Find("GridInfo", true).FirstOrDefault();
            for (int i = StartRangeLot; i <= EndRangeLot; i++) //Проверка общего диапазона 
                if (i == ShortSN)
                { result = true; break; }

            if (!result) //Не попал в общий диапазон
                return "NotRangeLot";
            
            using (FASEntities FAS = new FASEntities())
            {
                //Проверяем сколько осталось неупакованных номеров в диапазоне Лота, которые готовы к упаковке
                var R = FAS.FAS_SerialNumbers  
                  .Where(c => c.IsUsed == true && c.IsActive == true && c.IsPacked == false
                  && c.SerialNumber >= StartRangeLot && c.SerialNumber <= EndRangeLot).Count();

               if (R == 0) //Закончились номера в диапазоне лота для упаковки                
                   return "Abort";
               
            };

            result = false;
            for (int i = StartRange; i <= EndRange; i++) //Проверка диапазона Литера
                if (i == ShortSN)
                { result = true; break; }

            if (!result) //Не попал в Литер диапазон
                return "NotRange";

            using (FASEntities FAS = new FASEntities())
            {
                //Проверяем сколько осталось неупакованных номеров в диапазоне литера, которые готовы к упаковке
                var R = FAS.FAS_SerialNumbers  
                    .Where(c => c.IsUsed == true && c.IsActive == true && c.IsUploaded == true && c.IsWeighted == true && c.IsPacked == false
                    && c.SerialNumber >= StartRange && c.SerialNumber <= EndRange).Count();

                if (R == 0) //Закончились номера в диапазоне Литера
                {
                    FixedRange FR = new FixedRange(LOTID, this, "В базе для этого диапазона закончились серийные номера \n выберите следующий или вызовите технолога", LitIndex);
                    var Result = FR.ShowDialog();

                    if (Result == DialogResult.OK)
                    {
                        ArrayList[6] = GridInfo[1, 6].Value.ToString().First() + " " + LitIndex;
                        GridInfo[1, 6].Value = ArrayList[6];
                        return "New";
                    }
                }
            };

            //if (!result) //Не попал в Литер диапазон
            //    return "NotRange";

            return "False";

        }

        bool CheckLot()
        {
            if (LOTID == 148)
                return true;
            return false;
        
        }
        bool GetWeightCheck()
        {
            using (var fas = new FASEntities() )
            {
                var result =  fas.FAS_GS_LOTs.Where(c => c.LOTID == LOTID).Select(c => c.GetWeight).FirstOrDefault();
                if (result == null) //Проверки нет              
                    return false;

                return (bool)result;
            }
        }
    }
}
