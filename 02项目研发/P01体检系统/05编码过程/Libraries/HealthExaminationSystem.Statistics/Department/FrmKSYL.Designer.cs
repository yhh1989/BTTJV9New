namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    partial class FrmKSYL
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
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtpEnd = new DevExpress.XtraEditors.DateEdit();
            this.dtpStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcItem = new DevExpress.XtraGrid.GridControl();
            this.gvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnKSJX = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chkcmbDepartment = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.科室名称 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.科室名称)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.emptySpaceItem1,
            this.科室名称});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(923, 588);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.btnSearch);
            this.layoutControlBase.Controls.Add(this.btnKSJX);
            this.layoutControlBase.Controls.Add(this.btnExportToExcel);
            this.layoutControlBase.Controls.Add(this.gcItem);
            this.layoutControlBase.Controls.Add(this.dtpStart);
            this.layoutControlBase.Controls.Add(this.dtpEnd);
            this.layoutControlBase.Controls.Add(this.chkcmbDepartment);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(923, 588);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dtpEnd;
            this.layoutControlItem2.Location = new System.Drawing.Point(339, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(381, 24);
            this.layoutControlItem2.Text = "结束时间：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // dtpEnd
            // 
            this.dtpEnd.EditValue = new System.DateTime(2018, 8, 11, 0, 0, 0, 0);
            this.dtpEnd.Location = new System.Drawing.Point(415, 12);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEnd.Properties.DisplayFormat.FormatString = "";
            this.dtpEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEnd.Properties.EditFormat.FormatString = "";
            this.dtpEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEnd.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.dtpEnd.Properties.Mask.EditMask = "D";
            this.dtpEnd.Properties.NullText = "请选择";
            this.dtpEnd.Size = new System.Drawing.Size(313, 20);
            this.dtpEnd.StyleController = this.layoutControlBase;
            this.dtpEnd.TabIndex = 5;
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
            this.dtpStart.Properties.Mask.EditMask = "D";
            this.dtpStart.Properties.NullText = "请选择";
            this.dtpStart.Size = new System.Drawing.Size(271, 20);
            this.dtpStart.StyleController = this.layoutControlBase;
            this.dtpStart.TabIndex = 7;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(903, 520);
            this.layoutControlGroup2.Text = "结果";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gcItem;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(879, 477);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // gcItem
            // 
            this.gcItem.Location = new System.Drawing.Point(24, 91);
            this.gcItem.MainView = this.gvItem;
            this.gcItem.Name = "gcItem";
            this.gcItem.Size = new System.Drawing.Size(875, 473);
            this.gcItem.TabIndex = 11;
            this.gcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItem});
            // 
            // gvItem
            // 
            this.gvItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn15});
            this.gvItem.GridControl = this.gcItem;
            this.gvItem.Name = "gvItem";
            this.gvItem.OptionsBehavior.Editable = false;
            this.gvItem.OptionsView.AllowCellMerge = true;
            this.gvItem.OptionsView.ShowGroupPanel = false;
            this.gvItem.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gvItem_CellMerge);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "科室名称";
            this.gridColumn1.FieldName = "DepartmentName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 83;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "医生";
            this.gridColumn2.FieldName = "InspectEmployeeName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 85;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "组合名称";
            this.gridColumn5.FieldName = "ItemGroupName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 129;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "最大人数";
            this.gridColumn3.FieldName = "MaxNum";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 63;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "总人数";
            this.gridColumn8.FieldName = "TotalNum";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 63;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "在检人数";
            this.gridColumn6.FieldName = "CheckingNum";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 72;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "已检人数";
            this.gridColumn7.FieldName = "CheckedNum";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 92;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "未检人数";
            this.gridColumn9.FieldName = "UnCheckedNum";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 72;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "科室开始检查时间";
            this.gridColumn4.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "StartTime";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 101;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "平均时间";
            this.gridColumn15.FieldName = "AvgTime";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            this.gridColumn15.Width = 67;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dtpStart;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(849, 24);
            this.layoutControlItem1.Text = "开始时间：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtpStart;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(339, 24);
            this.layoutControlItem5.Text = "开始时间：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AutoWidthInLayoutControl = true;
            this.btnExportToExcel.Location = new System.Drawing.Point(850, 12);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(61, 22);
            this.btnExportToExcel.StyleController = this.layoutControlBase;
            this.btnExportToExcel.TabIndex = 12;
            this.btnExportToExcel.Text = "导出Excel";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportItemResult_Click);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnExportToExcel;
            this.layoutControlItem9.Location = new System.Drawing.Point(838, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(65, 48);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // btnKSJX
            // 
            this.btnKSJX.AutoWidthInLayoutControl = true;
            this.btnKSJX.Location = new System.Drawing.Point(732, 12);
            this.btnKSJX.Name = "btnKSJX";
            this.btnKSJX.Size = new System.Drawing.Size(57, 22);
            this.btnKSJX.StyleController = this.layoutControlBase;
            this.btnKSJX.TabIndex = 12;
            this.btnKSJX.Text = "科室绩效";
            this.btnKSJX.Click += new System.EventHandler(this.btnKSJX_Click);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnKSJX;
            this.layoutControlItem11.Location = new System.Drawing.Point(720, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(61, 48);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoWidthInLayoutControl = true;
            this.btnSearch.Location = new System.Drawing.Point(793, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(53, 22);
            this.btnSearch.StyleController = this.layoutControlBase;
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "   查询  ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnSearch;
            this.layoutControlItem12.Location = new System.Drawing.Point(781, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(57, 48);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(339, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(381, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chkcmbDepartment
            // 
            this.chkcmbDepartment.Location = new System.Drawing.Point(76, 36);
            this.chkcmbDepartment.Name = "chkcmbDepartment";
            this.chkcmbDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.chkcmbDepartment.Size = new System.Drawing.Size(271, 20);
            this.chkcmbDepartment.StyleController = this.layoutControlBase;
            this.chkcmbDepartment.TabIndex = 0;
            // 
            // 科室名称
            // 
            this.科室名称.Control = this.chkcmbDepartment;
            this.科室名称.CustomizationFormText = "科室名称：";
            this.科室名称.Location = new System.Drawing.Point(0, 24);
            this.科室名称.Name = "科室名称";
            this.科室名称.Size = new System.Drawing.Size(339, 24);
            this.科室名称.Text = "科室名称：";
            this.科室名称.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FrmKSYL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 588);
            this.Name = "FrmKSYL";
            this.Text = "科室工作量统计";
            this.Load += new System.EventHandler(this.FrmKSGZL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.科室名称)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dtpEnd;
        private DevExpress.XtraEditors.DateEdit dtpStart;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnKSJX;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraGrid.GridControl gcItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbDepartment;
        private DevExpress.XtraLayout.LayoutControlItem 科室名称;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}