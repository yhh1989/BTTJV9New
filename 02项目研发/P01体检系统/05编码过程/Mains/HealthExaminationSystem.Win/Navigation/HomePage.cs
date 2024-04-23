using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Navigation.DefinedControl;

namespace HealthExaminationSystem.Win.Navigation
{
    /// <summary>
    /// 起始页
    /// </summary>
    public partial class HomePage : XtraForm
    {
        /// <summary>
        /// 初始化 起始页
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 预约排期导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = schedulingAnAppointmentXtraUserControl1;
            schedulingAnAppointmentXtraUserControl1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement1.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement1;
        }

        /// <summary>
        /// 财务管理导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = financeManagerXtraUserControl1;
            financeManagerXtraUserControl1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement4.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement4;
        }

        /// <summary>
        /// 窗体第一次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomePage_Shown(object sender, EventArgs e)
        {
            tableLayoutPanel1.Tag = schedulingAnAppointmentXtraUserControl1;
            accordionControl1.Tag = accordionControlElement1;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// 前台管理导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl前台管理1;
            xtraUserControl前台管理1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement5.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement5;
        }

        /// <summary>
        /// 健康管理导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl健康体检1;
            xtraUserControl健康体检1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement2.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement2;
        }

        /// <summary>
        /// 职业健康导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl职业健康1;
            xtraUserControl职业健康1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement3.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement3;
        }

        /// <summary>
        /// 从业体检导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl从业体检1;
            xtraUserControl从业体检1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement6.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement6;
        }

        /// <summary>
        /// 检后导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl检后1;
            xtraUserControl检后1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement7.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement7;
        }

        /// <summary>
        /// 主任管理导航点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Tag is XtraUserControl control)
            {
                control.Hide();
            }
            tableLayoutPanel1.Tag = xtraUserControl主任管理1;
            xtraUserControl主任管理1.Show();
            if (accordionControl1.Tag is AccordionControlElement element)
            {
                element.Appearance.Normal.BackColor = Color.FromArgb(0, 0, 0, 0);
            }

            accordionControlElement8.Appearance.Normal.BackColor = SystemColors.HotTrack;
            accordionControl1.Tag = accordionControlElement8;
        }
    }
}