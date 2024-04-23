namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    partial class frmSearchClient
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
            this.treClientInfo = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListClientBM = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListClientName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnClientAbbreviation = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnHelpCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnClientSource = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnLinkMan = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.txtClientName = new DevExpress.XtraEditors.SearchControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dataNavigator = new DevExpress.XtraEditors.DataNavigator();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treClientInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.emptySpaceItem3});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(933, 525);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton2);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.dataNavigator);
            this.layoutControlBase.Controls.Add(this.txtClientName);
            this.layoutControlBase.Controls.Add(this.btnSearch);
            this.layoutControlBase.Controls.Add(this.treClientInfo);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(555, 274, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(933, 525);
            // 
            // treClientInfo
            // 
            this.treClientInfo.Caption = "单位信息";
            this.treClientInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnId,
            this.treeListClientBM,
            this.treeListClientName,
            this.treeListColumnClientAbbreviation,
            this.treeListColumnHelpCode,
            this.treeListColumnClientSource,
            this.treeListColumnLinkMan});
            this.treClientInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.treClientInfo.KeyFieldName = "Id";
            this.treClientInfo.Location = new System.Drawing.Point(12, 38);
            this.treClientInfo.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.treClientInfo.Name = "treClientInfo";
            this.treClientInfo.OptionsBehavior.Editable = false;
            this.treClientInfo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treClientInfo.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.treClientInfo.ParentFieldName = "Parent.Id";
            this.treClientInfo.Size = new System.Drawing.Size(909, 421);
            this.treClientInfo.TabIndex = 27;
            this.treClientInfo.DoubleClick += new System.EventHandler(this.treClientInfo_DoubleClick);
            // 
            // treeListColumnId
            // 
            this.treeListColumnId.Caption = "Id";
            this.treeListColumnId.FieldName = "Id";
            this.treeListColumnId.Name = "treeListColumnId";
            // 
            // treeListClientBM
            // 
            this.treeListClientBM.AppearanceCell.Options.UseTextOptions = true;
            this.treeListClientBM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListClientBM.Caption = "单位编码";
            this.treeListClientBM.FieldName = "ClientBM";
            this.treeListClientBM.Name = "treeListClientBM";
            this.treeListClientBM.Visible = true;
            this.treeListClientBM.VisibleIndex = 0;
            this.treeListClientBM.Width = 150;
            // 
            // treeListClientName
            // 
            this.treeListClientName.AppearanceCell.Options.UseTextOptions = true;
            this.treeListClientName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListClientName.Caption = "单位名称";
            this.treeListClientName.FieldName = "ClientName";
            this.treeListClientName.Name = "treeListClientName";
            this.treeListClientName.Visible = true;
            this.treeListClientName.VisibleIndex = 1;
            this.treeListClientName.Width = 152;
            // 
            // treeListColumnClientAbbreviation
            // 
            this.treeListColumnClientAbbreviation.Caption = "单位简称";
            this.treeListColumnClientAbbreviation.FieldName = "ClientAbbreviation";
            this.treeListColumnClientAbbreviation.Name = "treeListColumnClientAbbreviation";
            this.treeListColumnClientAbbreviation.Visible = true;
            this.treeListColumnClientAbbreviation.VisibleIndex = 2;
            this.treeListColumnClientAbbreviation.Width = 150;
            // 
            // treeListColumnHelpCode
            // 
            this.treeListColumnHelpCode.Caption = "助记码";
            this.treeListColumnHelpCode.FieldName = "HelpCode";
            this.treeListColumnHelpCode.Name = "treeListColumnHelpCode";
            this.treeListColumnHelpCode.Visible = true;
            this.treeListColumnHelpCode.VisibleIndex = 3;
            this.treeListColumnHelpCode.Width = 150;
            // 
            // treeListColumnClientSource
            // 
            this.treeListColumnClientSource.Caption = "来源";
            this.treeListColumnClientSource.FieldName = "ClientSource";
            this.treeListColumnClientSource.Name = "treeListColumnClientSource";
            this.treeListColumnClientSource.Visible = true;
            this.treeListColumnClientSource.VisibleIndex = 4;
            this.treeListColumnClientSource.Width = 150;
            // 
            // treeListColumnLinkMan
            // 
            this.treeListColumnLinkMan.Caption = "联系人";
            this.treeListColumnLinkMan.FieldName = "LinkMan";
            this.treeListColumnLinkMan.Name = "treeListColumnLinkMan";
            this.treeListColumnLinkMan.Visible = true;
            this.treeListColumnLinkMan.VisibleIndex = 5;
            this.treeListColumnLinkMan.Width = 150;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treClientInfo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(913, 425);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoWidthInLayoutControl = true;
            this.btnSearch.Location = new System.Drawing.Point(873, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSearch.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 22);
            this.btnSearch.StyleController = this.layoutControlBase;
            this.btnSearch.TabIndex = 31;
            this.btnSearch.Text = "查询";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSearch;
            this.layoutControlItem3.Location = new System.Drawing.Point(861, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(245, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(616, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(12, 12);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.txtClientName.Size = new System.Drawing.Size(241, 20);
            this.txtClientName.StyleController = this.layoutControlBase;
            this.txtClientName.TabIndex = 32;
            this.txtClientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClientName_KeyDown);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtClientName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(245, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // dataNavigator
            // 
            this.dataNavigator.Buttons.Append.Visible = false;
            this.dataNavigator.Buttons.CancelEdit.Visible = false;
            this.dataNavigator.Buttons.EndEdit.Visible = false;
            this.dataNavigator.Buttons.Next.Visible = false;
            this.dataNavigator.Buttons.Prev.Visible = false;
            this.dataNavigator.Buttons.Remove.Visible = false;
            this.dataNavigator.Location = new System.Drawing.Point(12, 463);
            this.dataNavigator.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dataNavigator.Name = "dataNavigator";
            this.dataNavigator.Size = new System.Drawing.Size(198, 20);
            this.dataNavigator.StyleController = this.layoutControlBase;
            this.dataNavigator.TabIndex = 39;
            this.dataNavigator.Text = "dataNavigator1";
            this.dataNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
            this.dataNavigator.TextStringFormat = "第{0}页,共{0}页";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dataNavigator;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 451);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(913, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(410, 487);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(43, 26);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 40;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton1;
            this.layoutControlItem5.Location = new System.Drawing.Point(398, 475);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(47, 30);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(457, 487);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(43, 26);
            this.simpleButton2.StyleController = this.layoutControlBase;
            this.simpleButton2.TabIndex = 41;
            this.simpleButton2.Text = "退出";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton2;
            this.layoutControlItem6.Location = new System.Drawing.Point(445, 475);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(47, 30);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 475);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(398, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(492, 475);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(421, 30);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmSearchClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 525);
            this.Name = "frmSearchClient";
            this.Text = "检索单位";
            this.Load += new System.EventHandler(this.frmSearchClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treClientInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraTreeList.TreeList treClientInfo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListClientBM;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListClientName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnClientAbbreviation;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnHelpCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnClientSource;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnLinkMan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SearchControl txtClientName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DataNavigator dataNavigator;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}