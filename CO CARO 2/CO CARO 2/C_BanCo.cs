using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CO_CARO_2
{
    class C_BanCo
    {
        //khai báo 2 ảnh để vẽ ảnh lên bàn cờ
        Image ImageO = new Bitmap(Properties.Resources.o);
        Image ImageX = new Bitmap(Properties.Resources.x);
        Image ImageEmpty = new Bitmap(Properties.Resources.empty);

        private int _soDong;

        public int SoDong
        {
            get { return _soDong; }
            set { _soDong = value; }
        }
        private int _soCot;

        public int SoCot
        {
            get { return _soCot; }
            set { _soCot = value; }
        }

        public C_BanCo()
        {
            _soCot = 0;
            _soDong = 0;
        }

        public C_BanCo(int SoDong, int SoCot)
        {
            _soCot = SoCot;
            _soDong = SoDong;
        }

        //vẽ bàn cờ
        public void veBanCo(Graphics g)
        {
            //vẽ cột
            for (int i = 0; i <= _soCot; i++)
            {
                g.DrawLine(C_DieuKhien.pen, i * C_OCo.CHIEU_RONG, 0, i * C_OCo.CHIEU_RONG, _soDong * C_OCo.CHIEU_CAO);
            }
            //vẽ dòng
            for (int i = 0; i <= _soDong; i++)
            {
                g.DrawLine(C_DieuKhien.pen, 0, i * C_OCo.CHIEU_CAO, _soCot * C_OCo.CHIEU_RONG, i * C_OCo.CHIEU_CAO);
            }
        }
        
        //vẽ quân cờ
        public void veQuanCo(Graphics g, int X, int Y, int SoHuu)
        {
            //quân đen
            if (SoHuu == 1)
            {
                g.DrawImage(ImageO, X, Y);
                //g.FillEllipse(C_DieuKhien.sbBlack, X+2, Y+2, C_OCo.CHIEU_RONG-4, C_OCo.CHIEU_CAO-4);

            }
            else//quân trắng
            {
                //g.FillEllipse(C_DieuKhien.sbWhite, X+2, Y+2, C_OCo.CHIEU_RONG-4, C_OCo.CHIEU_CAO-4);
                g.DrawImage(ImageX, X+2, Y+2);
            }
        }

        public void xoaQuanCo(Graphics g, int X, int Y)
        {
            g.DrawImage(ImageEmpty, X+1 , Y+1 );
        }
    }
}
