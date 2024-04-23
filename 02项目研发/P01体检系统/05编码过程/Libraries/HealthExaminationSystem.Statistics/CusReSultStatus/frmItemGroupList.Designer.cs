namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus
{
    partial class frmItemGroupList
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
            this.treeListDepartments = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnItemGroupId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnItemGroupName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
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
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(529, 399);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.checkEdit1);
            this.layoutControlBase.Controls.Add(this.searchControl1);
            this.layoutControlBase.Controls.Add(this.simpleButton2);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.treeListDepartments);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(529, 399);
            // 
            // treeListDepartments
            // 
            this.treeListDepartments.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnItemGroupId,
            this.treeListColumnItemGroupName});
            this.treeListDepartments.Location = new System.Drawing.Point(12, 59);
            this.treeListDepartments.Margin = new System.Windows.Forms.Padding(15, 236087, 15, 236087);
            this.treeListDepartments.Name = "treeListDepartments";
            this.treeListDepartments.OptionsBehavior.Editable = false;
            this.treeListDepartments.OptionsView.ShowCheckBoxes = true;
            this.treeListDepartments.Size = new System.Drawing.Size(505, 302);
            this.treeListDepartments.TabIndex = 8;
            // 
            // treeListColumnItemGroupId
            // 
            this.treeListColumnItemGroupId.Caption = "Id";
            this.treeListColumnItemGroupId.FieldName = "Id";
            this.treeListColumnItemGroupId.Name = "treeListColumnItemGroupId";
            // 
            // treeListColumnItemGroupName
            // 
            this.treeListColumnItemGroupName.Caption = "组合";
            this.treeListColumnItemGroupName.FieldName = "ItemGroupName";
            this.treeListColumnItemGroupName.MinWidth = 34;
            this.treeListColumnItemGroupName.Name = "treeListColumnItemGroupName";
            this.treeListColumnItemGroupName.Visible = true;
            this.treeListColumnItemGroupName.VisibleIndex = 0;
            this.treeListColumnItemGroupName.Width = 60;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeListDepartments;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(509, 306);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(214, 365);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(53, 22);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton1;
            this.layoutControlItem2.Location = new System.Drawing.Point(202, 353);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(57, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(271, 365);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(61, 22);
            this.simpleButton2.StyleController = this.layoutControlBase;
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "退出";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton2;
            this.layoutControlItem3.Location = new System.Drawing.Point(259, 353);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 353);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(202, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(324, 353);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(185, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(12, 12);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(49, 442, 49, 442);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.NullValuePrompt = "项目名称/助记码";
            this.searchControl1.Size = new System.Drawing.Size(505, 20);
            this.searchControl1.StyleController = this.layoutControlBase;
            this.searchControl1.TabIndex = 66;
            this.searchControl1.TextChanged += new System.EventHandler(this.searchControl1_TextChanged);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.searchControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(509, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(12, 36);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "全选/反选";
            this.checkEdit1.Size = new System.Drawing.Size(505, 19);
            this.checkEdit1.StyleController = this.layoutControlBase;
            this.checkEdit1.TabIndex = 69;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.checkEdit1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(509, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmItemGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 399);
            this.Name = "frmItemGroupList";
            this.Text = "选择组合";
            this.Load += new System.EventHandler(this.frmItemGroupList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.TreeList treeListDepartments;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnItemGroupId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnItemGroupName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}