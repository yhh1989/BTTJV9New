namespace Sw.Hospital.HealthExaminationSystem.UserSettings.PersonnelCategory
{
    partial class PersonnelCategoryList
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
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkIsFree = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewPersonnelCategoryViewDto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnPersonnelCategoryViewDtoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPersonnelCategoryViewDtoName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPersonnelCategoryViewDtoIsFree = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPersonnelCategoryViewDtoIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtSearchName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFree.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPersonnelCategoryViewDto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem8});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton3);
            this.layoutControlBase.Controls.Add(this.simpleButton2);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.txtSearchName);
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.Controls.Add(this.chkIsActive);
            this.layoutControlBase.Controls.Add(this.chkIsFree);
            this.layoutControlBase.Controls.Add(this.txtName);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(896, 248, 450, 400);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(76, 517);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(422, 20);
            this.txtName.StyleController = this.layoutControlBase;
            this.txtName.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 505);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(490, 36);
            this.layoutControlItem1.Text = "类别名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // chkIsFree
            // 
            this.chkIsFree.Location = new System.Drawing.Point(502, 517);
            this.chkIsFree.Name = "chkIsFree";
            this.chkIsFree.Properties.Caption = "免费";
            this.chkIsFree.Size = new System.Drawing.Size(46, 19);
            this.chkIsFree.StyleController = this.layoutControlBase;
            this.chkIsFree.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkIsFree;
            this.layoutControlItem2.Location = new System.Drawing.Point(490, 505);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(50, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // chkIsActive
            // 
            this.chkIsActive.Location = new System.Drawing.Point(552, 517);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Properties.Caption = "启用";
            this.chkIsActive.Size = new System.Drawing.Size(107, 19);
            this.chkIsActive.StyleController = this.layoutControlBase;
            this.chkIsActive.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkIsActive;
            this.layoutControlItem3.Location = new System.Drawing.Point(540, 505);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(111, 36);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(12, 38);
            this.gridControl.MainView = this.gridViewPersonnelCategoryViewDto;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(760, 475);
            this.gridControl.TabIndex = 7;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPersonnelCategoryViewDto});
            // 
            // gridViewPersonnelCategoryViewDto
            // 
            this.gridViewPersonnelCategoryViewDto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPersonnelCategoryViewDtoId,
            this.gridColumnPersonnelCategoryViewDtoName,
            this.gridColumnPersonnelCategoryViewDtoIsFree,
            this.gridColumnPersonnelCategoryViewDtoIsActive});
            this.gridViewPersonnelCategoryViewDto.GridControl = this.gridControl;
            this.gridViewPersonnelCategoryViewDto.Name = "gridViewPersonnelCategoryViewDto";
            this.gridViewPersonnelCategoryViewDto.OptionsBehavior.Editable = false;
            this.gridViewPersonnelCategoryViewDto.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPersonnelCategoryViewDto.OptionsView.ShowGroupPanel = false;
            this.gridViewPersonnelCategoryViewDto.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gdvCategory_RowClick);
            // 
            // gridColumnPersonnelCategoryViewDtoId
            // 
            this.gridColumnPersonnelCategoryViewDtoId.Caption = "Id";
            this.gridColumnPersonnelCategoryViewDtoId.FieldName = "Id";
            this.gridColumnPersonnelCategoryViewDtoId.Name = "gridColumnPersonnelCategoryViewDtoId";
            // 
            // gridColumnPersonnelCategoryViewDtoName
            // 
            this.gridColumnPersonnelCategoryViewDtoName.Caption = "类别";
            this.gridColumnPersonnelCategoryViewDtoName.FieldName = "Name";
            this.gridColumnPersonnelCategoryViewDtoName.Name = "gridColumnPersonnelCategoryViewDtoName";
            this.gridColumnPersonnelCategoryViewDtoName.Visible = true;
            this.gridColumnPersonnelCategoryViewDtoName.VisibleIndex = 0;
            // 
            // gridColumnPersonnelCategoryViewDtoIsFree
            // 
            this.gridColumnPersonnelCategoryViewDtoIsFree.Caption = "免费";
            this.gridColumnPersonnelCategoryViewDtoIsFree.FieldName = "IsFree";
            this.gridColumnPersonnelCategoryViewDtoIsFree.Name = "gridColumnPersonnelCategoryViewDtoIsFree";
            this.gridColumnPersonnelCategoryViewDtoIsFree.Visible = true;
            this.gridColumnPersonnelCategoryViewDtoIsFree.VisibleIndex = 1;
            // 
            // gridColumnPersonnelCategoryViewDtoIsActive
            // 
            this.gridColumnPersonnelCategoryViewDtoIsActive.Caption = "启用";
            this.gridColumnPersonnelCategoryViewDtoIsActive.FieldName = "IsActive";
            this.gridColumnPersonnelCategoryViewDtoIsActive.Name = "gridColumnPersonnelCategoryViewDtoIsActive";
            this.gridColumnPersonnelCategoryViewDtoIsActive.Visible = true;
            this.gridColumnPersonnelCategoryViewDtoIsActive.VisibleIndex = 2;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(764, 479);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(76, 12);
            this.txtSearchName.MaximumSize = new System.Drawing.Size(120, 0);
            this.txtSearchName.MinimumSize = new System.Drawing.Size(120, 0);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(120, 20);
            this.txtSearchName.StyleController = this.layoutControlBase;
            this.txtSearchName.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtSearchName;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(188, 26);
            this.layoutControlItem5.Text = "类别名称：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(200, 12);
            this.simpleButton1.MaximumSize = new System.Drawing.Size(48, 0);
            this.simpleButton1.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(48, 22);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton1;
            this.layoutControlItem6.Location = new System.Drawing.Point(188, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(651, 531);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(52, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(240, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(524, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(715, 517);
            this.simpleButton2.MaximumSize = new System.Drawing.Size(80, 0);
            this.simpleButton2.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(57, 22);
            this.simpleButton2.StyleController = this.layoutControlBase;
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "保存";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButton2;
            this.layoutControlItem7.Location = new System.Drawing.Point(703, 505);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(61, 36);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // simpleButton3
            // 
            this.simpleButton3.AutoWidthInLayoutControl = true;
            this.simpleButton3.Location = new System.Drawing.Point(663, 517);
            this.simpleButton3.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(48, 22);
            this.simpleButton3.StyleController = this.layoutControlBase;
            this.simpleButton3.TabIndex = 11;
            this.simpleButton3.Text = "删除";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.simpleButton3;
            this.layoutControlItem8.Location = new System.Drawing.Point(651, 505);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // PersonnelCategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "PersonnelCategoryList";
            this.Text = "人员类别";
            this.Load += new System.EventHandler(this.FrmCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFree.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPersonnelCategoryViewDto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.CheckEdit chkIsActive;
        private DevExpress.XtraEditors.CheckEdit chkIsFree;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPersonnelCategoryViewDto;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtSearchName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPersonnelCategoryViewDtoId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPersonnelCategoryViewDtoName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPersonnelCategoryViewDtoIsFree;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPersonnelCategoryViewDtoIsActive;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}