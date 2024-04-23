namespace Sw.Hospital.HealthExaminationSystem.UserSettings.TargetDisease
{
    partial class OccupationalDiseaseIncludeItemGroupAdministration
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
            this.gridControlSelectedItemGroup = new DevExpress.XtraGrid.GridControl();
            this.gridViewSelectedItemGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSelectid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSelectDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSelectItemGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlNotItemGroup = new DevExpress.XtraGrid.GridControl();
            this.gridViewNotItemGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNotItemGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNotDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNotItemGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNotItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
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
            this.emptySpaceItem2,
            this.layoutControlItem3});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(944, 625);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonOK);
            this.layoutControlBase.Controls.Add(this.gridControlNotItemGroup);
            this.layoutControlBase.Controls.Add(this.gridControlSelectedItemGroup);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(530, 370, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(944, 625);
            // 
            // gridControlSelectedItemGroup
            // 
            this.gridControlSelectedItemGroup.Location = new System.Drawing.Point(12, 38);
            this.gridControlSelectedItemGroup.MainView = this.gridViewSelectedItemGroup;
            this.gridControlSelectedItemGroup.Name = "gridControlSelectedItemGroup";
            this.gridControlSelectedItemGroup.Size = new System.Drawing.Size(444, 575);
            this.gridControlSelectedItemGroup.TabIndex = 4;
            this.gridControlSelectedItemGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSelectedItemGroup});
            // 
            // gridViewSelectedItemGroup
            // 
            this.gridViewSelectedItemGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSelectid,
            this.gridColumnSelectDepartmentName,
            this.gridColumnSelectItemGroupName});
            this.gridViewSelectedItemGroup.GridControl = this.gridControlSelectedItemGroup;
            this.gridViewSelectedItemGroup.GroupCount = 1;
            this.gridViewSelectedItemGroup.Name = "gridViewSelectedItemGroup";
            this.gridViewSelectedItemGroup.OptionsBehavior.Editable = false;
            this.gridViewSelectedItemGroup.OptionsView.ShowGroupPanel = false;
            this.gridViewSelectedItemGroup.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnSelectDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewSelectedItemGroup.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewSelectedItemGroup_RowClick);
            // 
            // gridColumnSelectid
            // 
            this.gridColumnSelectid.Caption = "id";
            this.gridColumnSelectid.FieldName = "Id";
            this.gridColumnSelectid.Name = "gridColumnSelectid";
            // 
            // gridColumnSelectDepartmentName
            // 
            this.gridColumnSelectDepartmentName.Caption = "科室";
            this.gridColumnSelectDepartmentName.FieldName = "Department.Name";
            this.gridColumnSelectDepartmentName.FieldNameSortGroup = "Department.OrderNum";
            this.gridColumnSelectDepartmentName.Name = "gridColumnSelectDepartmentName";
            this.gridColumnSelectDepartmentName.Visible = true;
            this.gridColumnSelectDepartmentName.VisibleIndex = 0;
            // 
            // gridColumnSelectItemGroupName
            // 
            this.gridColumnSelectItemGroupName.Caption = "项目";
            this.gridColumnSelectItemGroupName.FieldName = "ItemGroupName";
            this.gridColumnSelectItemGroupName.Name = "gridColumnSelectItemGroupName";
            this.gridColumnSelectItemGroupName.Visible = true;
            this.gridColumnSelectItemGroupName.VisibleIndex = 0;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlSelectedItemGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(448, 579);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridControlNotItemGroup
            // 
            this.gridControlNotItemGroup.Location = new System.Drawing.Point(460, 38);
            this.gridControlNotItemGroup.MainView = this.gridViewNotItemGroup;
            this.gridControlNotItemGroup.Name = "gridControlNotItemGroup";
            this.gridControlNotItemGroup.Size = new System.Drawing.Size(472, 575);
            this.gridControlNotItemGroup.TabIndex = 5;
            this.gridControlNotItemGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNotItemGroup});
            // 
            // gridViewNotItemGroup
            // 
            this.gridViewNotItemGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNotItemGroupId,
            this.gridColumnNotDepartmentName,
            this.gridColumnNotItemGroupName});
            this.gridViewNotItemGroup.GridControl = this.gridControlNotItemGroup;
            this.gridViewNotItemGroup.GroupCount = 1;
            this.gridViewNotItemGroup.Name = "gridViewNotItemGroup";
            this.gridViewNotItemGroup.OptionsBehavior.Editable = false;
            this.gridViewNotItemGroup.OptionsView.ShowGroupPanel = false;
            this.gridViewNotItemGroup.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnNotDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewNotItemGroup.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewNotItemGroup_RowClick);
            // 
            // gridColumnNotItemGroupId
            // 
            this.gridColumnNotItemGroupId.Caption = "id";
            this.gridColumnNotItemGroupId.FieldName = "Id";
            this.gridColumnNotItemGroupId.Name = "gridColumnNotItemGroupId";
            // 
            // gridColumnNotDepartmentName
            // 
            this.gridColumnNotDepartmentName.Caption = "科室";
            this.gridColumnNotDepartmentName.FieldName = "Department.Name";
            this.gridColumnNotDepartmentName.FieldNameSortGroup = "Department.OrderNum";
            this.gridColumnNotDepartmentName.Name = "gridColumnNotDepartmentName";
            this.gridColumnNotDepartmentName.Visible = true;
            this.gridColumnNotDepartmentName.VisibleIndex = 0;
            // 
            // gridColumnNotItemGroupName
            // 
            this.gridColumnNotItemGroupName.Caption = "项目";
            this.gridColumnNotItemGroupName.FieldName = "ItemGroupName";
            this.gridColumnNotItemGroupName.Name = "gridColumnNotItemGroupName";
            this.gridColumnNotItemGroupName.Visible = true;
            this.gridColumnNotItemGroupName.VisibleIndex = 0;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlNotItemGroup;
            this.layoutControlItem2.Location = new System.Drawing.Point(448, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(476, 579);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(852, 12);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(80, 22);
            this.simpleButtonOK.StyleController = this.layoutControlBase;
            this.simpleButtonOK.TabIndex = 6;
            this.simpleButtonOK.Text = "确定";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOK;
            this.layoutControlItem3.Location = new System.Drawing.Point(840, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(426, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(426, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(414, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // OccupationalDiseaseIncludeItemGroupAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 625);
            this.Name = "OccupationalDiseaseIncludeItemGroupAdministration";
            this.Text = "职业健康相关项目";
            this.Load += new System.EventHandler(this.UpdateOccupationalDiseaseIncludeItemGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNotItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlSelectedItemGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSelectedItemGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControlNotItemGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNotItemGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotItemGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotItemGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelectid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelectDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelectItemGroupName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
    }
}