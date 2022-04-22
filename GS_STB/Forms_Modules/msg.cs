using GS_STB.Class_Modules;
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
    public partial class msg : Form

        
    {
        BaseClass BC;
        public msg(string Text, BaseClass BC)
        {
            InitializeComponent();
            this.Select();
            this.BC = BC;
            label1.Text = Text;

            if (BC is FASStart)            
                button3.Enabled = false;
            
        }

        public msg(string Text, string Night,string Day)
        {
            InitializeComponent();
            this.Select();
            label1.Text = Text;
            button1.Text = Day;
            button2.Text = Night;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            var us = new ConfimUser();
            var re = us.ShowDialog();

            if (re != DialogResult.OK)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            var us = new ConfimUser();
            var re = us.ShowDialog();

            if (re != DialogResult.OK)
            {
                return;
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void msg_KeyDown(object sender, KeyEventArgs e)
        {
           

            if (e.KeyCode == Keys.Space)
            {
                var us = new ConfimUser();
                var re = us.ShowDialog();

                if (re != DialogResult.OK)
                {
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();             
            }

            else if (e.KeyCode == Keys.N)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }

            else if (e.KeyCode == Keys.M)
            {
                var us = new ConfimUser();
                var re = us.ShowDialog();

                if (re != DialogResult.OK)
                {
                    return;
                }

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                return;
            }


        }

        private void msg_Load(object sender, EventArgs e)
        {

        }
    }
}
