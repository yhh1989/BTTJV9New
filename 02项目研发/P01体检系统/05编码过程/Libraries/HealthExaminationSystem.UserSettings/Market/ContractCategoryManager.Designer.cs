namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Market
{
    partial class ContractCategoryManager
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
            this.gridControl合同类别列表 = new DevExpress.XtraGrid.GridControl();
            this.gridView合同类别列表 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEdit名称检索 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleButton查询 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButton添加 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton编辑 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton启用或禁用 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同类别列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同类别列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit名称检索.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton启用或禁用);
            this.layoutControlBase.Controls.Add(this.simpleButton编辑);
            this.layoutControlBase.Controls.Add(this.simpleButton添加);
            this.layoutControlBase.Controls.Add(this.simpleButton查询);
            this.layoutControlBase.Controls.Add(this.textEdit名称检索);
            this.layoutControlBase.Controls.Add(this.gridControl合同类别列表);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 171, 450, 400);
            // 
            // gridControl合同类别列表
            // 
            this.gridControl合同类别列表.Location = new System.Drawing.Point(12, 105);
            this.gridControl合同类别列表.MainView = this.gridView合同类别列表;
            this.gridControl合同类别列表.Name = "gridControl合同类别列表";
            this.gridControl合同类别列表.Size = new System.Drawing.Size(760, 444);
            this.gridControl合同类别列表.TabIndex = 4;
            this.gridControl合同类别列表.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView合同类别列表});
            // 
            // gridView合同类别列表
            // 
            this.gridView合同类别列表.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView合同类别列表.GridControl = this.gridControl合同类别列表;
            this.gridView合同类别列表.Name = "gridView合同类别列表";
            this.gridView合同类别列表.OptionsBehavior.Editable = false;
            this.gridView合同类别列表.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "标识";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "助记码";
            this.gridColumn3.FieldName = "MnemonicCode";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "已启用";
            this.gridColumn4.FieldName = "IsActive";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl合同类别列表;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 448);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // textEdit名称检索
            // 
            this.textEdit名称检索.Location = new System.Drawing.Point(52, 43);
            this.textEdit名称检索.MaximumSize = new System.Drawing.Size(150, 0);
            this.textEdit名称检索.MinimumSize = new System.Drawing.Size(150, 0);
            this.textEdit名称检索.Name = "textEdit名称检索";
            this.textEdit名称检索.Size = new System.Drawing.Size(150, 20);
            this.textEdit名称检索.StyleController = this.layoutControlBase;
            this.textEdit名称检索.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEdit名称检索;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(182, 24);
            this.layoutControlItem2.Text = "名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Flow;
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(764, 67);
            this.layoutControlGroup1.Text = "条件检索";
            // 
            // simpleButton查询
            // 
            this.simpleButton查询.AutoWidthInLayoutControl = true;
            this.simpleButton查询.Location = new System.Drawing.Point(496, 79);
            this.simpleButton查询.Name = "simpleButton查询";
            this.simpleButton查询.Size = new System.Drawing.Size(60, 22);
            this.simpleButton查询.StyleController = this.layoutControlBase;
            this.simpleButton查询.TabIndex = 6;
            this.simpleButton查询.Text = "查询(&Q)";
            this.simpleButton查询.Click += new System.EventHandler(this.simpleButton查询_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton查询;
            this.layoutControlItem3.Location = new System.Drawing.Point(484, 67);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 67);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(484, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButton添加
            // 
            this.simpleButton添加.AutoWidthInLayoutControl = true;
            this.simpleButton添加.Location = new System.Drawing.Point(560, 79);
            this.simpleButton添加.Name = "simpleButton添加";
            this.simpleButton添加.Size = new System.Drawing.Size(59, 22);
            this.simpleButton添加.StyleController = this.layoutControlBase;
            this.simpleButton添加.TabIndex = 7;
            this.simpleButton添加.Text = "添加(&A)";
            this.simpleButton添加.Click += new System.EventHandler(this.simpleButton添加_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButton添加;
            this.layoutControlItem4.Location = new System.Drawing.Point(548, 67);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButton编辑
            // 
            this.simpleButton编辑.AutoWidthInLayoutControl = true;
            this.simpleButton编辑.Location = new System.Drawing.Point(623, 79);
            this.simpleButton编辑.Name = "simpleButton编辑";
            this.simpleButton编辑.Size = new System.Drawing.Size(58, 22);
            this.simpleButton编辑.StyleController = this.layoutControlBase;
            this.simpleButton编辑.TabIndex = 8;
            this.simpleButton编辑.Text = "编辑(&E)";
            this.simpleButton编辑.Click += new System.EventHandler(this.simpleButton编辑_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton编辑;
            this.layoutControlItem5.Location = new System.Drawing.Point(611, 67);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // simpleButton启用或禁用
            // 
            this.simpleButton启用或禁用.AutoWidthInLayoutControl = true;
            this.simpleButton启用或禁用.Location = new System.Drawing.Point(685, 79);
            this.simpleButton启用或禁用.Name = "simpleButton启用或禁用";
            this.simpleButton启用或禁用.Size = new System.Drawing.Size(87, 22);
            this.simpleButton启用或禁用.StyleController = this.layoutControlBase;
            this.simpleButton启用或禁用.TabIndex = 9;
            this.simpleButton启用或禁用.Text = "启用/禁用(&C)";
            this.simpleButton启用或禁用.Click += new System.EventHandler(this.simpleButton启用或禁用_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton启用或禁用;
            this.layoutControlItem6.Location = new System.Drawing.Point(673, 67);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // ContractCategoryManager
            // 
            this.AcceptButton = this.simpleButton查询;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "ContractCategoryManager";
            this.Text = "合同类别管理";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同类别列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同类别列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit名称检索.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl合同类别列表;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView合同类别列表;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit textEdit名称检索;
        private DevExpress.XtraEditors.SimpleButton simpleButton查询;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButton启用或禁用;
        private DevExpress.XtraEditors.SimpleButton simpleButton编辑;
        private DevExpress.XtraEditors.SimpleButton simpleButton添加;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}