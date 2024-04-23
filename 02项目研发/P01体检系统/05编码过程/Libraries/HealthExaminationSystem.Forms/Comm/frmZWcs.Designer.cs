namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    partial class frmZWcs
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
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.butClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labInfo = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butZW = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butQXZW = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(571, 317);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.butOK);
            this.layoutControlBase.Controls.Add(this.butQXZW);
            this.layoutControlBase.Controls.Add(this.butZW);
            this.layoutControlBase.Controls.Add(this.labInfo);
            this.layoutControlBase.Controls.Add(this.butClose);
            this.layoutControlBase.Margin = new System.Windows.Forms.Padding(6);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1070, 213, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(571, 317);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 271);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(137, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(368, 271);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(183, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // butClose
            // 
            this.butClose.AutoWidthInLayoutControl = true;
            this.butClose.Location = new System.Drawing.Point(328, 283);
            this.butClose.MinimumSize = new System.Drawing.Size(48, 0);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(48, 22);
            this.butClose.StyleController = this.layoutControlBase;
            this.butClose.TabIndex = 7;
            this.butClose.Text = "退出";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.butClose;
            this.layoutControlItem4.Location = new System.Drawing.Point(316, 271);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // labInfo
            // 
            this.labInfo.EditValue = "提示信息";
            this.labInfo.Location = new System.Drawing.Point(12, 12);
            this.labInfo.Name = "labInfo";
            this.labInfo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labInfo.Properties.Appearance.Options.UseFont = true;
            this.labInfo.Properties.ReadOnly = true;
            this.labInfo.Size = new System.Drawing.Size(547, 267);
            this.labInfo.StyleController = this.layoutControlBase;
            this.labInfo.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labInfo;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(551, 271);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // butZW
            // 
            this.butZW.Location = new System.Drawing.Point(149, 283);
            this.butZW.Name = "butZW";
            this.butZW.Size = new System.Drawing.Size(57, 22);
            this.butZW.StyleController = this.layoutControlBase;
            this.butZW.TabIndex = 9;
            this.butZW.Text = "采集指纹";
            this.butZW.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.butZW;
            this.layoutControlItem1.Location = new System.Drawing.Point(137, 271);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // butQXZW
            // 
            this.butQXZW.Location = new System.Drawing.Point(210, 283);
            this.butQXZW.Name = "butQXZW";
            this.butQXZW.Size = new System.Drawing.Size(57, 22);
            this.butQXZW.StyleController = this.layoutControlBase;
            this.butQXZW.TabIndex = 10;
            this.butQXZW.Text = "取消采集";
            this.butQXZW.Click += new System.EventHandler(this.simpleButton2_Click_1);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.butQXZW;
            this.layoutControlItem2.Location = new System.Drawing.Point(198, 271);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(271, 283);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(53, 22);
            this.butOK.StyleController = this.layoutControlBase;
            this.butOK.TabIndex = 11;
            this.butOK.Text = "确定";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.butOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(259, 271);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(57, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // frmZWcs
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 317);
            this.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmZWcs";
            this.Text = "指纹识别";
            this.Load += new System.EventHandler(this.frmZWcs_Load);
            this.Shown += new System.EventHandler(this.frmZWcs_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton butClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.MemoEdit labInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton butQXZW;
        private DevExpress.XtraEditors.SimpleButton butZW;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton butOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}