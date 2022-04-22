using GS_STB.Class_Modules;
using GS_STB.Forms_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetListApp();            
            GetModule();//Вывод списка в ЛистБокс            
            List<BaseClass> ListClasses = new List<BaseClass>() { new FAS_END(), new UploadStation(), new FASStart(), new Desassembly_STB()};

            BT_OK.Click += (a, e) => // Событие нажатие кнопки
            {
                IndexOpen(ListClasses);
            };

            listBox1.DoubleClick += (a, e) => // Событие нажатие кнопки
            {
                IndexOpen(ListClasses);
            };

            if (ApplicationDeployment.IsNetworkDeployed) //Показывает версию публикации
            {
                label2.Text = "Verison Product - " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                label2.Visible = true;
            }
           
        }
       
        private void IndexOpen(List<BaseClass> ListClasses)
        {
            
            if (CheckIndex())
            {
                if (listBox1.SelectedIndex == 4)
                { OpenModule(); return; }

                if (listBox1.SelectedIndex == 5)
                { OpenModuleAbortSN(); return; }

                OpenModule(ListClasses[listBox1.SelectedIndex]);//Открытие модулей GS_STB, которые работают на линии
            }
        }

        protected List<string> ListApp = new List<string>() {  };
        // Список модулей
        void GetModule() //Вывод списка в ЛистБокс
        {
            foreach (var item in ListApp)           
                listBox1.Items.Add(item);           
        }

        void GetListApp()
        {
            using (FASEntities Fas = new FASEntities()) //131,37
            {   short[] sh = new short[]{4,3,5,2,24, 37};
                ListApp.AddRange(Fas.FAS_Applications.Where(c => sh.Contains(c.App_ID)).Select(c => c.App_Name).AsEnumerable());
            }
        }

        bool CheckIndex() //Проверка номера индека
        {
            if (listBox1.SelectedIndex == -1)
            { MessageBox.Show("Выберите модуль");  return false; }
            //if (listBox1.SelectedIndex == 1 || listBox1.SelectedIndex == 2)
            //    return true;
            return true;

        }

        void OpenModule(BaseClass BC) //Открытие определенного класса
        {            
            SettingsForm settingsForm = new SettingsForm(BC);           
            var r = settingsForm.ShowDialog();           
        }

        void OpenModule() //Открытие определенного класса
        {
            FAS_LOT_Managment Managment = new FAS_LOT_Managment();
            Managment.ShowDialog();
        }

        void OpenModuleAbortSN() //Открытие определенного класса
        {
            AbortSNcs AbortForm = new AbortSNcs();
            AbortForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void BT_OK_Click(object sender, EventArgs e)
        {

        }
    }
}
