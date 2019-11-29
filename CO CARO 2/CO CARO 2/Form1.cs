using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CO_CARO_2
{
    
    public partial class fmCoCaro : Form
    {
        public static int ChieuRongBanCo;
        public static int ChieuCaoBanCo;
        private Graphics grp;
        private C_DieuKhien DieuKhien;
        private fmLuatChoi LuatChoi;
        private int tempX;
        private int tempY;
        private int tmpBotX;
        private int tmpBotY;

        public fmCoCaro()
        {
            InitializeComponent();
            //vẽ nên pnlBanCo
            grp = pnlBanCo.CreateGraphics();
            
            //lấy chiều rộng và chiều cao pnBanCo để vẽ bàn cờ
            ChieuCaoBanCo = pnlBanCo.Height;
            ChieuRongBanCo = pnlBanCo.Width;

            //khởi tạo đối tượng điều khiển trò chơi
            DieuKhien = new C_DieuKhien();

            LuatChoi = new fmLuatChoi();

            //avp_btn.Checked = true;

            //DieuKhien.LuotDi = 1;
            //chơiVớiMáyToolStripMenuItem_Click();
        }

        private void pnlBanCo_Paint(object sender, PaintEventArgs e)
        {
            if (DieuKhien.SanSang)
            {
                //vẽ bàn cờ
                DieuKhien.veBanCo(grp);
                //vẽ lại các quân cờ trong stack
                DieuKhien.veLaiQuanCo(grp);
            }
        }

        private void chơiVớiNgườiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DieuKhien.choiVoiNguoi(grp);

            grp.Clear(pnlBanCo.BackColor);
            Image image = new Bitmap(Properties.Resources.b);
            pnlBanCo.BackgroundImage = image;
            //xóa tất cả các hình đã vẽ trên panel chỉ giữ lại màu nền panel
        }

        private void pnlBanCo_MouseClick(object sender, MouseEventArgs e)
        {
            if (DieuKhien.SanSang)
            {
                //chơi với người
                if (DieuKhien.CheDoChoi == 1)
                {
                    //đánh cờ với tọa độ chuột khi lick vào panel bàn cờ
                    DieuKhien.danhCo(grp, e.Location.X, e.Location.Y);
                    tempX = e.Location.X;
                    tempY = e.Location.Y;
                    //sau khi đánh cờ thì kiểm tra chiến thắng luôn
                    DieuKhien.kiemTraChienThang(grp);
                }
                //chơi với máy
                else
                {
                    //người chơi đánh
                    DieuKhien.danhCo(grp, e.Location.X, e.Location.Y);
                    tempX = e.Location.X;
                    tempY = e.Location.Y;
                    //kiểm tra người chơi chưa chiến thắng thì cho máy đánh
                    if (!DieuKhien.kiemTraChienThang(grp))
                    {
                        //máy đánh
                        var pair = DieuKhien.mayDanh(grp);
                        tmpBotX = pair.Key;
                        tmpBotY = pair.Value;
                        DieuKhien.kiemTraChienThang(grp);
                    }
                }
            }
        }

        private void chơiVớiMáyToolStripMenuItem_Click(object sender = null, EventArgs e = null)
        {
            DieuKhien.choiVoiMay(grp);

            grp.Clear(pnlBanCo.BackColor);
            Image image = new Bitmap(Properties.Resources.b);
            pnlBanCo.BackgroundImage = image;
            //xóa tất cả các hình đã vẽ trên panel chỉ giữ lại màu nền panel
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void luậtChơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LuatChoi.ShowDialog();
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bananas Caro Battle \nGroup: The Fellowship of Bananas \n","",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.GetType().ToString());

            //string selected = comboBox1.SelectedItem.ToString();
            ////MessageBox.Show(selected);
            //if (selected == "Person vs AI")
            //{
            //    groupBox1.Visible = true;
            //    //DieuKhien.LuotDi;
            //    chơiVớiMáyToolStripMenuItem_Click(sender, e);
            //}
            //else if (selected == "Person vs Person")
            //    chơiVớiNgườiToolStripMenuItem_Click(sender, e);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

       

        //private void newgame_Click(object sender, EventArgs e)
        //{
        //    if (avp_btn.Checked)
        //    {
        //        DieuKhien.LuotDi = 1;
        //        chơiVớiMáyToolStripMenuItem_Click(sender, e);
        //    }else if (pvp_btn.Checked)
        //        chơiVớiNgườiToolStripMenuItem_Click(sender, e);
        //}

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fmCoCaro_Load(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DieuKhien.undoMove(grp, tempX, tempY, tmpBotX, tmpBotY);
        }
    }
}
