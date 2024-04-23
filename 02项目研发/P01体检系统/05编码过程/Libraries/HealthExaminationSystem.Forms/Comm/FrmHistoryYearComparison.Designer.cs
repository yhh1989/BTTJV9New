namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    partial class FrmHistoryYearComparison
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
            this.simpleButtonDaoChu = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonQuxianTuShi = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlContrast = new DevExpress.XtraGrid.GridControl();
            this.gridViewContrast = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDepartmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkedComboBoxEditDepartment = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkedComboBoxEditItemGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkedComboBoxEditItem = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSelect = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditItemGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonSelect);
            this.layoutControlBase.Controls.Add(this.checkedComboBoxEditItem);
            this.layoutControlBase.Controls.Add(this.checkedComboBoxEditItemGroup);
            this.layoutControlBase.Controls.Add(this.checkedComboBoxEditDepartment);
            this.layoutControlBase.Controls.Add(this.gridControlContrast);
            this.layoutControlBase.Controls.Add(this.simpleButtonQuxianTuShi);
            this.layoutControlBase.Controls.Add(this.simpleButtonDaoChu);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(478, 179, 450, 400);
            // 
            // simpleButtonDaoChu
            // 
            this.simpleButtonDaoChu.Location = new System.Drawing.Point(632, 12);
            this.simpleButtonDaoChu.Name = "simpleButtonDaoChu";
            this.simpleButtonDaoChu.Size = new System.Drawing.Size(67, 22);
            this.simpleButtonDaoChu.StyleController = this.layoutControlBase;
            this.simpleButtonDaoChu.TabIndex = 9;
            this.simpleButtonDaoChu.Text = "导出";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonDaoChu;
            this.layoutControlItem6.Location = new System.Drawing.Point(620, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleButtonQuxianTuShi
            // 
            this.simpleButtonQuxianTuShi.Location = new System.Drawing.Point(703, 12);
            this.simpleButtonQuxianTuShi.Name = "simpleButtonQuxianTuShi";
            this.simpleButtonQuxianTuShi.Size = new System.Drawing.Size(69, 22);
            this.simpleButtonQuxianTuShi.StyleController = this.layoutControlBase;
            this.simpleButtonQuxianTuShi.TabIndex = 11;
            this.simpleButtonQuxianTuShi.Text = "曲线图势";
            this.simpleButtonQuxianTuShi.Click += new System.EventHandler(this.simpleButtonQuxianTuShi_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonQuxianTuShi;
            this.layoutControlItem5.Location = new System.Drawing.Point(691, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // gridControlContrast
            // 
            this.gridControlContrast.Location = new System.Drawing.Point(12, 38);
            this.gridControlContrast.MainView = this.gridViewContrast;
            this.gridControlContrast.Name = "gridControlContrast";
            this.gridControlContrast.Size = new System.Drawing.Size(760, 511);
            this.gridControlContrast.TabIndex = 12;
            this.gridControlContrast.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewContrast});
            // 
            // gridViewContrast
            // 
            this.gridViewContrast.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDepartmentId,
            this.gridColumnDepartmentName,
            this.gridColumnItemGroupId,
            this.gridColumnItemGroupName,
            this.gridColumnItemId,
            this.gridColumnItemName,
            this.gridColumnStand});
            this.gridViewContrast.GridControl = this.gridControlContrast;
            this.gridViewContrast.Name = "gridViewContrast";
            this.gridViewContrast.OptionsBehavior.Editable = false;
            this.gridViewContrast.OptionsView.AllowCellMerge = true;
            this.gridViewContrast.OptionsView.RowAutoHeight = true;
            this.gridViewContrast.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnDepartmentId
            // 
            this.gridColumnDepartmentId.Caption = "科室id";
            this.gridColumnDepartmentId.FieldName = "DepartmentId";
            this.gridColumnDepartmentId.Name = "gridColumnDepartmentId";
            // 
            // gridColumnDepartmentName
            // 
            this.gridColumnDepartmentName.Caption = "科室";
            this.gridColumnDepartmentName.FieldName = "DepartmentName";
            this.gridColumnDepartmentName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnDepartmentName.MaxWidth = 100;
            this.gridColumnDepartmentName.MinWidth = 100;
            this.gridColumnDepartmentName.Name = "gridColumnDepartmentName";
            this.gridColumnDepartmentName.Visible = true;
            this.gridColumnDepartmentName.VisibleIndex = 0;
            this.gridColumnDepartmentName.Width = 100;
            // 
            // gridColumnItemGroupId
            // 
            this.gridColumnItemGroupId.Caption = "组合id";
            this.gridColumnItemGroupId.FieldName = "ItemGroupId";
            this.gridColumnItemGroupId.Name = "gridColumnItemGroupId";
            // 
            // gridColumnItemGroupName
            // 
            this.gridColumnItemGroupName.Caption = "组合";
            this.gridColumnItemGroupName.FieldName = "ItemGroupName";
            this.gridColumnItemGroupName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnItemGroupName.MaxWidth = 100;
            this.gridColumnItemGroupName.MinWidth = 100;
            this.gridColumnItemGroupName.Name = "gridColumnItemGroupName";
            this.gridColumnItemGroupName.Visible = true;
            this.gridColumnItemGroupName.VisibleIndex = 1;
            this.gridColumnItemGroupName.Width = 100;
            // 
            // gridColumnItemId
            // 
            this.gridColumnItemId.Caption = "项目id";
            this.gridColumnItemId.FieldName = "ItemId";
            this.gridColumnItemId.Name = "gridColumnItemId";
            // 
            // gridColumnItemName
            // 
            this.gridColumnItemName.Caption = "项目";
            this.gridColumnItemName.FieldName = "ItemName";
            this.gridColumnItemName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnItemName.MaxWidth = 100;
            this.gridColumnItemName.MinWidth = 100;
            this.gridColumnItemName.Name = "gridColumnItemName";
            this.gridColumnItemName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnItemName.Visible = true;
            this.gridColumnItemName.VisibleIndex = 2;
            this.gridColumnItemName.Width = 100;
            // 
            // gridColumnStand
            // 
            this.gridColumnStand.Caption = "参考值";
            this.gridColumnStand.FieldName = "Stand";
            this.gridColumnStand.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnStand.MaxWidth = 100;
            this.gridColumnStand.MinWidth = 100;
            this.gridColumnStand.Name = "gridColumnStand";
            this.gridColumnStand.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnStand.Visible = true;
            this.gridColumnStand.VisibleIndex = 3;
            this.gridColumnStand.Width = 100;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControlContrast;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(764, 515);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // checkedComboBoxEditDepartment
            // 
            this.checkedComboBoxEditDepartment.Location = new System.Drawing.Point(52, 12);
            this.checkedComboBoxEditDepartment.Name = "checkedComboBoxEditDepartment";
            this.checkedComboBoxEditDepartment.Properties.AllowMultiSelect = true;
            this.checkedComboBoxEditDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEditDepartment.Properties.DisplayMember = "Name";
            this.checkedComboBoxEditDepartment.Properties.ValueMember = "Id";
            this.checkedComboBoxEditDepartment.Size = new System.Drawing.Size(136, 20);
            this.checkedComboBoxEditDepartment.StyleController = this.layoutControlBase;
            this.checkedComboBoxEditDepartment.TabIndex = 13;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.checkedComboBoxEditDepartment;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem7.Text = "科室：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(36, 14);
            // 
            // checkedComboBoxEditItemGroup
            // 
            this.checkedComboBoxEditItemGroup.Location = new System.Drawing.Point(232, 12);
            this.checkedComboBoxEditItemGroup.Name = "checkedComboBoxEditItemGroup";
            this.checkedComboBoxEditItemGroup.Properties.AllowMultiSelect = true;
            this.checkedComboBoxEditItemGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEditItemGroup.Properties.DisplayMember = "Name";
            this.checkedComboBoxEditItemGroup.Properties.ValueMember = "Id";
            this.checkedComboBoxEditItemGroup.Size = new System.Drawing.Size(136, 20);
            this.checkedComboBoxEditItemGroup.StyleController = this.layoutControlBase;
            this.checkedComboBoxEditItemGroup.TabIndex = 14;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.checkedComboBoxEditItemGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(180, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem1.Text = "组合：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // checkedComboBoxEditItem
            // 
            this.checkedComboBoxEditItem.Location = new System.Drawing.Point(412, 12);
            this.checkedComboBoxEditItem.Name = "checkedComboBoxEditItem";
            this.checkedComboBoxEditItem.Properties.AllowMultiSelect = true;
            this.checkedComboBoxEditItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEditItem.Properties.DisplayMember = "Name";
            this.checkedComboBoxEditItem.Properties.ValueMember = "Id";
            this.checkedComboBoxEditItem.Size = new System.Drawing.Size(136, 20);
            this.checkedComboBoxEditItem.StyleController = this.layoutControlBase;
            this.checkedComboBoxEditItem.TabIndex = 15;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.checkedComboBoxEditItem;
            this.layoutControlItem2.Location = new System.Drawing.Point(360, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem2.Text = "项目：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonSelect
            // 
            this.simpleButtonSelect.Location = new System.Drawing.Point(552, 12);
            this.simpleButtonSelect.Name = "simpleButtonSelect";
            this.simpleButtonSelect.Size = new System.Drawing.Size(76, 22);
            this.simpleButtonSelect.StyleController = this.layoutControlBase;
            this.simpleButtonSelect.TabIndex = 16;
            this.simpleButtonSelect.Text = "查询";
            this.simpleButtonSelect.Click += new System.EventHandler(this.simpleButtonSelect_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonSelect;
            this.layoutControlItem3.Location = new System.Drawing.Point(540, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmHistoryYearComparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "FrmHistoryYearComparison";
            this.Text = "历年对比";
            this.Load += new System.EventHandler(this.FrmCalendarYearComparison_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditItemGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonQuxianTuShi;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDaoChu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gridControlContrast;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewContrast;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEditItem;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEditItemGroup;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEditDepartment;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepartmentId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStand;
    }
}