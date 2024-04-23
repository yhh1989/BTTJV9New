namespace Sw.Hospital.HealthExaminationSystem.Market
{
    partial class ContractInvoiceEditor
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
            this.textEdit发票号 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.spinEdit金额 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEdit日期 = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton保存 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton取消 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit发票号.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit金额.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit日期.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit日期.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
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
            this.emptySpaceItem2});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(234, 161);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton取消);
            this.layoutControlBase.Controls.Add(this.simpleButton保存);
            this.layoutControlBase.Controls.Add(this.dateEdit日期);
            this.layoutControlBase.Controls.Add(this.spinEdit金额);
            this.layoutControlBase.Controls.Add(this.textEdit发票号);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(234, 161);
            // 
            // textEdit发票号
            // 
            this.textEdit发票号.Location = new System.Drawing.Point(52, 12);
            this.textEdit发票号.Name = "textEdit发票号";
            this.textEdit发票号.Size = new System.Drawing.Size(170, 20);
            this.textEdit发票号.StyleController = this.layoutControlBase;
            this.textEdit发票号.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit发票号;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(214, 24);
            this.layoutControlItem1.Text = "发票号";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(214, 43);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // spinEdit金额
            // 
            this.spinEdit金额.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit金额.Location = new System.Drawing.Point(52, 36);
            this.spinEdit金额.Name = "spinEdit金额";
            this.spinEdit金额.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit金额.Properties.DisplayFormat.FormatString = "c";
            this.spinEdit金额.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit金额.Properties.Mask.EditMask = "c";
            this.spinEdit金额.Size = new System.Drawing.Size(170, 20);
            this.spinEdit金额.StyleController = this.layoutControlBase;
            this.spinEdit金额.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEdit金额;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(214, 24);
            this.layoutControlItem2.Text = "金额";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // dateEdit日期
            // 
            this.dateEdit日期.EditValue = null;
            this.dateEdit日期.Location = new System.Drawing.Point(52, 60);
            this.dateEdit日期.Name = "dateEdit日期";
            this.dateEdit日期.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit日期.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit日期.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit日期.Size = new System.Drawing.Size(170, 20);
            this.dateEdit日期.StyleController = this.layoutControlBase;
            this.dateEdit日期.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateEdit日期;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(214, 24);
            this.layoutControlItem3.Text = "日期";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButton保存
            // 
            this.simpleButton保存.AutoWidthInLayoutControl = true;
            this.simpleButton保存.Location = new System.Drawing.Point(102, 127);
            this.simpleButton保存.Name = "simpleButton保存";
            this.simpleButton保存.Size = new System.Drawing.Size(58, 22);
            this.simpleButton保存.StyleController = this.layoutControlBase;
            this.simpleButton保存.TabIndex = 7;
            this.simpleButton保存.Text = "保存(&S)";
            this.simpleButton保存.Click += new System.EventHandler(this.simpleButton保存_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButton保存;
            this.layoutControlItem4.Location = new System.Drawing.Point(90, 115);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButton取消
            // 
            this.simpleButton取消.AutoWidthInLayoutControl = true;
            this.simpleButton取消.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton取消.Location = new System.Drawing.Point(164, 127);
            this.simpleButton取消.Name = "simpleButton取消";
            this.simpleButton取消.Size = new System.Drawing.Size(58, 22);
            this.simpleButton取消.StyleController = this.layoutControlBase;
            this.simpleButton取消.TabIndex = 8;
            this.simpleButton取消.Text = "取消(&C)";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton取消;
            this.layoutControlItem5.Location = new System.Drawing.Point(152, 115);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 115);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(90, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ContractInvoiceEditor
            // 
            this.AcceptButton = this.simpleButton保存;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButton取消;
            this.ClientSize = new System.Drawing.Size(234, 161);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContractInvoiceEditor";
            this.Text = "合同开票记录编辑";
            this.Load += new System.EventHandler(this.ContractInvoiceEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit发票号.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit金额.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit日期.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit日期.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEdit发票号;
        private DevExpress.XtraEditors.SpinEdit spinEdit金额;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton取消;
        private DevExpress.XtraEditors.SimpleButton simpleButton保存;
        private DevExpress.XtraEditors.DateEdit dateEdit日期;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}