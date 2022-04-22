using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_STB
{
    public class RocksoftCrcModel
    {
        /// <summary>Таблица предвычисленных значений для расчёта контрольной суммы.</summary>
        public readonly uint[] CrcLookupTable;
        private int _CrcWidth;
        private uint _Polynom;
        private bool _ReflectIn;
        private uint _InitRegister;
        private bool _ReflectOut;
        private uint _XorOut;
        private uint _TopBit;
        private uint _WidMask;

        /// <summary>
        /// Порядок CRC, в битах (8/16/32).
        /// Изменение этого свойства ведёт к пересчёту таблицы.
        /// </summary>
        public int CrcWidth
        {
            get
            {
                return this._CrcWidth;
            }
            set
            {
                if (this._CrcWidth == value)
                    return;
                this._CrcWidth = value;
                this._TopBit = this.getBitMask(checked(this._CrcWidth - 1));
                this._WidMask = (uint)((int)checked(unchecked((uint)(1 << checked(this._CrcWidth - 1))) - 1U) << 1 | 1);
                this.generateLookupTable();
            }
        }

        /// <summary>
        /// Образующий многочлен.
        /// Изменение этого свойства ведёт к пересчёту таблицы.
        /// </summary>
        public uint Polynom
        {
            get
            {
                return this._Polynom;
            }
            set
            {
                if ((int)this._Polynom == (int)value)
                    return;
                this._Polynom = value;
                this.generateLookupTable();
            }
        }

        /// <summary>
        /// Обращать байты сообщения?
        /// Изменение этого свойства ведёт к пересчёту таблицы.
        /// </summary>
        public bool ReflectIn
        {
            get
            {
                return this._ReflectIn;
            }
            set
            {
                if (this._ReflectIn == value)
                    return;
                this._ReflectIn = value;
                this.generateLookupTable();
            }
        }

        /// <summary>Начальное содержимое регистра.</summary>
        public uint InitRegister
        {
            get
            {
                return this._InitRegister;
            }
            set
            {
                if ((int)this._InitRegister == (int)value)
                    return;
                this._InitRegister = value;
            }
        }

        /// <summary>Обращать выходное значение CRC?</summary>
        public bool ReflectOut
        {
            get
            {
                return this._ReflectOut;
            }
            set
            {
                if (this._ReflectOut == value)
                    return;
                this._ReflectOut = value;
            }
        }

        /// <summary>Значение, с которым XOR-ится выходное значение CRC.</summary>
        public uint XorOut
        {
            get
            {
                return this._XorOut;
            }
            set
            {
                if ((int)this._XorOut == (int)value)
                    return;
                this._XorOut = value;
            }
        }

        /// <summary>Возвращает старший разряд полинома.</summary>
        public uint TopBit
        {
            get
            {
                return this._TopBit;
            }
        }

        /// <summary>Возвращает длинное слово со значением (2^width)-1.</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private uint WidMask
        {
            get
            {
                return this._WidMask;
            }
        }

        /// <summary>
        /// Конструктор, инициализированный параметрами по умолчанию для алгоритма CRC32.
        /// </summary>
        public RocksoftCrcModel()
        {
            //base..ctor();
            this.CrcLookupTable = new uint[256];
            this._CrcWidth = 32;
            this._Polynom = 79764919U;
            this._ReflectIn = true;
            this._InitRegister = uint.MaxValue;
            this._ReflectOut = true;
            this._XorOut = uint.MaxValue;
            this._TopBit = this.getBitMask(checked(this.CrcWidth - 1));
            this._WidMask = (uint)((int)checked(unchecked((uint)(1 << checked(this.CrcWidth - 1))) - 1U) << 1 | 1);
            this.generateLookupTable();
        }

        /// <summary>
        /// Инициализирует новый экземпляр параметрической модели CRC с настраиваемыми параметрами.
        /// </summary>
        /// <param name="width">Разрядность контрольной суммы в битах.</param>
        /// <param name="poly">Полином.</param>
        /// <param name="initReg">начальное содержимое регистра.</param>
        /// <param name="isReflectIn">Обращать ли входящие байты сообщения?</param>
        /// <param name="isReflectOut">Обратить ли CRC перед финальным XOR.</param>
        /// <param name="xorOut">Конечное значение XOR.</param>
        public RocksoftCrcModel(int width, uint poly, uint initReg = 4294967295, bool isReflectIn = true, bool isReflectOut = true, uint xorOut = 4294967295)
        {
            //base..ctor();
            this.CrcLookupTable = new uint[256];
            this._CrcWidth = 32;
            this._Polynom = 79764919U;
            this._ReflectIn = true;
            this._InitRegister = uint.MaxValue;
            this._ReflectOut = true;
            this._XorOut = uint.MaxValue;
            this._TopBit = this.getBitMask(checked(this.CrcWidth - 1));
            this._WidMask = (uint)((int)checked(unchecked((uint)(1 << checked(this.CrcWidth - 1))) - 1U) << 1 | 1);
            this.CrcWidth = width;
            this.Polynom = poly;
            this.InitRegister = initReg;
            this.ReflectIn = isReflectIn;
            this.ReflectOut = isReflectOut;
            this.XorOut = xorOut;
            this.generateLookupTable();
        }

        /// <summary>Вычисляет значение контрольной суммы переданного сообщения.</summary>
        /// <param name="message">Исходное сообщение, для которого нужно посчитать контрольную сумму.</param>
        /// <returns></returns>
        public uint ComputeCrc(ref byte[] message)
        {
            uint num1 = this.InitRegister;
            byte[] numArray = message;
            int index = 0;
            while (index < numArray.Length)
            {
                byte num2 = numArray[index];
                num1 = this.getNextRegisterContent(num1, num2);
                checked { ++index; }
            }
            return this.getFinalCrc(num1);
        }

        /// <summary>
        /// Вычисляет значение контрольной суммы переданного сообщения и возвращает его в виде массива байтов.
        /// </summary>
        /// <param name="message">Исходное сообщение, для которого нужно посчитать контрольную сумму.</param>
        /// <returns></returns>
        public byte[] ComputeCrcAsBytes(byte[] message)
        {
            byte[] bytes = BitConverter.GetBytes(this.ComputeCrc(ref message));
            byte[] numArray = new byte[checked(bytes.Length - 1 + 1)];
            int num1 = 0;
            int num2 = checked(bytes.Length - 1);
            int index = num1;
            while (index <= num2)
            {
                numArray[index] = bytes[checked(bytes.Length - 1 - index)];
                checked { ++index; }
            }
            return numArray;
        }

        /// <summary>Обрабатывает один байт сообщения (0..255).</summary>
        /// <param name="prevRegContent">Содержимое регистра на предыдущем шаге.</param>
        /// <param name="value">Значение очередного байта из сообщения.</param>
        private uint getNextRegisterContent(uint prevRegContent, byte value)
        {
            uint num1 = (uint)value;
            if (this.ReflectIn)
                num1 = this.reflect(num1, 8);
            uint num2 = prevRegContent ^ num1 << checked(this.CrcWidth - 8);
            int num3 = 0;
            do
            {
                num2 = (((int)num2 & (int)this.TopBit) != (int)this.TopBit ? num2 << 1 : num2 << 1 ^ this.Polynom) & this.WidMask;
                checked { ++num3; }
            }
            while (num3 <= 7);
            return num2;
        }

        /// <summary>Возвращает значение CRC для обработанного сообщения.</summary>
        /// <param name="regContent">Значение регистра до финального обращения и XORа.</param>
        /// <returns></returns>
        private uint getFinalCrc(uint regContent)
        {
            if (this.ReflectOut)
                return this.XorOut ^ this.reflect(regContent, this.CrcWidth);
            return this.XorOut ^ regContent;
        }

        /// <summary>Вычисляет таблицу предвычисленных значений для расчёта контрольной суммы.</summary>
        private void generateLookupTable()
        {
            int index = 0;
            do
            {
                this.CrcLookupTable[index] = this.generateTableItem(index);
                checked { ++index; }
            }
            while (index <= (int)byte.MaxValue);
        }

        /// <summary>
        /// Рассчитывает один байт таблицы значений для расчёта контрольной суммы
        /// по алгоритму Rocksoft^tm Model CRC Algorithm.
        /// </summary>
        /// <param name="index">Индекс записи в таблице, 0..255.</param>
        private uint generateTableItem(int index)
        {
            uint num1 = checked((uint)index);
            if (this.ReflectIn)
                num1 = this.reflect(num1, 8);
            uint num2 = num1 << checked(this.CrcWidth - 8);
            int num3 = 0;
            do
            {
                if (((int)num2 & (int)this.TopBit) == (int)this.TopBit)
                    num2 = num2 << 1 ^ this.Polynom;
                else
                    num2 <<= 1;
                checked { ++num3; }
            }
            while (num3 <= 7);
            if (this.ReflectIn)
                num2 = this.reflect(num2, this.CrcWidth);
            return num2 & this.WidMask;
        }

        /// <summary>Возвращает наибольший разряд числа.</summary>
        /// <param name="number">Число, разрядность которого следует определить, степень двойки.</param>
        /// <returns></returns>
        private uint getBitMask(int number)
        {
            return (uint)(1 << number);
        }

        /// <summary>Обращает заданное число младших битов переданного числа.</summary>
        /// <param name="value">Число, которое требуется обратить ("отзеркалить").</param>
        /// <param name="bitsToReflect">Сколько младших битов числа обратить, 0..32.</param>
        /// <returns></returns>
        /// <remarks>Например: reflect(0x3E23, 3) == 0x3E26.</remarks>
        private uint reflect(uint value, int bitsToReflect = 32)
        {
            uint num1 = value;
            uint num2 = value;
            int num3 = 0;
            int num4 = checked(bitsToReflect - 1);
            int num5 = num3;
            while (num5 <= num4)
            {
                uint bitMask = this.getBitMask(checked(bitsToReflect - 1 - num5));
                if (((long)num1 & 1L) == 1L)
                    num2 |= bitMask;
                else
                    num2 &= ~bitMask;
                num1 >>= 1;
                checked { ++num5; }
            }
            return num2;
        }
    }
}
