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
    public partial class ConfimUser : Form
    {
        public ConfimUser()
        {
            InitializeComponent();
        }

        private void ConfimUser_Load(object sender, EventArgs e)
        {
            RFID.Select();
        }

        public int UserID { get; set; }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!GetUser())
            {
                MessageBox.Show("Не верный логин");
                RFID.Clear();
                RFID.Select();
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!GetUser())
                {
                    MessageBox.Show("Не верный логин");
                    RFID.Clear();
                    RFID.Select();
                    return;
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }              
        }     
        bool GetUser()
        {
            using (FASEntities FAS = new FASEntities())
            {
                //UserName = FAS.FAS_Users.Where(c => c.RFID == textBox1.Text && c.IsActiv == true).Select(c => c.UserName).FirstOrDefault();
                //if (string.IsNullOrEmpty(UserName))
                //    return true;
                var r = FAS.FAS_Users.Where(c => c.RFID == RFID.Text && c.IsActiv == true && new List<int>() {1,3 }.Contains(c.UsersGroupID)).Select(c => c.UserID == c.UserID).FirstOrDefault();
                //BaseC.ArrayList.Add(Name);
                if (r == false)
                    return false; //Не верный логин

                UserID = FAS.FAS_Users.Where(c => c.RFID == RFID.Text && c.IsActiv == true && new List<int>() { 1, 3 }.Contains(c.UsersGroupID)).Select(c => c.UserID).FirstOrDefault();
                return true;// Верный

            }
        }
       
    }
}
