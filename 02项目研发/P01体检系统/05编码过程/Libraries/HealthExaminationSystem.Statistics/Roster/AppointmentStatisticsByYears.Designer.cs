namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    partial class AppointmentStatisticsByYears
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
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridViewYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewRegPersonCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lueClient = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEditView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lueClientInfoClientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueClientInfoClientAbbreviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueClientInfoHelpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueClientInfoLinkMan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueClientInfoClientSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.deStartYear = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.deEndYear = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbReload = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartYear.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndYear.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleSeparator1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(1008, 562);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.sbReload);
            this.layoutControlBase.Controls.Add(this.deEndYear);
            this.layoutControlBase.Controls.Add(this.deStartYear);
            this.layoutControlBase.Controls.Add(this.lueClient);
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(358, 302, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(1008, 562);
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(12, 40);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(984, 510);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridViewYear,
            this.gridViewRegPersonCount});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsCustomization.AllowColumnMoving = false;
            this.gridView.OptionsCustomization.AllowFilter = false;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridViewYear
            // 
            this.gridViewYear.AppearanceCell.Options.UseTextOptions = true;
            this.gridViewYear.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewYear.AppearanceHeader.Options.UseTextOptions = true;
            this.gridViewYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewYear.Caption = "年份";
            this.gridViewYear.FieldName = "Year";
            this.gridViewYear.Name = "gridViewYear";
            this.gridViewYear.Visible = true;
            this.gridViewYear.VisibleIndex = 0;
            // 
            // gridViewRegPersonCount
            // 
            this.gridViewRegPersonCount.AppearanceCell.Options.UseTextOptions = true;
            this.gridViewRegPersonCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewRegPersonCount.AppearanceHeader.Options.UseTextOptions = true;
            this.gridViewRegPersonCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewRegPersonCount.Caption = "预约人数";
            this.gridViewRegPersonCount.FieldName = "RegPersonCount";
            this.gridViewRegPersonCount.Name = "gridViewRegPersonCount";
            this.gridViewRegPersonCount.Visible = true;
            this.gridViewRegPersonCount.VisibleIndex = 1;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(988, 514);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 26);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(988, 2);
            // 
            // lueClient
            // 
            this.lueClient.Location = new System.Drawing.Point(52, 12);
            this.lueClient.MaximumSize = new System.Drawing.Size(150, 0);
            this.lueClient.Name = "lueClient";
            this.lueClient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueClient.Properties.DisplayMember = "ClientName";
            this.lueClient.Properties.NullText = "";
            this.lueClient.Properties.ValueMember = "Id";
            this.lueClient.Properties.View = this.searchLookUpEditView;
            this.lueClient.Size = new System.Drawing.Size(138, 20);
            this.lueClient.StyleController = this.layoutControlBase;
            this.lueClient.TabIndex = 5;
            this.lueClient.Enter += new System.EventHandler(this.lueClient_Enter);
            // 
            // searchLookUpEditView
            // 
            this.searchLookUpEditView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.lueClientInfoClientName,
            this.lueClientInfoClientAbbreviation,
            this.lueClientInfoHelpCode,
            this.lueClientInfoLinkMan,
            this.lueClientInfoClientSource});
            this.searchLookUpEditView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditView.Name = "searchLookUpEditView";
            this.searchLookUpEditView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditView.OptionsView.ShowGroupPanel = false;
            // 
            // lueClientInfoClientName
            // 
            this.lueClientInfoClientName.Caption = "单位名称";
            this.lueClientInfoClientName.FieldName = "ClientName";
            this.lueClientInfoClientName.Name = "lueClientInfoClientName";
            this.lueClientInfoClientName.Visible = true;
            this.lueClientInfoClientName.VisibleIndex = 0;
            // 
            // lueClientInfoClientAbbreviation
            // 
            this.lueClientInfoClientAbbreviation.Caption = "单位简称";
            this.lueClientInfoClientAbbreviation.FieldName = "ClientAbbreviation";
            this.lueClientInfoClientAbbreviation.Name = "lueClientInfoClientAbbreviation";
            this.lueClientInfoClientAbbreviation.Visible = true;
            this.lueClientInfoClientAbbreviation.VisibleIndex = 1;
            // 
            // lueClientInfoHelpCode
            // 
            this.lueClientInfoHelpCode.Caption = "助记码";
            this.lueClientInfoHelpCode.FieldName = "HelpCode";
            this.lueClientInfoHelpCode.Name = "lueClientInfoHelpCode";
            this.lueClientInfoHelpCode.Visible = true;
            this.lueClientInfoHelpCode.VisibleIndex = 2;
            // 
            // lueClientInfoLinkMan
            // 
            this.lueClientInfoLinkMan.Caption = "企业负责人";
            this.lueClientInfoLinkMan.FieldName = "LinkMan";
            this.lueClientInfoLinkMan.Name = "lueClientInfoLinkMan";
            this.lueClientInfoLinkMan.Visible = true;
            this.lueClientInfoLinkMan.VisibleIndex = 3;
            // 
            // lueClientInfoClientSource
            // 
            this.lueClientInfoClientSource.Caption = "来源";
            this.lueClientInfoClientSource.FieldName = "ClientSource";
            this.lueClientInfoClientSource.Name = "lueClientInfoClientSource";
            this.lueClientInfoClientSource.Visible = true;
            this.lueClientInfoClientSource.VisibleIndex = 4;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lueClient;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem2.Text = "单位：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // deStartYear
            // 
            this.deStartYear.EditValue = null;
            this.deStartYear.Location = new System.Drawing.Point(234, 12);
            this.deStartYear.MaximumSize = new System.Drawing.Size(150, 0);
            this.deStartYear.MinimumSize = new System.Drawing.Size(60, 0);
            this.deStartYear.Name = "deStartYear";
            this.deStartYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartYear.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartYear.Properties.DisplayFormat.FormatString = "yyyy";
            this.deStartYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deStartYear.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            this.deStartYear.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.deStartYear.Size = new System.Drawing.Size(138, 20);
            this.deStartYear.StyleController = this.layoutControlBase;
            this.deStartYear.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.deStartYear;
            this.layoutControlItem3.Location = new System.Drawing.Point(182, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem3.Text = "年份：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // deEndYear
            // 
            this.deEndYear.EditValue = null;
            this.deEndYear.Location = new System.Drawing.Point(416, 12);
            this.deEndYear.MaximumSize = new System.Drawing.Size(150, 0);
            this.deEndYear.MinimumSize = new System.Drawing.Size(60, 0);
            this.deEndYear.Name = "deEndYear";
            this.deEndYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndYear.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndYear.Properties.DisplayFormat.FormatString = "yyyy";
            this.deEndYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEndYear.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            this.deEndYear.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.deEndYear.Size = new System.Drawing.Size(138, 20);
            this.deEndYear.StyleController = this.layoutControlBase;
            this.deEndYear.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deEndYear;
            this.layoutControlItem4.Location = new System.Drawing.Point(364, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem4.Text = "~";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // sbReload
            // 
            this.sbReload.AutoWidthInLayoutControl = true;
            this.sbReload.Location = new System.Drawing.Point(948, 12);
            this.sbReload.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbReload.Name = "sbReload";
            this.sbReload.Size = new System.Drawing.Size(48, 22);
            this.sbReload.StyleController = this.layoutControlBase;
            this.sbReload.TabIndex = 8;
            this.sbReload.Text = "查询";
            this.sbReload.Click += new System.EventHandler(this.sbReload_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbReload;
            this.layoutControlItem5.Location = new System.Drawing.Point(936, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(546, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(390, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // AppointmentStatisticsByYears
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Name = "AppointmentStatisticsByYears";
            this.Text = "历年预约统计";
            this.Load += new System.EventHandler(this.AppointmentStatisticsByYears_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartYear.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndYear.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SearchLookUpEdit lueClient;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditView;
        private DevExpress.XtraEditors.DateEdit deEndYear;
        private DevExpress.XtraEditors.DateEdit deStartYear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton sbReload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn lueClientInfoClientName;
        private DevExpress.XtraGrid.Columns.GridColumn lueClientInfoClientAbbreviation;
        private DevExpress.XtraGrid.Columns.GridColumn lueClientInfoHelpCode;
        private DevExpress.XtraGrid.Columns.GridColumn lueClientInfoClientSource;
        private DevExpress.XtraGrid.Columns.GridColumn lueClientInfoLinkMan;
        private DevExpress.XtraGrid.Columns.GridColumn gridViewYear;
        private DevExpress.XtraGrid.Columns.GridColumn gridViewRegPersonCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}