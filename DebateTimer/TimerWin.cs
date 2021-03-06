﻿using DebateTimer.Properties;
using DebateTimer;

namespace DebateTimer
{
    using DebateTimer.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Windows.Forms;

    public class TimerWin : Form
    {
        private ToolStripMenuItem AffP;
        internal ToolStripMenuItem AffPMinus;
        internal ToolStripMenuItem AffPReset;
        private ToolStripMenuItem bC;
        private ToolStripMenuItem bExit;
        private ToolStripMenuItem bR;
        private ToolStripMenuItem bX;
        internal ToolStripMenuItem c000;
        internal ToolStripMenuItem c030;
        internal ToolStripMenuItem c100;
        internal ToolStripMenuItem c130;
        internal ToolStripMenuItem c200;
        internal ToolStripMenuItem c300;
        internal ToolStripMenuItem c400;
        internal ToolStripMenuItem c500;
        internal ToolStripMenuItem c600;
        private IContainer components = null;
        internal ToolStripMenuItem Config;
        internal TextBox Disp;
        private int flashInt = 0;
        internal ToolStripMenuItem mCons;
        internal ToolStripMenuItem mCros;
        internal MenuStrip Menubar;
        internal ToolStripMenuItem mPrep;
        internal ToolStripMenuItem mRebu;
        private ToolStripMenuItem NegP;
        internal ToolStripMenuItem NegPMinus;
        internal ToolStripMenuItem NegPReset;
        internal ToolStripMenuItem oAB;
        internal ToolStripMenuItem oAV;
        internal ToolStripMenuItem oDefaults;
        internal ToolStripMenuItem oReset;
        internal ToolStripMenuItem oTran;
        internal ToolStripMenuItem oTU;
        internal ToolStripMenuItem p000;
        internal ToolStripMenuItem p030;
        internal ToolStripMenuItem p100;
        internal ToolStripMenuItem p130;
        internal ToolStripMenuItem p200;
        internal ToolStripMenuItem p300;
        internal ToolStripMenuItem p400;
        internal ToolStripMenuItem p500;
        internal ToolStripMenuItem p600;
        internal ToolStripMenuItem r000;
        internal ToolStripMenuItem r030;
        internal ToolStripMenuItem r100;
        internal ToolStripMenuItem r130;
        internal ToolStripMenuItem r200;
        internal ToolStripMenuItem r300;
        internal ToolStripMenuItem r400;
        internal ToolStripMenuItem r500;
        private ToolStripMenuItem Start;
        private int sysT = DateTime.Now.Second;
        private Timer timerTick;
        internal ToolStripMenuItem x000;
        internal ToolStripMenuItem x030;
        internal ToolStripMenuItem x100;
        internal ToolStripMenuItem x130;
        private ToolStripMenuItem bS;
        internal ToolStripMenuItem x200;

        public TimerWin()
        {
            InitializeComponent();
        }

        private void AffP_Click(object sender, EventArgs e)
        {
            TimeSet(AffP.Text, "Aff Prep");
        }

        private void bC_Click(object sender, EventArgs e)
        {
            TimeSet(bC.Text, "Constructive");
        }

        private void BeepAlert()
        {
            new SoundPlayer(Resources.beep).Play();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void bR_Click(object sender, EventArgs e)
        {
            TimeSet(bR.Text, "Rebuttal");
        }

        private void bX_Click(object sender, EventArgs e)
        {
            TimeSet(bX.Text, "Cross-x");
        }

        private void Config_Click(object sender, EventArgs e)
        {
            oTran.Checked = !oTran.Checked;
        }

        private void Config_MouseEnter(object sender, EventArgs e)
        {
            Config.ShowDropDown();
            Config.DropDown.MouseLeave += new EventHandler(ConfigDropDownMouseLeave);
            mCons.DropDown.Opening += new CancelEventHandler(ConfigDropDownMouseLeaveRemoveEvent);
            mCros.DropDown.Opening += new CancelEventHandler(ConfigDropDownMouseLeaveRemoveEvent);
            mRebu.DropDown.Opening += new CancelEventHandler(ConfigDropDownMouseLeaveRemoveEvent);
            mPrep.DropDown.Opening += new CancelEventHandler(ConfigDropDownMouseLeaveRemoveEvent);
        }

        private void ConfigDropDownMouseLeave(object sender, EventArgs e)
        {
            Config.HideDropDown();
        }

        private void ConfigDropDownMouseLeaveRemoveEvent(object sender, EventArgs e)
        {
            Config.DropDown.MouseLeave -= new EventHandler(ConfigDropDownMouseLeave);
        }

        private string convertInput()
        {
            string text = Disp.Text;
            int num = 0;
            int length = text.Length;
            int index = text.IndexOf(":");
            if (index > 0)
            {
                num = (val(text.Substring(0, index)) * 60) + val(text.Substring((length - index) - 1));
            }
            else if (length < 2)
            {
                num = val(text) * 60;
            }
            else if (length == 2)
            {
                num = val(text);
            }
            else if (length > 2)
            {
                num = (val(text.Substring(0, length - 2)) * 60) + val(text.Substring(length - 2));
            }
            num = Math.Min(0x1734, num);
            return i2s(num);
        }

        private void Disp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                StartClick();
            }
        }

