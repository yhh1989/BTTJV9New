namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    partial class ScheduleQuery
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
            this.gridControlScheduling = new DevExpress.XtraGrid.GridControl();
            this.gridViewScheduling = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSchedulingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingScheduleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingTimeFrame = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingTotalNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingItemGroupCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingIntroducer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlItemGroup = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnItemGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupChartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupNumberOfPeople = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.radioGroupType = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonQuery = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonPrint = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlScheduling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewScheduling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
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
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem7});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonPrint);
            this.layoutControlBase.Controls.Add(this.simpleButtonQuery);
            this.layoutControlBase.Controls.Add(this.radioGroupType);
            this.layoutControlBase.Controls.Add(this.dateEditEnd);
            this.layoutControlBase.Controls.Add(this.dateEditStart);
            this.layoutControlBase.Controls.Add(this.gridControlItemGroup);
            this.layoutControlBase.Controls.Add(this.gridControlScheduling);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(545, 293, 450, 400);
            // 
            // gridControlScheduling
            // 
            this.gridControlScheduling.Location = new System.Drawing.Point(12, 65);
            this.gridControlScheduling.MainView = this.gridViewScheduling;
            this.gridControlScheduling.Name = "gridControlScheduling";
            this.gridControlScheduling.Size = new System.Drawing.Size(356, 484);
            this.gridControlScheduling.TabIndex = 4;
            this.gridControlScheduling.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewScheduling});
            // 
            // gridViewScheduling
            // 
            this.gridViewScheduling.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSchedulingId,
            this.gridColumnSchedulingScheduleDate,
            this.gridColumnSchedulingTimeFrame,
            this.gridColumnSchedulingName,
            this.gridColumnSchedulingTotalNumber,
            this.gridColumnSchedulingItemGroupCount,
            this.gridColumnSchedulingRemarks,
            this.gridColumnSchedulingIntroducer});
            this.gridViewScheduling.GridControl = this.gridControlScheduling;
            this.gridViewScheduling.GroupCount = 1;
            this.gridViewScheduling.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalNumber", null, "(总人数: {0}人)"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ItemGroupCount", null, "(总项目: {0}个项目)")});
            this.gridViewScheduling.Name = "gridViewScheduling";
            this.gridViewScheduling.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewScheduling.OptionsBehavior.Editable = false;
            this.gridViewScheduling.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewScheduling.OptionsCustomization.AllowSort = false;
            this.gridViewScheduling.OptionsView.ShowGroupPanel = false;
            this.gridViewScheduling.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnSchedulingScheduleDate, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewScheduling.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridViewScheduling_CustomDrawGroupRow);
            this.gridViewScheduling.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewScheduling_FocusedRowChanged);
            // 
            // gridColumnSchedulingId
            // 
            this.gridColumnSchedulingId.Caption = "Id";
            this.gridColumnSchedulingId.FieldName = "Id";
            this.gridColumnSchedulingId.Name = "gridColumnSchedulingId";
            // 
            // gridColumnSchedulingScheduleDate
            // 
            this.gridColumnSchedulingScheduleDate.Caption = "日期";
            this.gridColumnSchedulingScheduleDate.DisplayFormat.FormatString = "D";
            this.gridColumnSchedulingScheduleDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnSchedulingScheduleDate.FieldName = "ScheduleDate";
            this.gridColumnSchedulingScheduleDate.GroupFormat.FormatString = "D";
            this.gridColumnSchedulingScheduleDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnSchedulingScheduleDate.Name = "gridColumnSchedulingScheduleDate";
            this.gridColumnSchedulingScheduleDate.Visible = true;
            this.gridColumnSchedulingScheduleDate.VisibleIndex = 0;
            // 
            // gridColumnSchedulingTimeFrame
            // 
            this.gridColumnSchedulingTimeFrame.Caption = "时段";
            this.gridColumnSchedulingTimeFrame.FieldName = "TimeFrame";
            this.gridColumnSchedulingTimeFrame.Name = "gridColumnSchedulingTimeFrame";
            this.gridColumnSchedulingTimeFrame.Visible = true;
            this.gridColumnSchedulingTimeFrame.VisibleIndex = 0;
            // 
            // gridColumnSchedulingName
            // 
            this.gridColumnSchedulingName.Caption = "名称";
            this.gridColumnSchedulingName.FieldName = "Name";
            this.gridColumnSchedulingName.Name = "gridColumnSchedulingName";
            this.gridColumnSchedulingName.Visible = true;
            this.gridColumnSchedulingName.VisibleIndex = 1;
            // 
            // gridColumnSchedulingTotalNumber
            // 
            this.gridColumnSchedulingTotalNumber.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnSchedulingTotalNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnSchedulingTotalNumber.Caption = "人数";
            this.gridColumnSchedulingTotalNumber.DisplayFormat.FormatString = "{0}人";
            this.gridColumnSchedulingTotalNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnSchedulingTotalNumber.FieldName = "TotalNumber";
            this.gridColumnSchedulingTotalNumber.Name = "gridColumnSchedulingTotalNumber";
            this.gridColumnSchedulingTotalNumber.Visible = true;
            this.gridColumnSchedulingTotalNumber.VisibleIndex = 2;
            // 
            // gridColumnSchedulingItemGroupCount
            // 
            this.gridColumnSchedulingItemGroupCount.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnSchedulingItemGroupCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnSchedulingItemGroupCount.Caption = "项目";
            this.gridColumnSchedulingItemGroupCount.DisplayFormat.FormatString = "{0}个项目";
            this.gridColumnSchedulingItemGroupCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnSchedulingItemGroupCount.FieldName = "ItemGroupCount";
            this.gridColumnSchedulingItemGroupCount.Name = "gridColumnSchedulingItemGroupCount";
            this.gridColumnSchedulingItemGroupCount.Visible = true;
            this.gridColumnSchedulingItemGroupCount.VisibleIndex = 3;
            // 
            // gridColumnSchedulingRemarks
            // 
            this.gridColumnSchedulingRemarks.Caption = "备注";
            this.gridColumnSchedulingRemarks.FieldName = "Remarks";
            this.gridColumnSchedulingRemarks.Name = "gridColumnSchedulingRemarks";
            this.gridColumnSchedulingRemarks.Visible = true;
            this.gridColumnSchedulingRemarks.VisibleIndex = 4;
            // 
            // gridColumnSchedulingIntroducer
            // 
            this.gridColumnSchedulingIntroducer.Caption = "介绍人";
            this.gridColumnSchedulingIntroducer.FieldName = "Introducer";
            this.gridColumnSchedulingIntroducer.Name = "gridColumnSchedulingIntroducer";
            this.gridColumnSchedulingIntroducer.Visible = true;
            this.gridColumnSchedulingIntroducer.VisibleIndex = 5;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlScheduling;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(360, 488);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridControlItemGroup
            // 
            this.gridControlItemGroup.Location = new System.Drawing.Point(372, 65);
            this.gridControlItemGroup.MainView = this.gridViewItemGroup;
            this.gridControlItemGroup.MaximumSize = new System.Drawing.Size(400, 0);
            this.gridControlItemGroup.MinimumSize = new System.Drawing.Size(400, 0);
            this.gridControlItemGroup.Name = "gridControlItemGroup";
            this.gridControlItemGroup.Size = new System.Drawing.Size(400, 484);
            this.gridControlItemGroup.TabIndex = 5;
            this.gridControlItemGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemGroup});
            // 
            // gridViewItemGroup
            // 
            this.gridViewItemGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnItemGroupId,
            this.gridColumnItemGroupChartName,
            this.gridColumnItemGroupName,
            this.gridColumnItemGroupNumberOfPeople});
            this.gridViewItemGroup.GridControl = this.gridControlItemGroup;
            this.gridViewItemGroup.GroupCount = 1;
            this.gridViewItemGroup.Name = "gridViewItemGroup";
            this.gridViewItemGroup.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewItemGroup.OptionsBehavior.Editable = false;
            this.gridViewItemGroup.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewItemGroup.OptionsCustomization.AllowSort = false;
            this.gridViewItemGroup.OptionsView.ShowGroupPanel = false;
            this.gridViewItemGroup.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnItemGroupChartName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnItemGroupId
            // 
            this.gridColumnItemGroupId.Caption = "Id";
            this.gridColumnItemGroupId.FieldName = "Id";
            this.gridColumnItemGroupId.Name = "gridColumnItemGroupId";
            // 
            // gridColumnItemGroupChartName
            // 
            this.gridColumnItemGroupChartName.Caption = "科别";
            this.gridColumnItemGroupChartName.FieldName = "ChartName";
            this.gridColumnItemGroupChartName.Name = "gridColumnItemGroupChartName";
            this.gridColumnItemGroupChartName.Visible = true;
            this.gridColumnItemGroupChartName.VisibleIndex = 0;
            // 
            // gridColumnItemGroupName
            // 
            this.gridColumnItemGroupName.Caption = "项目";
            this.gridColumnItemGroupName.FieldName = "ItemGroupName";
            this.gridColumnItemGroupName.Name = "gridColumnItemGroupName";
            this.gridColumnItemGroupName.Visible = true;
            this.gridColumnItemGroupName.VisibleIndex = 0;
            // 
            // gridColumnItemGroupNumberOfPeople
            // 
            this.gridColumnItemGroupNumberOfPeople.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnItemGroupNumberOfPeople.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnItemGroupNumberOfPeople.Caption = "人次";
            this.gridColumnItemGroupNumberOfPeople.DisplayFormat.FormatString = "{0}人次";
            this.gridColumnItemGroupNumberOfPeople.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnItemGroupNumberOfPeople.FieldName = "NumberOfPeople";
            this.gridColumnItemGroupNumberOfPeople.Name = "gridColumnItemGroupNumberOfPeople";
            this.gridColumnItemGroupNumberOfPeople.Visible = true;
            this.gridColumnItemGroupNumberOfPeople.VisibleIndex = 1;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlItemGroup;
            this.layoutControlItem2.Location = new System.Drawing.Point(360, 53);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(404, 488);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // dateEditStart
            // 
            this.dateEditStart.EditValue = null;
            this.dateEditStart.Location = new System.Drawing.Point(76, 12);
            this.dateEditStart.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateEditStart.MinimumSize = new System.Drawing.Size(150, 0);
            this.dateEditStart.Name = "dateEditStart";
            this.dateEditStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditStart.Size = new System.Drawing.Size(150, 20);
            this.dateEditStart.StyleController = this.layoutControlBase;
            this.dateEditStart.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateEditStart;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(218, 24);
            this.layoutControlItem3.Text = "起止日期：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = null;
            this.dateEditEnd.Location = new System.Drawing.Point(244, 12);
            this.dateEditEnd.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateEditEnd.MinimumSize = new System.Drawing.Size(150, 0);
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditEnd.Size = new System.Drawing.Size(150, 20);
            this.dateEditEnd.StyleController = this.layoutControlBase;
            this.dateEditEnd.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateEditEnd;
            this.layoutControlItem4.Location = new System.Drawing.Point(218, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(168, 24);
            this.layoutControlItem4.Text = "~";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // radioGroupType
            // 
            this.radioGroupType.AutoSizeInLayoutControl = true;
            this.radioGroupType.EditValue = 1;
            this.radioGroupType.Location = new System.Drawing.Point(125, 36);
            this.radioGroupType.Name = "radioGroupType";
            this.radioGroupType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "选中日期"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "查询时间"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "选中对象")});
            this.radioGroupType.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.radioGroupType.Size = new System.Drawing.Size(269, 25);
            this.radioGroupType.StyleController = this.layoutControlBase;
            this.radioGroupType.TabIndex = 8;
            this.radioGroupType.SelectedIndexChanged += new System.EventHandler(this.radioGroupType_SelectedIndexChanged);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.radioGroupType;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(386, 29);
            this.layoutControlItem5.Text = "项目列表展示模式：";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(108, 14);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // simpleButtonQuery
            // 
            this.simpleButtonQuery.AutoWidthInLayoutControl = true;
            this.simpleButtonQuery.Location = new System.Drawing.Point(672, 39);
            this.simpleButtonQuery.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonQuery.Name = "simpleButtonQuery";
            this.simpleButtonQuery.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonQuery.StyleController = this.layoutControlBase;
            this.simpleButtonQuery.TabIndex = 9;
            this.simpleButtonQuery.Text = "查询";
            this.simpleButtonQuery.Click += new System.EventHandler(this.simpleButtonQuery_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonQuery;
            this.layoutControlItem6.Location = new System.Drawing.Point(660, 27);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(386, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(274, 53);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(660, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(104, 27);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonPrint
            // 
            this.simpleButtonPrint.AutoWidthInLayoutControl = true;
            this.simpleButtonPrint.Location = new System.Drawing.Point(724, 39);
            this.simpleButtonPrint.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonPrint.Name = "simpleButtonPrint";
            this.simpleButtonPrint.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonPrint.StyleController = this.layoutControlBase;
            this.simpleButtonPrint.TabIndex = 10;
            this.simpleButtonPrint.Text = "打印";
            this.simpleButtonPrint.Click += new System.EventHandler(this.simpleButtonPrint_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButtonPrint;
            this.layoutControlItem7.Location = new System.Drawing.Point(712, 27);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // ScheduleQuery
            // 
            this.AcceptButton = this.simpleButtonQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "ScheduleQuery";
            this.Text = "排期查询";
            this.Load += new System.EventHandler(this.ScheduleQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlScheduling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewScheduling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlScheduling;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewScheduling;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControlItemGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DateEdit dateEditStart;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.RadioGroup radioGroupType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButtonQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingScheduleDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingTimeFrame;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingTotalNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingIntroducer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupChartName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupNumberOfPeople;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingItemGroupCount;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}