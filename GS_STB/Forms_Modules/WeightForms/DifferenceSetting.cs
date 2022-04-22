using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules.WeightForms
{
    public partial class DifferenceSetting : Form
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public DifferenceSetting(string Weight)
        {
            InitializeComponent();

            LBWeight.Text = $"Вес объекта равен = {Weight}\nУкажите разность\nот нормы веса +/-";
        }

        private void BTBack_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            DialogResult = DialogResult.Cancel;
        }

        private void BTContiune_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            if (Plus.Value <= 0 || Minus.Value <= 0)
            {
                MessageBox.Show("Значение не может быть равно или меньше 0","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            Max = (int)Plus.Value;
            Min = (int)Minus.Value;

            DialogResult = DialogResult.OK;
        }
    }
}
