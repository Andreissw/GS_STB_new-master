using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    public class FAS_Weight_Control 
    {
        int MinDifference { get; set; }
        int MaxDifference { get; set; }
  
        public int Weight { get; set; }
        public string Message { get; set; }
        public FAS_Weight_Control(int minDifference, int maxDifference, int weight)
        {
            MinDifference = minDifference;
            MaxDifference = maxDifference;
            Weight = weight;
        }

        public FAS_Weight_Control()
        { 
        
        }

        public bool ControlWeight()
        {
            if (Weight <= 0)
            {
                Message = $"Недопустимый параметр веса {Weight}, параметр должен быть больше 0";
                return false;
            }

            var weight = GetWeight();

            if (weight == -1)
            {
                Message = $"Ошибка получение веса";
                return false;
            }

            if (weight == 0)
            {
                Message = $"Весовой контроль вернул значение веса {weight}. Взвешивание не удалось\nили вы забыли положить объект на весы";
                return false;
            }

            if ((Weight - MinDifference) > weight)
            {
                Message = $"Недостаточно веса для минимальной нормы\n минимальная норма {(Weight - MinDifference)}\nВес объекта {weight}\nНе хватает до минмальной нормы {(Weight - MinDifference) - weight}";
                return false;
            }

            if ((Weight + MaxDifference) < weight)
            {
                Message = $"Переизбыток веса для максимальной нормы\n максимальная норма {(Weight - MinDifference)}\nВес объекта {weight}\nПереизбыток состовляет до максимальной нормы {weight - (Weight + MinDifference)}";
                return false;
            }

            Message = $"Объект прошел весовой контроль\n его вес состовляет {weight}";
            Weight = weight;
            return true;
        }

        public int GetWeight()
        {            
            int intSize = 0;
            string W = "";
            var arr = new byte[1024];
            var serial = GetSerialPort();
            try
            {
                serial.Open();            
                for (int i = 0; i < 3; i++)
                {
                    W = string.Empty;
                    serial.Write($"000000 {Environment.NewLine}");
                    Thread.Sleep(100);
                                
                    while (serial.BytesToRead > 0)               
                        intSize = serial.Read(arr, 0,1024);
               

                    var ResHex = BitConverter.ToString(arr, 0, intSize).Replace("-","");
                    if (ResHex.Contains("0D1E"))
                    {
                        var result = ResHex.Substring(28,6);
                    
                        for (int b = 0; b < result.Length; b += 2)                    
                            W += result.Substring(b+ 1, 1).ToString();

                        serial.Write($"! {Environment.NewLine}");
                        break;                    
                    }

                }
                serial.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.ToString()}");
                return -1;
            }

            int _w;

            if (!int.TryParse(W, out _w))            
                return 0;            
            
            return _w;
        }

        SerialPort GetSerialPort()
        {
            var serial = new SerialPort() {
                BaudRate = 9600
                ,DataBits = 8
                ,DiscardNull = false
                ,DtrEnable = false
                ,ParityReplace = 63
                ,ReadBufferSize = 4096
                ,PortName = GetPortName()
                ,ReadTimeout = -1
                ,ReceivedBytesThreshold = 1
                ,RtsEnable = false
                ,StopBits = StopBits.One
                ,WriteBufferSize = 2048
                ,WriteTimeout = -1          
            };

            return serial;

        }

        string GetPortName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
            var R = searcher.Get();
            try
            {
                foreach (var item in R)
                {
                    var Result = item["Name"];
                    if (Result != null)
                        if (Result.ToString().Contains("COM"))
                        {
                            var Name = item["Name"].ToString();
                            if (Name.IndexOf("COM") == 17)
                            {
                                if (Name.Substring(20,2) == "10")
                                {
                                    return Name.Substring(17,5);
                                }
                                return Name.Substring(17,4); 
                            }

                        }
                }

                return "COM9";

            }
            catch (Exception)
            {

                return "COM9";
            }
        }


    }
}
