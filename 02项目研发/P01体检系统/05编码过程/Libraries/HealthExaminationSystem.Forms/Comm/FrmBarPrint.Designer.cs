namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    partial class FrmBarPrint
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
            this.components = new System.ComponentModel.Container();
            this.grcw = new DevExpress.XtraEditors.GroupControl();
            this.GrdNoPrint = new DevExpress.XtraGrid.GridControl();
            this.GrdvNoPrint = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.未打印 = new DevExpress.XtraLayout.LayoutControlItem();
            this.grcy = new DevExpress.XtraEditors.GroupControl();
            this.grdPrint = new DevExpress.XtraGrid.GridControl();
            this.grdvPrint = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.已打印 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnvoid = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.空白行 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcw)).BeginInit();
            this.grcw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdNoPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdvNoPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.未打印)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcy)).BeginInit();
            this.grcy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.已打印)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.空白行)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.未打印,
            this.已打印,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.simpleSeparator1,
            this.空白行,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(830, 416);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.btnvoid);
            this.layoutControlBase.Controls.Add(this.btnClose);
            this.layoutControlBase.Controls.Add(this.btnPrint);
            this.layoutControlBase.Controls.Add(this.grcy);
            this.layoutControlBase.Controls.Add(this.grcw);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(536, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(830, 416);
            // 
            // grcw
            // 
            this.grcw.Controls.Add(this.GrdNoPrint);
            this.grcw.Location = new System.Drawing.Point(12, 12);
            this.grcw.Name = "grcw";
            this.grcw.Size = new System.Drawing.Size(400, 364);
            this.grcw.TabIndex = 4;
            this.grcw.Text = "未打印";
            // 
            // GrdNoPrint
            // 
            this.GrdNoPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdNoPrint.Location = new System.Drawing.Point(2, 21);
            this.GrdNoPrint.MainView = this.GrdvNoPrint;
            this.GrdNoPrint.Name = "GrdNoPrint";
            this.GrdNoPrint.Size = new System.Drawing.Size(396, 341);
            this.GrdNoPrint.TabIndex = 0;
            this.GrdNoPrint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdvNoPrint});
            // 
            // GrdvNoPrint
            // 
            this.GrdvNoPrint.GridControl = this.GrdNoPrint;
            this.GrdvNoPrint.Name = "GrdvNoPrint";
            this.GrdvNoPrint.OptionsView.ShowFooter = true;
            this.GrdvNoPrint.OptionsView.ShowGroupPanel = false;
            // 
            // 未打印
            // 
            this.未打印.Control = this.grcw;
            this.未打印.Location = new System.Drawing.Point(0, 0);
            this.未打印.Name = "未打印";
            this.未打印.Size = new System.Drawing.Size(404, 368);
            this.未打印.TextSize = new System.Drawing.Size(0, 0);
            this.未打印.TextVisible = false;
            // 
            // grcy
            // 
            this.grcy.Controls.Add(this.grdPrint);
            this.grcy.Location = new System.Drawing.Point(416, 12);
            this.grcy.Name = "grcy";
            this.grcy.Size = new System.Drawing.Size(402, 364);
            this.grcy.TabIndex = 5;
            this.grcy.Text = "已打印";
            // 
            // grdPrint
            // 
            this.grdPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPrint.Location = new System.Drawing.Point(2, 21);
            this.grdPrint.MainView = this.grdvPrint;
            this.grdPrint.Name = "grdPrint";
            this.grdPrint.Size = new System.Drawing.Size(398, 341);
            this.grdPrint.TabIndex = 1;
            this.grdPrint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvPrint});
            // 
            // grdvPrint
            // 
            this.grdvPrint.GridControl = this.grdPrint;
            this.grdvPrint.Name = "grdvPrint";
            this.grdvPrint.OptionsView.ShowFooter = true;
            this.grdvPrint.OptionsView.ShowGroupPanel = false;
            // 
            // 已打印
            // 
            this.已打印.Control = this.grcy;
            this.已打印.Location = new System.Drawing.Point(404, 0);
            this.已打印.Name = "已打印";
            this.已打印.Size = new System.Drawing.Size(406, 368);
            this.已打印.TextSize = new System.Drawing.Size(0, 0);
            this.已打印.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 368);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(810, 2);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(321, 382);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(91, 22);
            this.btnPrint.StyleController = this.layoutControlBase;
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnPrint;
            this.layoutControlItem3.Location = new System.Drawing.Point(309, 370);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(416, 382);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 22);
            this.btnClose.StyleController = this.layoutControlBase;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            this.layoutControlItem4.Location = new System.Drawing.Point(404, 370);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // btnvoid
            // 
            this.btnvoid.Location = new System.Drawing.Point(729, 382);
            this.btnvoid.Name = "btnvoid";
            this.btnvoid.Size = new System.Drawing.Size(89, 22);
            this.btnvoid.StyleController = this.layoutControlBase;
            this.btnvoid.TabIndex = 8;
            this.btnvoid.Text = "作废";
            this.btnvoid.Click += new System.EventHandler(this.btnvoid_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnvoid;
            this.layoutControlItem5.Location = new System.Drawing.Point(717, 370);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // 空白行
            // 
            this.空白行.AllowHotTrack = false;
            this.空白行.Location = new System.Drawing.Point(0, 370);
            this.空白行.Name = "空白行";
            this.空白行.Size = new System.Drawing.Size(309, 26);
            this.空白行.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(491, 370);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(226, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmBarPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 416);
            this.Name = "FrmBarPrint";
            this.Text = "条码打印";
            this.Load += new System.EventHandler(this.FrmBarPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcw)).EndInit();
            this.grcw.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdNoPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdvNoPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.未打印)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcy)).EndInit();
            this.grcy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.已打印)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.空白行)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem 未打印;
        private DevExpress.XtraEditors.GroupControl grcw;
        private DevExpress.XtraGrid.GridControl GrdNoPrint;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdvNoPrint;
        private DevExpress.XtraLayout.LayoutControlItem 已打印;
        private DevExpress.XtraEditors.GroupControl grcy;
        private DevExpress.XtraGrid.GridControl grdPrint;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvPrint;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnvoid;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem 空白行;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
    }
}