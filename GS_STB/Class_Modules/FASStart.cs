using GS_STB.Forms_Modules;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FASStart : BaseClass
    {
        //public string PCBID { get; set; }
        public string FullSNPCB { get; set; }
        public string FullSTBSN { get; set; }

       int SerNumber;

        string FullLotCode;

        DateTime DateText;
        public FASStart()
        {
            ListHeader = new List<string>() { "№", "CH приемника", "CH платы", "Печать", "ФактДата","ДатаЭтикетки" };
            IDApp = 4;
        }

        public override void LoadWorkForm()
        {
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            var LengthCheck = control.Controls.Find("LengthCheck", true).FirstOrDefault().Visible = true;
            var FUG = control.Controls.Find("FAS_Print", true).FirstOrDefault();
            var SNPrint = control.Controls.Find("SNPRINT", true).FirstOrDefault();
            var CHPrintSN = control.Controls.Find("CHPrintSN", true).OfType<CheckBox>().FirstOrDefault();
            var PrintCheckSN = control.Controls.Find("PrintCheckSN", true).OfType<CheckBox>().FirstOrDefault(); //Перепечатка серийного номера по серийному номеру
            PrintCheckSN.Visible = true;
            var CountLBSN = control.Controls.Find("CountLBSN", true).OfType<Label>().FirstOrDefault();            
            var XSN = control.Controls.Find("XSN", true).OfType<NumericUpDown>().FirstOrDefault();
            var YSN = control.Controls.Find("YSN", true).OfType<NumericUpDown>().FirstOrDefault();
            var PrintLBName = control.Controls.Find("PrintLBName", true).OfType<Label>().FirstOrDefault();
            Label Fas_StartRange = control.Controls.Find("Fas_StartRange", true).OfType<Label>().FirstOrDefault();
            PrintLBName.Text = "Текущий принтер \n" + printName;

            var ListPrinter = control.Controls.Find("ListPrinter", true).OfType<ListBox>().FirstOrDefault();
            foreach (var item in PrinterSettings.InstalledPrinters)
                if (item.ToString().Contains("ZDesigner"))
                    ListPrinter.Items.Add(item);

            if (DateFas_Start)
            {
                Fas_StartRange.Visible = true;
                Fas_StartRange.Text = $"диапазон: Start {StartRange}, End {EndRange} \n Литер {LitIndex}";
            }
            else         
                Fas_StartRange.Visible = false;
           
            FUG.Visible = true;
            FUG.Location = new Point(264, 19);
            FUG.Size = new Size(585, 186);

            GetLineForPrint();

            if (CheckPathPrinterSettings())
                CreatePathPrinter();

            var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
            XSN.Value = int.Parse(GetPrSet(list, "XSN"));
            YSN.Value = int.Parse(GetPrSet(list, "YSN"));

            CHPrintSN.Checked = UpPrintSN;
            SNPrint.Visible = UpPrintSN;
            CountLBSN.Text = "Кол-во этикеток SN = " + labelCount.ToString();

            if (CHPrintSN.Checked)
                CHPrintSN.BackColor = Color.Green;
            else
                CHPrintSN.BackColor = Color.White;

            ShiftCounterStart();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();
        }

       
        public override void KeyDownMethod()
        {
            var LengthCheck = control.Controls.Find("LengthCheck", true).OfType<CheckBox>().FirstOrDefault();            
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            var PrintCheckSN = control.Controls.Find("PrintCheckSN", true).OfType<CheckBox>().FirstOrDefault(); //Перепечатка серийного номера по серийному номеру

            if (PrintCheckSN.Checked)                     
                if (TB.TextLength == 23)
                {
                    var Result = CheckSerialNumber(TB.Text);
                    if (Result == TB.Text)
                    {
                        Print(TB.Text, TB.Text, Controllabel); return;
                    }
                    else
                    {LabelStatus(Controllabel, $"{Result}", Color.Red); return;}
                }
                else               
                {LabelStatus(Controllabel, $"{"Не верный формат номера, включён флажок 'Печать дубликата серийного номера'! \n по серийному номеру"}", Color.Red); return; }              

          
            if (LengthCheck.Checked) //Галочка проверки на 21 символ (Когда на SMT косячат с длиной баркода)
                if (TB.TextLength != 21) //Проверка на длину в 21 символ
                { LabelStatus(Controllabel, $"{TB.Text} не верный формат номера", Color.Red); return; } //Вывод ошибки            
           
            Label ShiftCounterl = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            Label Fas_StartRange = control.Controls.Find("Fas_StartRange", true).OfType<Label>().FirstOrDefault();
            DataGridView DG_UpLog = control.Controls.Find("DG_UpLog", true).OfType<DataGridView>().FirstOrDefault();

            string decode = TB.Text;

            if (IsBunch()) //Проверка связки BOT и TOP
            {
                var result = GetPCBID(TB.Text);
                if (result == 0)
                {
                    { LabelStatus(Controllabel, $"{TB.Text} не найден в LazerBase", Color.Red); return; }
                }

                var r = GetTopDecode(result);
                if (r.Length > 22)
                {
                    { LabelStatus(Controllabel, r, Color.Red); return; }
                }
                decode = r;
            }


            var resultTHTLazer = CheckLazerPCBID(decode);
            if (resultTHTLazer != "true") //Проверка номера в базе LazerBase //Добавить THT Start
                {LabelStatus(Controllabel, resultTHTLazer, Color.Red); return;}



            var FullSN = CheckAssemlyPCBID(); //Проверка отсканированного баркода в таблице FasStart

            if (FullSN == "False") //Плата с таким баркодом была отскнирована в другом лоте
            { LabelStatus(Controllabel, $"{decode} уже был отсканирован в лоте {FullLotCode}", Color.Red); return; }

            var ct_op_Result = CheckCt_OperLog();
            if (ct_op_Result != "true")
            { LabelStatus(Controllabel, $"{decode}  {ct_op_Result}", Color.Red); return; }

            if (!string.IsNullOrEmpty(FullSN)) //Если плата с таким баркодом была отсканирована на FasStart и имеет лот такой же с каким и работает программа
            {
                if (!UpPrintSN) //Проверка печати, если печати нет, то вывод ошибки
                 { LabelStatus(Controllabel, $"{decode} уже присвоен {FullSN}", Color.Red); return; }

                Print(decode, FullSN, Controllabel);

                return; 
            }

            //=================================================================================
            //работа с диапазоном

            //False  Все прошло успешно
            //True   Ошибка
            //Abort  Закончились серийные номера во всех диапазонах
            //New    Если открывается новый диапазон

          Link: var R = GetlabelDate(); //Метод, который проверяет, есть ли диапазон, если есть, то работа идет по диапазону

            if (R == "True") //Ошибка
            { LabelStatus(Controllabel, $"Ошибка при получении Серийного номера с таблицы \n FAS_SerialNumbers", Color.Red); return; }

            if (R == "Abort") //Закончилсь номера
            { LabelStatus(Controllabel, $"Закончились серийные номера в диапазонах! Вызовите технолога", Color.Red); return; }

            if (R == "New") //Открывается новый диапазон          
                goto Link; //Снова переходим к 156 строке и проверяем новый выбранный диапазон

            //Вывод на лейбл инфо о диапазоне
            Fas_StartRange.Text = $"Дата Label - {DateText} \n диапазон: Start {StartRange}, End {EndRange} \n Литер {LitIndex}";

            if (UpPrintSN) //Принт, печатать этикетку или нет
            {
                if (string.IsNullOrEmpty(printName))
                { LabelStatus(Controllabel, $"Принтер не идентифицирован!", Color.Red); return; }
            }

                if (WriteDB(0, Controllabel)) { return; } //Операция изменение информации о серийном номере, который мы определили

                if (FullSTBSN.Length == 23)
                {
                    if (UpPrintSN) //Принт, печатать этикетку или нет
                    {
                        if (string.IsNullOrEmpty(printName))
                        { LabelStatus(Controllabel, $"Принтер не идентифицирован!", Color.Red); return; }

                        var printCodeSN = FullSTBSN.Substring(0, 22) + ">6" + FullSTBSN.Substring(22);
                        var PrintTextSN = FullSTBSN.Substring(0, 2) + " " + FullSTBSN.Substring(2, 4) + " " + FullSTBSN.Substring(6, 2) + " " + FullSTBSN.Substring(8, 2) +
                                          " " + FullSTBSN.Substring(10, 2) + " " + FullSTBSN.Substring(12, 3) + " " + FullSTBSN.Substring(15, 8);

                        if (CheckPathPrinterSettings())
                            CreatePathPrinter();

                        var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
                        var X = GetPrSet(list, "XSN");
                        var Y = GetPrSet(list, "YSN");

                        DateText = GetManufDate(FullSTBSN);
                        print(LabelSN(PrintTextSN, printCodeSN, int.Parse(X), int.Parse(Y), GetLabelScenario())); //LabelStatus(Controllabel, $"Номер платы {TB.Text} /n успешно соединен с номером приемника", Color.Green);
                    }

                    AddCt_OperLog(32);
                    AddToOperLogFasStart(FullSTBSN); //Добавляем в лог данные                    
                    ShiftCounter += + 1; //+ к выпуску
                    LotCounter += 1;
                    ShiftCounterl.Text = ShiftCounter.ToString(); //Обновляем лейбл
                    LB_LOTCounter.Text = LotCounter.ToString();
                    ShiftCounterUpdate(); //обновляем ShiftCounter
                    LotCounterUpdate();
                    DG_UpLog.Rows.Add(ShiftCounter,FullSTBSN, decode, UpPrintSN, DateTime.UtcNow.AddHours(2), DateText.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"));//Добавляем в грид строку
                    DG_UpLog.Sort(DG_UpLog.Columns[0], System.ComponentModel.ListSortDirection.Descending); //Сортировка
                    { LabelStatus(Controllabel, $"Прошло успешно!", Color.Green); return; }
                }
                else if (FullSNPCB.Length != 23)
                {
                    LabelStatus(Controllabel, $"Серийный номeр не сформирован!", Color.Red);
                    update();
                }   
        }

        //bool GetDate()
        //{
        //    var Basedate = CheckBaseDate();
        //    if (Basedate == "False")
        //        return true;

        //    if (!string.IsNullOrEmpty(Basedate))
        //    {
        //        DateText = DateTime.Parse(Basedate + " " + DateTime.Now.ToString("HH:mm:ss")); GetLineForPrint(15); return false;     
        //    }
        //    else if (this.DateFas_Start)
        //    { DateText = DateTime.UtcNow.AddHours(2); return false; }
        //    else
        //    { DateText = DateTime.Parse(DateFas_ST_Text + " " + DateTime.Now.ToString("HH:mm:ss")); return false; }
        // }

        string GetTopDecode(int BOTPCBID)
        {
            
            using (var fas = new FASEntities())
            {
                var r = fas.FAS_Bunch_Decode.Where(c => c.PCBIDBOT == BOTPCBID).Select(c => c.PCBIDTOP).FirstOrDefault();
                if (r == null)
                {
                    return $"Отсканированная плата - {BOTPCBID} не была связана с Вверхом(TOP) \nпередайте плату на станцию связки вверха(TOP) и низа платы(BOT)";
                }
                return GetDecode((int)r);
            }
           
        }

        string GetDecode(int pcbid)
        {
            using (var smd = new SMDCOMPONETSEntities())
            {
                return smd.LazerBase.Where(c => c.IDLaser == pcbid).Select(c => c.Content).FirstOrDefault();
            }
        }

        int GetPCBID(string BOT)
        {
            using (var smd = new SMDCOMPONETSEntities())
            {
                return smd.LazerBase.Where(c => c.Content == BOT).Select(c => c.IDLaser).FirstOrDefault();                
            }
        }

        void Print(string TBTEXT, string FullSN, Label Controllabel)
        {
            msg Mes = new msg($"{TBTEXT} уже присвоен \n SN номер{FullSN} \n Хотите перепечатать SN номер?",this); //Запрос на перепечатку
            var Result = Mes.ShowDialog();

            if (Result != DialogResult.OK) //Если печать отменена
            { LabelStatus(Controllabel, $"{TBTEXT} уже присвоен SN {FullSN} \n печать отменена!", Color.Black); return; }

            if (string.IsNullOrEmpty(printName)) //Проверка подклчюение принетера
            { LabelStatus(Controllabel, $"Принтер не идентифицирован!", Color.Red); return; }

            //Текстовые переменные для печати
            var printCodeSN = FullSN.Substring(0, 22) + ">6" + FullSN.Substring(22);
            var PrintTextSN = FullSN.Substring(0, 2) + " " + FullSN.Substring(2, 4) + " " + FullSN.Substring(6, 2) + " " + FullSN.Substring(8, 2) +
                              " " + FullSN.Substring(10, 2) + " " + FullSN.Substring(12, 3) + " " + FullSN.Substring(15, 8);

            if (CheckPathPrinterSettings()) //Првоерка координат принтера
                CreatePathPrinter(); //Если не найдена настройка, создаем её

            var list = Directory.GetFiles(@"C:\PrinterSettings").ToList(); //Берем с папки координаты
            var X = GetPrSet(list, "XSN"); //Определение X координаты
            var Y = GetPrSet(list, "YSN"); //Определение Y координаты
            DateText = GetManufDate(FullSN);  //Берем дату на FasStart этого номера                 
            print(LabelSN(PrintTextSN, printCodeSN, int.Parse(X), int.Parse(Y), GetLabelScenario())); //Печать номера
                                                                                  //Вывод сообщения что все хорошо завершение события
            LabelStatus(Controllabel, $"{FullSN} \n Печать готова!", Color.Green);
        }

        string CheckBaseDate()
        {
            using (var FAS = new FASEntities())
            {
                var Result = FAS.FAS_GS_LOTs.Where(c => c.LOTID == LOTID).Select(c => c.Fixed_Range).FirstOrDefault();

                if (Result != true)
                    return "";

                var ResultDate = FAS.FAS_GS_LOTs.Where(c => c.LOTID == LOTID).Select(c => c.Fixed_Range_Date).FirstOrDefault();

                if (string.IsNullOrEmpty(ResultDate.ToString()))
                    return "False";

                return FAS.FAS_GS_LOTs.Where(c => c.LOTID == LOTID).Select(c => c.Fixed_Range_Date).FirstOrDefault().Value.ToString("dd.MM.yyyy");
            }
        }
        void update()
        {
            using (FASEntities FAS = new FASEntities())
            {              
                var F = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == SerNumber);
                F.FirstOrDefault().IsActive = false;
                FAS.SaveChanges();
            }
        }      
        
        void print(string content)
        {
            RawPrinterHelper.SendStringToPrinter(printName, content); //Нужно получать ответ от принтера Для Кости 
        }

        bool WriteDB(int itter,Label label)
        {
            //SerNumber = 0;
            //if (DateFas_Start)
            //{
            //    SerNumber = GetSerialNumberRange();
            //}
            //else
            //    SerNumber = GetSerialNumber();


            updateSerNum(SerNumber); //в таблице Fas_serialNumbers номер делаем used = true и присваиваем printstationID
            Thread.Sleep(003); //Задержка 3 миллисекунды
            if (CheckSerialNumer(SerNumber) == 0) //Проверка действий выше, прверяем этот серийный номер с printStationID 
            { //Если ничего не нашли
                itter += 1; // Повторяем иттерацию
                if (itter == 4) //
                {
                    if (TempSN())
                    { LabelStatus(label, "В БАЗЕ ЗАКОНЧИЛИСЬ СЕРИЙНЫЕ НОМЕРА!", Color.Red); return true; }
                    LabelStatus(label, "Запись в базу не удалась, поробуйте еще раз!", Color.Red); return true;                 
                }
                else
                { WriteDB(itter, label); return false; }
            }
            else
            {
                InsertFasStart(SerNumber);
                FullSTBSN = GenerateFullSTBSN(SerNumber);
                if (string.IsNullOrEmpty(FullSTBSN) || FullSTBSN.Length != 23 )
                {
                    LabelStatus(label, $"FullSTBSN не с генерировался {FullSTBSN} ", Color.Red); return true;
                }
                UpdateSerialNumber(FullSTBSN, SerNumber);                
                return false;
            }          
        }

        string GetLabelScenario()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_GS_LOTs.FirstOrDefault(c => c.LOTID == LOTID).LabelScenarioID.ToString();
            }
        }

        void UpdateSerialNumber(string FullSTBSN,int serialNumer)
        {
            using (var FAS = new FASEntities())
            {
                int _serNumber = serialNumer;
                var _fas = FAS.FAS_Start.Where(c => c.SerialNumber == _serNumber);
                _fas.FirstOrDefault().FullSTBSN = FullSTBSN;
                _fas.FirstOrDefault().AssemblyByID = (short)UserID;
                FAS.SaveChanges();               
            }
        }

        string GenerateFullSTBSN(int serialnumber)
        {
            using (FASEntities f = new FASEntities())
            {
                string FullSTBSN;                
                var ProdDate = f.FAS_Start.Where(c => c.SerialNumber == serialnumber).Select(c => c.ManufDate).FirstOrDefault().ToString("ddMMyyyy");
                var FullSTBSN_Arr = "0" + ProdDate + LineForPrint + LotCode + serialnumber; 

                var D =  StringToIntArray(FullSTBSN_Arr);

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
            }
            
            List<int> StringToIntArray(string raw)
            {
                var d = new List<int>();

                for (int i = 0; i < raw.Length; i++)
                    d.Add(int.Parse(raw.Substring(i,1)));

                return d;
            }            
        }
        bool TempSN()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var  _serialNumber = FAS.FAS_SerialNumbers.Where(c => c.LOTID == LOTID & c.IsUsed == false).Select(c=>c.SerialNumber).FirstOrDefault();
                if (_serialNumber == 0)
                    return true;
                return false;
            }
        }

        public string CheckCt_OperLog()
        {
            using (var fas = new FASEntities())
            {
                var list = fas.Ct_OperLog.OrderByDescending(c => c.StepDate).Where(c => c.PCBID == PCBID).Select(c => new { c.StepID, c.TestResultID } ).ToList();
                if (list.Count == 0)
                {
                    return "true";
                }
                var Result = list[0].StepID;
                var TestResult = list[0].TestResultID;

                //if (Result == null) //Если результат нулл                   
                //    return "true"; //Если фасСтарт то проверка прошла  
                if (Result == 4) //Ремонта
                {
                    if (TestResult == 4)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла в ремонте и имеет статус 'Подтверждение списание от ОТК'";
                    if (TestResult == 3)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата последний этап прошла в ремонте и имеет статус Fail!";
                    if (TestResult == 2)
                        return "true";
                }
                else if (Result == 31) //Scrap
                {
                    if (TestResult == 3)
                        return "Проверка таблицы Ct_OperLog не пройдена! \nПлата Забракована в таблице системы Scrap";
                    if (TestResult == 2)
                        return "true";
                }
                else if (Result == 34) //Dissamble
                {
                    if (TestResult == 3)
                        return "Проверка таблицы Ct_OperLog не пройдена!\nПлата  Забракована в таблице в FAS_Disassembly\n Передайте плату в ремонт";
                    if (TestResult == 2)
                        return "true";
                }
                else if (Result == 32)
                {
                    return "true";
                }

                var stepName = fas.Ct_StepScan.FirstOrDefault(c => c.ID == Result).StepName;                    
                return $"Проверка таблицы Ct_OperLog не пройдена! \nПоследний фактический пройденный этап платы ('{stepName}')";
                
            }
        }



        void InsertFasStart(int serialnumber)
        {
            var R = DateTime.Parse(DateText.ToString("yyyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss")); 
            using (FASEntities FAS = new FASEntities())
            {
                var fas_start = new FAS_Start()
                {
                    SerialNumber = serialnumber,
                    PCBID =  PCBID,
                    LineID = (byte)LineID,
                    ManufDate =  R,
                    AssemblyDate = DateTime.UtcNow.AddHours(2),
                    PrintStationID = (short)StationID   
                };
                FAS.FAS_Start.Add(fas_start);
                FAS.SaveChanges();
            }
        }

        string CheckSerialNumber(string SerialNumber)
        {
            if (!int.TryParse(SerialNumber.Substring(15), out int k)) 
                return $"Ошибка преобразования серийного номера - {SerialNumber} - в число, не верный номер!"; //Ошибка преобразования

            int serial = k;

            using (var fas = new FASEntities())
            {
                bool Result = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == serial & c.LOTID == LOTID).Select(c => c.SerialNumber == c.SerialNumber).FirstOrDefault();
                if (!Result)
                    return $"Серийный номер - {SerialNumber} - не найден в таблице Fas_SerialNumbers в текущем лоте!"; //номер не найден в лоте

                Result = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == serial & c.LOTID == LOTID & c.IsUsed == true).Select(c => c.SerialNumber == c.SerialNumber).FirstOrDefault();
                if (!Result)
                    return $"Серийный номер - {SerialNumber} - имеет статус IsUsed = 0, номер не использован в базе "; //Номер не использован

                var ser = fas.FAS_Start.Where(c => c.FullSTBSN == SerialNumber).Select(c => c.FullSTBSN).FirstOrDefault();
                if (string.IsNullOrEmpty(ser))
                    return $"Серийный номер - {SerialNumber} - не найден в таблице Fas_Start"; //Номер не зарегистрирован на FasStart

                return ser;

            }
        }
        bool GetSerialNumber()
        {
            using (FASEntities FAS = new FASEntities())
            {
                //var list = GetListRange(LOTID); //Список номеров которые входят в диапазоны
                //Берем первый серийный номер, который не касается диапазонов
                var ser = FAS.FAS_SerialNumbers.Where(c => c.LOTID == (short)LOTID && c.IsUsed == false && c.IsActive == true & c.FixedID == null).Select(c => c.SerialNumber).Take(1).FirstOrDefault(); 
                if (ser != 0)
                {
                    SerNumber = ser;
                    DateText = DateTime.UtcNow.AddHours(2);
                    return false;
                }
                return true;
            }
        }
        string GetSerialNumberRange()
        {
            using (FASEntities FAS = new FASEntities())
            {               
                for (int i = 0; i < GridRange.RowCount; i++) //Берем дипозон, который мы выбрали в настройки формы
                {
                   var st = int.Parse(GridRange[0, i].Value.ToString()); //Берем начало диапазона
                   var end = int.Parse(GridRange[1, i].Value.ToString()); //Берем конец диапазона

                    //Берем первый свободный номер с диапазона
                   var ser =  FAS.FAS_SerialNumbers.Where
                   (c => c.LOTID == (short)LOTID && c.IsUsed == false && c.IsActive == true && c.SerialNumber >= st && c.SerialNumber <= end)
                   .Select(c => c.SerialNumber).Take(1).FirstOrDefault();

                    if (ser != 0) //Если номер в диапазоне найден, сохраняем данные, успешно выходим из метода
                    {
                        SerNumber = ser;
                        DateText = DateTime.Parse(GridRange[2, i].Value.ToString());
                        return "False";
                    }  
                }

                //Если мы дошли да этого кода, то значит в диапазоне закончились номера
                FixedRange FR = new FixedRange(LOTID, this,"В базе для этого диапазона закончились серийные номера \n выберите следующий или вызовите технолога",LitIndex);
                var Result = FR.ShowDialog(); //Открываем форму, чтобы выбрать следующий диапазон

                if (Result == DialogResult.Abort) //Если вернул Abort, номера закончились полностью
                    return "Abort";
                if (Result == DialogResult.OK) //Если вернул OK, то работает по новому диапазону         
                    return "New";
                if (Result == DialogResult.Retry) //Если вернул OK, то работаем БЕЗ диапазона        
                    return "Retry";

                return "True";
                //BaseC.DateFas_Start = true;
            }
        }

        void updateSerNum(int serialNumber) 
        {
            using (FASEntities FAS = new FASEntities())
            {
                var FAS_S = FAS.FAS_SerialNumbers.Where(c=>c.SerialNumber == serialNumber);
                FAS_S.FirstOrDefault().IsUsed = true;
                FAS_S.FirstOrDefault().PrintStationID = (short)StationID;
                FAS.SaveChanges();
            }
        }

        int CheckSerialNumer(int serialnumber)
        {
            using (FASEntities FAS = new FASEntities())
            {
                return FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == serialnumber && c.PrintStationID == StationID).Select(c => c.SerialNumber).FirstOrDefault();

            }
        }
        string CheckAssemlyPCBID()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var SerNum = FAS.FAS_Start.Where(v => v.PCBID == PCBID).Select(c => c.SerialNumber).FirstOrDefault(); //Поиск серийного номера по баркоду
                if (SerNum == 0) //Не найден
                    return ""; //Возвращаем пустое значение, все хорошо

                //Серийный номер найден по баркоду
                //Ищем лот по серийному номеру, добавяем его в локальную переменную
                var list =  FAS.FAS_GS_LOTs.Where(c => FAS.FAS_SerialNumbers.Where(b => b.SerialNumber == SerNum).FirstOrDefault().LOTID == c.LOTID).Select(c=> new { FullLotCode = c.FULL_LOT_Code, LOTID = c.LOTID}).ToList();
                FullLotCode = list[0].FullLotCode;

                if (list[0].LOTID == (short)this.LOTID) //Если найденный лот равен текущему лоту в котром работает программа
                    return FAS.FAS_Start.Where(c => c.PCBID == PCBID).Select(c => c.FullSTBSN).FirstOrDefault(); //Возращаем полный серийный номер

                return "False"; //Возвращаем False, пишем ошибку
            }
        }

        List<int> GetListRange(int lotid)
        {
            var list = new List<int>();
            using (var fas = new FASEntities())
            {
                var count = fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).Count();
                for (int i = 1; i <= count; i++)
                {
                    var st = fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).Select(c => c.RGStart).ToList();
                    var end = fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).Select(c => c.RGEnd).ToList();

                    for (int k = st[i-1]; k <= end[i-1]; k++)
                        list.Add(k);
                }
            }
            return list;
        }
        string CheckLazerPCBID(string barcode) //Проверка LazerBase и THT Start
        {
            using (SMDCOMPONETSEntities SMD = new SMDCOMPONETSEntities())
            {
                PCBID = SMD.LazerBase.Where(c => c.Content == barcode).Select(c => c.IDLaser).FirstOrDefault(); //LazerBase
                if (PCBID == 0)
                  return $"Номер - '{barcode}' не найден в таблице LazerBase";

                var check = SMD.THTStart.Where(c => c.PCBserial == barcode).Select(c => c.PCBserial).FirstOrDefault(); //THTStart

                if (string.IsNullOrWhiteSpace(check))
                    return $"Номер - '{barcode}' не найден в таблице THTStart";

                if (LOTID == 134) //Временное условие
                    return "true";

                //var res = SMD.THTStart.OrderByDescending(c=>c.PCBScanTime).FirstOrDefault(c => c.PCBserial == barcode & c.PCBResult == true).PCBResult;
                var res = SMD.THTStart.OrderByDescending(c => c.PCBScanTime).Where(c => c.PCBserial == barcode & c.PCBResult == true).Select(c=>c.PCBResult).FirstOrDefault();

                if (res)
                    return "true";

                var aoipass = SMD.THTStart.OrderByDescending(c => c.PCBScanTime).FirstOrDefault(c => c.PCBserial == barcode).AOIpass;
                var aoiverif = SMD.THTStart.OrderByDescending(c => c.PCBScanTime).FirstOrDefault(c => c.PCBserial == barcode).AOIverify;

                if (!aoipass & !aoiverif)              
                    return $"Номер - '{barcode}' не прошел проверку \nAOI инспекцию и верификацию в таблице THTStart";                
                else if (!aoipass)
                    return $"Номер - '{barcode}' не прошел проверку \nAOI инспекцию в таблице THTStart";
                else 
                    return $"Номер - '{barcode}' не прошел проверку \nВерификации в таблице THTStart";
           

                //Добавить THT Start
            }
        }

        string GetlabelDate()
        {
            SerNumber = 0; //очистка локальной переменной 

            #region проверка диапазона с каждым сканированем (откл)
            // С каждым сканом, проверяет не установился ли диапазон на лот
            //if (!DateFas_Start)
            //    if (Checkrange()) // Проверка, есть ли в этом лоте диапазон          
            //        DateFas_Start = true;
            //    else
            //        GetLineForPrint();
            //========================================
            #endregion

            if (DateFas_Start) // Работа по диапазону, если он существует
            {
                LineForPrint = "01";
                //Abort = Закончились серийные номера во всех диапазонах
                //False = Прошло успешно!
                //True = Ошибка
                //New = Открывается новый диапазон
                //None = Работа без диапазона
                var Result = GetSerialNumberRange(); //Получаем результат

                if (Result == "False")
                    return "False"; //Если все прошло успешно, возвращаем False

                if (Result == "New") //Если открывается новый диапазон
                    return "New";                

                if (Result == "Abort")
                    return "Abort"; //Закончились серийные номера во всех диапазонах

                if (Result == "Retry") //Если начали без диапазона работать
                    return NotRange();

                return "True";
            }
            else            
                return NotRange();
                
            
            
            string NotRange()
            { 
                if (GetSerialNumber()) //Если (ошибка) серийный номер не получен Возвращаем True
                    return "True";
                 return "False"; //Если все прошло успешно, возвращаем False
            }
        }

        bool IsBunch()
        {
            using (var fas = new FASEntities())
            {
                var result = fas.FAS_GS_LOTs.FirstOrDefault(c => c.LOTID == LOTID).IsBunch;
                if (result == null)
                    return false;
                return (bool)result;
            }
        }
        public override void GetComponentClass()
        {
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Visible = true; // делает видимым GroupBox для  Fas_Start
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Location = new Point(LocX, LocY); // Устанавливает расположение 
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Size = new Size(249, 260); //Устанавливает размер
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault(); //Записывает Grid Формы в переменную
            GetLot(Grid); //Получение заказов
        }

        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
                                    LOTCode,FULL_LOT_Code,ModelName,
                                    (select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
                                    (select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
                                    (select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used,
                                    LOTID, Scenario ,RangeStart,RangeEnd, FixedRG,StartDate
                                    from FAS_GS_LOTs as gs
                                    left join FAS_Models as m on gs.ModelID = m.ModelID
                                    left join FAS_LabelScenario as L on gs.LabelScenarioID = L.ID
                                    where IsActive = 1
                                    order by LOTID desc ");

            #region Старый код
            //using (FASEntities FAS = new FASEntities())
            //{
            //    var list = from Lot in FAS.FAS_GS_LOTs
            //               join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
            //               join Label in FAS.FAS_LabelScenario on Lot.LabelScenarioID equals Label.ID
            //               where Lot.IsActive == true
            //               orderby Lot.LOTID descending
            //               select new
            //               {
            //                   Lot = Lot.LOTCode,
            //                   Full_Lot = Lot.FULL_LOT_Code,
            //                   Model = model.ModelName,
            //                   InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   LOTID = Lot.LOTID,
            //                   Scenario = Label.Scenario,
            //                   Стартдиапазон = Lot.RangeStart,
            //                   Конецдиапазон = Lot.RangeEnd,
            //                   FixedRG = Lot.FixedRG,
            //                   StartDate = Lot.StartDate

            //               };

            //    Grid.DataSource = list.ToList();
            //}
            #endregion
        }
        bool Checkrange()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == LOTID).Select(c => c.LotID == c.LotID).FirstOrDefault();
            }
        }

        string LabelSN(string printTextSN, string printCodeSN,int X ,int Y, string LabelScenario )
        {
            if (LabelScenario == "6")
            {
                return @$"
^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
^BY3,3,128^FT{89 + X},{246 + Y}^BCN,,N,N
^FD>;{printCodeSN}^FS
^FT{53 + X},{62 + Y}^A0N,33,33^FH\^FD{"GSL RAVEN"}^FS
^FT{362 + X},{62 + Y}^A0N,33,33^FH\^FD{DateText.ToString("dd.MM.yyyy HH:mm:ss")}^FS
^FT{104 + X},{108 + Y}^A0N,38,38^FH\^FD{printTextSN}^FS
^FT{53 + X},{305 + Y}^A0N,33,33^FH\^FDMade in Russia^FS
^FT{503 + X},{305 + Y}^A0N,33,33^FH\^FDQC Passed^FS
^FT{458 + X},{268 + Y}^A0N,25,24^FH\^FDwww.gs-labs.ru^FS
^PQ{labelCount},0,1,Y^XZ
        ";
            }
            else
            {
                return $@"
^ XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH20,30^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
положение и контент сервисная служба
^FO{44 + X},{185 + Y}^GFA,04352,04352,00068,:Z64:
eJzt1D9v1DAUAPBnMpgpb2UINt8AxlS4DR+Fj5CKAZ8uOlOdBBv9AsDnYEDgKBIdb2VA4KhCbJUjFg/hzHOuFCRaKIIBpHtjfPrd8/tjgG1sYxvb+C+i+XOCjxef4fpyBv4FQ4afnF3SED85w8sRoP6CUV54kpGRRc661hkwFkbuZl56nWfO6cpWDioWFVdKlRYdOPBl4eod50uo/ALyfeuAi8nIuraPB9GytfTzUAUtuPfaOOPAZLFIxpzokgytvCZDgxki5EMyim9GF222lsGM1aj3MDgdXXQQl1FgodT9sQqqBt0oH8ioIZKBycgVjnzFORld11n+WinTKKUX8prXbvCODctVMrQZb+ni1NADGX3XnholGYccqR6sW1oOZdHsqUIvFDgNg3OZzVaYC6WbPSjzkoyyJiPU4DoLeEBGoWUy5JmhCy0oj7Wim58ahygkFUGQoaAcy9lYD8GzPhnMelAegYzrXSSjswhmJCPMyIgB/FcDo0sGL0Ct9extvR8c9G2AnFEek4F8lwxDNd0YlZ+9S4b0kQz6i4Ing24tQJ3UyRgdDO2Y8qC71JOx08U+2jNjnE+G8WZjvJiMKqQ8Tuqdt27+hBpmF5s8hJaAyPfJ6CdDQ6qHeZYMf2bgLtVDpXpcX9W779z8qYX4QG7qQX2BHPnQtcdte/zVEEakvtAkJQOz59QXLXTqi1z56rG7/dQyC3yTR5oxgbxPRpqPKxpoPJJxlQDvyTjELKTeFlrRfMgjJw9dJe1ARo6sn2b9hrjJWzKmGXuori4IWQhRpjycY/0rZIHmlBquaU7xERm2QjvEjGPOPpGRy7WJR2T0H5Oxrmggd5tGyOD1QPPBIhmehGpsQukn49HG6N5LZNEDo3eMjINpX5bTvqSda8S0L2RAfLkxZKjewB0yrORW4gMfj9cyh3g3vR/JSLv/+Whp2Yhh2v3N3t6jihlFhlYCnXySfr60OBnDh2FEhObu9IbUVOLvQv/4yrhzXp7U1W/xS4P9K4b94RN9jN/LvzSy8wzal/O+/p5hLia2sY1tbOP/ii8gSQz/:A196
положение и контент general satellite
^FO{224 + X},{217 + Y}^GFA,02688,02688,00028,:Z64:
eJzt1M1KxDAQB/CEHPYYwatsHmFfoDT4RF4rVLOwh30LH0UKHjzuIxjpxYNgZBEjhIwzST+268dREDpdevkxZPJPs4zNNdc/qLP0PsGHfl1xwPI8MBWMd6UOYBe2zCbInAhMo3k04waTyWRgBi2QedmbJrMKjdUsliW7CtJ1ZsgaHVjorKpVb5DMBObRINnSl+OYsEZzzDy93OpntGpigFYY/35Ls5SdpS0A7y2SFXW2xYHVbB9LZlxdHFgUnb2SWRMPLCzQfGeXrjc5GOXicL0PB/gezMvernWonFfHRhNazLP6sHKVTKWoFZry2LdEu3JSj2Y1Gl8fn/doX0vlqH+29W8GPxv73mh/kZPpJhgw+6YQO3gY9h5PyUxn9cSCIINv+zBqtLfzaKAWuW9YzydzPAqLVoimUsP5YdRkIls9MYwaLYidsKbNfWk9kb5qsphsn/tGW5FdbHebaR/PUdP53W830z6Wo0aT97CZzkl3hTywxQ7upnPiHYsTA70fDD8VngwexB2uR7H160nwItuNaMn0aByazpa8PerDu5zNl/yxMS3odrTx72Suueb60/oEc4oKnw==:460E
модель
^FT{20 + X},{32 + Y}^A0N,33,33^FH\^FD{ArrayList[5]}^FS
дата
^FT{320 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("dd.MM.yyyy")}^FS
время
^FT{490 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("HH:mm:ss")}^FS
сн_текст 
^FT{86 + X},{68 + Y}^A0N,33,36^FH\^FD{printTextSN}^FS
штрихкод
^BY3,3,125^FT{45 + X},{197 + Y}^BCN,,N,N
^FD>;{printCodeSN}^FS
made_QC
^FT{20 + X},{282 + Y}^A0N,33,31^FH\^FDMade in Russia^FS
^FT{448 + X},{282 + Y}^A0N,33,33^FH\^FDQC Passed^FS
количество этикеток
^PQ{labelCount},0,1,Y^XZ";
            }
         
        }


    }
}