        private void Disp_MouseDown(object sender, MouseEventArgs e)
        {
            if (timerTick.Enabled)
            {
                StartClick();
            }
        }

        private void Disp_TextChanged(object sender, EventArgs e)
        {
            string text = Disp.Text;
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (!":1234567890".Contains(ch.ToString()))
                {
                    text.Remove(i, 1);
                }
            }
            if (Text == "Aff Prep")
            {
                AffP.Text = convertInput();
            }
            else if (Text == "Neg Prep")
            {
                NegP.Text = convertInput();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing & (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string i2s(int t)
        {
            int num = (int) Math.Floor((double) (((double) t) / 60.0));
            string str = num + ":";
            t -= num * 60;
            if (t < 10)
            {
                str = str + "0";
            }
            return (str + t);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerWin));
            this.Menubar = new System.Windows.Forms.MenuStrip();
            this.Config = new System.Windows.Forms.ToolStripMenuItem();
            this.oTran = new System.Windows.Forms.ToolStripMenuItem();
            this.mPrep = new System.Windows.Forms.ToolStripMenuItem();
            this.p600 = new System.Windows.Forms.ToolStripMenuItem();
            this.p500 = new System.Windows.Forms.ToolStripMenuItem();
            this.p400 = new System.Windows.Forms.ToolStripMenuItem();
            this.p300 = new System.Windows.Forms.ToolStripMenuItem();
            this.p200 = new System.Windows.Forms.ToolStripMenuItem();
            this.p130 = new System.Windows.Forms.ToolStripMenuItem();
            this.p100 = new System.Windows.Forms.ToolStripMenuItem();
            this.p030 = new System.Windows.Forms.ToolStripMenuItem();
            this.p000 = new System.Windows.Forms.ToolStripMenuItem();
            this.mCons = new System.Windows.Forms.ToolStripMenuItem();
            this.c600 = new System.Windows.Forms.ToolStripMenuItem();
            this.c500 = new System.Windows.Forms.ToolStripMenuItem();
            this.c400 = new System.Windows.Forms.ToolStripMenuItem();
            this.c300 = new System.Windows.Forms.ToolStripMenuItem();
            this.c200 = new System.Windows.Forms.ToolStripMenuItem();
            this.c130 = new System.Windows.Forms.ToolStripMenuItem();
            this.c100 = new System.Windows.Forms.ToolStripMenuItem();
            this.c030 = new System.Windows.Forms.ToolStripMenuItem();
            this.c000 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRebu = new System.Windows.Forms.ToolStripMenuItem();
            this.r500 = new System.Windows.Forms.ToolStripMenuItem();
            this.r400 = new System.Windows.Forms.ToolStripMenuItem();
            this.r300 = new System.Windows.Forms.ToolStripMenuItem();
            this.r200 = new System.Windows.Forms.ToolStripMenuItem();
            this.r130 = new System.Windows.Forms.ToolStripMenuItem();
            this.r100 = new System.Windows.Forms.ToolStripMenuItem();
            this.r030 = new System.Windows.Forms.ToolStripMenuItem();
            this.r000 = new System.Windows.Forms.ToolStripMenuItem();
            this.mCros = new System.Windows.Forms.ToolStripMenuItem();
            this.x200 = new System.Windows.Forms.ToolStripMenuItem();
            this.x130 = new System.Windows.Forms.ToolStripMenuItem();
            this.x100 = new System.Windows.Forms.ToolStripMenuItem();
            this.x030 = new System.Windows.Forms.ToolStripMenuItem();
            this.x000 = new System.Windows.Forms.ToolStripMenuItem();
            this.oAB = new System.Windows.Forms.ToolStripMenuItem();
            this.oAV = new System.Windows.Forms.ToolStripMenuItem();
            this.bExit = new System.Windows.Forms.ToolStripMenuItem();
            this.Start = new System.Windows.Forms.ToolStripMenuItem();
            this.bS = new System.Windows.Forms.ToolStripMenuItem();
            this.bC = new System.Windows.Forms.ToolStripMenuItem();
            this.bR = new System.Windows.Forms.ToolStripMenuItem();
            this.bX = new System.Windows.Forms.ToolStripMenuItem();
            this.AffP = new System.Windows.Forms.ToolStripMenuItem();
            this.NegP = new System.Windows.Forms.ToolStripMenuItem();
            this.oReset = new System.Windows.Forms.ToolStripMenuItem();
            this.oDefaults = new System.Windows.Forms.ToolStripMenuItem();
            this.oTU = new System.Windows.Forms.ToolStripMenuItem();
            this.NegPMinus = new System.Windows.Forms.ToolStripMenuItem();
            this.NegPReset = new System.Windows.Forms.ToolStripMenuItem();
            this.AffPMinus = new System.Windows.Forms.ToolStripMenuItem();
            this.AffPReset = new System.Windows.Forms.ToolStripMenuItem();
            this.Disp = new System.Windows.Forms.TextBox();
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.Menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menubar
            // 
            this.Menubar.BackColor = System.Drawing.Color.Transparent;
            this.Menubar.Dock = System.Windows.Forms.DockStyle.None;
            this.Menubar.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Menubar.GripMargin = new System.Windows.Forms.Padding(0);
            this.Menubar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Config,
            this.Start});
            this.Menubar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.Menubar.Location = new System.Drawing.Point(0, 0);
            this.Menubar.Name = "Menubar";
            this.Menubar.Padding = new System.Windows.Forms.Padding(0);
            this.Menubar.Size = new System.Drawing.Size(138, 86);
            this.Menubar.TabIndex = 4;
            this.Menubar.Text = "MenuStrip1";
            // 
            // Config
            // 
            this.Config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Config.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oTran,
            this.mPrep,
            this.mCons,
            this.mRebu,
            this.mCros,
            this.oAB,
            this.oAV,
            this.bExit});
            this.Config.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Config.Image = ((System.Drawing.Image)(resources.GetObject("Config.Image")));
            this.Config.Name = "Config";
            this.Config.Padding = new System.Windows.Forms.Padding(0);
            this.Config.Size = new System.Drawing.Size(137, 28);
            this.Config.Text = "Options";
            this.Config.Click += new System.EventHandler(this.Config_Click);
            this.Config.MouseEnter += new System.EventHandler(this.Config_MouseEnter);
            this.Config.MouseLeave += new System.EventHandler(this.Config_MouseLeave);
            // 
            // oTran
            // 
            this.oTran.BackColor = System.Drawing.Color.White;
            this.oTran.CheckOnClick = true;
            this.oTran.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.oTran.Name = "oTran";
            this.oTran.Size = new System.Drawing.Size(212, 30);
            this.oTran.Text = "Transparent";
            this.oTran.CheckedChanged += new System.EventHandler(this.oTran_CheckedChanged);
            // 
            // mPrep
            // 
            this.mPrep.BackColor = System.Drawing.Color.White;
            this.mPrep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mPrep.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.p600,
            this.p500,
            this.p400,
            this.p300,
            this.p200,
            this.p130,
            this.p100,
            this.p030,
            this.p000});
            this.mPrep.Name = "mPrep";
            this.mPrep.Size = new System.Drawing.Size(212, 30);
            this.mPrep.Text = "Prep Times";
            // 
            // p600
            // 
            this.p600.BackColor = System.Drawing.Color.White;
            this.p600.CheckOnClick = true;
            this.p600.Name = "p600";
            this.p600.Size = new System.Drawing.Size(133, 30);
            this.p600.Text = "6:00";
            // 
            // p500
            // 
            this.p500.BackColor = System.Drawing.Color.White;
            this.p500.Checked = true;
            this.p500.CheckOnClick = true;
            this.p500.CheckState = System.Windows.Forms.CheckState.Checked;
            this.p500.Name = "p500";
            this.p500.Size = new System.Drawing.Size(133, 30);
            this.p500.Text = "5:00";
            // 
            // p400
            // 
            this.p400.BackColor = System.Drawing.Color.White;
            this.p400.CheckOnClick = true;
            this.p400.Name = "p400";
            this.p400.Size = new System.Drawing.Size(133, 30);
            this.p400.Text = "4:00";
            // 
            // p300
            // 
            this.p300.BackColor = System.Drawing.Color.White;
            this.p300.Checked = true;
            this.p300.CheckOnClick = true;
            this.p300.CheckState = System.Windows.Forms.CheckState.Checked;
            this.p300.Name = "p300";
            this.p300.Size = new System.Drawing.Size(133, 30);
            this.p300.Text = "3:00";
            // 
            // p200
            // 
            this.p200.BackColor = System.Drawing.Color.White;
            this.p200.CheckOnClick = true;
            this.p200.Name = "p200";
            this.p200.Size = new System.Drawing.Size(133, 30);
            this.p200.Text = "2:00";
            // 
            // p130
            // 
            this.p130.BackColor = System.Drawing.Color.White;
            this.p130.CheckOnClick = true;
            this.p130.Name = "p130";
            this.p130.Size = new System.Drawing.Size(133, 30);
            this.p130.Text = "1:30";
            // 
            // p100
            // 
            this.p100.BackColor = System.Drawing.Color.White;
            this.p100.Checked = true;
            this.p100.CheckOnClick = true;
            this.p100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.p100.Name = "p100";
            this.p100.Size = new System.Drawing.Size(133, 30);
            this.p100.Text = "1:00";
            // 
            // p030
            // 
            this.p030.BackColor = System.Drawing.Color.White;
            this.p030.CheckOnClick = true;
            this.p030.Name = "p030";
            this.p030.Size = new System.Drawing.Size(133, 30);
            this.p030.Text = "0:30";
            // 
            // p000
            // 
            this.p000.BackColor = System.Drawing.Color.White;
            this.p000.Checked = true;
            this.p000.CheckOnClick = true;
            this.p000.CheckState = System.Windows.Forms.CheckState.Checked;
            this.p000.Name = "p000";
            this.p000.Size = new System.Drawing.Size(133, 30);
            this.p000.Text = "0:00";
            // 
            // mCons
            // 
            this.mCons.BackColor = System.Drawing.Color.White;
            this.mCons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mCons.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c600,
            this.c500,
            this.c400,
            this.c300,
            this.c200,
            this.c130,
            this.c100,
            this.c030,
            this.c000});
            this.mCons.Name = "mCons";
            this.mCons.Size = new System.Drawing.Size(212, 30);
            this.mCons.Text = "Constructives";
            // 
            // c600
            // 
            this.c600.BackColor = System.Drawing.Color.White;
            this.c600.CheckOnClick = true;
            this.c600.Name = "c600";
            this.c600.Size = new System.Drawing.Size(133, 30);
            this.c600.Text = "6:00";
            // 
            // c500
            // 
            this.c500.BackColor = System.Drawing.Color.White;
            this.c500.Checked = true;
            this.c500.CheckOnClick = true;
            this.c500.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c500.Name = "c500";
            this.c500.Size = new System.Drawing.Size(133, 30);
            this.c500.Text = "5:00";
            // 
            // c400
            // 
            this.c400.BackColor = System.Drawing.Color.White;
            this.c400.CheckOnClick = true;
            this.c400.Name = "c400";
            this.c400.Size = new System.Drawing.Size(133, 30);
            this.c400.Text = "4:00";
            // 
            // c300
            // 
            this.c300.BackColor = System.Drawing.Color.White;
            this.c300.CheckOnClick = true;
            this.c300.Name = "c300";
            this.c300.Size = new System.Drawing.Size(133, 30);
            this.c300.Text = "3:00";
            // 
            // c200
            // 
            this.c200.BackColor = System.Drawing.Color.White;
            this.c200.CheckOnClick = true;
            this.c200.Name = "c200";
            this.c200.Size = new System.Drawing.Size(133, 30);
            this.c200.Text = "2:00";
            // 
            // c130
            // 
            this.c130.BackColor = System.Drawing.Color.White;
            this.c130.CheckOnClick = true;
            this.c130.Name = "c130";
            this.c130.Size = new System.Drawing.Size(133, 30);
            this.c130.Text = "1:30";
            // 
            // c100
            // 
            this.c100.BackColor = System.Drawing.Color.White;
            this.c100.Checked = true;
            this.c100.CheckOnClick = true;
            this.c100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c100.Name = "c100";
            this.c100.Size = new System.Drawing.Size(133, 30);
            this.c100.Text = "1:00";
            // 
            // c030
            // 
            this.c030.BackColor = System.Drawing.Color.White;
            this.c030.CheckOnClick = true;
            this.c030.Name = "c030";
            this.c030.Size = new System.Drawing.Size(133, 30);
            this.c030.Text = "0:30";
            // 
            // c000
            // 
            this.c000.BackColor = System.Drawing.Color.White;
            this.c000.Checked = true;
            this.c000.CheckOnClick = true;
            this.c000.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c000.Name = "c000";
            this.c000.Size = new System.Drawing.Size(133, 30);
            this.c000.Text = "0:00";
            // 
            // mRebu
            // 
            this.mRebu.BackColor = System.Drawing.Color.White;
            this.mRebu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mRebu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.r500,
            this.r400,
            this.r300,
            this.r200,
            this.r130,
            this.r100,
            this.r030,
            this.r000});
            this.mRebu.Name = "mRebu";
            this.mRebu.Size = new System.Drawing.Size(212, 30);
            this.mRebu.Text = "Rebuttals";
            // 
            // r500
            // 
            this.r500.BackColor = System.Drawing.Color.White;
            this.r500.CheckOnClick = true;
            this.r500.Name = "r500";
            this.r500.Size = new System.Drawing.Size(133, 30);
            this.r500.Text = "5:00";
            // 
            // r400
            // 
            this.r400.BackColor = System.Drawing.Color.White;
            this.r400.CheckOnClick = true;
            this.r400.Name = "r400";
            this.r400.Size = new System.Drawing.Size(133, 30);
            this.r400.Text = "4:00";
            // 
            // r300
            // 
            this.r300.BackColor = System.Drawing.Color.White;
            this.r300.Checked = true;
            this.r300.CheckOnClick = true;
            this.r300.CheckState = System.Windows.Forms.CheckState.Checked;
            this.r300.Name = "r300";
            this.r300.Size = new System.Drawing.Size(133, 30);
            this.r300.Text = "3:00";
            // 
            // r200
            // 
            this.r200.BackColor = System.Drawing.Color.White;
            this.r200.CheckOnClick = true;
            this.r200.Name = "r200";
            this.r200.Size = new System.Drawing.Size(133, 30);
            this.r200.Text = "2:00";
            // 
            // r130
            // 
            this.r130.BackColor = System.Drawing.Color.White;
            this.r130.CheckOnClick = true;
            this.r130.Name = "r130";
            this.r130.Size = new System.Drawing.Size(133, 30);
            this.r130.Text = "1:30";
            // 
            // r100
            // 
            this.r100.BackColor = System.Drawing.Color.White;
            this.r100.Checked = true;
            this.r100.CheckOnClick = true;
            this.r100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.r100.Name = "r100";
            this.r100.Size = new System.Drawing.Size(133, 30);
            this.r100.Text = "1:00";
            // 
            // r030
            // 
            this.r030.BackColor = System.Drawing.Color.White;
            this.r030.CheckOnClick = true;
            this.r030.Name = "r030";
            this.r030.Size = new System.Drawing.Size(133, 30);
            this.r030.Text = "0:30";
            // 
            // r000
            // 
            this.r000.BackColor = System.Drawing.Color.White;
            this.r000.Checked = true;
            this.r000.CheckOnClick = true;
            this.r000.CheckState = System.Windows.Forms.CheckState.Checked;
            this.r000.Name = "r000";
            this.r000.Size = new System.Drawing.Size(133, 30);
            this.r000.Text = "0:00";
            // 
            // mCros
            // 
            this.mCros.BackColor = System.Drawing.Color.White;
            this.mCros.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mCros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x200,
            this.x130,
            this.x100,
            this.x030,
            this.x000});
            this.mCros.Name = "mCros";
            this.mCros.Size = new System.Drawing.Size(212, 30);
            this.mCros.Text = "Cross-X";
            // 
            // x200
            // 
            this.x200.BackColor = System.Drawing.Color.White;
            this.x200.CheckOnClick = true;
            this.x200.Name = "x200";
            this.x200.Size = new System.Drawing.Size(133, 30);
            this.x200.Text = "2:00";
            // 
            // x130
            // 
            this.x130.BackColor = System.Drawing.Color.White;
            this.x130.CheckOnClick = true;
            this.x130.Name = "x130";
            this.x130.Size = new System.Drawing.Size(133, 30);
            this.x130.Text = "1:30";
            // 
            // x100
            // 
            this.x100.BackColor = System.Drawing.Color.White;
            this.x100.CheckOnClick = true;
            this.x100.Name = "x100";
            this.x100.Size = new System.Drawing.Size(133, 30);
            this.x100.Text = "1:00";
            // 
            // x030
            // 
            this.x030.BackColor = System.Drawing.Color.White;
            this.x030.CheckOnClick = true;
            this.x030.Name = "x030";
            this.x030.Size = new System.Drawing.Size(133, 30);
            this.x030.Text = "0:30";
            // 
            // x000
            // 
            this.x000.BackColor = System.Drawing.Color.White;
            this.x000.Checked = true;
            this.x000.CheckOnClick = true;
            this.x000.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x000.Name = "x000";
            this.x000.Size = new System.Drawing.Size(133, 30);
            this.x000.Text = "0:00";
            // 
            // oAB
            // 
            this.oAB.BackColor = System.Drawing.Color.White;
            this.oAB.CheckOnClick = true;
            this.oAB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.oAB.Name = "oAB";
            this.oAB.Size = new System.Drawing.Size(212, 30);
            this.oAB.Text = "Beep Alert";
            this.oAB.CheckedChanged += new System.EventHandler(this.oAB_CheckedChanged);
            // 
            // oAV
            // 
            this.oAV.BackColor = System.Drawing.Color.White;
            this.oAV.CheckOnClick = true;
            this.oAV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.oAV.Name = "oAV";
            this.oAV.Size = new System.Drawing.Size(212, 30);
            this.oAV.Text = "Voice Alert";
            this.oAV.CheckedChanged += new System.EventHandler(this.oAV_CheckedChanged);
            // 
            // bExit
            // 
            this.bExit.BackColor = System.Drawing.Color.White;
            this.bExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(212, 30);
            this.bExit.Text = "Exit";
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // Start
            // 
            this.Start.AutoToolTip = true;
            this.Start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Start.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bS,
            this.bC,
            this.bR,
            this.bX,
            this.AffP,
            this.NegP,
            this.oReset,
            this.oDefaults,
            this.oTU});
            this.Start.Image = ((System.Drawing.Image)(resources.GetObject("Start.Image")));
            this.Start.Name = "Start";
            this.Start.Padding = new System.Windows.Forms.Padding(0);
            this.Start.Size = new System.Drawing.Size(27, 28);
            this.Start.Text = "8";
            this.Start.Click += new System.EventHandler(this.Start_Click);
            this.Start.MouseLeave += new System.EventHandler(this.Start_MouseLeave);
            this.Start.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Start_MouseMove);
            // 
            // bS
            // 
            this.bS.BackColor = System.Drawing.Color.White;
            this.bS.Name = "bS";
            this.bS.Size = new System.Drawing.Size(177, 30);
            this.bS.Text = "Start/Stop";
            this.bS.Click += new System.EventHandler(this.bS_Click);
            // 
            // bC
            // 
            this.bC.BackColor = System.Drawing.Color.White;
            this.bC.Name = "bC";
            this.bC.Padding = new System.Windows.Forms.Padding(0);
            this.bC.Size = new System.Drawing.Size(177, 28);
            this.bC.Text = "8:00";
            this.bC.Click += new System.EventHandler(this.bC_Click);
            // 
            // bR
            // 
            this.bR.BackColor = System.Drawing.Color.White;
            this.bR.Name = "bR";
            this.bR.Padding = new System.Windows.Forms.Padding(0);
            this.bR.Size = new System.Drawing.Size(177, 28);
            this.bR.Text = "5:00";
            this.bR.Click += new System.EventHandler(this.bR_Click);
            // 
            // bX
            // 
            this.bX.BackColor = System.Drawing.Color.White;
            this.bX.Name = "bX";
            this.bX.Size = new System.Drawing.Size(177, 30);
            this.bX.Text = "3:00";
            this.bX.Click += new System.EventHandler(this.bX_Click);
            // 
            // AffP
            // 
            this.AffP.BackColor = System.Drawing.Color.White;
            this.AffP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(68)))), ((int)(((byte)(192)))));
            this.AffP.Name = "AffP";
            this.AffP.Padding = new System.Windows.Forms.Padding(0);
            this.AffP.Size = new System.Drawing.Size(177, 28);
            this.AffP.Text = "8:00";
            this.AffP.Click += new System.EventHandler(this.AffP_Click);
            // 
            // NegP
            // 
            this.NegP.BackColor = System.Drawing.Color.White;
            this.NegP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NegP.Name = "NegP";
            this.NegP.Padding = new System.Windows.Forms.Padding(0);
            this.NegP.Size = new System.Drawing.Size(177, 28);
            this.NegP.Text = "8:00";
            this.NegP.Click += new System.EventHandler(this.NegP_Click);
            // 
            // oReset
            // 
            this.oReset.BackColor = System.Drawing.Color.White;
            this.oReset.Name = "oReset";
            this.oReset.Size = new System.Drawing.Size(177, 30);
            this.oReset.Text = "Reset Prep";
            this.oReset.Click += new System.EventHandler(this.oReset_Click);
            // 
            // oDefaults
            // 
            this.oDefaults.BackColor = System.Drawing.Color.White;
            this.oDefaults.Name = "oDefaults";
            this.oDefaults.Size = new System.Drawing.Size(177, 30);
            this.oDefaults.Text = "Configure";
            this.oDefaults.Click += new System.EventHandler(this.oDefaults_Click);
            // 
            // oTU
            // 
            this.oTU.BackColor = System.Drawing.Color.White;
            this.oTU.CheckOnClick = true;
            this.oTU.Name = "oTU";
            this.oTU.Size = new System.Drawing.Size(177, 30);
            this.oTU.Text = "Count Up";
            this.oTU.Click += new System.EventHandler(this.oTU_Click);
            // 
            // NegPMinus
            // 
            this.NegPMinus.Name = "NegPMinus";
            this.NegPMinus.Size = new System.Drawing.Size(154, 22);
            this.NegPMinus.Text = "-5s Neg Prep";
            // 
            // NegPReset
            // 
            this.NegPReset.Name = "NegPReset";
            this.NegPReset.Size = new System.Drawing.Size(154, 22);
            this.NegPReset.Text = "Reset Neg Prep";
            // 
            // AffPMinus
            // 
            this.AffPMinus.Name = "AffPMinus";
            this.AffPMinus.Size = new System.Drawing.Size(148, 22);
            this.AffPMinus.Text = "-5s Aff Prep";
            // 
            // AffPReset
            // 
            this.AffPReset.Name = "AffPReset";
            this.AffPReset.Size = new System.Drawing.Size(148, 22);
            this.AffPReset.Text = "Reset Aff Prep";
            // 
            // Disp
            // 
            this.Disp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(194)))), ((int)(((byte)(238)))));
            this.Disp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Disp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Disp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Disp.Font = new System.Drawing.Font("Tahoma", 44F, System.Drawing.FontStyle.Bold);
            this.Disp.Location = new System.Drawing.Point(0, 0);
            this.Disp.Margin = new System.Windows.Forms.Padding(0);
            this.Disp.MaxLength = 5;
            this.Disp.Name = "Disp";
            this.Disp.Size = new System.Drawing.Size(184, 107);
            this.Disp.TabIndex = 7;
            this.Disp.TabStop = false;
            this.Disp.Text = "8:00";
            this.Disp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Disp.WordWrap = false;
            this.Disp.TextChanged += new System.EventHandler(this.Disp_TextChanged);
            this.Disp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Disp_KeyUp);
            this.Disp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disp_MouseDown);
            // 
            // timerTick
            // 
            this.timerTick.Tick += new System.EventHandler(this.timeTick_Tick);
            // 
            // TimerWin
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(194)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(184, 66);
            this.Controls.Add(this.Menubar);
            this.Controls.Add(this.Disp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Menubar;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "TimerWin";
            this.ShowIcon = false;
            this.Text = "Constructive";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimerWin_FormClosing);
            this.Load += new System.EventHandler(this.TimerWin_Load);
            this.Resize += new System.EventHandler(this.TimerWin_Resize);
            this.Menubar.ResumeLayout(false);
            this.Menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void loadProfile()
        {
            prof(AffP, "p");
            prof(NegP, "p");
            prof(bC, "c");
            prof(bX, "x");
            prof(bR, "r");
        }

        private void loadProfileOnOK(object sender, FormClosingEventArgs e)
        {
            loadProfile();
        }

        private void NegP_Click(object sender, EventArgs e)
        {
            TimeSet(NegP.Text, "Neg Prep");
        }

        private void oAB_CheckedChanged(object sender, EventArgs e)
        {
            if (oAB.Checked)
            {
                oAV.Checked = false;
            }
        }

     
        private void oAV_CheckedChanged(object sender, EventArgs e)
        {
            if (oAV.Checked)
            {
                oAB.Checked = false;
            }
        }

        private void oDefaults_Click(object sender, EventArgs e)
        {
            TimeProfiles profiles = new TimeProfiles();
            profiles.Show();
            profiles.TopMost = base.TopMost;
            profiles.FormClosing += new FormClosingEventHandler(loadProfileOnOK);
        }

        private void oReset_Click(object sender, EventArgs e)
        {
            prof(AffP, "p");
            prof(NegP, "p");
        }


        private void oTran_CheckedChanged(object sender, EventArgs e)
        {
            if (oTran.Checked)
            {
                base.FormBorderStyle = FormBorderStyle.None;
                base.TransparencyKey = BackColor;
            }
            else
            {
                base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                base.TransparencyKey = Color.Empty;
            }
        }

        private void oTU_Click(object sender, EventArgs e)
        {
            if (oTU.Checked & !timerTick.Enabled)
            {
                Disp.Text = "0:00";
            }
        }

        private void prof(ToolStripMenuItem obj, string val)
        {
            Settings settings = Settings.Default;
            int num = 0;
            string str = val;
            if (str != null)
            {
                if (!(str == "p"))
                {
                    if (str == "x")
                    {
                        num = settings.p1x;
                    }
                    else if (str == "c")
                    {
                        num = settings.p1c;
                    }
                    else if (str == "r")
                    {
                        num = settings.p1r;
                    }
                }
                else
                {
                    num = settings.p1p;
                }
            }
            obj.Text = num + ":00";
        }

        private int s2i(string s)
        {
            int index = s.IndexOf(":");
            return ((val(s.Substring(0, index)) * 60) + val(s.Substring(index + 1)));
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartClick();
        }

        private void Start_MouseMove(object sender, MouseEventArgs e)
        {
            Start.ShowDropDown();
            Start.DropDown.MouseLeave += new EventHandler(StartDropDownMouseLeave);
        }

        private void StartClick()
        {
            if (timerTick.Enabled)
            {
                Disp.BackColor = BackColor;
                base.Activate();
                timerTick.Stop();
                Start.Image = Resources.go1;
            }
            else
            {
                Menubar.Focus();
                if (oAB.Checked | oAV.Checked)
                {
                    BeepAlert();
                }
                Disp.Text = convertInput();
                sysT = DateTime.Now.Second;
                timerTick.Start();
                Start.Image = Resources.go2;
            }
        }

        private void bS_Click(object sender, EventArgs e)
        {
            StartClick();
        }

        private void StartDropDownMouseLeave(object sender, EventArgs e)
        {
            Start.HideDropDown();
        }

        private void timeTick_Tick(object sender, EventArgs e)
        {
            if (sysT != DateTime.Now.Second)
            {
                sysT = DateTime.Now.Second;
                string str = (Text.Length > 2) ? Text.Substring(0, 2) : "";
                int t = s2i(Disp.Text);
                if (oTU.Checked)
                {
                    t++;
                    Disp.Text = i2s(t);
                    if (t >= 0xe10)
                    {
                        StartClick();
                    }
                }
                else if (t > 0)
                {
                    if (((((str == "Co") & ((((((((c030.Checked & (t == 30)) | (c100.Checked & (t == 60))) | (c130.Checked & (t == 90))) | (c200.Checked & (t == 120))) | (c300.Checked & (t == 180))) | (c400.Checked & (t == 240))) | (c500.Checked & (t == 300))) | (c600.Checked & (t == 360)))) | ((str == "Cr") & ((((x030.Checked & (t == 30)) | (x100.Checked & (t == 60))) | (x130.Checked & (t == 90))) | (x200.Checked & (t == 120))))) | ((str == "Re") & (((((((r030.Checked & (t == 30)) | (r100.Checked & (t == 60))) | (r130.Checked & (t == 90))) | (r200.Checked & (t == 120))) | (r300.Checked & (t == 180))) | (r400.Checked & (t == 240))) | (r500.Checked & (t == 300))))) | (((str == "Af") | (str == "Ne")) & ((((((((p030.Checked & (t == 30)) | (p100.Checked & (t == 60))) | (p130.Checked & (t == 90))) | (p200.Checked & (t == 120))) | (p300.Checked & (t == 180))) | (p400.Checked & (t == 240))) | (p500.Checked & (t == 300))) | (p600.Checked & (t == 360)))))
                    {
                        if (oAB.Checked)
                        {
                            BeepAlert();
                            Disp.BackColor = Color.FromArgb(0xff, 0, 0);
                            flashInt = t - 4;
                        }
                        if (oAV.Checked)
                        {
                            UnmanagedMemoryStream stream = Resources._30;
                            switch (t)
                            {
                                case 90:
                                    stream = Resources._130;
                                    break;

                                case 120:
                                    stream = Resources._200;
                                    break;

                                case 30:
                                    stream = Resources._30;
                                    break;

                                case 60:
                                    stream = Resources._100;
                                    break;

                                case 180:
                                    stream = Resources._300;
                                    break;

                                case 240:
                                    stream = Resources._400;
                                    break;

                                case 300:
                                    stream = Resources._500;
                                    break;

                                case 360:
                                    stream = Resources._600;
                                    break;
                            }
                            new SoundPlayer(stream).Play();
                        }
                    }
                    if (flashInt == t)
                    {
                        flashInt = 0;
                        Disp.BackColor = BackColor;
                    }
                    t--;
                    Disp.Text = i2s(t);
                    if (Text == "Aff Prep")
                    {
                        AffP.Text = Disp.Text;
                    }
                    else if (Text == "Neg Prep")
                    {
                        NegP.Text = Disp.Text;
                    }
                }
                else
                {
                    if ((((((str == "Co") & c000.Checked) | ((str == "Re") & r000.Checked)) | ((str == "Cr") & x000.Checked)) | (((str == "Af") | (str == "Ne")) & p000.Checked)) & (oAB.Checked | oAV.Checked))
                    {
                        new SoundPlayer(Resources.beep_final).Play();
                    }
                    base.Activate();
                    timerTick.Stop();
                    Start.Image = Resources.go1;
                    string str2 = str;
                    if (str2 != null)
                    {
                        if ((!(str2 == "Af") && !(str2 == "Ne")) && !(str2 == "Re"))
                        {
                            if (str2 == "Co")
                            {
                                bX.PerformClick();
                            }
                            else if (str2 == "Cr")
                            {
                                bC.PerformClick();
                            }
                        }
                        else
                        {
                            bR.PerformClick();
                        }
                    }
                }
            }
        }

        private void TimerWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings settings = Settings.Default;
            settings.c000 = c000.Checked;
            settings.c030 = c030.Checked;
            settings.c100 = c100.Checked;
            settings.c130 = c130.Checked;
            settings.c200 = c200.Checked;
            settings.c300 = c300.Checked;
            settings.c400 = c400.Checked;
            settings.c500 = c500.Checked;
            settings.c600 = c600.Checked;
            settings.r000 = r000.Checked;
            settings.r030 = r030.Checked;
            settings.r100 = r100.Checked;
            settings.r130 = r130.Checked;
            settings.r200 = r200.Checked;
            settings.r300 = r300.Checked;
            settings.r400 = r400.Checked;
            settings.r500 = r500.Checked;
            settings.p000 = p000.Checked;
            settings.p030 = p030.Checked;
            settings.p100 = p100.Checked;
            settings.p130 = p130.Checked;
            settings.p200 = p200.Checked;
            settings.p300 = p300.Checked;
            settings.p400 = p400.Checked;
            settings.p500 = p500.Checked;
            settings.p600 = p600.Checked;
            settings.x000 = x000.Checked;
            settings.x030 = x030.Checked;
            settings.x100 = x100.Checked;
            settings.x130 = x130.Checked;
            settings.x200 = x200.Checked;
            settings.oAV = oAV.Checked;
            settings.oAB = oAB.Checked;
            settings.oTU = oTU.Checked;
            settings.oTran = oTran.Checked;
            settings.wX = base.Location.X;
            settings.wY = base.Location.Y;
            settings.wS = base.Width;
            settings.Disp = Disp.Text;
            settings.AffP = AffP.Text;
            settings.NegP = NegP.Text;
            settings.speechType = Text;
            settings.Save();
        }

        private void TimerWin_Load(object sender, EventArgs e)
        {
            Settings settings = Settings.Default;
            loadProfile();
            c000.Checked = settings.c000;
            c030.Checked = settings.c030;
            c100.Checked = settings.c100;
            c130.Checked = settings.c130;
            c200.Checked = settings.c200;
            c300.Checked = settings.c300;
            c400.Checked = settings.c400;
            c500.Checked = settings.c500;
            c600.Checked = settings.c600;
            r000.Checked = settings.r000;
            r030.Checked = settings.r030;
            r100.Checked = settings.r100;
            r130.Checked = settings.r130;
            r200.Checked = settings.r200;
            r300.Checked = settings.r300;
            r400.Checked = settings.r400;
            r500.Checked = settings.r500;
            p000.Checked = settings.p000;
            p030.Checked = settings.p030;
            p100.Checked = settings.p100;
            p130.Checked = settings.p130;
            p200.Checked = settings.p200;
            p300.Checked = settings.p300;
            p400.Checked = settings.p400;
            p500.Checked = settings.p500;
            p600.Checked = settings.p600;
            x000.Checked = settings.x000;
            x030.Checked = settings.x030;
            x100.Checked = settings.x100;
            x130.Checked = settings.x130;
            x200.Checked = settings.x200;
            oAV.Checked = settings.oAV;
            oAB.Checked = settings.oAB;
            oTU.Checked = settings.oTU;
            oTran.Checked = settings.oTran;
            base.Location = new Point(Math.Max(settings.wX, 0), Math.Max(settings.wY, 0));
            base.Width = settings.wS;
            Disp.Text = settings.Disp;
            AffP.Text = settings.AffP;
            NegP.Text = settings.NegP;
            Text = settings.speechType;
            if (Text == "Aff Prep")
            {
                Disp.ForeColor = AffP.ForeColor;
            }
            if (Text == "Neg Prep")
            {
                Disp.ForeColor = NegP.ForeColor;
            }
            Menubar.Cursor = Cursors.Hand;
        }

        private void TimerWin_Resize(object sender, EventArgs e)
        {
            Disp.Font = new Font(Disp.Font.Name, (float) ((int) (0.22 * base.Width)), FontStyle.Bold, Disp.Font.Unit, Disp.Font.GdiCharSet);
            base.Height = (int) (Disp.Height * 1.2);
        }

        private void TimeSet(string timeSet, string name)
        {
            if (timerTick.Enabled)
            {
                StartClick();
            }
            Text = name;
            Disp.Text = timeSet;
            Disp.ForeColor = (name == "Neg Prep") ? Color.FromArgb(0xc0, 0, 0) : ((name == "Aff Prep") ? Color.FromArgb(0, 0x44, 0xc0) : Color.Black);
        }

        private int val(string s)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(s);
            }
            catch
            {
            }
            return num;
        }

        private void Start_MouseLeave(object sender, EventArgs e)
        {
           // Start.HideDropDown();
        }

        private void Config_MouseLeave(object sender, EventArgs e)
        {
            // Config.HideDropDown();
           

        }

    }
}
