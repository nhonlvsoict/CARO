namespace CO_CARO_2
{
    partial class fmCoCaro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chơiVớiNgườiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chơiVớiMáyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hepleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.luậtChơiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBanCo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newgame = new System.Windows.Forms.Button();
            this.grType = new System.Windows.Forms.GroupBox();
            this.avp_btn = new System.Windows.Forms.RadioButton();
            this.pvp_btn = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grType.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chơiVớiNgườiToolStripMenuItem,
            this.chơiVớiMáyToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // chơiVớiNgườiToolStripMenuItem
            // 
            this.chơiVớiNgườiToolStripMenuItem.Name = "chơiVớiNgườiToolStripMenuItem";
            this.chơiVớiNgườiToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.chơiVớiNgườiToolStripMenuItem.Text = "Chơi với người";
            this.chơiVớiNgườiToolStripMenuItem.Click += new System.EventHandler(this.chơiVớiNgườiToolStripMenuItem_Click);
            // 
            // chơiVớiMáyToolStripMenuItem
            // 
            this.chơiVớiMáyToolStripMenuItem.Name = "chơiVớiMáyToolStripMenuItem";
            this.chơiVớiMáyToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.chơiVớiMáyToolStripMenuItem.Text = "Chơi với máy";
            this.chơiVớiMáyToolStripMenuItem.Click += new System.EventHandler(this.chơiVớiMáyToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // hepleToolStripMenuItem
            // 
            this.hepleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.luậtChơiToolStripMenuItem,
            this.thôngTinToolStripMenuItem});
            this.hepleToolStripMenuItem.Name = "hepleToolStripMenuItem";
            this.hepleToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.hepleToolStripMenuItem.Text = "&Trợ giúp";
            // 
            // luậtChơiToolStripMenuItem
            // 
            this.luậtChơiToolStripMenuItem.Name = "luậtChơiToolStripMenuItem";
            this.luậtChơiToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.luậtChơiToolStripMenuItem.Text = "&Luật chơi";
            this.luậtChơiToolStripMenuItem.Click += new System.EventHandler(this.luậtChơiToolStripMenuItem_Click);
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.thôngTinToolStripMenuItem.Text = "&Thông tin";
            this.thôngTinToolStripMenuItem.Click += new System.EventHandler(this.thôngTinToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.56773F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.43227F));
            this.tableLayoutPanel1.Controls.Add(this.pnlBanCo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 749F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1351, 766);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnlBanCo
            // 
            this.pnlBanCo.BackColor = System.Drawing.Color.FloralWhite;
            this.pnlBanCo.BackgroundImage = global::CO_CARO_2.Properties.Resources.b;
            this.pnlBanCo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlBanCo.Location = new System.Drawing.Point(4, 4);
            this.pnlBanCo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBanCo.Name = "pnlBanCo";
            this.pnlBanCo.Size = new System.Drawing.Size(1120, 758);
            this.pnlBanCo.TabIndex = 0;
            this.pnlBanCo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBanCo_Paint);
            this.pnlBanCo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlBanCo_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(1132, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 760);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newgame);
            this.groupBox2.Controls.Add(this.grType);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 754);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Main Menu";
            // 
            // newgame
            // 
            this.newgame.Location = new System.Drawing.Point(36, 47);
            this.newgame.Name = "newgame";
            this.newgame.Size = new System.Drawing.Size(141, 46);
            this.newgame.TabIndex = 7;
            this.newgame.Text = "New Game";
            this.newgame.UseVisualStyleBackColor = true;
            this.newgame.Click += new System.EventHandler(this.newgame_Click);
            // 
            // grType
            // 
            this.grType.Controls.Add(this.avp_btn);
            this.grType.Controls.Add(this.pvp_btn);
            this.grType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grType.Location = new System.Drawing.Point(16, 116);
            this.grType.Name = "grType";
            this.grType.Size = new System.Drawing.Size(176, 123);
            this.grType.TabIndex = 6;
            this.grType.TabStop = false;
            this.grType.Text = "Type";
            this.grType.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // avp_btn
            // 
            this.avp_btn.AutoSize = true;
            this.avp_btn.Location = new System.Drawing.Point(6, 72);
            this.avp_btn.Name = "avp_btn";
            this.avp_btn.Size = new System.Drawing.Size(137, 24);
            this.avp_btn.TabIndex = 3;
            this.avp_btn.TabStop = true;
            this.avp_btn.Text = "AI vs Person";
            this.avp_btn.UseVisualStyleBackColor = true;
            //this.avp_btn.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // pvp_btn
            // 
            this.pvp_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvp_btn.Location = new System.Drawing.Point(6, 24);
            this.pvp_btn.Name = "pvp_btn";
            this.pvp_btn.Size = new System.Drawing.Size(165, 46);
            this.pvp_btn.TabIndex = 2;
            this.pvp_btn.Text = "Person vs Person";
            this.pvp_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.pvp_btn.UseVisualStyleBackColor = true;
            //this.pvp_btn.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(36, 702);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 46);
            this.button3.TabIndex = 5;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(36, 636);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "About";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.thôngTinToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(36, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Rules";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.luậtChơiToolStripMenuItem_Click);
            // 
            // fmCoCaro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 770);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "fmCoCaro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ultimate Caro Battle";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.grType.ResumeLayout(false);
            this.grType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chơiVớiNgườiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chơiVớiMáyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hepleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem luậtChơiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBanCo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox grType;
        private System.Windows.Forms.RadioButton avp_btn;
        private System.Windows.Forms.RadioButton pvp_btn;
        private System.Windows.Forms.Button newgame;
    }
}

