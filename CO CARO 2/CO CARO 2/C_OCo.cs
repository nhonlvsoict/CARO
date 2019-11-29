using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CO_CARO_2
{
    class C_OCo
    {
        public const int CHIEU_RONG = 30;
        public const int CHIEU_CAO = 30;

        private int _dong;

        public int Dong
        {
            get { return _dong; }
            set { _dong = value; }
        }
        private int _cot;

        public int Cot
        {
            get { return _cot; }
            set { _cot = value; }
        }
        private int _soHuu;

        //SoHuu ==1 là máy; = 2 là người
        public int SoHuu
        {
            get { return _soHuu; }
            set { _soHuu = value; }
        }

        public C_OCo(int Dong, int Cot, int SoHuu)
        {
            _dong = Dong;
            _cot = Cot;
            _soHuu = SoHuu;
        }

        public C_OCo()
        {
 
        }
    }
}
