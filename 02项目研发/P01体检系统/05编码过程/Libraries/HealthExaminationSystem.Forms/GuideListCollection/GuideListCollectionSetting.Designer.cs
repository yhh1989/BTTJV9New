namespace Sw.Hospital.HealthExaminationSystem.GuideListCollection
{
    partial class GuideListCollectionSetting
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
            this.textEditCompany = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ClientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClientAbbreviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HelpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonQuery = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridViewCustomerReg = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CustomerSex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GuidanceNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonPrint = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dataNavigator1 = new DevExpress.XtraEditors.DataNavigator();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.permissionManager1 = new Sw.Hospital.HealthExaminationSystem.Common.Bases.PermissionManager();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCustomerReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(800, 450);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.dataNavigator1);
            this.layoutControlBase.Controls.Add(this.simpleButtonPrint);
            this.layoutControlBase.Controls.Add(this.gridControl1);
            this.layoutControlBase.Controls.Add(this.simpleButtonQuery);
            this.layoutControlBase.Controls.Add(this.textEditCompany);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(800, 450);
            // 
            // textEditCompany
            // 
            this.textEditCompany.Location = new System.Drawing.Point(52, 12);
            this.textEditCompany.Name = "textEditCompany";
            this.textEditCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEditCompany.Properties.DisplayMember = "ClientInfo.ClientName";
            this.textEditCompany.Properties.NullText = "";
            this.textEditCompany.Properties.ValueMember = "Id";
            this.textEditCompany.Properties.View = this.searchLookUpEdit1View;
            this.textEditCompany.Size = new System.Drawing.Size(201, 20);
            this.textEditCompany.StyleController = this.layoutControlBase;
            this.textEditCompany.TabIndex = 4;
            this.textEditCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCompany_KeyDown);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ClientName,
            this.gridColumn1,
            this.ClientAbbreviation,
            this.HelpCode});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ClientName
            // 
            this.ClientName.Caption = "单位名称";
            this.ClientName.FieldName = "ClientInfo.ClientName";
            this.ClientName.Name = "ClientName";
            this.ClientName.Visible = true;
            this.ClientName.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "开始时间";
            this.gridColumn1.FieldName = "StartCheckDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // ClientAbbreviation
            // 
            this.ClientAbbreviation.Caption = "单位简码";
            this.ClientAbbreviation.FieldName = "ClientInfo.ClientAbbreviation";
            this.ClientAbbreviation.Name = "ClientAbbreviation";
            this.ClientAbbreviation.Visible = true;
            this.ClientAbbreviation.VisibleIndex = 2;
            // 
            // HelpCode
            // 
            this.HelpCode.Caption = "助记码";
            this.HelpCode.FieldName = "ClientInfo.HelpCode";
            this.HelpCode.Name = "HelpCode";
            this.HelpCode.Visible = true;
            this.HelpCode.VisibleIndex = 3;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditCompany;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(245, 26);
            this.layoutControlItem1.Text = "单位：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonQuery
            // 
            this.simpleButtonQuery.AutoWidthInLayoutControl = true;
            this.simpleButtonQuery.Location = new System.Drawing.Point(686, 12);
            this.simpleButtonQuery.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonQuery.Name = "simpleButtonQuery";
            this.simpleButtonQuery.Size = new System.Drawing.Size(49, 22);
            this.simpleButtonQuery.StyleController = this.layoutControlBase;
            this.simpleButtonQuery.TabIndex = 5;
            this.simpleButtonQuery.Text = "  查询  ";
            this.simpleButtonQuery.Click += new System.EventHandler(this.simpleButtonQuery_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonQuery;
            this.layoutControlItem2.Location = new System.Drawing.Point(674, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(53, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(245, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(429, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridViewCustomerReg;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(776, 376);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCustomerReg});
            // 
            // gridViewCustomerReg
            // 
            this.gridViewCustomerReg.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CustomerName,
            this.CustomerSex,
            this.GuidanceNum});
            this.gridViewCustomerReg.GridControl = this.gridControl1;
            this.gridViewCustomerReg.Name = "gridViewCustomerReg";
            this.gridViewCustomerReg.OptionsBehavior.Editable = false;
            this.gridViewCustomerReg.OptionsView.ShowGroupPanel = false;
            // 
            // CustomerName
            // 
            this.CustomerName.Caption = "姓名";
            this.CustomerName.FieldName = "Customer.Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Visible = true;
            this.CustomerName.VisibleIndex = 0;
            // 
            // CustomerSex
            // 
            this.CustomerSex.Caption = "性别";
            this.CustomerSex.FieldName = "Customer.Sex";
            this.CustomerSex.Name = "CustomerSex";
            this.CustomerSex.Visible = true;
            this.CustomerSex.VisibleIndex = 1;
            // 
            // GuidanceNum
            // 
            this.GuidanceNum.Caption = "导引单号";
            this.GuidanceNum.FieldName = "GuidanceNum";
            this.GuidanceNum.Name = "GuidanceNum";
            this.GuidanceNum.Visible = true;
            this.GuidanceNum.VisibleIndex = 2;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(780, 380);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleButtonPrint
            // 
            this.simpleButtonPrint.AutoWidthInLayoutControl = true;
            this.simpleButtonPrint.Location = new System.Drawing.Point(739, 12);
            this.simpleButtonPrint.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButtonPrint.Name = "simpleButtonPrint";
            this.simpleButtonPrint.Size = new System.Drawing.Size(49, 22);
            this.simpleButtonPrint.StyleController = this.layoutControlBase;
            this.simpleButtonPrint.TabIndex = 7;
            this.simpleButtonPrint.Text = "  打印  ";
            this.simpleButtonPrint.Click += new System.EventHandler(this.simpleButtonPring_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonPrint;
            this.layoutControlItem4.Location = new System.Drawing.Point(727, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(53, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Buttons.Append.Visible = false;
            this.dataNavigator1.Buttons.CancelEdit.Visible = false;
            this.dataNavigator1.Buttons.EndEdit.Visible = false;
            this.dataNavigator1.Buttons.Next.Visible = false;
            this.dataNavigator1.Buttons.Prev.Visible = false;
            this.dataNavigator1.Buttons.Remove.Visible = false;
            this.dataNavigator1.Location = new System.Drawing.Point(12, 418);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.Size = new System.Drawing.Size(776, 20);
            this.dataNavigator1.StyleController = this.layoutControlBase;
            this.dataNavigator1.TabIndex = 8;
            this.dataNavigator1.TabStop = true;
            this.dataNavigator1.Text = "dataNavigator1";
            this.dataNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
            this.dataNavigator1.TextStringFormat = "第 {0}页，共 {1}页";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dataNavigator1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 406);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(218, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(780, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // permissionManager1
            // 
            this.permissionManager1.Enabled = false;
            // 
            // GuideListCollectionSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "GuideListCollectionSetting";
            this.Text = "导引清单";
            this.Load += new System.EventHandler(this.GuideListCollectionSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCustomerReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchLookUpEdit textEditCompany;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton simpleButtonQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn ClientName;
        private DevExpress.XtraGrid.Columns.GridColumn ClientAbbreviation;
        private DevExpress.XtraGrid.Columns.GridColumn HelpCode;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPrint;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCustomerReg;
        private DevExpress.XtraGrid.Columns.GridColumn CustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn CustomerSex;
        private DevExpress.XtraGrid.Columns.GridColumn GuidanceNum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.DataNavigator dataNavigator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Common.Bases.PermissionManager permissionManager1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}