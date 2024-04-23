namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Dictionary
{
    partial class DictionarySetting
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
            this.gridControlDictionary = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.OrderNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Department = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ItemInfos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textEditItemDepartName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbReload = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbReset = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemDepartName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(717, 430);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.lueDepartment);
            this.layoutControlBase.Controls.Add(this.searchLookUpEdit1);
            this.layoutControlBase.Controls.Add(this.sbReset);
            this.layoutControlBase.Controls.Add(this.gridControlDictionary);
            this.layoutControlBase.Controls.Add(this.simpleButton2);
            this.layoutControlBase.Controls.Add(this.sbReload);
            this.layoutControlBase.Controls.Add(this.textEditItemDepartName);
            this.layoutControlBase.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(717, 430);
            // 
            // gridControlDictionary
            // 
            this.gridControlDictionary.Location = new System.Drawing.Point(12, 64);
            this.gridControlDictionary.MainView = this.gridViewItemInfo;
            this.gridControlDictionary.Name = "gridControlDictionary";
            this.gridControlDictionary.Size = new System.Drawing.Size(693, 354);
            this.gridControlDictionary.TabIndex = 9;
            this.gridControlDictionary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemInfo});
            // 
            // gridViewItemInfo
            // 
            this.gridViewItemInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.OrderNum,
            this.DepartmentId,
            this.Department,
            this.Id,
            this.ItemInfos});
            this.gridViewItemInfo.GridControl = this.gridControlDictionary;
            this.gridViewItemInfo.GroupCount = 1;
            this.gridViewItemInfo.Name = "gridViewItemInfo";
            this.gridViewItemInfo.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewItemInfo.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewItemInfo.OptionsBehavior.Editable = false;
            this.gridViewItemInfo.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewItemInfo.OptionsCustomization.AllowFilter = false;
            this.gridViewItemInfo.OptionsCustomization.AllowGroup = false;
            this.gridViewItemInfo.OptionsDetail.AllowZoomDetail = false;
            this.gridViewItemInfo.OptionsDetail.ShowDetailTabs = false;
            this.gridViewItemInfo.OptionsView.ColumnAutoWidth = false;
            this.gridViewItemInfo.OptionsView.ShowGroupPanel = false;
            this.gridViewItemInfo.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.Department, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewItemInfo.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // OrderNum
            // 
            this.OrderNum.Caption = "项目编号";
            this.OrderNum.FieldName = "OrderNum";
            this.OrderNum.Name = "OrderNum";
            this.OrderNum.Visible = true;
            this.OrderNum.VisibleIndex = 0;
            // 
            // DepartmentId
            // 
            this.DepartmentId.Caption = "科室Id";
            this.DepartmentId.FieldName = "Department.Id";
            this.DepartmentId.Name = "DepartmentId";
            // 
            // Department
            // 
            this.Department.Caption = "科室";
            this.Department.FieldName = "Department.Name";
            this.Department.FieldNameSortGroup = "Department.OrderNum";
            this.Department.Name = "Department";
            this.Department.Visible = true;
            this.Department.VisibleIndex = 1;
            // 
            // Id
            // 
            this.Id.Caption = "项目Id";
            this.Id.FieldName = "Id";
            this.Id.Name = "Id";
            // 
            // ItemInfos
            // 
            this.ItemInfos.Caption = "项目名称";
            this.ItemInfos.FieldName = "Name";
            this.ItemInfos.Name = "ItemInfos";
            this.ItemInfos.Visible = true;
            this.ItemInfos.VisibleIndex = 1;
            this.ItemInfos.Width = 200;
            // 
            // textEditItemDepartName
            // 
            this.textEditItemDepartName.Location = new System.Drawing.Point(76, 12);
            this.textEditItemDepartName.MaximumSize = new System.Drawing.Size(150, 0);
            this.textEditItemDepartName.Name = "textEditItemDepartName";
            this.textEditItemDepartName.Size = new System.Drawing.Size(150, 20);
            this.textEditItemDepartName.StyleController = this.layoutControlBase;
            this.textEditItemDepartName.TabIndex = 5;
            this.textEditItemDepartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyDown);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditItemDepartName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(218, 26);
            this.layoutControlItem2.Text = "项目名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // sbReload
            // 
            this.sbReload.AutoWidthInLayoutControl = true;
            this.sbReload.Location = new System.Drawing.Point(605, 12);
            this.sbReload.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbReload.Name = "sbReload";
            this.sbReload.Size = new System.Drawing.Size(48, 22);
            this.sbReload.StyleController = this.layoutControlBase;
            this.sbReload.TabIndex = 7;
            this.sbReload.Text = "查询";
            this.sbReload.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbReload;
            this.layoutControlItem4.Location = new System.Drawing.Point(593, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(436, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(157, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(657, 38);
            this.simpleButton2.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(48, 22);
            this.simpleButton2.StyleController = this.layoutControlBase;
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "查看";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton2;
            this.layoutControlItem5.Location = new System.Drawing.Point(645, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(645, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlDictionary;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(697, 358);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sbReset
            // 
            this.sbReset.AutoWidthInLayoutControl = true;
            this.sbReset.Location = new System.Drawing.Point(657, 12);
            this.sbReset.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbReset.Name = "sbReset";
            this.sbReset.Size = new System.Drawing.Size(48, 22);
            this.sbReset.StyleController = this.layoutControlBase;
            this.sbReset.TabIndex = 12;
            this.sbReset.Text = "重置";
            this.sbReset.Click += new System.EventHandler(this.sbReset_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbReset;
            this.layoutControlItem7.Location = new System.Drawing.Point(645, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(289, 12);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(50, 20);
            this.searchLookUpEdit1.StyleController = this.layoutControlBase;
            this.searchLookUpEdit1.TabIndex = 13;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.searchLookUpEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(168, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(163, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 20);
            // 
            // lueDepartment
            // 
            this.lueDepartment.Location = new System.Drawing.Point(294, 12);
            this.lueDepartment.MaximumSize = new System.Drawing.Size(150, 0);
            this.lueDepartment.Name = "lueDepartment";
            this.lueDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDepartment.Properties.DisplayMember = "Name";
            this.lueDepartment.Properties.NullText = "";
            this.lueDepartment.Properties.ValueMember = "Id";
            this.lueDepartment.Properties.View = this.searchLookUpEdit2View;
            this.lueDepartment.Size = new System.Drawing.Size(150, 20);
            this.lueDepartment.StyleController = this.layoutControlBase;
            this.lueDepartment.TabIndex = 14;
            this.lueDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lueDepartment_KeyDown);
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "科室名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lueDepartment;
            this.layoutControlItem8.Location = new System.Drawing.Point(218, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(218, 26);
            this.layoutControlItem8.Text = "所属科室";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // DictionarySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 430);
            this.Name = "DictionarySetting";
            this.Text = "项目字典";
            this.Load += new System.EventHandler(this.DictionarySetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditItemDepartName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit textEditItemDepartName;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton sbReload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gridControlDictionary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn Department;
        private DevExpress.XtraGrid.Columns.GridColumn ItemInfos;
        private DevExpress.XtraEditors.SimpleButton sbReset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn OrderNum;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SearchLookUpEdit lueDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn DepartmentId;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
    }
}