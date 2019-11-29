using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CO_CARO_2
{
    class C_DieuKhien
    {
        //3 loại bút vẽ
        public static Pen pen;
        public static SolidBrush sbBlack;
        public static SolidBrush sbWhite;


        private Random rd = new Random();//random ô cờ máy sẽ đánh đầu tiên, và random lượt đi đầu tiên
        private C_BanCo BanCo;
        private C_OCo[,] MangOCo;
        private int _cheDoChoi;
        private int _luotDi;
        //_luotDi ==1 là máy, 2 là người
        private bool _sanSang;
        private int MaxDepth = 2;
        private const int INFINITY = 10000000;
        private C_OCo oco = new C_OCo();
        private Panel _panelGayBan;
        private Label _textGayBan;
        private PictureBox _pictureChuoi1;

        public PictureBox PictureChuoi1
        {
            get { return _pictureChuoi1; }
            set { _pictureChuoi1 = value; }
        }

        public Panel PanelGayBan
        {
            get { return _panelGayBan; }
            set { _panelGayBan = value; }
        }

        public Label TextGayBan
        {
            get { return _textGayBan; }
            set { _textGayBan = value; }
        }

        public int LuotDi
        {
            get { return _luotDi; }
            set { _luotDi = value; }
        }
        public bool SanSang
        {
            get { return _sanSang; }
            set { _sanSang = value; }
        }
        private Stack<C_OCo> _stkCacNuocDaDi;

        public int CheDoChoi
        {
            get { return _cheDoChoi; }
            set { _cheDoChoi = value; }
        }

        public C_DieuKhien()
        {
            //khởi tạo bàn cờ với số dòng 20, số cột 20
            BanCo = new C_BanCo(fmCoCaro.ChieuCaoBanCo / C_OCo.CHIEU_CAO, fmCoCaro.ChieuRongBanCo / C_OCo.CHIEU_RONG);
            //khởi tạo 3 loại bút vẽ
            pen = new Pen(Color.DarkKhaki, 1);
            sbBlack = new SolidBrush(Color.Black);
            sbWhite = new SolidBrush(Color.White);

            _sanSang = false;
            //khai báo mảng các ô cờ 
            MangOCo = new C_OCo[BanCo.SoDong, BanCo.SoCot];
        }

        //vẽ bàn cờ
        public void veBanCo(Graphics g)
        {
            BanCo.veBanCo(g);
        }

        //khởi tạo mảng ô cờ
        public void khoiTaoMangOCo()
        {
            for (int i = 0; i < BanCo.SoDong; i++)
                for (int j = 0; j < BanCo.SoCot; j++)
                {
                    MangOCo[i, j] = new C_OCo(i, j, 0);
                }
        }

        // đánh cờ
        public void danhCo(Graphics g, int mouseX, int mouseY)
        {
            int dong = mouseY / C_OCo.CHIEU_CAO;
            int cot = mouseX / C_OCo.CHIEU_RONG;

            //loại bỏ trường hợp người chơi kích vào giữa đường kẻ vạch
            try
            {
                if (mouseY % C_OCo.CHIEU_CAO != 0 && mouseX % C_OCo.CHIEU_RONG != 0)
                {
                    //chỉ đánh vào những ô trống
                    if (MangOCo[dong, cot].SoHuu == 0)
                    {
                        //quân đen đánh
                        if (_luotDi == 1)
                        {
                            BanCo.veQuanCo(g, cot * C_OCo.CHIEU_CAO, dong * C_OCo.CHIEU_RONG, _luotDi);
                            MangOCo[dong, cot].SoHuu = 1;

                            //sao chép o cờ ra một vùng nhớ mới để đẩy vào stack
                            C_OCo OCo = new C_OCo(MangOCo[dong, cot].Dong, MangOCo[dong, cot].Cot, MangOCo[dong, cot].SoHuu);
                            //sau khi đánh xong thì đẩy o cờ vào trong stack
                            _stkCacNuocDaDi.Push(OCo);

                            _luotDi = 2;
                        }
                        //quân trắng đánh
                        else
                        {
                            BanCo.veQuanCo(g, cot * C_OCo.CHIEU_CAO, dong * C_OCo.CHIEU_RONG, _luotDi);
                            MangOCo[dong, cot].SoHuu = 2;
                            //sao chép o cờ ra một vùng nhớ mới để đẩy vào stack
                            C_OCo OCo = new C_OCo(MangOCo[dong, cot].Dong, MangOCo[dong, cot].Cot, MangOCo[dong, cot].SoHuu);
                            //sau khi đánh xong thì đẩy o cờ vào trong stack
                            _stkCacNuocDaDi.Push(OCo);

                            _luotDi = 1;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException e) {
                Console.WriteLine(e);
            };
        }

        //Delete move
        public void deleteMove(Graphics g, int moveX, int moveY, int botX, int botY)
        {
                //Console.WriteLine(moveX +" "+ moveY);
                int dong = moveY / C_OCo.CHIEU_CAO;
                int cot = moveX / C_OCo.CHIEU_RONG;
                MangOCo[dong, cot].SoHuu = 0;
                int dongBot = botY / C_OCo.CHIEU_CAO;
                int cotBot = botX / C_OCo.CHIEU_RONG;
                MangOCo[dongBot, cotBot].SoHuu = 0;
            //Console.WriteLine(botX + " " + botY);
            //Console.WriteLine(MangOCo[dongBot, cotBot].SoHuu);
            if (_stkCacNuocDaDi.Count != 0 && _sanSang)
            {
                _stkCacNuocDaDi.Pop();
                BanCo.veQuanCo(g, cot * C_OCo.CHIEU_CAO + 1, dong * C_OCo.CHIEU_RONG + 1, 0);
                _stkCacNuocDaDi.Pop();
                BanCo.veQuanCo(g, cotBot * C_OCo.CHIEU_CAO + 1, dongBot * C_OCo.CHIEU_RONG + 1, 0);
            }
        }

        //Undo move
        public void undoMove(Graphics g, int moveX, int moveY, int botX, int botY)
        {
            try
            {
                if (_luotDi == 0) return;
                //if (_luotDi == 1)
                //{
                //    MessageBox.Show("You cannot undo!");
                //    return;
                //}
                if (_luotDi == 2 || _luotDi == 1)
                {
                    if (!_sanSang)
                    {
                        MessageBox.Show("The battle has finished, cannot undo!");
                    }
                    else
                        deleteMove(g, moveX, moveY, botX, botY);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
            };
        }

        //vẽ lại quân cờ
        public void veLaiQuanCo(Graphics g)
        {
            //trong stack có quân cờ thì thực hiện vẽ lại quân cờ
            if (_stkCacNuocDaDi.Count != 0)
            {
                foreach (C_OCo oco in _stkCacNuocDaDi)
                {
                    BanCo.veQuanCo(g, oco.Cot * C_OCo.CHIEU_RONG, oco.Dong * C_OCo.CHIEU_CAO, oco.SoHuu);
                }
            }
        }

        //chơi với người
        public void choiVoiNguoi(Graphics g)
        {
            //chơi với người
            _cheDoChoi = 1;
            //random lượt đi
            _luotDi = rd.Next(0, 2);
            if (_luotDi == 1)
            {
                MessageBox.Show("Red player goes first");
            }
            else MessageBox.Show("Green player goes first");
            _sanSang = true;
            //khởi tạo mảng ô cờ
            khoiTaoMangOCo();
            //khởi tạo lại stack
            _stkCacNuocDaDi = new Stack<C_OCo>();
            //vẽ bàn cờ
            //veBanCo(g);
        }

        //chơi với máy
        public void choiVoiMay(Graphics g)
        {
            //chơi với máy
            _cheDoChoi = 2;
            //random lượt đi
            //_luotDi = rd.Next(0, 2);
            _luotDi = 1;
            //if (_luotDi == 1)
            //{
            //    MessageBox.Show("AI goes first");
            //}
            //else MessageBox.Show("Player goes first");

            _sanSang = true;
            khoiTaoMangOCo();
            _stkCacNuocDaDi = new Stack<C_OCo>();
            //veBanCo(g);
            mayDanh(g);
        }

        //máy đánh
        public void mayDanh1(Graphics g)
        {
            int DiemMax = 0;
            int DiemPhongNgu = 0;
            int DiemTanCong = 0;
            C_OCo oco = new C_OCo();

            if (_luotDi == 1)
            {
                //lượt đi đầu tiên sẽ đánh random trong khoảng chính giữa đến 3 nước trên bàn cờ
                if (_stkCacNuocDaDi.Count == 0)
                {
                    danhCo(g, rd.Next((BanCo.SoCot / 2 - 3) * C_OCo.CHIEU_RONG + 1, (BanCo.SoCot / 2 + 3) * C_OCo.CHIEU_RONG + 1), rd.Next((BanCo.SoDong / 2 - 3) * C_OCo.CHIEU_CAO, (BanCo.SoDong / 2 + 3) * C_OCo.CHIEU_CAO));
                }
                else
                {
                    //thuật toán minmax tìm điểm cao nhất để đánh
                    for (int i = 0; i < BanCo.SoDong; i++)
                    {
                        for (int j = 0; j < BanCo.SoCot; j++)
                        {
                            //nếu nước cờ chưa có ai đánh và không bị cắt tỉa thì mới xét giá trị MinMax
                            if (MangOCo[i, j].SoHuu == 0 && !catTia(MangOCo[i, j]))
                            {
                                int DiemTam;

                                DiemTanCong = duyetTC_Ngang(i, j) + duyetTC_Doc(i, j) + duyetTC_CheoXuoi(i, j) + duyetTC_CheoNguoc(i, j);
                                DiemPhongNgu = duyetPN_Ngang(i, j) + duyetPN_Doc(i, j) + duyetPN_CheoXuoi(i, j) + duyetPN_CheoNguoc(i, j);

                                if (DiemPhongNgu > DiemTanCong)
                                {
                                    DiemTam = DiemPhongNgu;
                                }
                                else
                                {
                                    DiemTam = DiemTanCong;
                                }

                                if (DiemMax < DiemTam)
                                {
                                    DiemMax = DiemTam;
                                    oco = new C_OCo(MangOCo[i, j].Dong, MangOCo[i, j].Cot, MangOCo[i, j].SoHuu);
                                }
                            }
                        }
                    }

                    danhCo(g, oco.Cot * C_OCo.CHIEU_RONG + 1, oco.Dong * C_OCo.CHIEU_CAO + 1);
                }
            }
        }

        public KeyValuePair<int, int> mayDanh(Graphics g)
        {
            //int DiemMax = 0;
            //int DiemPhongNgu = 0;
            //int DiemTanCong = 0;
            //C_OCo oco = new C_OCo();
            var pair = new KeyValuePair<int, int>();

            if (_luotDi == 1)
            {
                //lượt đi đầu tiên sẽ đánh random trong khoảng chính giữa đến 3 nước trên bàn cờ
                if (_stkCacNuocDaDi.Count == 0)
                {
                    danhCo(g, rd.Next((BanCo.SoCot / 2 - 3) * C_OCo.CHIEU_RONG + 1, (BanCo.SoCot / 2 + 3) * C_OCo.CHIEU_RONG + 1), rd.Next((BanCo.SoDong / 2 - 3) * C_OCo.CHIEU_CAO, (BanCo.SoDong / 2 + 3) * C_OCo.CHIEU_CAO));
                    pair = new KeyValuePair<int, int>(rd.Next((BanCo.SoCot / 2 - 3) * C_OCo.CHIEU_RONG + 1, (BanCo.SoCot / 2 + 3) * C_OCo.CHIEU_RONG + 1), rd.Next((BanCo.SoDong / 2 - 3) * C_OCo.CHIEU_CAO, (BanCo.SoDong / 2 + 3) * C_OCo.CHIEU_CAO));
                }
                else
                {
                    //thuật toán minmax tìm điểm cao nhất để đánh

                    int x = MaxValue(MangOCo, 0, 0, 1, -INFINITY, INFINITY);

                    danhCo(g, oco.Cot * C_OCo.CHIEU_RONG + 1, oco.Dong * C_OCo.CHIEU_CAO + 1);
                    pair = new KeyValuePair<int, int>(oco.Cot * C_OCo.CHIEU_RONG + 1, oco.Dong * C_OCo.CHIEU_CAO + 1);

                }
            }
            return pair;
        }

        private int MaxValue(C_OCo[,] b, int m, int n, int depth, int alpha, int beta)
        {
            if (depth >= MaxDepth || (depth != 1 && gameOver(b, m, n)))
                return Analysis(b, m, n, 2, 1);
            int max = -INFINITY;
            for (int i = 0; i < BanCo.SoDong; i++)
            {
                for (int j = 0; j < BanCo.SoCot; j++)
                {
                    //nếu nước cờ chưa có ai đánh và không bị cắt tỉa thì mới xét giá trị MinMax
                    if (b[i, j].SoHuu == 0 && !catTia(b[i, j]))
                    {
                        C_OCo[,] c = (C_OCo[,])b.Clone();
                        c[i, j].SoHuu = 1;
                        int x = MinValue(c, i, j, depth + 1, alpha, beta);
                        c[i, j].SoHuu = 0;
                        if (x > max)
                        {
                            max = x;
                            if (depth == 1)
                                oco = new C_OCo(MangOCo[i, j].Dong, MangOCo[i, j].Cot, MangOCo[i, j].SoHuu);
                        }
                        if (x > alpha) alpha = x;
                        if (alpha >= beta) return alpha;
                    }
                }
            }
            return max;
        }
        private int MinValue(C_OCo[,] b, int m, int n, int depth, int alpha, int beta)
        {
            if (depth >= MaxDepth || gameOver(b, m, n))
                return Analysis(b, m, n, 1, 2);
            int min = INFINITY;
            for (int i = 0; i < BanCo.SoDong; i++)
            {
                for (int j = 0; j < BanCo.SoCot; j++)
                {
                    //nếu nước cờ chưa có ai đánh và không bị cắt tỉa thì mới xét giá trị MinMax
                    if (b[i, j].SoHuu == 0 && !catTia(b[i, j]))
                    {
                        C_OCo[,] c = (C_OCo[,])b.Clone();
                        c[i, j].SoHuu = 2;
                        int x = MaxValue(c, i, j, depth + 1, alpha, beta);
                        c[i, j].SoHuu = 0;
                        if (x < min) min = x;
                        if (x < beta) beta = x;
                        if (alpha >= beta) return beta;
                    }
                }
            }
            return min;
        }

        #region check gameOver
        bool gameOver(C_OCo[,] MangOCo, int m, int n)
        {
            foreach (C_OCo oco in MangOCo)
            {
                //duyệt theo 8 hướng mỗi quân cờ
                if (gameOverNgangPhai(MangOCo, m, n, MangOCo[m,n].SoHuu) || gameOverNgangTrai(MangOCo, m, n, MangOCo[m,n].SoHuu)
                    || gameOverDocTren(MangOCo, m, n, MangOCo[m,n].SoHuu) || gameOverDocDuoi(MangOCo, m, n, MangOCo[m,n].SoHuu)
                    || gameOverCheoXuoiTren(MangOCo, m, n, MangOCo[m,n].SoHuu) || gameOverCheoXuoiDuoi(MangOCo, m, n, MangOCo[m,n].SoHuu)
                    || gameOverCheoNguocTren(MangOCo, m, n, MangOCo[m,n].SoHuu) || gameOverCheoNguocDuoi(MangOCo, m, n, MangOCo[m,n].SoHuu))
                {
                    return true;
                }
            }
            return false;
        }

        public bool gameOverNgangPhai(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, (cotHT) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2, (cotHT + 5) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2);
            return true;
        }

        public bool gameOverNgangTrai(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2, (cotHT - 4) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2);
            return true;
        }

        public bool gameOverDocTren(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT + 1) * C_OCo.CHIEU_CAO, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool gameOverDocDuoi(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, dongHT * C_OCo.CHIEU_CAO, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool gameOverCheoXuoiTren(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4 || cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, (dongHT + 1) * C_OCo.CHIEU_CAO, (cotHT - 4) * C_OCo.CHIEU_RONG, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool gameOverCheoXuoiDuoi(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5 || cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO, (cotHT + 5) * C_OCo.CHIEU_RONG, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool gameOverCheoNguocDuoi(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5 || cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO, (cotHT - 4) * C_OCo.CHIEU_RONG, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool gameOverCheoNguocTren(C_OCo[,] MangOCo, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4 || cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            //veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG, (dongHT + 1) * C_OCo.CHIEU_CAO, (cotHT + 5) * C_OCo.CHIEU_RONG, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        #endregion

        #region Analysis

        private int Analysis(C_OCo[,] MangOCo, int i, int j, int ta, int dich)
        {
            int DiemTam = 0;
            int DiemPhongNgu = 0;
            int DiemTanCong = 0;
            //C_OCo oco = new C_OCo();

            //nếu nước cờ chưa có ai đánh và không bị cắt tỉa thì mới xét giá trị MinMax
            DiemTanCong = analysisTC_Ngang(i, j, ta, dich) 
                + analysisTC_Doc(i, j, ta, dich) 
                + analysisTC_CheoXuoi(i, j, ta, dich) 
                + analysisTC_CheoNguoc(i, j, ta, dich);
            DiemPhongNgu = analysisPN_Ngang(i, j, ta, dich) 
                + analysisPN_Doc(i, j, ta, dich) 
                + analysisPN_CheoXuoi(i, j, ta, dich) 
                + analysisPN_CheoNguoc(i, j, ta, dich);

            if (DiemPhongNgu > DiemTanCong)
            {
                DiemTam = DiemPhongNgu;
            }
            else
            {
                DiemTam = DiemTanCong;
            }

            if (ta == 1) return DiemTam;
            else return -DiemTam;
        }
        #region AI

        private int[] MangDiemTanCong = new int[7] { 0, 4, 25, 246, 7300, 6561, 59049 };
        private int[] MangDiemPhongNgu = new int[7] { 0, 3, 24, 243, 2197, 19773, 177957 };
        //private int[] MangDiemPhongNgu = new int[7] { 0, 1, 9, 81, 729, 6561, 59049 };
        #region Tấn công
        //duyệt ngang
        public int analysisTC_Ngang(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichPhai = 0;
            int SoQuanDichTrai = 0;
            int KhoangChong = 0;

            //bên phải
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5; dem++)
            {

                if (MangOCo[dongHT, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;
                }
                else
                    if (MangOCo[dongHT, cotHT + dem].SoHuu == dich)
                {
                    SoQuanDichPhai++;
                    break;
                }
                else KhoangChong++;
            }
            //bên trái
            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT, cotHT - dem].SoHuu == dich)
                {
                    SoQuanDichTrai++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichPhai > 0 && SoQuanDichTrai > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichPhai + SoQuanDichTrai];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //duyệt dọc
        public int analysisTC_Doc(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichTren = 0;
            int SoQuanDichDuoi = 0;
            int KhoangChong = 0;

            //bên trên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT].SoHuu == dich)
                {
                    SoQuanDichTren++;
                    break;
                }
                else KhoangChong++;
            }
            //bên dưới
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT].SoHuu == dich)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo xuôi
        public int analysisTC_CheoXuoi(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemTanCong = 1;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //bên chéo xuôi xuống
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == dich)
                {
                    SoQuanDichCheoTren++;
                    break;
                }
                else KhoangChong++;
            }
            //chéo xuôi lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == dich)
                {
                    SoQuanDichCheoDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo ngược
        public int analysisTC_CheoNguoc(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //chéo ngược lên
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == dich)
                {
                    SoQuanDichCheoTren++;
                    break;
                }
                else KhoangChong++;
            }
            //chéo ngược xuống
            for (int dem = 1; dem <= 4 && cotHT > 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == dich)
                {
                    SoQuanDichCheoDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }
        #endregion

        #region phòng ngự

        //duyệt ngang
        public int analysisPN_Ngang(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongPhai = 0;
            int KhoangChongTrai = 0;
            bool ok = false;

            //ben phai
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5; dem++)
            {
                if (MangOCo[dongHT, cotHT + dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongPhai++;
                }
            }

            // nếu có 3 địch và địch đó cách quân vừa đánh 1 con về bên phải
            //(nghia la có 1 khoang trong và khoang trong do ngay sat ben phai: ok = true)
            //thi tru diem phong ngu nhieu
            if (SoQuanDich == 3 && KhoangChongPhai == 1 && ok)
                DiemPhongNgu -= 200;


            //ben trai
            ok = false;

            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTrai++;
                }
            }
            //tuong tu nhu tren 
            if (SoQuanDich == 3 && KhoangChongTrai == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTrai + KhoangChongPhai + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaTrai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //duyệt dọc
        public int analysisPN_Doc(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                //gặp quân địch
                if (MangOCo[dongHT + dem, cotHT].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];
            return DiemPhongNgu;
        }

        //chéo xuôi
        public int analysisPN_CheoXuoi(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5 && cotHT < BanCo.SoCot - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaTrai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //chéo ngược
        public int analysisPN_CheoNguoc(int dongHT, int cotHT, int ta, int dich)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT < BanCo.SoCot - 5; dem++)
            {

                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }


            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;

            //xuống
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == dich)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == ta)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        #endregion

        #endregion

        #endregion


        #region Cắt tỉa Alpha betal
        bool catTia(C_OCo oCo)
        {
            //nếu cả 4 hướng đều không có nước cờ thì cắt tỉa
            if (catTiaNgang(oCo) && catTiaDoc(oCo) && catTiaCheoPhai(oCo) && catTiaCheoTrai(oCo))
                return true;

            //chạy đến đây thì 1 trong 4 hướng vẫn có nước cờ thì không được cắt tỉa
            return false;
        }

        bool catTiaNgang(C_OCo oCo)
        {
            //duyệt bên phải
            if (oCo.Cot <= BanCo.SoCot - 5)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong, oCo.Cot + i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //duyệt bên trái
            if (oCo.Cot >= 4)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong, oCo.Cot - i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //nếu chạy đến đây tức duyệt 2 bên đều không có nước đánh thì cắt tỉa
            return true;
        }
        bool catTiaDoc(C_OCo oCo)
        {
            //duyệt phía giưới
            if (oCo.Dong <= BanCo.SoDong - 5)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong + i, oCo.Cot].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //duyệt phía trên
            if (oCo.Dong >= 4)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong - i, oCo.Cot].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //nếu chạy đến đây tức duyệt 2 bên đều không có nước đánh thì cắt tỉa
            return true;
        }
        bool catTiaCheoPhai(C_OCo oCo)
        {
            //duyệt từ trên xuống
            if (oCo.Dong <= BanCo.SoDong - 5 && oCo.Cot >= 4)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong + i, oCo.Cot - i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //duyệt từ giưới lên
            if (oCo.Cot <= BanCo.SoCot - 5 && oCo.Dong >= 4)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong - i, oCo.Cot + i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //nếu chạy đến đây tức duyệt 2 bên đều không có nước đánh thì cắt tỉa
            return true;
        }
        bool catTiaCheoTrai(C_OCo oCo)
        {
            //duyệt từ trên xuống
            if (oCo.Dong <= BanCo.SoDong - 5 && oCo.Cot <= BanCo.SoCot - 5)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong + i, oCo.Cot + i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //duyệt từ giưới lên
            if (oCo.Cot >= 4 && oCo.Dong >= 4)
                for (int i = 1; i <= 4; i++)
                    if (MangOCo[oCo.Dong - i, oCo.Cot - i].SoHuu != 0)//nếu có nước cờ thì không cắt tỉa
                        return false;

            //nếu chạy đến đây tức duyệt 2 bên đều không có nước đánh thì cắt tỉa
            return true;
        }

        #endregion

        #region AI

        //private int[] MangDiemTanCong = new int[7] { 0, 4, 25, 246, 7300, 6561, 59049 };
        //private int[] MangDiemPhongNgu = new int[7] { 0, 3, 24, 243, 2197, 19773, 177957 };
        //private int[] MangDiemPhongNgu = new int[7] { 0, 1, 9, 81, 729, 6561, 59049 };
        #region Tấn công
        //duyệt ngang
        public int duyetTC_Ngang(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichPhai = 0;
            int SoQuanDichTrai = 0;
            int KhoangChong = 0;

            //bên phải
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5; dem++)
            {

                if (MangOCo[dongHT, cotHT + dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;
                }
                else
                    if (MangOCo[dongHT, cotHT + dem].SoHuu == 2)
                    {
                        SoQuanDichPhai++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bên trái
            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT, cotHT - dem].SoHuu == 2)
                    {
                        SoQuanDichTrai++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichPhai > 0 && SoQuanDichTrai > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichPhai + SoQuanDichTrai];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //duyệt dọc
        public int duyetTC_Doc(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichTren = 0;
            int SoQuanDichDuoi = 0;
            int KhoangChong = 0;

            //bên trên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT].SoHuu == 2)
                    {
                        SoQuanDichTren++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bên dưới
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT].SoHuu == 2)
                    {
                        SoQuanDichDuoi++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo xuôi
        public int duyetTC_CheoXuoi(int dongHT, int cotHT)
        {
            int DiemTanCong = 1;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //bên chéo xuôi xuống
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == 2)
                    {
                        SoQuanDichCheoTren++;
                        break;
                    }
                    else KhoangChong++;
            }
            //chéo xuôi lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == 2)
                    {
                        SoQuanDichCheoDuoi++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo ngược
        public int duyetTC_CheoNguoc(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //chéo ngược lên
            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == 2)
                    {
                        SoQuanDichCheoTren++;
                        break;
                    }
                    else KhoangChong++;
            }
            //chéo ngược xuống
            for (int dem = 1; dem <= 4 && cotHT > 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == 2)
                    {
                        SoQuanDichCheoDuoi++;
                        break;
                    }
                    else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }
        #endregion

        #region phòng ngự

        //duyệt ngang
        public int duyetPN_Ngang(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongPhai = 0;
            int KhoangChongTrai = 0;
            bool ok = false;


            for (int dem = 1; dem <= 4 && cotHT < BanCo.SoCot - 5; dem++)
            {
                if (MangOCo[dongHT, cotHT + dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT, cotHT + dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaTrai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongPhai++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongPhai == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;

            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT, cotHT - dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaPhai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongTrai++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongTrai == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTrai + KhoangChongPhai + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //duyệt dọc
        public int duyetPN_Doc(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;

                }
                else
                    if (MangOCo[dongHT - dem, cotHT].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaPhai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongTren++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5; dem++)
            {
                //gặp quân địch
                if (MangOCo[dongHT + dem, cotHT].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaTrai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongDuoi++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];
            return DiemPhongNgu;
        }

        //chéo xuôi
        public int duyetPN_CheoXuoi(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5 && cotHT < BanCo.SoCot - 5; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT + dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaPhai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongTren++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT - dem, cotHT - dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaTrai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongDuoi++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaTrai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //chéo ngược
        public int duyetPN_CheoNguoc(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT < BanCo.SoCot - 5; dem++)
            {

                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT - dem, cotHT + dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaPhai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongTren++;
                    }
            }
            

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;

            //xuống
            for (int dem = 1; dem <= 4 && dongHT < BanCo.SoDong - 5 && cotHT > 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (MangOCo[dongHT + dem, cotHT - dem].SoHuu == 1)
                    {
                        if (dem == 4)
                            DiemPhongNgu -= 170;

                        SoQuanTaTrai++;
                        break;
                    }
                    else
                    {
                        if (dem == 1)
                            ok = true;

                        KhoangChongDuoi++;
                    }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        #endregion

        #endregion

        #region duyệt chiến thắng theo 8 hướng
        //kiểm tra chiến thắng
        public bool kiemTraChienThang(Graphics g)
        {
            if (_stkCacNuocDaDi.Count != 0)
            {
                foreach (C_OCo oco in _stkCacNuocDaDi)
                {
                    //duyệt theo 8 hướng mỗi quân cờ
                    if (duyetNgangPhai(g, oco.Dong, oco.Cot, oco.SoHuu) || duyetNgangTrai(g, oco.Dong, oco.Cot, oco.SoHuu)
                        || duyetDocTren(g, oco.Dong, oco.Cot, oco.SoHuu) || duyetDocDuoi(g, oco.Dong, oco.Cot, oco.SoHuu)
                        || duyetCheoXuoiTren(g, oco.Dong, oco.Cot, oco.SoHuu) || duyetCheoXuoiDuoi(g, oco.Dong, oco.Cot, oco.SoHuu)
                        || duyetCheoNguocTren(g, oco.Dong, oco.Cot, oco.SoHuu) || duyetCheoNguocDuoi(g, oco.Dong, oco.Cot, oco.SoHuu))
                    {
                        ketThucTroChoi(oco);
                        return true;
                    }
                }
            }

            return false;
        }

        //vẽ đường kẻ trên 5 nước thắng
        public void veDuongChienThang(Graphics g, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(new Pen(Color.Blue,3f), x1, y1, x2, y2);
        }

        public bool duyetNgangPhai(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, (cotHT) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2, (cotHT + 5) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2);
            return true;
        }

        public bool duyetNgangTrai(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2, (cotHT - 4) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO + C_OCo.CHIEU_CAO / 2);
            return true;
        }

        public bool duyetDocTren(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT + 1) * C_OCo.CHIEU_CAO, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool duyetDocDuoi(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, dongHT * C_OCo.CHIEU_CAO, cotHT * C_OCo.CHIEU_RONG + C_OCo.CHIEU_RONG / 2, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool duyetCheoXuoiTren(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4 || cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, (dongHT + 1) * C_OCo.CHIEU_CAO, (cotHT - 4) * C_OCo.CHIEU_RONG, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool duyetCheoXuoiDuoi(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5 || cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO, (cotHT + 5) * C_OCo.CHIEU_RONG, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool duyetCheoNguocDuoi(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT > BanCo.SoDong - 5 || cotHT < 4)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT + dem, cotHT - dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, (cotHT + 1) * C_OCo.CHIEU_RONG, dongHT * C_OCo.CHIEU_CAO, (cotHT - 4) * C_OCo.CHIEU_RONG, (dongHT + 5) * C_OCo.CHIEU_CAO);
            return true;
        }

        public bool duyetCheoNguocTren(Graphics g, int dongHT, int cotHT, int SoHuu)
        {
            if (dongHT < 4 || cotHT > BanCo.SoCot - 5)
                return false;
            for (int dem = 1; dem <= 4; dem++)
            {
                if (MangOCo[dongHT - dem, cotHT + dem].SoHuu != SoHuu)
                {
                    return false;
                }

            }
            veDuongChienThang(g, cotHT * C_OCo.CHIEU_RONG, (dongHT + 1) * C_OCo.CHIEU_CAO, (cotHT + 5) * C_OCo.CHIEU_RONG, (dongHT - 4) * C_OCo.CHIEU_CAO);
            return true;
        }

        #endregion

        //kết thúc trò chơi
        public void ketThucTroChoi(C_OCo oco)
        {
            //chơi với người
            if (_cheDoChoi == 1)
            {
                if (oco.SoHuu == 1)
                    MessageBox.Show("Red player wins");
                else
                    MessageBox.Show("Green player wins");
            }
            else//chơi với máy
            {
                if (oco.SoHuu == 1)
                {
                    MessageBox.Show("AI wins");
                    gayBan(1);
                }
                else
                {
                    MessageBox.Show("Player wins");
                    gayBan(2);
                }

            }

            _sanSang = false;
        }

        private void gayBan(int soHuu)
        {
            if(soHuu == 1)
            {
                String[] arrGay = { "Ohh I didnt mean to hurt you", "Roses are red violets are blue and i....beat you 🙂", "I'm inevitable 8)" };
                String gay = arrGay[rd.Next(0, 3)];
                PictureChuoi1.Image = global::CO_CARO_2.Properties.Resources.gif31;
                TextGayBan.Text = gay;

                PanelGayBan.Visible = true;
                Delayed(4000, () => PanelGayBan.Visible = false);
            }
            else
            {
                String[] arrGay = { "Lucky shot😀", "Hmmmm...someone was talking while I was playing😌", "Good game, next time i will use 100% my power" };
                String gay = arrGay[rd.Next(0, 3)];
                PictureChuoi1.Image = global::CO_CARO_2.Properties.Resources.gif31;
                TextGayBan.Text = gay;

                PanelGayBan.Visible = true;
                Delayed(4000, () => PanelGayBan.Visible = false);
            }
            
        }
        public void Delayed(int delay, Action action)
        {
            Timer timer = new Timer();
            timer.Interval = delay;
            timer.Tick += (s, e) => {
                action();
                timer.Stop();
            };
            timer.Start();
        }

    }
}
