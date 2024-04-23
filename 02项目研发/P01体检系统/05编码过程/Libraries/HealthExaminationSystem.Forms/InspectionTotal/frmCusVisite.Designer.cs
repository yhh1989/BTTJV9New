namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    partial class frmCusVisite
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
            this.radioVisiteState = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.comColour = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.memoRemark = new DevExpress.XtraRichEdit.RichEditControl();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.timeVistit = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioVisiteState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comColour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeVistit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeVistit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem2});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(649, 417);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.butClose);
            this.layoutControlBase.Controls.Add(this.butOK);
            this.layoutControlBase.Controls.Add(this.memoRemark);
            this.layoutControlBase.Controls.Add(this.comColour);
            this.layoutControlBase.Controls.Add(this.radioVisiteState);
            this.layoutControlBase.Controls.Add(this.timeVistit);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1264, 205, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(649, 417);
            // 
            // radioVisiteState
            // 
            this.radioVisiteState.Location = new System.Drawing.Point(12, 12);
            this.radioVisiteState.Name = "radioVisiteState";
            this.radioVisiteState.Properties.Columns = 2;
            this.radioVisiteState.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "回访"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "取消回访")});
            this.radioVisiteState.Size = new System.Drawing.Size(625, 25);
            this.radioVisiteState.StyleController = this.layoutControlBase;
            this.radioVisiteState.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioVisiteState;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(629, 29);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.timeVistit;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(314, 24);
            this.layoutControlItem2.Text = "回访时间：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // comColour
            // 
            this.comColour.Location = new System.Drawing.Point(390, 41);
            this.comColour.Name = "comColour";
            this.comColour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comColour.Properties.Items.AddRange(new object[] {
            "无颜色",
            "橙色",
            "黄色"});
            this.comColour.Size = new System.Drawing.Size(247, 20);
            this.comColour.StyleController = this.layoutControlBase;
            this.comColour.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.comColour;
            this.layoutControlItem3.Location = new System.Drawing.Point(314, 29);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(315, 24);
            this.layoutControlItem3.Text = "颜色：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(362, 371);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(267, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // memoRemark
            // 
            this.memoRemark.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.memoRemark.Location = new System.Drawing.Point(76, 65);
            this.memoRemark.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Size = new System.Drawing.Size(561, 314);
            this.memoRemark.TabIndex = 20;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.memoRemark;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(629, 318);
            this.layoutControlItem4.Text = "备注：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(270, 383);
            this.butOK.MinimumSize = new System.Drawing.Size(48, 0);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(48, 22);
            this.butOK.StyleController = this.layoutControlBase;
            this.butOK.TabIndex = 21;
            this.butOK.Text = "保存";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.butOK;
            this.layoutControlItem5.Location = new System.Drawing.Point(258, 371);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(322, 383);
            this.butClose.MinimumSize = new System.Drawing.Size(48, 0);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(48, 22);
            this.butClose.StyleController = this.layoutControlBase;
            this.butClose.TabIndex = 22;
            this.butClose.Text = "关闭";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.butClose;
            this.layoutControlItem6.Location = new System.Drawing.Point(310, 371);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 371);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(258, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // timeVistit
            // 
            this.timeVistit.EditValue = new System.DateTime(2021, 10, 8, 0, 0, 0, 0);
            this.timeVistit.Location = new System.Drawing.Point(76, 41);
            this.timeVistit.Name = "timeVistit";
            this.timeVistit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeVistit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeVistit.Properties.DisplayFormat.FormatString = "";
            this.timeVistit.Properties.EditFormat.FormatString = "";
            this.timeVistit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.timeVistit.Properties.Mask.EditMask = "D";
            this.timeVistit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.timeVistit.Size = new System.Drawing.Size(246, 20);
            this.timeVistit.StyleController = this.layoutControlBase;
            this.timeVistit.TabIndex = 5;
            // 
            // frmCusVisite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 417);
            this.Name = "frmCusVisite";
            this.Text = "回访设置";
            this.Load += new System.EventHandler(this.frmCusVisite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioVisiteState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comColour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeVistit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeVistit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.RadioGroup radioVisiteState;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ComboBoxEdit comColour;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraRichEdit.RichEditControl memoRemark;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton butClose;
        private DevExpress.XtraEditors.SimpleButton butOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.DateEdit timeVistit;
    }
}