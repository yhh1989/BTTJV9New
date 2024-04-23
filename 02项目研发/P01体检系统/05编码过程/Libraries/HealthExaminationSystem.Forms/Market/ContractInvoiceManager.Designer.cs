namespace Sw.Hospital.HealthExaminationSystem.Market
{
    partial class ContractInvoiceManager
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
            this.gridControl合同开票记录 = new DevExpress.XtraGrid.GridControl();
            this.gridView合同开票记录 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton新增发票 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton删除发票 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem回款金额 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同开票记录)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同开票记录)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem回款金额)).BeginInit();
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
            this.emptySpaceItem1,
            this.simpleLabelItem回款金额});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton删除发票);
            this.layoutControlBase.Controls.Add(this.simpleButton新增发票);
            this.layoutControlBase.Controls.Add(this.gridControl合同开票记录);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(640, 230, 450, 400);
            // 
            // gridControl合同开票记录
            // 
            this.gridControl合同开票记录.Location = new System.Drawing.Point(12, 38);
            this.gridControl合同开票记录.MainView = this.gridView合同开票记录;
            this.gridControl合同开票记录.Name = "gridControl合同开票记录";
            this.gridControl合同开票记录.Size = new System.Drawing.Size(760, 511);
            this.gridControl合同开票记录.TabIndex = 4;
            this.gridControl合同开票记录.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView合同开票记录});
            // 
            // gridView合同开票记录
            // 
            this.gridView合同开票记录.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView合同开票记录.GridControl = this.gridControl合同开票记录;
            this.gridView合同开票记录.Name = "gridView合同开票记录";
            this.gridView合同开票记录.OptionsBehavior.Editable = false;
            this.gridView合同开票记录.OptionsView.ShowFooter = true;
            this.gridView合同开票记录.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "标识";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "金额";
            this.gridColumn2.DisplayFormat.FormatString = "c";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "Amount";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", "开票合计 {0:c}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "日期";
            this.gridColumn3.DisplayFormat.FormatString = "d";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "Date";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "发票号";
            this.gridColumn4.FieldName = "InvoiceNumber";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl合同开票记录;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 515);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButton新增发票
            // 
            this.simpleButton新增发票.AutoWidthInLayoutControl = true;
            this.simpleButton新增发票.Location = new System.Drawing.Point(602, 12);
            this.simpleButton新增发票.Name = "simpleButton新增发票";
            this.simpleButton新增发票.Size = new System.Drawing.Size(83, 22);
            this.simpleButton新增发票.StyleController = this.layoutControlBase;
            this.simpleButton新增发票.TabIndex = 5;
            this.simpleButton新增发票.Text = "新增发票(&A)";
            this.simpleButton新增发票.Click += new System.EventHandler(this.simpleButton新增发票_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton新增发票;
            this.layoutControlItem2.Location = new System.Drawing.Point(590, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleButton删除发票
            // 
            this.simpleButton删除发票.AutoWidthInLayoutControl = true;
            this.simpleButton删除发票.Location = new System.Drawing.Point(689, 12);
            this.simpleButton删除发票.Name = "simpleButton删除发票";
            this.simpleButton删除发票.Size = new System.Drawing.Size(83, 22);
            this.simpleButton删除发票.StyleController = this.layoutControlBase;
            this.simpleButton删除发票.TabIndex = 6;
            this.simpleButton删除发票.Text = "删除发票(&D)";
            this.simpleButton删除发票.Click += new System.EventHandler(this.simpleButton删除发票_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton删除发票;
            this.layoutControlItem3.Location = new System.Drawing.Point(677, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(95, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(495, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem回款金额
            // 
            this.simpleLabelItem回款金额.AllowHotTrack = false;
            this.simpleLabelItem回款金额.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem回款金额.Name = "simpleLabelItem回款金额";
            this.simpleLabelItem回款金额.Size = new System.Drawing.Size(95, 26);
            this.simpleLabelItem回款金额.Text = "回款金额：{0:c}";
            this.simpleLabelItem回款金额.TextSize = new System.Drawing.Size(89, 14);
            // 
            // ContractInvoiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ContractInvoiceManager";
            this.Text = "{0} - 合同开票记录";
            this.Shown += new System.EventHandler(this.ContractInvoiceManager_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同开票记录)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同开票记录)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem回款金额)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl合同开票记录;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView合同开票记录;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton新增发票;
        private DevExpress.XtraEditors.SimpleButton simpleButton删除发票;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem回款金额;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}