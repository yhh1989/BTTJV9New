namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    partial class CopySchedule
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridViewItemGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnItemGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupItemGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupHelpChar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewScheduling = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSchedulingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingClientInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingPersonalName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingScheduleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingTotalNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSchedulingIntroducer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEditMonth = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.radioGroupIsTeam = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonQuery = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewScheduling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupIsTeam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.emptySpaceItem3,
            this.layoutControlItem6});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonCancel);
            this.layoutControlBase.Controls.Add(this.simpleButtonOk);
            this.layoutControlBase.Controls.Add(this.simpleButtonQuery);
            this.layoutControlBase.Controls.Add(this.radioGroupIsTeam);
            this.layoutControlBase.Controls.Add(this.dateEditMonth);
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(540, 264, 450, 400);
            // 
            // gridViewItemGroup
            // 
            this.gridViewItemGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnItemGroupId,
            this.gridColumnItemGroupItemGroupName,
            this.gridColumnItemGroupHelpChar});
            this.gridViewItemGroup.GridControl = this.gridControl;
            this.gridViewItemGroup.Name = "gridViewItemGroup";
            this.gridViewItemGroup.OptionsBehavior.Editable = false;
            this.gridViewItemGroup.OptionsCustomization.AllowGroup = false;
            this.gridViewItemGroup.OptionsCustomization.AllowSort = false;
            this.gridViewItemGroup.OptionsView.ShowFooter = true;
            this.gridViewItemGroup.OptionsView.ShowGroupPanel = false;
            this.gridViewItemGroup.ViewCaption = "项目";
            // 
            // gridColumnItemGroupId
            // 
            this.gridColumnItemGroupId.Caption = "Id";
            this.gridColumnItemGroupId.FieldName = "Id";
            this.gridColumnItemGroupId.Name = "gridColumnItemGroupId";
            // 
            // gridColumnItemGroupItemGroupName
            // 
            this.gridColumnItemGroupItemGroupName.Caption = "项目名称";
            this.gridColumnItemGroupItemGroupName.FieldName = "ItemGroupName";
            this.gridColumnItemGroupItemGroupName.Name = "gridColumnItemGroupItemGroupName";
            this.gridColumnItemGroupItemGroupName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ItemGroupName", "共{0}个项目")});
            this.gridColumnItemGroupItemGroupName.Visible = true;
            this.gridColumnItemGroupItemGroupName.VisibleIndex = 0;
            // 
            // gridColumnItemGroupHelpChar
            // 
            this.gridColumnItemGroupHelpChar.Caption = "助记码";
            this.gridColumnItemGroupHelpChar.FieldName = "HelpChar";
            this.gridColumnItemGroupHelpChar.Name = "gridColumnItemGroupHelpChar";
            this.gridColumnItemGroupHelpChar.Visible = true;
            this.gridColumnItemGroupHelpChar.VisibleIndex = 1;
            // 
            // gridControl
            // 
            gridLevelNode1.LevelTemplate = this.gridViewItemGroup;
            gridLevelNode1.RelationName = "ItemGroups";
            this.gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl.Location = new System.Drawing.Point(12, 65);
            this.gridControl.MainView = this.gridViewScheduling;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(760, 458);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewScheduling,
            this.gridViewItemGroup});
            // 
            // gridViewScheduling
            // 
            this.gridViewScheduling.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSchedulingId,
            this.gridColumnSchedulingClientInfo,
            this.gridColumnSchedulingPersonalName,
            this.gridColumnSchedulingScheduleDate,
            this.gridColumnSchedulingTotalNumber,
            this.gridColumnSchedulingRemarks,
            this.gridColumnSchedulingIntroducer});
            this.gridViewScheduling.GridControl = this.gridControl;
            this.gridViewScheduling.Name = "gridViewScheduling";
            this.gridViewScheduling.OptionsBehavior.Editable = false;
            this.gridViewScheduling.OptionsCustomization.AllowGroup = false;
            this.gridViewScheduling.OptionsCustomization.AllowSort = false;
            this.gridViewScheduling.OptionsView.ShowFooter = true;
            this.gridViewScheduling.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnSchedulingId
            // 
            this.gridColumnSchedulingId.Caption = "Id";
            this.gridColumnSchedulingId.FieldName = "Id";
            this.gridColumnSchedulingId.Name = "gridColumnSchedulingId";
            // 
            // gridColumnSchedulingClientInfo
            // 
            this.gridColumnSchedulingClientInfo.Caption = "单位";
            this.gridColumnSchedulingClientInfo.FieldName = "ClientInfo.ClientName";
            this.gridColumnSchedulingClientInfo.Name = "gridColumnSchedulingClientInfo";
            this.gridColumnSchedulingClientInfo.Visible = true;
            this.gridColumnSchedulingClientInfo.VisibleIndex = 0;
            // 
            // gridColumnSchedulingPersonalName
            // 
            this.gridColumnSchedulingPersonalName.Caption = "姓名";
            this.gridColumnSchedulingPersonalName.FieldName = "PersonalName";
            this.gridColumnSchedulingPersonalName.Name = "gridColumnSchedulingPersonalName";
            // 
            // gridColumnSchedulingScheduleDate
            // 
            this.gridColumnSchedulingScheduleDate.Caption = "排期";
            this.gridColumnSchedulingScheduleDate.FieldName = "ScheduleDate";
            this.gridColumnSchedulingScheduleDate.Name = "gridColumnSchedulingScheduleDate";
            this.gridColumnSchedulingScheduleDate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ScheduleDate", "共{0}个排期")});
            this.gridColumnSchedulingScheduleDate.Visible = true;
            this.gridColumnSchedulingScheduleDate.VisibleIndex = 1;
            // 
            // gridColumnSchedulingTotalNumber
            // 
            this.gridColumnSchedulingTotalNumber.Caption = "人数";
            this.gridColumnSchedulingTotalNumber.FieldName = "TotalNumber";
            this.gridColumnSchedulingTotalNumber.Name = "gridColumnSchedulingTotalNumber";
            this.gridColumnSchedulingTotalNumber.Visible = true;
            this.gridColumnSchedulingTotalNumber.VisibleIndex = 2;
            // 
            // gridColumnSchedulingRemarks
            // 
            this.gridColumnSchedulingRemarks.Caption = "备注";
            this.gridColumnSchedulingRemarks.FieldName = "Remarks";
            this.gridColumnSchedulingRemarks.Name = "gridColumnSchedulingRemarks";
            this.gridColumnSchedulingRemarks.Visible = true;
            this.gridColumnSchedulingRemarks.VisibleIndex = 3;
            // 
            // gridColumnSchedulingIntroducer
            // 
            this.gridColumnSchedulingIntroducer.Caption = "介绍人";
            this.gridColumnSchedulingIntroducer.FieldName = "Introducer";
            this.gridColumnSchedulingIntroducer.Name = "gridColumnSchedulingIntroducer";
            this.gridColumnSchedulingIntroducer.Visible = true;
            this.gridColumnSchedulingIntroducer.VisibleIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 462);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // dateEditMonth
            // 
            this.dateEditMonth.EditValue = null;
            this.dateEditMonth.Location = new System.Drawing.Point(52, 12);
            this.dateEditMonth.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateEditMonth.MinimumSize = new System.Drawing.Size(150, 0);
            this.dateEditMonth.Name = "dateEditMonth";
            this.dateEditMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMonth.Properties.Mask.EditMask = "y";
            this.dateEditMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEditMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEditMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dateEditMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dateEditMonth.Size = new System.Drawing.Size(150, 20);
            this.dateEditMonth.StyleController = this.layoutControlBase;
            this.dateEditMonth.TabIndex = 5;
            this.dateEditMonth.DateTimeChanged += new System.EventHandler(this.dateEditMonth_DateTimeChanged);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditMonth;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(194, 24);
            this.layoutControlItem2.Text = "月份：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(194, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(500, 53);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // radioGroupIsTeam
            // 
            this.radioGroupIsTeam.EditValue = true;
            this.radioGroupIsTeam.Location = new System.Drawing.Point(52, 36);
            this.radioGroupIsTeam.Name = "radioGroupIsTeam";
            this.radioGroupIsTeam.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "单位"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "个人")});
            this.radioGroupIsTeam.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.radioGroupIsTeam.Size = new System.Drawing.Size(150, 25);
            this.radioGroupIsTeam.StyleController = this.layoutControlBase;
            this.radioGroupIsTeam.TabIndex = 6;
            this.radioGroupIsTeam.SelectedIndexChanged += new System.EventHandler(this.radioGroupIsTeam_SelectedIndexChanged);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.radioGroupIsTeam;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(194, 29);
            this.layoutControlItem3.Text = "类别：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonQuery
            // 
            this.simpleButtonQuery.AutoWidthInLayoutControl = true;
            this.simpleButtonQuery.Location = new System.Drawing.Point(706, 39);
            this.simpleButtonQuery.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonQuery.Name = "simpleButtonQuery";
            this.simpleButtonQuery.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonQuery.StyleController = this.layoutControlBase;
            this.simpleButtonQuery.TabIndex = 7;
            this.simpleButtonQuery.Text = "查询(&Q)";
            this.simpleButtonQuery.Click += new System.EventHandler(this.simpleButtonQuery_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonQuery;
            this.layoutControlItem4.Location = new System.Drawing.Point(694, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(694, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(70, 27);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.AutoWidthInLayoutControl = true;
            this.simpleButtonOk.Location = new System.Drawing.Point(636, 527);
            this.simpleButtonOk.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonOk.StyleController = this.layoutControlBase;
            this.simpleButtonOk.TabIndex = 8;
            this.simpleButtonOk.Text = "复制(&S)";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonOk;
            this.layoutControlItem5.Location = new System.Drawing.Point(624, 515);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 515);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(624, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AutoWidthInLayoutControl = true;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(706, 527);
            this.simpleButtonCancel.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonCancel.StyleController = this.layoutControlBase;
            this.simpleButtonCancel.TabIndex = 9;
            this.simpleButtonCancel.Text = "取消(&C)";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonCancel;
            this.layoutControlItem6.Location = new System.Drawing.Point(694, 515);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // CopySchedule
            // 
            this.AcceptButton = this.simpleButtonQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CopySchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "复制排期内容";
            this.Load += new System.EventHandler(this.CopySchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewScheduling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupIsTeam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewScheduling;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dateEditMonth;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.RadioGroup radioGroupIsTeam;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingScheduleDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingTotalNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingClientInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingIntroducer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulingPersonalName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupItemGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupHelpChar;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}