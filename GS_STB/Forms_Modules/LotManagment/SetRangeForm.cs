using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace GS_STB.Forms_Modules
{
    public partial class SetRangeForm : Form
    {
        int lotid;
        int LotCode;
        string NotUsedLB = $"Лот еще не был использован, \n Укажите кол-во серийных номеров в диапазоне отгрузки и начальную дату";        
        int count;
        string UsedLB = $"В лоте уже используется номера, \nукажите кол-во серийный номеров для начальной отгрузки Литера 0 и дату";
        string UsedLB2 = $"В лоте уже используется номера, \n Укажите кол-во серийных номеров в диапазоне отгрузки и начальную дату";
        bool UsedLot = false;
        int SNCount;
        string lot;
        public SetRangeForm(int LotID, int LotCode)
        {           
            InitializeComponent();
            this.lotid = LotID;
            this.LotCode = LotCode;
        }

        private void SetRangeForm_Load(object sender, EventArgs e)
        {
            GetSNCount(); //Инфо о количество номеров в лоте
            CheckLot(); //Проверяет выпускался ли лот, ставит флажок на булвой перемнной UsedLot (true/false)
            label1.Text = $"Лот - {GetLotName()}. Размер лота - {SNCount}.";
            DateRange.Value = DateTime.Now;
            label1.Text = "Укажите кол-во серийных номеров в диапазоне отгрузки и начальную дату";
            if (!UsedLot) //Лот не использованный 
            {
                GRNotUsed.Visible = true; GRNotUsed.Location = new Point(17, 91);
            }
            else //Лот использованный 
            { GRNotUsed.Visible = true; GRNotUsed.Location = new Point(17, 91); }//Инициализация компонентов}
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            var EndRange = GetRangeEnd(); // Последний номер в лоте            
            //Присутствует 2  условия, если лот вообще не использован и если лот уже выпускается
            if (!UsedLot) //Лот не использованный
            {                
                DateTime d = DateRange.Value;

                int RangeStart;
                //Есть 2 условия, если диапазн создавался ранее и если не создавался

                if (Checkrange()) //Создавался ранее
                //Берем последние данные по диапазону, прибавляем + 1
                { RangeStart = getrange() + 1;  /*d = GetDatetime().AddDays(1); */ }
                else // Не создавался ранее
                    RangeStart = GetRangeStart(); //Берем самый начальный номер в диапазоне

                AddRangeMethod(RangeStart, EndRange, d); //Метод Добавление диапазона                
            }
            else //Если лот уже выпускается
            {
                //var RangeEnd = GetRangeEnd();
                if (!Checkrange())//Если ранее диапазоны не устанавливались
                {                   
                    var Start = GetRangeEndUsed() + 1;
                    AddRangeMethod(Start, EndRange, DateRange.Value);
                }
                else
                {
                    var RangeStart = GetRangeTop(); //Определяем с какого номера создать диапазон
                    var literindex = (short)(GetLiterIndex() + 1);                    
                    AddRangeMethod(RangeStart, EndRange, DateRange.Value);
                } //Если диапазоны уже существуют

                #region СтарыйКод
                //var RangeEnd = GetRangeEnd();

                //if (!Checkrange()) //Если ранее диапазоны не устанавливались
                //{
                //    var RangeStart = GetRangeStart();
                //    DateTime d = DateRange.Value;
                //    var count = GetCountSNUsed();
                //    if (SNNum.Value > count)
                //    { MessageBox.Show($"Кол-во номеров в диапазоне которое вы выбрали '{SNNum.Value}' меньше текущего количества использованных номеров в лоту {count}"); return; }

                //    AddRangeMethod(RangeStart, RangeEnd, d, 0);
                //}
                //else
                //{
                //    var RangeStart = getrange() + 1;
                //    var literindex = (short)(GetLiterIndex() + 1);
                //    var d = GetDatetime().AddDays(1);
                //    AddRangeMethod(RangeStart, RangeEnd, d, literindex);
                //}
                #endregion
            }

            if (LoadExcel.Checked)
            {
                OpenExcel();
            }

            if (GridExcelReport.RowCount > 1) //Выгрузка Excel 
            {                
                MessageBox.Show("диапазон добавлен успешно!");
                DialogResult = DialogResult.OK;
                this.Close();
            }
            

            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
        void GetSNCount()
        {
            using (var fas = new FASEntities())
            {
                SNCount = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Count();
            }
        }

        string GetLotName()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid).Select(c => c.FULL_LOT_Code).FirstOrDefault();
            }
        }

        void CheckLot()
        {
            using (var fas = new FASEntities())
            {
                var count = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.IsUsed == true).Count();
                if (count != 0)
                    UsedLot = true;
            }
        }

        void AddRangeMethod(int RangeStart,int RangeEnd, DateTime d)
        {
            //var Sum = (int)((RangeStart - 1) + SNNum.Value); // Берем стартовое значение и прибавляем к нему значение которое ввели в поле
            var Sum = SerialNumbersCount();
            //Если результат больше конечного диапазона, ошибка
            if (SNNum.Value > Sum)
            {
                MessageBox.Show($"Кол-во номеров в диапазоне которое вы выбрали '{SNNum.Value}'превышает диапазон в лоте, в диапазоне осталось '{RemainSerialNumbersCount(RangeStart)}'"); return;
            }
            //Проверка, есть ли диапазон с указанной датой
            if (CheckDateRange())
            {
                MessageBox.Show($"Уже существует лот '{lot}', который работал в диапазоне с такой датой '{DateRange.Value.ToString("dd.MM.yyyy")}'"); return;
            }

            if (CheckRangeInLot()) //Если серийные номера в лоте идут последовательно друг за другом
            {
                SequentialRange(RangeStart, RangeEnd, d); //Применяется, если в лоте нет разрыва серийных номер и они последовательно идут друг за другом
                return;
            }
            BreakedRange(RangeStart, RangeEnd, d); //Если в лоте Серийные номера идут не последовательно друг за другом

        }

        bool CheckRangeInLot()
        {
            loadgrid.Loadgrid(BreakRangedGrid, @$"use fas SELECT MIN([SerialNumber]) AS start_range , MAX([SerialNumber]) AS end_range
                                                ,count(grp) as Count_range FROM(SELECT[SerialNumber],[SerialNumber] - ROW_NUMBER() OVER(ORDER BY SerialNumber) AS grp
                                                FROM dbo.[FAS_SerialNumbers] where LOTID = {lotid}) AS D  GROUP BY grp order by start_range");
            if (BreakRangedGrid.RowCount == 1)
                return true; //Диапозон ровный
            return false; //диапозон не по порядку
        }

        void BreakedRange(int RangeStart, int RangeEnd, DateTime d)
        {
            for (int i = 0; i < BreakRangedGrid.RowCount; i++)
            {
                var EndBreakRange = int.Parse(BreakRangedGrid[1, i].Value.ToString()); //Конечный обрывок диапозона

                if (RangeStart <= EndBreakRange) //Если серийный номер Меньше конечного текущего обрывистого диапозона
                {
                    var Sum = (int)((RangeStart - 1) + SNNum.Value); //RangeStart + Кол-во серийных номеров, которые мы установили

                    if (Sum > EndBreakRange)//Если количество Сер номеров перепрыгивает диапозон 
                    {
                        //Если  самый последний номер в Лоте равен последнему номеру в обрывистом диапозоне и если сумма превышает самый последний номер в лоте
                        if (RangeEnd == EndBreakRange)
                        {
                            MessageBox.Show($"Количество серийных номеров, которое вы указали превышает максимальный диапозон"); return;
                        }

                        var Itter = GetItter(i); //Количество иттерация на добавление диапозона
                        var CountSerNumber = SNNum.Value; //Записываем в переменную, кол-во серийных номеров для добавления

                        var r = EndBreakRange - RangeStart + 1; //Узнаем количество номеров
                        CountSerNumber -= r;

                        AddRange(RangeStart, RangeEnd,ref d, r);

                        for (int b = 0; b < Itter - 1; b++)
                        {
                            var StarR = int.Parse(BreakRangedGrid[0, i + (b + 1)].Value.ToString());
                            var EndR = int.Parse(BreakRangedGrid[1, i + (b+1)].Value.ToString());
                            var Result = CountSerNumber + StarR - 1;

                            if (Result >= EndR) //Если остаток снова перепрыгивает конец текущего диапозона
                            {
                                var Re = EndR - StarR;
                                CountSerNumber -= Re;
                                AddRange(StarR, RangeEnd, ref d, Re);
                                continue;
                            }

                            AddRange(StarR, RangeEnd, ref d, CountSerNumber);
                        }
                        return;
                    }
                    else //Количество номеров не перепрыгивает диапозон
                    {
                        AddRange(RangeStart, RangeEnd,ref d, SNNum.Value); //Добавление диапозона
                        return;
                    }                    
                }
            }
        }

        int RemainSerialNumbersCount(int serialNumber)
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.SerialNumber >= serialNumber & c.FixedID == null & c.IsUsed == false).Count();
            }
        }
        int SerialNumbersCount()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Count();
            }
        }

        int GetItter(int i)
        {
            var count = 0;
            for (int k = i; k < BreakRangedGrid.RowCount; k++)            
                count++;
            return count;
        }

        void SequentialRange(int RangeStart, int RangeEnd, DateTime d)
        {
            var Sum = (int)((RangeStart - 1) + SNNum.Value); // Берем стартовое значение и прибавляем к нему значение которое ввели в поле
            //Если результат больше конечного диапазона, ошибка
            if (Sum > RangeEnd)
            {
                MessageBox.Show($"Кол-во номеров в диапазоне которое вы выбрали '{SNNum.Value}'превышает диапазон в лоте, в диапазоне осталось '{RangeEnd - RangeStart + 1}'"); return;
            }
            AddRange( RangeStart, RangeEnd, ref d,  SNNum.Value);
        }

        void AddRange( int RangeStart, int RangeEnd, ref DateTime d, decimal Value)
        {
            //Существует 2 условия, Если значение которое мы указали равно или меньше 15000 шт. И если больше 15000 шт.

            if (Value <= 15000) //Равно или меньше 15000 шт.
            {
                RangeActivate(RangeStart, RangeEnd); //Активируем диапазон в таблице GsLots   
                addFixedRange( RangeStart, (int)(RangeStart + Value) - 1, d); //Добавляет диапазон в таблицу FAS_Fixed_Range
            }
            else //Если больше 15000 шт.
            {
                RangeActivate(RangeStart, RangeEnd); //Активируем диапазон в таблице GsLots  

                int C = (int)(Value / 15000);
                for (int i = 0; i < C; i++) //Делим на диапазоны по датам
                {
                    addFixedRange( RangeStart, RangeStart + (15000 - 1), d);
                    RangeStart = getrange() + 1; d = d.AddDays(1);
                }

                //добавляем отстаток
                var r = (int)(Value - (15000 * C));
                if (r > 0)
                {
                    addFixedRange( RangeStart, RangeStart + (r - 1), d);
                }
            }
        }     

        DateTime GetDatetime()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.LabDate).FirstOrDefault();
            }
        }

        int GetCountSNUsed()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.IsUsed == true).Count();
            }
        }

        short GetLiterIndex()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.LitIndex).FirstOrDefault();
            }
        }

        int GetRangeEnd()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();
            }
        }

        int GetRangeStart()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
            }
        }
       
        int GetRangeEndUsed()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid && c.IsUsed == true && c.FixedID == null).Select(c => c.SerialNumber).Max();
            }
        }

        int GetRangeTop()
        {
            using (var fas = new FASEntities())
            {
                // Находим в лоте в таблице Fas_Start самый большой серийный номер 
                var sers = (from st in fas.FAS_Start
                           join ser in fas.FAS_SerialNumbers on st.SerialNumber equals ser.SerialNumber
                           where ser.LOTID == lotid
                           orderby st.SerialNumber descending
                           select st.SerialNumber).FirstOrDefault();

                //Определяем есть ли fixedRange
                var R = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == sers).Select(c => c.FixedID).FirstOrDefault();

                var Re = (from Fi in fas.FAS_Fixed_RG
                          where Fi.LotID == lotid
                          select Fi.RGEnd).Max();
                

                if (R != null)//Если номер в диапазоне                                  
                    return fas.FAS_SerialNumbers.Where(c => c.SerialNumber > Re & c.LOTID == lotid).Select(c=>c.SerialNumber).Take(1).FirstOrDefault();

                if (sers <= Re) //Если перед нашим максимально найденным номером, есть диапазон
                    return fas.FAS_SerialNumbers.Where(c => c.SerialNumber > Re & c.LOTID == lotid).Select(c => c.SerialNumber).Take(1).FirstOrDefault();
                else //Если перед номером нет диапазонов
                    return fas.FAS_SerialNumbers.Where(c => c.SerialNumber > sers & c.LOTID == lotid).Select(c => c.SerialNumber).Take(1).FirstOrDefault();

            }
        }

        bool Checkrange()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).Select(c => c.LotID == c.LotID).FirstOrDefault();
            }
        }
        int getrange()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.RGEnd).FirstOrDefault();
                //fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.FixedID == null).Select(c => c.SerialNumber).Take(1).FirstOrDefault();
            }
        }

        //int getranged() //Фнукция используется если лот уже выпускается без дипозона
        //{
        //    using (var fas = new FASEntities())
        //    {
        //        return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.RGEnd).FirstOrDefault();
        //    }
        //}

        void addFixedRange(int st, int end,DateTime d)
        {
            var Date = DateTime.Parse(d.ToString("dd.MM.yyyy"));
            using (var fas = new FASEntities())
            {
                var F = new FAS_Fixed_RG() //Добавляем диапазон в FixedRange
                {
                    LotID = lotid,
                    LitIndex = (short)LiterIndex.Value,
                    RGStart = st,
                    RGEnd = end,
                    LabDate = Date
                };                
                
                fas.FAS_Fixed_RG.Add(F);
                fas.SaveChanges(); //Сохраняем

                //Берем ID записи, которую мы сохранили
                var FixedID = fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.id).FirstOrDefault();

                #region Медленный Update
                ////Присваиваем ID  в FAS_serialNumbers всем номерам которые попадают в диапазон
                //var Fas_ser = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.SerialNumber >= st & c.SerialNumber <= end).AsEnumerable()
                //    .Select(c => { c.FixedID = FixedID; return c; });


                //foreach (FAS_SerialNumbers item in Fas_ser)
                //    fas.Entry(item).State = (System.Data.Entity.EntityState)EntityState.Modified;
                //fas.SaveChanges();

                #endregion

                var obj = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.SerialNumber >= st & c.SerialNumber <= end).ToList();

                obj.ForEach(c => c.FixedID = FixedID);
                fas.SaveChanges();
                ReportExcel(st,end,Date);
            }
        }

        void ReportExcel(int st, int end, DateTime d)
        {
            for (int i = st; i <= end; i++)
            {
                var date = d.ToString("ddMMyyyy");
                var FullSTBSN = GenerateFullSTBSN(i,date);
                GridExcelReport.Rows.Add(GridExcelReport.RowCount,i,d.ToString("dd.MM.yyyy"), FullSTBSN);
            }
        }

        void RangeActivate(int st, int end )
        {
            using (var fas = new FASEntities())
            {
                var Result = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid).Select(c => c.FixedRG).FirstOrDefault();
                if (Result != null)
                    return;

                var gs = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                gs.FirstOrDefault().RangeStart = st;
                gs.FirstOrDefault().RangeEnd = end;
                gs.FirstOrDefault().FixedRG = true;
                gs.FirstOrDefault().StartDate = DateTime.Parse(DateRange.Value.ToString("dd.MM.yyyy"));
                fas.SaveChanges();
            }
        }
               

        bool CheckDateRange()
        {
            using (var fas = new FASEntities())
            {
                var D = DateTime.Parse(DateRange.Value.ToString("dd.MM.yyyy"));
                //lot = fas.FAS_Start.Where(c =>c.ManufDate == D).Select(c => fas.FAS_GS_LOTs.Where(b=>b.LOTID == c.LotID ).Select(b =>b.FULL_LOT_Code).FirstOrDefault()).FirstOrDefault();
                return fas.FAS_Start.Where(c => c.ManufDate == D).Select(c => c.ManufDate == c.ManufDate).FirstOrDefault();
            }
        }

        string GenerateFullSTBSN(int serialnumber,string ProdDate) //Генерация Полного Серийного номера
        {        
                string FullSTBSN;
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
    }
}
