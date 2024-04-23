namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    partial class frmApplivation
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
            this.gridInvo = new DevExpress.XtraGrid.GridControl();
            this.gridViewInvoice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.conApplicationNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.conZHMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.conSQSTATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.conCreatTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtpayMoney = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.操作 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtFP = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textRemark = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpayMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.操作)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.操作,
            this.layoutControlItem10});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(589, 384);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.labelControl1);
            this.layoutControlBase.Controls.Add(this.simpleButton4);
            this.layoutControlBase.Controls.Add(this.textRemark);
            this.layoutControlBase.Controls.Add(this.simpleButton3);
            this.layoutControlBase.Controls.Add(this.txtFP);
            this.layoutControlBase.Controls.Add(this.checkEdit1);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.gridInvo);
            this.layoutControlBase.Controls.Add(this.txtpayMoney);
            this.layoutControlBase.Controls.Add(this.simpleButton2);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(589, 384);
            // 
            // gridInvo
            // 
            this.gridInvo.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.gridInvo.Location = new System.Drawing.Point(12, 12);
            this.gridInvo.MainView = this.gridViewInvoice;
            this.gridInvo.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.gridInvo.Name = "gridInvo";
            this.gridInvo.Size = new System.Drawing.Size(565, 191);
            this.gridInvo.TabIndex = 5;
            this.gridInvo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInvoice});
            // 
            // gridViewInvoice
            // 
            this.gridViewInvoice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.conApplicationNum,
            this.conZHMoney,
            this.conSQSTATUS,
            this.conCreatTime,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridViewInvoice.GridControl = this.gridInvo;
            this.gridViewInvoice.Name = "gridViewInvoice";
            this.gridViewInvoice.OptionsBehavior.Editable = false;
            this.gridViewInvoice.OptionsSelection.MultiSelect = true;
            this.gridViewInvoice.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewInvoice.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewInvoice.OptionsView.ShowGroupPanel = false;
            // 
            // conApplicationNum
            // 
            this.conApplicationNum.AppearanceCell.Options.UseTextOptions = true;
            this.conApplicationNum.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.conApplicationNum.Caption = "申请单号";
            this.conApplicationNum.FieldName = "ApplicationNum";
            this.conApplicationNum.Name = "conApplicationNum";
            this.conApplicationNum.Visible = true;
            this.conApplicationNum.VisibleIndex = 1;
            // 
            // conZHMoney
            // 
            this.conZHMoney.Caption = "申请单金额";
            this.conZHMoney.FieldName = "ZHMoney";
            this.conZHMoney.Name = "conZHMoney";
            this.conZHMoney.Visible = true;
            this.conZHMoney.VisibleIndex = 2;
            // 
            // conSQSTATUS
            // 
            this.conSQSTATUS.AppearanceCell.Options.UseTextOptions = true;
            this.conSQSTATUS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.conSQSTATUS.Caption = "申请单状态";
            this.conSQSTATUS.FieldName = "fSQSTATUS";
            this.conSQSTATUS.Name = "conSQSTATUS";
            this.conSQSTATUS.Visible = true;
            this.conSQSTATUS.VisibleIndex = 3;
            // 
            // conCreatTime
            // 
            this.conCreatTime.AppearanceCell.Options.UseTextOptions = true;
            this.conCreatTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.conCreatTime.Caption = "申请单时间";
            this.conCreatTime.FieldName = "CreatTime";
            this.conCreatTime.Name = "conCreatTime";
            this.conCreatTime.Visible = true;
            this.conCreatTime.VisibleIndex = 4;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "发票抬头";
            this.gridColumn1.FieldName = "FPName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "备注";
            this.gridColumn2.FieldName = "Remark";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridInvo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(569, 195);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(340, 320);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.simpleButton1.MinimumSize = new System.Drawing.Size(91, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(91, 22);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "收费申请";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtpayMoney
            // 
            this.txtpayMoney.EditValue = "0.00";
            this.txtpayMoney.Location = new System.Drawing.Point(88, 238);
            this.txtpayMoney.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.txtpayMoney.Name = "txtpayMoney";
            this.txtpayMoney.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.txtpayMoney.Properties.Appearance.Options.UseFont = true;
            this.txtpayMoney.Properties.Mask.EditMask = "c";
            this.txtpayMoney.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtpayMoney.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtpayMoney.Properties.MaxLength = 16;
            this.txtpayMoney.Size = new System.Drawing.Size(477, 30);
            this.txtpayMoney.StyleController = this.layoutControlBase;
            this.txtpayMoney.TabIndex = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtpayMoney;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(545, 34);
            this.layoutControlItem3.Text = "申请金额：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton1;
            this.layoutControlItem5.Location = new System.Drawing.Point(316, 82);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // 操作
            // 
            this.操作.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem2,
            this.layoutControlItem9});
            this.操作.Location = new System.Drawing.Point(0, 195);
            this.操作.Name = "操作";
            this.操作.Size = new System.Drawing.Size(569, 151);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtFP;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(545, 24);
            this.layoutControlItem4.Text = "发票抬头：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // txtFP
            // 
            this.txtFP.Location = new System.Drawing.Point(88, 272);
            this.txtFP.Name = "txtFP";
            this.txtFP.Size = new System.Drawing.Size(477, 20);
            this.txtFP.StyleController = this.layoutControlBase;
            this.txtFP.TabIndex = 10;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.textRemark;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 58);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(545, 24);
            this.layoutControlItem8.Text = "备注：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // textRemark
            // 
            this.textRemark.Location = new System.Drawing.Point(88, 296);
            this.textRemark.Name = "textRemark";
            this.textRemark.Size = new System.Drawing.Size(477, 20);
            this.textRemark.StyleController = this.layoutControlBase;
            this.textRemark.TabIndex = 13;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton2;
            this.layoutControlItem6.Location = new System.Drawing.Point(264, 82);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.AutoWidthInLayoutControl = true;
            this.simpleButton2.Location = new System.Drawing.Point(288, 320);
            this.simpleButton2.MinimumSize = new System.Drawing.Size(48, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(48, 22);
            this.simpleButton2.StyleController = this.layoutControlBase;
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "删除";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.simpleButton3;
            this.layoutControlItem7.Location = new System.Drawing.Point(411, 82);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // simpleButton3
            // 
            this.simpleButton3.AutoWidthInLayoutControl = true;
            this.simpleButton3.Location = new System.Drawing.Point(435, 320);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(69, 22);
            this.simpleButton3.StyleController = this.layoutControlBase;
            this.simpleButton3.TabIndex = 12;
            this.simpleButton3.Text = "打印申请单";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.checkEdit1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 82);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(24, 320);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "打印单据";
            this.checkEdit1.Size = new System.Drawing.Size(260, 19);
            this.checkEdit1.StyleController = this.layoutControlBase;
            this.checkEdit1.TabIndex = 9;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.simpleButton4;
            this.layoutControlItem9.Location = new System.Drawing.Point(484, 82);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // simpleButton4
            // 
            this.simpleButton4.AutoWidthInLayoutControl = true;
            this.simpleButton4.Location = new System.Drawing.Point(508, 320);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(57, 22);
            this.simpleButton4.StyleController = this.layoutControlBase;
            this.simpleButton4.TabIndex = 14;
            this.simpleButton4.Text = "回款处理";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 358);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(276, 14);
            this.labelControl1.StyleController = this.layoutControlBase;
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "在收费系统不回传申请单的情况下可以点击回款处理";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl1;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 346);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(569, 18);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "回款金额";
            this.gridColumn3.FieldName = "REFYZK";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            // 
            // frmApplivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 384);
            this.Name = "frmApplivation";
            this.Text = "收费申请";
            this.Load += new System.EventHandler(this.frmApplivation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpayMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.操作)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridInvo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn conApplicationNum;
        private DevExpress.XtraGrid.Columns.GridColumn conSQSTATUS;
        private DevExpress.XtraGrid.Columns.GridColumn conCreatTime;
        private DevExpress.XtraGrid.Columns.GridColumn conZHMoney;
        private DevExpress.XtraLayout.LayoutControlGroup 操作;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txtpayMoney;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtFP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit textRemark;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}