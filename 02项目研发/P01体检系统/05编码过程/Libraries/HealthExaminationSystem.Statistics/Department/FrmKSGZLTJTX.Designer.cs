namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    partial class FrmKSGZLTJTX
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
            DevExpress.XtraCharts.ChartTitle chartTitle3 = new DevExpress.XtraCharts.ChartTitle();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dgc = new DevExpress.XtraGrid.GridControl();
            this.dgv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ChartControl = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cob_LeiXing = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.dtpStart = new DevExpress.XtraEditors.DateEdit();
            this.rdo_Period = new DevExpress.XtraEditors.RadioGroup();
            this.btn_Select = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.周期 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txt_Department = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtpEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnExprt = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cob_LeiXing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_Period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.周期)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Department.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
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
            this.layoutControlItem6,
            this.周期,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(1016, 593);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.btnExprt);
            this.layoutControlBase.Controls.Add(this.btnSelect);
            this.layoutControlBase.Controls.Add(this.ChartControl);
            this.layoutControlBase.Controls.Add(this.groupControl1);
            this.layoutControlBase.Controls.Add(this.dtpStart);
            this.layoutControlBase.Controls.Add(this.rdo_Period);
            this.layoutControlBase.Controls.Add(this.cob_LeiXing);
            this.layoutControlBase.Controls.Add(this.dtpEnd);
            this.layoutControlBase.Controls.Add(this.txt_Department);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(1016, 593);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dgc);
            this.groupControl1.Location = new System.Drawing.Point(690, 41);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(314, 540);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "科室";
            // 
            // dgc
            // 
            this.dgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgc.Location = new System.Drawing.Point(2, 21);
            this.dgc.MainView = this.dgv;
            this.dgc.Name = "dgc";
            this.dgc.Size = new System.Drawing.Size(310, 517);
            this.dgc.TabIndex = 12;
            this.dgc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv});
            // 
            // dgv
            // 
            this.dgv.GridControl = this.dgc;
            this.dgv.Name = "dgv";
            this.dgv.OptionsBehavior.Editable = false;
            this.dgv.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(678, 29);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(318, 544);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ChartControl
            // 
            this.ChartControl.DataBindings = null;
            this.ChartControl.Legend.Name = "Default Legend";
            this.ChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartControl.Location = new System.Drawing.Point(12, 41);
            this.ChartControl.Name = "ChartControl";
            this.ChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.ChartControl.Size = new System.Drawing.Size(674, 540);
            this.ChartControl.TabIndex = 13;
            chartTitle3.Text = "科室工作量统计";
            this.ChartControl.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle3});
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ChartControl;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(678, 544);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // cob_LeiXing
            // 
            this.cob_LeiXing.EditValue = "柱状图";
            this.cob_LeiXing.Location = new System.Drawing.Point(827, 12);
            this.cob_LeiXing.Name = "cob_LeiXing";
            this.cob_LeiXing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cob_LeiXing.Properties.Items.AddRange(new object[] {
            "柱状图",
            "饼图",
            "折线图"});
            this.cob_LeiXing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cob_LeiXing.Size = new System.Drawing.Size(71, 20);
            this.cob_LeiXing.StyleController = this.layoutControlBase;
            this.cob_LeiXing.TabIndex = 13;
            // 
            // btn_Export
            // 
            this.btn_Export.AutoWidthInLayoutControl = true;
            this.btn_Export.Location = new System.Drawing.Point(954, 12);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(49, 22);
            this.btn_Export.TabIndex = 9;
            this.btn_Export.Text = "  导出  ";
            // 
            // dtpStart
            // 
            this.dtpStart.EditValue = null;
            this.dtpStart.Location = new System.Drawing.Point(76, 12);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStart.Properties.NullText = "请选择";
            this.dtpStart.Size = new System.Drawing.Size(123, 20);
            this.dtpStart.StyleController = this.layoutControlBase;
            this.dtpStart.TabIndex = 0;
            // 
            // rdo_Period
            // 
            this.rdo_Period.EditValue = 1;
            this.rdo_Period.Location = new System.Drawing.Point(403, 12);
            this.rdo_Period.Name = "rdo_Period";
            this.rdo_Period.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "每周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "每月"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "每季度")});
            this.rdo_Period.Size = new System.Drawing.Size(209, 25);
            this.rdo_Period.StyleController = this.layoutControlBase;
            this.rdo_Period.TabIndex = 11;
            // 
            // btn_Select
            // 
            this.btn_Select.AutoWidthInLayoutControl = true;
            this.btn_Select.Location = new System.Drawing.Point(901, 12);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(49, 22);
            this.btn_Select.TabIndex = 9;
            this.btn_Select.Text = "  统计  ";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dtpStart;
            this.layoutControlItem3.CustomizationFormText = "起止时间：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(191, 29);
            this.layoutControlItem3.Text = "起止时间：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // 周期
            // 
            this.周期.Control = this.rdo_Period;
            this.周期.CustomizationFormText = "周期：";
            this.周期.Location = new System.Drawing.Point(327, 0);
            this.周期.Name = "周期";
            this.周期.Size = new System.Drawing.Size(277, 29);
            this.周期.Text = "周期：";
            this.周期.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt_Department;
            this.layoutControlItem4.CustomizationFormText = "科室名称：";
            this.layoutControlItem4.Location = new System.Drawing.Point(604, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 29);
            this.layoutControlItem4.Text = "科室名称：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // txt_Department
            // 
            this.txt_Department.EditValue = "";
            this.txt_Department.Location = new System.Drawing.Point(680, 12);
            this.txt_Department.Name = "txt_Department";
            this.txt_Department.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txt_Department.Properties.NullText = "请选择";
            this.txt_Department.Size = new System.Drawing.Size(79, 20);
            this.txt_Department.StyleController = this.layoutControlBase;
            this.txt_Department.TabIndex = 4;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cob_LeiXing;
            this.layoutControlItem5.CustomizationFormText = "图形类别：";
            this.layoutControlItem5.Location = new System.Drawing.Point(751, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(139, 29);
            this.layoutControlItem5.Text = "图形类别：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // dtpEnd
            // 
            this.dtpEnd.EditValue = null;
            this.dtpEnd.Location = new System.Drawing.Point(217, 12);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEnd.Properties.NullText = "请选择";
            this.dtpEnd.Size = new System.Drawing.Size(118, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dtpEnd;
            this.layoutControlItem6.CustomizationFormText = "~";
            this.layoutControlItem6.Location = new System.Drawing.Point(191, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(136, 29);
            this.layoutControlItem6.Text = "~";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // btnExprt
            // 
            this.btnExprt.AutoWidthInLayoutControl = true;
            this.btnExprt.Location = new System.Drawing.Point(955, 12);
            this.btnExprt.Name = "btnExprt";
            this.btnExprt.Size = new System.Drawing.Size(49, 22);
            this.btnExprt.StyleController = this.layoutControlBase;
            this.btnExprt.TabIndex = 14;
            this.btnExprt.Text = "  导出  ";
            this.btnExprt.Click += new System.EventHandler(this.btnExprt_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.AutoWidthInLayoutControl = true;
            this.btnSelect.Location = new System.Drawing.Point(902, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(49, 22);
            this.btnSelect.StyleController = this.layoutControlBase;
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "  统计  ";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnExprt;
            this.layoutControlItem7.Location = new System.Drawing.Point(943, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(53, 29);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSelect;
            this.layoutControlItem8.Location = new System.Drawing.Point(890, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(53, 29);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // FrmKSGZLTJTX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 593);
            this.Name = "FrmKSGZLTJTX";
            this.Text = "科室工作量统计图形";
            this.Load += new System.EventHandler(this.FrmKSGZLTJTX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cob_LeiXing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_Period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.周期)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Department.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl dgc;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraCharts.ChartControl ChartControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DateEdit dtpStart;
        private DevExpress.XtraEditors.RadioGroup rdo_Period;
        private DevExpress.XtraEditors.ComboBoxEdit cob_LeiXing;
        private DevExpress.XtraLayout.LayoutControlItem 周期;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraEditors.SimpleButton btn_Select;
        private DevExpress.XtraEditors.DateEdit dtpEnd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnExprt;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txt_Department;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}