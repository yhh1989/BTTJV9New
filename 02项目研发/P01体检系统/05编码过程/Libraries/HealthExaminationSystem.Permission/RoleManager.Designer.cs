namespace Sw.Hospital.HealthExaminationSystem.Permission
{
    partial class RoleManager
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
            this.gridViewFormModule = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnFormModuleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFormModuleTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFormModuleNickname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewFormRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnFormRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFormRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButtonCreate = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFormModule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFormRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.simpleSeparator1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.Controls.Add(this.simpleButtonCreate);
            this.layoutControlBase.Controls.Add(this.simpleButtonUpdate);
            this.layoutControlBase.Controls.Add(this.simpleButtonDelete);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 172, 450, 400);
            // 
            // gridViewFormModule
            // 
            this.gridViewFormModule.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnFormModuleId,
            this.gridColumnFormModuleTypeName,
            this.gridColumnFormModuleNickname});
            this.gridViewFormModule.GridControl = this.gridControl;
            this.gridViewFormModule.GroupCount = 1;
            this.gridViewFormModule.Name = "gridViewFormModule";
            this.gridViewFormModule.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewFormModule.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewFormModule.OptionsBehavior.Editable = false;
            this.gridViewFormModule.OptionsView.ShowGroupPanel = false;
            this.gridViewFormModule.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnFormModuleTypeName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnFormModuleId
            // 
            this.gridColumnFormModuleId.Caption = "Id";
            this.gridColumnFormModuleId.FieldName = "Id";
            this.gridColumnFormModuleId.Name = "gridColumnFormModuleId";
            // 
            // gridColumnFormModuleTypeName
            // 
            this.gridColumnFormModuleTypeName.Caption = "类型";
            this.gridColumnFormModuleTypeName.FieldName = "TypeName";
            this.gridColumnFormModuleTypeName.Name = "gridColumnFormModuleTypeName";
            this.gridColumnFormModuleTypeName.Visible = true;
            this.gridColumnFormModuleTypeName.VisibleIndex = 0;
            // 
            // gridColumnFormModuleNickname
            // 
            this.gridColumnFormModuleNickname.Caption = "名称";
            this.gridColumnFormModuleNickname.FieldName = "Nickname";
            this.gridColumnFormModuleNickname.Name = "gridColumnFormModuleNickname";
            this.gridColumnFormModuleNickname.Visible = true;
            this.gridColumnFormModuleNickname.VisibleIndex = 0;
            // 
            // gridControl
            // 
            gridLevelNode1.LevelTemplate = this.gridViewFormModule;
            gridLevelNode1.RelationName = "FormModules";
            this.gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl.Location = new System.Drawing.Point(12, 40);
            this.gridControl.MainView = this.gridViewFormRole;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(760, 509);
            this.gridControl.TabIndex = 6;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFormRole,
            this.gridViewFormModule});
            this.gridControl.DoubleClick += new System.EventHandler(this.gridControl_DoubleClick);
            // 
            // gridViewFormRole
            // 
            this.gridViewFormRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnFormRoleId,
            this.gridColumnFormRoleName});
            this.gridViewFormRole.GridControl = this.gridControl;
            this.gridViewFormRole.Name = "gridViewFormRole";
            this.gridViewFormRole.OptionsBehavior.Editable = false;
            this.gridViewFormRole.OptionsDetail.ShowDetailTabs = false;
            this.gridViewFormRole.OptionsView.ShowGroupPanel = false;
            this.gridViewFormRole.DoubleClick += new System.EventHandler(this.gridViewFormRole_DoubleClick);
            // 
            // gridColumnFormRoleId
            // 
            this.gridColumnFormRoleId.Caption = "Id";
            this.gridColumnFormRoleId.FieldName = "Id";
            this.gridColumnFormRoleId.Name = "gridColumnFormRoleId";
            // 
            // gridColumnFormRoleName
            // 
            this.gridColumnFormRoleName.Caption = "角色";
            this.gridColumnFormRoleName.FieldName = "Name";
            this.gridColumnFormRoleName.Name = "gridColumnFormRoleName";
            this.gridColumnFormRoleName.Visible = true;
            this.gridColumnFormRoleName.VisibleIndex = 0;
            // 
            // simpleButtonCreate
            // 
            this.simpleButtonCreate.AutoWidthInLayoutControl = true;
            this.simpleButtonCreate.Location = new System.Drawing.Point(620, 12);
            this.simpleButtonCreate.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonCreate.Name = "simpleButtonCreate";
            this.simpleButtonCreate.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonCreate.StyleController = this.layoutControlBase;
            this.simpleButtonCreate.TabIndex = 4;
            this.simpleButtonCreate.Text = "创建";
            this.simpleButtonCreate.Click += new System.EventHandler(this.simpleButtonCreate_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(608, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(608, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonUpdate
            // 
            this.simpleButtonUpdate.AutoWidthInLayoutControl = true;
            this.simpleButtonUpdate.Location = new System.Drawing.Point(672, 12);
            this.simpleButtonUpdate.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonUpdate.Name = "simpleButtonUpdate";
            this.simpleButtonUpdate.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonUpdate.StyleController = this.layoutControlBase;
            this.simpleButtonUpdate.TabIndex = 5;
            this.simpleButtonUpdate.Text = "修改";
            this.simpleButtonUpdate.Click += new System.EventHandler(this.simpleButtonUpdate_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonUpdate;
            this.layoutControlItem2.Location = new System.Drawing.Point(660, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 26);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(764, 2);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(764, 513);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleButtonDelete
            // 
            this.simpleButtonDelete.AutoWidthInLayoutControl = true;
            this.simpleButtonDelete.Location = new System.Drawing.Point(724, 12);
            this.simpleButtonDelete.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonDelete.Name = "simpleButtonDelete";
            this.simpleButtonDelete.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonDelete.StyleController = this.layoutControlBase;
            this.simpleButtonDelete.TabIndex = 7;
            this.simpleButtonDelete.Text = "删除";
            this.simpleButtonDelete.Click += new System.EventHandler(this.simpleButtonDelete_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonDelete;
            this.layoutControlItem4.Location = new System.Drawing.Point(712, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // RoleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "RoleManager";
            this.Text = "权限设置";
            this.Load += new System.EventHandler(this.RoleManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFormModule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFormRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCreate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonUpdate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFormRole;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFormModule;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFormRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFormRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFormModuleId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFormModuleTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFormModuleNickname;
    }
}