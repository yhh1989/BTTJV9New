namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{
    partial class BasicDictionaryList
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
            this.gridViewBasicDictionary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnBasicDictionaryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvBasicDictionaryValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvBasicDictionaryText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvBasicDictionaryRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonEdit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbDel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbReload = new DevExpress.XtraEditors.SimpleButton();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.treeView1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasicDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.simpleSeparator1,
            this.layoutControlItem9,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(784, 562);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton5);
            this.layoutControlBase.Controls.Add(this.searchControl1);
            this.layoutControlBase.Controls.Add(this.treeView1);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.sbDel);
            this.layoutControlBase.Controls.Add(this.simpleButtonEdit);
            this.layoutControlBase.Controls.Add(this.sbAdd);
            this.layoutControlBase.Controls.Add(this.sbReload);
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(784, 562);
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(252, 40);
            this.gridControl.MainView = this.gridViewBasicDictionary;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(520, 510);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBasicDictionary});
            // 
            // gridViewBasicDictionary
            // 
            this.gridViewBasicDictionary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnBasicDictionaryId,
            this.gvBasicDictionaryValue,
            this.gvBasicDictionaryText,
            this.gvBasicDictionaryRemarks,
            this.gridColumn2,
            this.gridColumn3});
            this.gridViewBasicDictionary.GridControl = this.gridControl;
            this.gridViewBasicDictionary.Name = "gridViewBasicDictionary";
            this.gridViewBasicDictionary.OptionsBehavior.Editable = false;
            this.gridViewBasicDictionary.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewBasicDictionary.OptionsCustomization.AllowFilter = false;
            this.gridViewBasicDictionary.OptionsCustomization.AllowGroup = false;
            this.gridViewBasicDictionary.OptionsView.ShowGroupPanel = false;
            this.gridViewBasicDictionary.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewBasicDictionary_RowClick);
            // 
            // gridColumnBasicDictionaryId
            // 
            this.gridColumnBasicDictionaryId.Caption = "Id";
            this.gridColumnBasicDictionaryId.FieldName = "Id";
            this.gridColumnBasicDictionaryId.Name = "gridColumnBasicDictionaryId";
            // 
            // gvBasicDictionaryValue
            // 
            this.gvBasicDictionaryValue.Caption = "值";
            this.gvBasicDictionaryValue.FieldName = "Value";
            this.gvBasicDictionaryValue.MaxWidth = 100;
            this.gvBasicDictionaryValue.Name = "gvBasicDictionaryValue";
            this.gvBasicDictionaryValue.Visible = true;
            this.gvBasicDictionaryValue.VisibleIndex = 0;
            // 
            // gvBasicDictionaryText
            // 
            this.gvBasicDictionaryText.Caption = "文本";
            this.gvBasicDictionaryText.FieldName = "Text";
            this.gvBasicDictionaryText.MaxWidth = 200;
            this.gvBasicDictionaryText.Name = "gvBasicDictionaryText";
            this.gvBasicDictionaryText.Visible = true;
            this.gvBasicDictionaryText.VisibleIndex = 1;
            // 
            // gvBasicDictionaryRemarks
            // 
            this.gvBasicDictionaryRemarks.Caption = "备注";
            this.gvBasicDictionaryRemarks.FieldName = "Remarks";
            this.gvBasicDictionaryRemarks.Name = "gvBasicDictionaryRemarks";
            this.gvBasicDictionaryRemarks.Visible = true;
            this.gvBasicDictionaryRemarks.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "编码";
            this.gridColumn2.FieldName = "Code";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "序号";
            this.gridColumn3.FieldName = "OrderNum";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(240, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(524, 514);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(240, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(227, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbAdd;
            this.layoutControlItem3.Location = new System.Drawing.Point(571, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // sbAdd
            // 
            this.sbAdd.AutoWidthInLayoutControl = true;
            this.sbAdd.Location = new System.Drawing.Point(583, 12);
            this.sbAdd.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbAdd.Name = "sbAdd";
            this.sbAdd.Size = new System.Drawing.Size(48, 22);
            this.sbAdd.StyleController = this.layoutControlBase;
            this.sbAdd.TabIndex = 6;
            this.sbAdd.Text = "新增";
            this.sbAdd.Click += new System.EventHandler(this.sbAdd_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(623, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButtonEdit
            // 
            this.simpleButtonEdit.AutoWidthInLayoutControl = true;
            this.simpleButtonEdit.Location = new System.Drawing.Point(635, 12);
            this.simpleButtonEdit.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonEdit.Name = "simpleButtonEdit";
            this.simpleButtonEdit.Size = new System.Drawing.Size(48, 22);
            this.simpleButtonEdit.StyleController = this.layoutControlBase;
            this.simpleButtonEdit.TabIndex = 7;
            this.simpleButtonEdit.Text = "修改";
            this.simpleButtonEdit.Click += new System.EventHandler(this.simpleButtonEdit_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbDel;
            this.layoutControlItem5.Location = new System.Drawing.Point(675, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // sbDel
            // 
            this.sbDel.AutoWidthInLayoutControl = true;
            this.sbDel.Location = new System.Drawing.Point(687, 12);
            this.sbDel.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbDel.Name = "sbDel";
            this.sbDel.Size = new System.Drawing.Size(48, 22);
            this.sbDel.StyleController = this.layoutControlBase;
            this.sbDel.TabIndex = 8;
            this.sbDel.Text = "删除";
            this.sbDel.Click += new System.EventHandler(this.sbDel_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbReload;
            this.layoutControlItem2.Location = new System.Drawing.Point(519, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // sbReload
            // 
            this.sbReload.AutoWidthInLayoutControl = true;
            this.sbReload.Location = new System.Drawing.Point(531, 12);
            this.sbReload.MinimumSize = new System.Drawing.Size(48, 0);
            this.sbReload.Name = "sbReload";
            this.sbReload.Size = new System.Drawing.Size(48, 22);
            this.sbReload.StyleController = this.layoutControlBase;
            this.sbReload.TabIndex = 5;
            this.sbReload.Text = "刷新";
            this.sbReload.Click += new System.EventHandler(this.sbReload_Click);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(240, 26);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(524, 2);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "类别名称";
            this.gridColumn1.FieldName = "Value";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(479, 12);
            this.simpleButton1.MaximumSize = new System.Drawing.Size(48, 0);
            this.simpleButton1.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(48, 22);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 12;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.simpleButton1;
            this.layoutControlItem9.Location = new System.Drawing.Point(467, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // treeView1
            // 
            this.treeView1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeView1.Enabled = false;
            this.treeView1.Location = new System.Drawing.Point(12, 36);
            this.treeView1.Name = "treeView1";
            this.treeView1.OptionsBehavior.ReadOnly = true;
            this.treeView1.Size = new System.Drawing.Size(236, 514);
            this.treeView1.TabIndex = 13;
            this.treeView1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeView1_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "Key";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "字典类别";
            this.treeListColumn2.FieldName = "Value";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.treeView1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(240, 518);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(12, 12);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(236, 20);
            this.searchControl1.StyleController = this.layoutControlBase;
            this.searchControl1.TabIndex = 14;
            this.searchControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchControl1_KeyPress);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.searchControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(240, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // simpleButton5
            // 
            this.simpleButton5.AutoWidthInLayoutControl = true;
            this.simpleButton5.Location = new System.Drawing.Point(739, 12);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(33, 22);
            this.simpleButton5.StyleController = this.layoutControlBase;
            this.simpleButton5.TabIndex = 27;
            this.simpleButton5.Text = "导入";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.simpleButton5;
            this.layoutControlItem8.Location = new System.Drawing.Point(727, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(37, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // BasicDictionaryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Name = "BasicDictionaryList";
            this.Text = "基本字典";
            this.Load += new System.EventHandler(this.BasicDictionary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasicDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBasicDictionary;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbReload;
        private DevExpress.XtraEditors.SimpleButton sbDel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonEdit;
        private DevExpress.XtraEditors.SimpleButton sbAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBasicDictionaryId;
        private DevExpress.XtraGrid.Columns.GridColumn gvBasicDictionaryValue;
        private DevExpress.XtraGrid.Columns.GridColumn gvBasicDictionaryText;
        private DevExpress.XtraGrid.Columns.GridColumn gvBasicDictionaryRemarks;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraTreeList.TreeList treeView1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}