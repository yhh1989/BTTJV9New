namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    partial class frmHistory
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
            this.gridControlHisResult = new DevExpress.XtraGrid.GridControl();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpGroup = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.butsearchHis = new DevExpress.XtraEditors.SimpleButton();
            this.searchLookUpItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchLookUpDepartMent = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHisResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpDepartMent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
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
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem5});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(1178, 685);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.searchLookUpGroup);
            this.layoutControlBase.Controls.Add(this.butsearchHis);
            this.layoutControlBase.Controls.Add(this.searchLookUpItem);
            this.layoutControlBase.Controls.Add(this.searchLookUpDepartMent);
            this.layoutControlBase.Controls.Add(this.gridControlHisResult);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(699, 319, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(1178, 685);
            // 
            // gridControlHisResult
            // 
            this.gridControlHisResult.Location = new System.Drawing.Point(12, 38);
            this.gridControlHisResult.MainView = this.gridView7;
            this.gridControlHisResult.Name = "gridControlHisResult";
            this.gridControlHisResult.Size = new System.Drawing.Size(1154, 635);
            this.gridControlHisResult.TabIndex = 9;
            this.gridControlHisResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView7});
            // 
            // gridView7
            // 
            this.gridView7.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn39,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn41});
            this.gridView7.GridControl = this.gridControlHisResult;
            this.gridView7.Name = "gridView7";
            this.gridView7.OptionsBehavior.Editable = false;
            this.gridView7.OptionsView.ShowGroupPanel = false;
            this.gridView7.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView7_CustomDrawCell);
            this.gridView7.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView7_CustomColumnDisplayText);
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "科室名称";
            this.gridColumn39.FieldName = "DepartName";
            this.gridColumn39.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn39.MaxWidth = 120;
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 0;
            this.gridColumn39.Width = 120;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "组合名称";
            this.gridColumn40.FieldName = "GroupName";
            this.gridColumn40.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn40.MaxWidth = 120;
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 1;
            this.gridColumn40.Width = 120;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "项目名称";
            this.gridColumn44.FieldName = "ItemName";
            this.gridColumn44.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn44.MaxWidth = 120;
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 2;
            this.gridColumn44.Width = 120;
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "参考范围";
            this.gridColumn41.FieldName = "Stand";
            this.gridColumn41.MaxWidth = 120;
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 3;
            this.gridColumn41.Width = 120;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlHisResult;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1158, 639);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // searchLookUpGroup
            // 
            this.searchLookUpGroup.Location = new System.Drawing.Point(341, 12);
            this.searchLookUpGroup.Name = "searchLookUpGroup";
            this.searchLookUpGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpGroup.Properties.DisplayMember = "ItemGroupName";
            this.searchLookUpGroup.Properties.NullText = "";
            this.searchLookUpGroup.Properties.ValueMember = "Id";
            this.searchLookUpGroup.Properties.View = this.searchLookUpEdit3View;
            this.searchLookUpGroup.Size = new System.Drawing.Size(246, 20);
            this.searchLookUpGroup.StyleController = this.layoutControlBase;
            this.searchLookUpGroup.TabIndex = 13;
            // 
            // searchLookUpEdit3View
            // 
            this.searchLookUpEdit3View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn45});
            this.searchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit3View.Name = "searchLookUpEdit3View";
            this.searchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "组合名称";
            this.gridColumn45.FieldName = "ItemGroupName";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 0;
            // 
            // butsearchHis
            // 
            this.butsearchHis.Location = new System.Drawing.Point(1053, 12);
            this.butsearchHis.Name = "butsearchHis";
            this.butsearchHis.Size = new System.Drawing.Size(113, 22);
            this.butsearchHis.StyleController = this.layoutControlBase;
            this.butsearchHis.TabIndex = 12;
            this.butsearchHis.Text = "查询";
            this.butsearchHis.Click += new System.EventHandler(this.butsearchHis_Click);
            // 
            // searchLookUpItem
            // 
            this.searchLookUpItem.Location = new System.Drawing.Point(631, 12);
            this.searchLookUpItem.Name = "searchLookUpItem";
            this.searchLookUpItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpItem.Properties.DisplayMember = "Name";
            this.searchLookUpItem.Properties.NullText = "";
            this.searchLookUpItem.Properties.ValueMember = "Id";
            this.searchLookUpItem.Properties.View = this.searchLookUpEdit2View;
            this.searchLookUpItem.Size = new System.Drawing.Size(245, 20);
            this.searchLookUpItem.StyleController = this.layoutControlBase;
            this.searchLookUpItem.TabIndex = 11;
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn46});
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "项目名称";
            this.gridColumn46.FieldName = "Name";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 0;
            // 
            // searchLookUpDepartMent
            // 
            this.searchLookUpDepartMent.EditValue = "";
            this.searchLookUpDepartMent.Location = new System.Drawing.Point(52, 12);
            this.searchLookUpDepartMent.Name = "searchLookUpDepartMent";
            this.searchLookUpDepartMent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpDepartMent.Properties.DisplayMember = "Name";
            this.searchLookUpDepartMent.Properties.NullText = "";
            this.searchLookUpDepartMent.Properties.ValueMember = "Id";
            this.searchLookUpDepartMent.Properties.View = this.gridView6;
            this.searchLookUpDepartMent.Size = new System.Drawing.Size(245, 20);
            this.searchLookUpDepartMent.StyleController = this.layoutControlBase;
            this.searchLookUpDepartMent.TabIndex = 10;
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn43});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "科室名称";
            this.gridColumn43.FieldName = "Name";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.Visible = true;
            this.gridColumn43.VisibleIndex = 0;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.searchLookUpGroup;
            this.layoutControlItem2.Location = new System.Drawing.Point(289, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(290, 26);
            this.layoutControlItem2.Text = "组合：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.butsearchHis;
            this.layoutControlItem3.Location = new System.Drawing.Point(1041, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(117, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.searchLookUpItem;
            this.layoutControlItem4.Location = new System.Drawing.Point(579, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(289, 26);
            this.layoutControlItem4.Text = "项目：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.searchLookUpDepartMent;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(289, 26);
            this.layoutControlItem5.Text = "科室：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(868, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(173, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 685);
            this.Name = "frmHistory";
            this.Text = "历史对比";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHisResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpDepartMent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlHisResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit3View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraEditors.SimpleButton butsearchHis;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpItem;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpDepartMent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}