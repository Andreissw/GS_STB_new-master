using GS_STB.Class_Modules;
using GS_STB.Forms_Modules.WeightForms;
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
    public partial class WeightSetting : Form
    {
        public int Weight { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public WeightSetting()
        {
            InitializeComponent();
        }

        private void BT_Click(object sender, EventArgs e)
        {
            GetStandartWeight();
        }

        private void WeightSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)          
                GetStandartWeight();
            
        }

        private void BT_packing_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        void GetStandartWeight()
        {
            FAS_Weight_Control Weight = new FAS_Weight_Control();
            var _weight = Weight.GetWeight();

            if (_weight == - 1)
            {
                MessageBox.Show("Ошибка получение веса", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TBWeight.Clear(); TBPlus.Clear(); TBMinus.Clear();
                return;
            }

            if (_weight <= 0)
            {
                MessageBox.Show("Вес не может быть равно или меньше 0", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TBWeight.Clear(); TBPlus.Clear(); TBMinus.Clear();
                return;
            }

            DifferenceSetting difference = new DifferenceSetting(_weight.ToString());
            var Result =  difference.ShowDialog();
            if (Result != DialogResult.OK)
            {

                TBWeight.Clear(); TBPlus.Clear(); TBMinus.Clear();
                return;
            }

            TBWeight.Text   = _weight.ToString();
            TBPlus.Text     =    difference.Max.ToString();
            TBMinus.Text    =   difference.Min.ToString();
        }

        private void BTContiune_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 3037596)
                return;

            if (TBWeight.Text == string.Empty)
            {
                MessageBox.Show("Не указана норма для весового контроля","Необходимо взвесить объект для дальнейшей работы",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (TBPlus.Text == string.Empty || TBMinus.Text == string.Empty)
            {
                MessageBox.Show($"Не указана разность от нормы веса {TBWeight.Text} для весового контроля", "Необходимо указать разность для дальнейшей работы", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Weight = int.Parse(TBWeight.Text);
            Max = int.Parse(TBPlus.Text);
            Min = int.Parse(TBMinus.Text);

            DialogResult = DialogResult.OK;

        }

        private void BTBack_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 3037596)
                return;

            DialogResult = DialogResult.Cancel;
        }       
    }
}
