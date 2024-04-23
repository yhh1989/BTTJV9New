namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    partial class ChargeList
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridInvice = new DevExpress.XtraGrid.GridControl();
            this.GridViewInvice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CustomerBM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClientReg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Actualmoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ReceiptSate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPayment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MInvoiceRecord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChargeDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.UserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Remarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ExportList = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butExit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewInvice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroupBase.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupBase.Size = new System.Drawing.Size(892, 525);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.butExit);
            this.layoutControlBase.Controls.Add(this.ExportList);
            this.layoutControlBase.Controls.Add(this.groupControl1);
            this.layoutControlBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(474, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(892, 525);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridInvice);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(868, 475);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "发票记录";
            // 
            // gridInvice
            // 
            this.gridInvice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvice.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridInvice.Location = new System.Drawing.Point(2, 21);
            this.gridInvice.MainView = this.GridViewInvice;
            this.gridInvice.Margin = new System.Windows.Forms.Padding(4);
            this.gridInvice.Name = "gridInvice";
            this.gridInvice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.gridInvice.Size = new System.Drawing.Size(864, 452);
            this.gridInvice.TabIndex = 1;
            this.gridInvice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewInvice});
            // 
            // GridViewInvice
            // 
            this.GridViewInvice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CustomerBM,
            this.CustomerName,
            this.ClientReg,
            this.Actualmoney,
            this.ReceiptSate,
            this.MPayment,
            this.MInvoiceRecord,
            this.ChargeDate,
            this.UserName,
            this.Remarks});
            this.GridViewInvice.GridControl = this.gridInvice;
            this.GridViewInvice.Name = "GridViewInvice";
            this.GridViewInvice.OptionsBehavior.Editable = false;
            this.GridViewInvice.OptionsView.ShowFooter = true;
            this.GridViewInvice.OptionsView.ShowGroupPanel = false;
            // 
            // CustomerBM
            // 
            this.CustomerBM.Caption = "体检号";
            this.CustomerBM.FieldName = "CustomerReg.CustomerBM";
            this.CustomerBM.Name = "CustomerBM";
            this.CustomerBM.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "CustomerReg.CustomerBM", "合计：{0}条")});
            this.CustomerBM.Visible = true;
            this.CustomerBM.VisibleIndex = 0;
            // 
            // CustomerName
            // 
            this.CustomerName.Caption = "体检人";
            this.CustomerName.FieldName = "CustomerReg.Customer.Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Visible = true;
            this.CustomerName.VisibleIndex = 1;
            // 
            // ClientReg
            // 
            this.ClientReg.Caption = "体检单位";
            this.ClientReg.FieldName = "ClientName";
            this.ClientReg.Name = "ClientReg";
            this.ClientReg.Visible = true;
            this.ClientReg.VisibleIndex = 2;
            // 
            // Actualmoney
            // 
            this.Actualmoney.Caption = "金额";
            this.Actualmoney.FieldName = "Actualmoney";
            this.Actualmoney.Name = "Actualmoney";
            this.Actualmoney.Visible = true;
            this.Actualmoney.VisibleIndex = 3;
            // 
            // ReceiptSate
            // 
            this.ReceiptSate.Caption = "收费类别";
            this.ReceiptSate.FieldName = "ReceiptSate";
            this.ReceiptSate.Name = "ReceiptSate";
            this.ReceiptSate.Visible = true;
            this.ReceiptSate.VisibleIndex = 4;
            // 
            // MPayment
            // 
            this.MPayment.Caption = "支付方式";
            this.MPayment.FieldName = "FormatMPayment";
            this.MPayment.Name = "MPayment";
            this.MPayment.Visible = true;
            this.MPayment.VisibleIndex = 5;
            // 
            // MInvoiceRecord
            // 
            this.MInvoiceRecord.Caption = "发票号";
            this.MInvoiceRecord.FieldName = "FormatMInvoiceRecord";
            this.MInvoiceRecord.Name = "MInvoiceRecord";
            this.MInvoiceRecord.Visible = true;
            this.MInvoiceRecord.VisibleIndex = 6;
            // 
            // ChargeDate
            // 
            this.ChargeDate.Caption = "收费时间";
            this.ChargeDate.ColumnEdit = this.repositoryItemTextEdit2;
            this.ChargeDate.FieldName = "ChargeDate";
            this.ChargeDate.Name = "ChargeDate";
            this.ChargeDate.Visible = true;
            this.ChargeDate.VisibleIndex = 7;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // UserName
            // 
            this.UserName.Caption = "收费人";
            this.UserName.FieldName = "User.Name";
            this.UserName.Name = "UserName";
            this.UserName.Visible = true;
            this.UserName.VisibleIndex = 8;
            // 
            // Remarks
            // 
            this.Remarks.Caption = "备注";
            this.Remarks.FieldName = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.Visible = true;
            this.Remarks.VisibleIndex = 9;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(872, 479);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ExportList
            // 
            this.ExportList.Location = new System.Drawing.Point(399, 491);
            this.ExportList.Margin = new System.Windows.Forms.Padding(4);
            this.ExportList.MinimumSize = new System.Drawing.Size(82, 0);
            this.ExportList.Name = "ExportList";
            this.ExportList.Size = new System.Drawing.Size(84, 22);
            this.ExportList.StyleController = this.layoutControlBase;
            this.ExportList.TabIndex = 13;
            this.ExportList.Text = "导出列表";
            this.ExportList.Click += new System.EventHandler(this.ExportList_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ExportList;
            this.layoutControlItem3.Location = new System.Drawing.Point(387, 479);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // butExit
            // 
            this.butExit.Location = new System.Drawing.Point(487, 491);
            this.butExit.Margin = new System.Windows.Forms.Padding(4);
            this.butExit.MinimumSize = new System.Drawing.Size(55, 0);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(56, 22);
            this.butExit.StyleController = this.layoutControlBase;
            this.butExit.TabIndex = 14;
            this.butExit.Text = "退出";
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.butExit;
            this.layoutControlItem4.Location = new System.Drawing.Point(475, 479);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(60, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(535, 479);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(337, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 479);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(387, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ChargeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 525);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChargeList";
            this.Text = "收费列表";
            this.Load += new System.EventHandler(this.ChargeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewInvice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton butExit;
        private DevExpress.XtraEditors.SimpleButton ExportList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gridInvice;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewInvice;
        private DevExpress.XtraGrid.Columns.GridColumn CustomerBM;
        private DevExpress.XtraGrid.Columns.GridColumn CustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn ClientReg;
        private DevExpress.XtraGrid.Columns.GridColumn Actualmoney;
        private DevExpress.XtraGrid.Columns.GridColumn ReceiptSate;
        private DevExpress.XtraGrid.Columns.GridColumn MPayment;
        private DevExpress.XtraGrid.Columns.GridColumn MInvoiceRecord;
        private DevExpress.XtraGrid.Columns.GridColumn ChargeDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn UserName;
        private DevExpress.XtraGrid.Columns.GridColumn Remarks;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}