namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.DailyReport
{
    partial class DailyReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyReport));
            this.dateSelect = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueCollector = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rptDaily = new Axgregn6Lib.AxGRPrintViewer();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.datestar = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSelect.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSelect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCollector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestar.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem7,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem6});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(933, 525);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.datestar);
            this.layoutControlBase.Controls.Add(this.rptDaily);
            this.layoutControlBase.Controls.Add(this.btnExit);
            this.layoutControlBase.Controls.Add(this.btnPrint);
            this.layoutControlBase.Controls.Add(this.btnSearch);
            this.layoutControlBase.Controls.Add(this.dateSelect);
            this.layoutControlBase.Controls.Add(this.lueCollector);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(581, 297, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(933, 525);
            // 
            // dateSelect
            // 
            this.dateSelect.EditValue = null;
            this.dateSelect.Location = new System.Drawing.Point(189, 12);
            this.dateSelect.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateSelect.Name = "dateSelect";
            this.dateSelect.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateSelect.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSelect.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dateSelect.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSelect.Properties.DisplayFormat.FormatString = "D";
            this.dateSelect.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateSelect.Properties.EditFormat.FormatString = "D";
            this.dateSelect.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateSelect.Properties.Mask.EditMask = "D";
            this.dateSelect.Size = new System.Drawing.Size(107, 20);
            this.dateSelect.StyleController = this.layoutControlBase;
            this.dateSelect.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateSelect;
            this.layoutControlItem1.CustomizationFormText = "日期";
            this.layoutControlItem1.Location = new System.Drawing.Point(163, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(125, 26);
            this.layoutControlItem1.Text = "~";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(769, 12);
            this.btnSearch.MaximumSize = new System.Drawing.Size(48, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 22);
            this.btnSearch.StyleController = this.layoutControlBase;
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSearch;
            this.layoutControlItem3.CustomizationFormText = "查询";
            this.layoutControlItem3.Location = new System.Drawing.Point(757, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(821, 12);
            this.btnPrint.MaximumSize = new System.Drawing.Size(48, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(48, 22);
            this.btnPrint.StyleController = this.layoutControlBase;
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnPrint;
            this.layoutControlItem4.CustomizationFormText = "打印";
            this.layoutControlItem4.Location = new System.Drawing.Point(809, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(873, 12);
            this.btnExit.MaximumSize = new System.Drawing.Size(48, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 22);
            this.btnExit.StyleController = this.layoutControlBase;
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnExit;
            this.layoutControlItem5.CustomizationFormText = "退出";
            this.layoutControlItem5.Location = new System.Drawing.Point(861, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(488, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(269, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lueCollector;
            this.layoutControlItem2.CustomizationFormText = "收费员";
            this.layoutControlItem2.Location = new System.Drawing.Point(288, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "收费员：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // lueCollector
            // 
            this.lueCollector.Location = new System.Drawing.Point(364, 12);
            this.lueCollector.MaximumSize = new System.Drawing.Size(150, 0);
            this.lueCollector.Name = "lueCollector";
            this.lueCollector.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCollector.Properties.DisplayMember = "Name";
            this.lueCollector.Properties.NullText = "";
            this.lueCollector.Properties.ValueMember = "Id";
            this.lueCollector.Properties.View = this.searchLookUpEdit1View;
            this.lueCollector.Size = new System.Drawing.Size(132, 20);
            this.lueCollector.StyleController = this.layoutControlBase;
            this.lueCollector.TabIndex = 10;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "用户";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // rptDaily
            // 
            this.rptDaily.Enabled = true;
            this.rptDaily.Location = new System.Drawing.Point(12, 38);
            this.rptDaily.Name = "rptDaily";
            this.rptDaily.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rptDaily.OcxState")));
            this.rptDaily.Size = new System.Drawing.Size(909, 475);
            this.rptDaily.TabIndex = 11;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.rptDaily;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(913, 479);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // datestar
            // 
            this.datestar.EditValue = null;
            this.datestar.Location = new System.Drawing.Point(76, 12);
            this.datestar.MaximumSize = new System.Drawing.Size(150, 0);
            this.datestar.Name = "datestar";
            this.datestar.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.datestar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestar.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.datestar.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestar.Properties.DisplayFormat.FormatString = "D";
            this.datestar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datestar.Properties.EditFormat.FormatString = "D";
            this.datestar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datestar.Properties.Mask.EditMask = "D";
            this.datestar.Size = new System.Drawing.Size(95, 20);
            this.datestar.StyleController = this.layoutControlBase;
            this.datestar.TabIndex = 12;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.datestar;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(163, 26);
            this.layoutControlItem6.Text = "开始时间：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // DailyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 525);
            this.Name = "DailyReport";
            this.Text = "日报表";
            this.Load += new System.EventHandler(this.DailyReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSelect.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSelect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCollector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestar.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit dateSelect;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Axgregn6Lib.AxGRPrintViewer rptDaily;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.DateEdit datestar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SearchLookUpEdit lueCollector;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}