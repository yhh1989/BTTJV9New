namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    partial class InspectionReturn
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnKeShi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnZuHe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnXiangMu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnJieGuo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2KeShi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2XiangMu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2YuanYin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonReturn = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            this.layoutControlItem4,
            this.layoutControlItem3});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonExit);
            this.layoutControlBase.Controls.Add(this.simpleButtonReturn);
            this.layoutControlBase.Controls.Add(this.gridControl2);
            this.layoutControlBase.Controls.Add(this.gridControl1);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(335, 511);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnKeShi,
            this.gridColumnZuHe,
            this.gridColumnXiangMu,
            this.gridColumnJieGuo});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // gridColumnKeShi
            // 
            this.gridColumnKeShi.Caption = "科室";
            this.gridColumnKeShi.FieldName = "DepartmentName";
            this.gridColumnKeShi.Name = "gridColumnKeShi";
            this.gridColumnKeShi.Visible = true;
            this.gridColumnKeShi.VisibleIndex = 0;
            // 
            // gridColumnZuHe
            // 
            this.gridColumnZuHe.Caption = "组合";
            this.gridColumnZuHe.FieldName = "ItemGroupName";
            this.gridColumnZuHe.Name = "gridColumnZuHe";
            this.gridColumnZuHe.Visible = true;
            this.gridColumnZuHe.VisibleIndex = 1;
            // 
            // gridColumnXiangMu
            // 
            this.gridColumnXiangMu.Caption = "项目";
            this.gridColumnXiangMu.FieldName = "ItemName";
            this.gridColumnXiangMu.Name = "gridColumnXiangMu";
            this.gridColumnXiangMu.Visible = true;
            this.gridColumnXiangMu.VisibleIndex = 2;
            // 
            // gridColumnJieGuo
            // 
            this.gridColumnJieGuo.Caption = "结果";
            this.gridColumnJieGuo.FieldName = "ItemResultChar";
            this.gridColumnJieGuo.Name = "gridColumnJieGuo";
            this.gridColumnJieGuo.Visible = true;
            this.gridColumnJieGuo.VisibleIndex = 3;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(339, 515);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(413, 12);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gridControl2.Size = new System.Drawing.Size(359, 511);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2KeShi,
            this.gridColumn2XiangMu,
            this.gridColumn2YuanYin});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView2_MouseDown);
            // 
            // gridColumn2KeShi
            // 
            this.gridColumn2KeShi.Caption = "科室";
            this.gridColumn2KeShi.FieldName = "DepartmentName";
            this.gridColumn2KeShi.MaxWidth = 60;
            this.gridColumn2KeShi.Name = "gridColumn2KeShi";
            this.gridColumn2KeShi.OptionsColumn.AllowEdit = false;
            this.gridColumn2KeShi.Visible = true;
            this.gridColumn2KeShi.VisibleIndex = 0;
            this.gridColumn2KeShi.Width = 50;
            // 
            // gridColumn2XiangMu
            // 
            this.gridColumn2XiangMu.Caption = "项目";
            this.gridColumn2XiangMu.FieldName = "ItemFlag";
            this.gridColumn2XiangMu.MaxWidth = 80;
            this.gridColumn2XiangMu.Name = "gridColumn2XiangMu";
            this.gridColumn2XiangMu.OptionsColumn.AllowEdit = false;
            this.gridColumn2XiangMu.Visible = true;
            this.gridColumn2XiangMu.VisibleIndex = 1;
            // 
            // gridColumn2YuanYin
            // 
            this.gridColumn2YuanYin.Caption = "退回原因";
            this.gridColumn2YuanYin.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn2YuanYin.FieldName = "ReturnCause";
            this.gridColumn2YuanYin.Name = "gridColumn2YuanYin";
            this.gridColumn2YuanYin.Visible = true;
            this.gridColumn2YuanYin.VisibleIndex = 2;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(401, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(363, 515);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(339, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(62, 515);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonReturn
            // 
            this.simpleButtonReturn.Location = new System.Drawing.Point(648, 527);
            this.simpleButtonReturn.MaximumSize = new System.Drawing.Size(60, 0);
            this.simpleButtonReturn.MinimumSize = new System.Drawing.Size(60, 0);
            this.simpleButtonReturn.Name = "simpleButtonReturn";
            this.simpleButtonReturn.Size = new System.Drawing.Size(60, 22);
            this.simpleButtonReturn.StyleController = this.layoutControlBase;
            this.simpleButtonReturn.TabIndex = 6;
            this.simpleButtonReturn.Text = "保存";
            this.simpleButtonReturn.Click += new System.EventHandler(this.simpleButtonReturn_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonReturn;
            this.layoutControlItem3.Location = new System.Drawing.Point(636, 515);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 515);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(636, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.Location = new System.Drawing.Point(712, 527);
            this.simpleButtonExit.MaximumSize = new System.Drawing.Size(60, 0);
            this.simpleButtonExit.MinimumSize = new System.Drawing.Size(60, 0);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(60, 22);
            this.simpleButtonExit.StyleController = this.layoutControlBase;
            this.simpleButtonExit.TabIndex = 7;
            this.simpleButtonExit.Text = "退出";
            this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonExit;
            this.layoutControlItem4.Location = new System.Drawing.Point(700, 515);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // InspectionReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "InspectionReturn";
            this.Text = "总检退回";
            this.Load += new System.EventHandler(this.InspectionReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnKeShi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnZuHe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnXiangMu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnJieGuo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2KeShi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2XiangMu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2YuanYin;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButtonReturn;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
    }
}