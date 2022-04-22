using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
    public partial class AbortSNcs : Form
    {
        public AbortSNcs()
        {
            InitializeComponent();
        }
        private void AbortSNcs_Load(object sender, EventArgs e)
        {
            GetLot();
        }
        void GetLot()
        {
            using (FASEntities FAS = new FASEntities())
            {
                DG_LOTList.DataSource = (from Lot in FAS.FAS_GS_LOTs
                                   join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
                                   where Lot.IsActive == true /*&& Lot.FixedRG == true*/
                                   orderby Lot.LOTID descending
                                   select new
                                   {
                                       Lot = Lot.LOTCode,
                                       Full_Lot = Lot.FULL_LOT_Code,
                                       Model = model.ModelName,
                                       InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                                       Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                                       Used = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                                       LotID = Lot.LOTID,
                                       Стартдиапазон = Lot.RangeStart,
                                       Конецдиапазон = Lot.RangeEnd,
                                       FixedRG = Lot.FixedRG,
                                       StartDate = Lot.StartDate
                                   }).ToArray();
            }
        }

        void GridLoadSN(int lotid)
        {
            SNGrid.Visible = true;
            using (var fas = new FASEntities())
            {
                SNGrid.DataSource = fas.FAS_SerialNumbers
                    .Where(c => c.SerialNumber >= Stser && c.SerialNumber <= EndSer && c.IsPacked == false && c.LOTID == lotid)
                    .Select(c=> new { LotID = c.LOTID, Серийный_номер =c.SerialNumber,  c.IsUsed, c.IsActive,c.IsUploaded,c.IsWeighted,c.IsPacked,c.PrintStationID })
                    .ToList();

                var count = fas.FAS_SerialNumbers
                    .Where(c => c.SerialNumber >= Stser && c.SerialNumber <= EndSer && c.IsPacked == false && c.LOTID == lotid)
                    .Select(c => new { Серийный_номер = c.SerialNumber, LotID = c.LOTID, c.IsUsed, c.IsActive, c.IsUploaded, c.IsWeighted, c.IsPacked, c.PrintStationID }).Count();
                INFO.Visible = true;
                INFO.Text = $"Найдено {count} использованных и не упакованных номера";
                AbortBT.Visible = true;
            }
        }

        void GridLoadSNNotRange(int lotid)
        {
            SNGrid.Visible = true;
            using (var fas = new FASEntities())
            {
                SNGrid.DataSource = fas.FAS_SerialNumbers
                    .Where(c=> c.IsPacked == false && c.LOTID == lotid)
                    .Select(c => new { LotID = c.LOTID, Серийный_номер = c.SerialNumber, c.IsUsed, c.IsActive, c.IsUploaded, c.IsWeighted, c.IsPacked, c.PrintStationID })
                    .ToList();

                var count = fas.FAS_SerialNumbers
                    .Where(c =>  c.IsPacked == false && c.LOTID == lotid)
                    .Select(c => new { Серийный_номер = c.SerialNumber, LotID = c.LOTID, c.IsUsed, c.IsActive, c.IsUploaded, c.IsWeighted, c.IsPacked, c.PrintStationID }).Count();
                INFO.Visible = true;
                INFO.Text = $"Найдено {count} использованных и не упакованных номера";
                AbortBT.Visible = true;
            }
        }

        int Stser;
        int EndSer;
        private void BT_OpenWorkForm_Click(object sender, EventArgs e)
        {
            Stser = 0;
            EndSer = 0;

            if (DG_LOTList.CurrentRow.Index == -1 || DG_LOTList.Rows.Count == 0)
            { MessageBox.Show("Лот не выбран!"); return; }

            int LOTID = int.Parse(DG_LOTList[6, DG_LOTList.CurrentRow.Index].Value.ToString());

            if (DG_LOTList[7, DG_LOTList.CurrentRow.Index].Value == null) //Если нет диапозона
            {
                GridLoadSNNotRange(LOTID);
                SNGrid.Select();
                return;
            }

            FixedRange FR = new FixedRange(LOTID);           
            var Result = FR.ShowDialog();
            if (Result == DialogResult.Cancel)//Если нажали отмена, выходим из метода
                return;

            Stser =  FR.STSer;
            EndSer = FR.EndSer;
            GridLoadSN(LOTID);
            SNGrid.Select();

        }
        private void AbortBT_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < SNGrid.RowCount; i++)
                list.Add(int.Parse(SNGrid[1, i].Value.ToString()));

            using (var fas = new FASEntities())
            {
                var delete = fas.FAS_Upload.Where(c => list.Contains(c.SerialNumber));
                fas.FAS_Upload.RemoveRange(delete);

                var deleteFas = fas.FAS_Start.Where(c => list.Contains(c.SerialNumber));
                fas.FAS_Start.RemoveRange(deleteFas);

                //--------------------------------------------------------------------------------------------------
                var _fas = fas.FAS_SerialNumbers.Where(c => list.Contains(c.SerialNumber)).AsEnumerable().Select(c=> 
                { c.IsUsed = false; c.IsActive = true; c.IsUploaded = false; c.IsWeighted = false; c.IsPacked = false;                    
                return c; });

                foreach (FAS_SerialNumbers item in _fas)                
                    fas.Entry(item).State = (System.Data.Entity.EntityState)EntityState.Modified;                      
                fas.SaveChanges();               
            }
            INFO.Text = "Номера откреплены успешно!";
            SNGrid.DataSource = null;
        }
    }
}
