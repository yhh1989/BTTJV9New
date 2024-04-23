namespace Sw.Hospital.HealthExaminationSystem.Statistics.GeneralDoctor
{
    partial class FrmTCTJ
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
            this.dgc = new DevExpress.XtraGrid.GridControl();
            this.gvItemSuit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTaoCanLeiBie = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaoCanMingCheng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChengBenJia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDanJia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShuLiang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShouldmoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualmoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dt_Starte = new DevExpress.XtraEditors.DateEdit();
            this.开始时间 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dt_End = new DevExpress.XtraEditors.DateEdit();
            this.结束时间 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Select = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lueItemSuitType = new DevExpress.XtraEditors.LookUpEdit();
            this.套餐类别 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkcmbItemSuit = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.套餐名称 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemSuit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Starte.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Starte.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.开始时间)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.结束时间)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueItemSuitType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.套餐类别)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbItemSuit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.套餐名称)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.结束时间,
            this.开始时间,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.套餐类别,
            this.套餐名称,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupBase.Size = new System.Drawing.Size(1350, 530);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.chkcmbItemSuit);
            this.layoutControlBase.Controls.Add(this.lueItemSuitType);
            this.layoutControlBase.Controls.Add(this.dgc);
            this.layoutControlBase.Controls.Add(this.dt_Starte);
            this.layoutControlBase.Controls.Add(this.dt_End);
            this.layoutControlBase.Controls.Add(this.btn_Select);
            this.layoutControlBase.Controls.Add(this.btn_Export);
            this.layoutControlBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(1350, 530);
            // 
            // dgc
            // 
            this.dgc.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.dgc.Location = new System.Drawing.Point(12, 38);
            this.dgc.MainView = this.gvItemSuit;
            this.dgc.Margin = new System.Windows.Forms.Padding(4);
            this.dgc.Name = "dgc";
            this.dgc.Size = new System.Drawing.Size(1326, 480);
            this.dgc.TabIndex = 7;
            this.dgc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemSuit});
            // 
            // gvItemSuit
            // 
            this.gvItemSuit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTaoCanLeiBie,
            this.colTaoCanMingCheng,
            this.colChengBenJia,
            this.colDanJia,
            this.colDiscount,
            this.colShuLiang,
            this.colShouldmoney,
            this.colActualmoney});
            this.gvItemSuit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gvItemSuit.GridControl = this.dgc;
            this.gvItemSuit.Name = "gvItemSuit";
            this.gvItemSuit.OptionsView.ShowGroupPanel = false;
            this.gvItemSuit.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvItemSuit_CustomDrawRowIndicator);
            // 
            // colTaoCanLeiBie
            // 
            this.colTaoCanLeiBie.Caption = "套餐类别";
            this.colTaoCanLeiBie.FieldName = "ItemSuitTypeDisplay";
            this.colTaoCanLeiBie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colTaoCanLeiBie.Name = "colTaoCanLeiBie";
            this.colTaoCanLeiBie.OptionsColumn.AllowEdit = false;
            this.colTaoCanLeiBie.Visible = true;
            this.colTaoCanLeiBie.VisibleIndex = 0;
            this.colTaoCanLeiBie.Width = 109;
            // 
            // colTaoCanMingCheng
            // 
            this.colTaoCanMingCheng.Caption = "套餐名称";
            this.colTaoCanMingCheng.FieldName = "ItemSuitName";
            this.colTaoCanMingCheng.Name = "colTaoCanMingCheng";
            this.colTaoCanMingCheng.OptionsColumn.AllowEdit = false;
            this.colTaoCanMingCheng.Visible = true;
            this.colTaoCanMingCheng.VisibleIndex = 1;
            this.colTaoCanMingCheng.Width = 170;
            // 
            // colChengBenJia
            // 
            this.colChengBenJia.Caption = "成本价";
            this.colChengBenJia.FieldName = "CostPrice";
            this.colChengBenJia.Name = "colChengBenJia";
            this.colChengBenJia.OptionsColumn.AllowEdit = false;
            this.colChengBenJia.Visible = true;
            this.colChengBenJia.VisibleIndex = 2;
            this.colChengBenJia.Width = 86;
            // 
            // colDanJia
            // 
            this.colDanJia.Caption = "单价";
            this.colDanJia.FieldName = "Price";
            this.colDanJia.Name = "colDanJia";
            this.colDanJia.OptionsColumn.AllowEdit = false;
            this.colDanJia.Visible = true;
            this.colDanJia.VisibleIndex = 3;
            this.colDanJia.Width = 86;
            // 
            // colDiscount
            // 
            this.colDiscount.Caption = "折扣率";
            this.colDiscount.FieldName = "Discount";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.OptionsColumn.AllowEdit = false;
            this.colDiscount.Visible = true;
            this.colDiscount.VisibleIndex = 4;
            this.colDiscount.Width = 89;
            // 
            // colShuLiang
            // 
            this.colShuLiang.Caption = "数量";
            this.colShuLiang.FieldName = "Count";
            this.colShuLiang.Name = "colShuLiang";
            this.colShuLiang.OptionsColumn.AllowEdit = false;
            this.colShuLiang.Visible = true;
            this.colShuLiang.VisibleIndex = 5;
            this.colShuLiang.Width = 78;
            // 
            // colShouldmoney
            // 
            this.colShouldmoney.Caption = "应收金额";
            this.colShouldmoney.FieldName = "Shouldmoney";
            this.colShouldmoney.Name = "colShouldmoney";
            this.colShouldmoney.OptionsColumn.AllowEdit = false;
            this.colShouldmoney.Visible = true;
            this.colShouldmoney.VisibleIndex = 6;
            this.colShouldmoney.Width = 78;
            // 
            // colActualmoney
            // 
            this.colActualmoney.Caption = "实收金额";
            this.colActualmoney.FieldName = "Actualmoney";
            this.colActualmoney.Name = "colActualmoney";
            this.colActualmoney.OptionsColumn.AllowEdit = false;
            this.colActualmoney.Visible = true;
            this.colActualmoney.VisibleIndex = 7;
            this.colActualmoney.Width = 93;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dgc;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1330, 484);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(822, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(404, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dt_Starte
            // 
            this.dt_Starte.EditValue = null;
            this.dt_Starte.Location = new System.Drawing.Point(512, 12);
            this.dt_Starte.Margin = new System.Windows.Forms.Padding(4);
            this.dt_Starte.MaximumSize = new System.Drawing.Size(150, 0);
            this.dt_Starte.Name = "dt_Starte";
            this.dt_Starte.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_Starte.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_Starte.Size = new System.Drawing.Size(150, 20);
            this.dt_Starte.StyleController = this.layoutControlBase;
            this.dt_Starte.TabIndex = 2;
            // 
            // 开始时间
            // 
            this.开始时间.Control = this.dt_Starte;
            this.开始时间.Location = new System.Drawing.Point(436, 0);
            this.开始时间.Name = "开始时间";
            this.开始时间.Size = new System.Drawing.Size(218, 26);
            this.开始时间.Text = "起止时间：";
            this.开始时间.TextSize = new System.Drawing.Size(60, 14);
            // 
            // dt_End
            // 
            this.dt_End.EditValue = null;
            this.dt_End.Location = new System.Drawing.Point(680, 12);
            this.dt_End.Margin = new System.Windows.Forms.Padding(4);
            this.dt_End.MaximumSize = new System.Drawing.Size(150, 0);
            this.dt_End.Name = "dt_End";
            this.dt_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_End.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_End.Size = new System.Drawing.Size(150, 20);
            this.dt_End.StyleController = this.layoutControlBase;
            this.dt_End.TabIndex = 3;
            // 
            // 结束时间
            // 
            this.结束时间.Control = this.dt_End;
            this.结束时间.Location = new System.Drawing.Point(654, 0);
            this.结束时间.Name = "结束时间";
            this.结束时间.Size = new System.Drawing.Size(168, 26);
            this.结束时间.Text = "~";
            this.结束时间.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.结束时间.TextSize = new System.Drawing.Size(9, 14);
            this.结束时间.TextToControlDistance = 5;
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(1238, 12);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Select.MinimumSize = new System.Drawing.Size(48, 0);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(48, 22);
            this.btn_Select.StyleController = this.layoutControlBase;
            this.btn_Select.TabIndex = 5;
            this.btn_Select.Text = "查询";
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_Select;
            this.layoutControlItem1.Location = new System.Drawing.Point(1226, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(1290, 12);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Export.MinimumSize = new System.Drawing.Size(48, 0);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(48, 22);
            this.btn_Export.StyleController = this.layoutControlBase;
            this.btn_Export.TabIndex = 6;
            this.btn_Export.Text = "导出";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_Export;
            this.layoutControlItem2.Location = new System.Drawing.Point(1278, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(52, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lueItemSuitType
            // 
            this.lueItemSuitType.Location = new System.Drawing.Point(76, 12);
            this.lueItemSuitType.Margin = new System.Windows.Forms.Padding(4);
            this.lueItemSuitType.MaximumSize = new System.Drawing.Size(150, 0);
            this.lueItemSuitType.Name = "lueItemSuitType";
            this.lueItemSuitType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lueItemSuitType.Properties.NullText = "";
            this.lueItemSuitType.Size = new System.Drawing.Size(150, 20);
            this.lueItemSuitType.StyleController = this.layoutControlBase;
            this.lueItemSuitType.TabIndex = 4;
            this.lueItemSuitType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueItemSuitType_ButtonClick);
            this.lueItemSuitType.EditValueChanged += new System.EventHandler(this.lueItemSuitType_EditValueChanged);
            // 
            // 套餐类别
            // 
            this.套餐类别.Control = this.lueItemSuitType;
            this.套餐类别.Location = new System.Drawing.Point(0, 0);
            this.套餐类别.Name = "套餐类别";
            this.套餐类别.Size = new System.Drawing.Size(218, 26);
            this.套餐类别.Text = "套餐类别：";
            this.套餐类别.TextSize = new System.Drawing.Size(60, 14);
            // 
            // chkcmbItemSuit
            // 
            this.chkcmbItemSuit.Location = new System.Drawing.Point(294, 12);
            this.chkcmbItemSuit.Margin = new System.Windows.Forms.Padding(4);
            this.chkcmbItemSuit.MaximumSize = new System.Drawing.Size(150, 0);
            this.chkcmbItemSuit.Name = "chkcmbItemSuit";
            this.chkcmbItemSuit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.chkcmbItemSuit.Size = new System.Drawing.Size(150, 20);
            this.chkcmbItemSuit.StyleController = this.layoutControlBase;
            this.chkcmbItemSuit.TabIndex = 0;
            this.chkcmbItemSuit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.chkcmbItemSuit_ButtonClick);
            // 
            // 套餐名称
            // 
            this.套餐名称.Control = this.chkcmbItemSuit;
            this.套餐名称.Location = new System.Drawing.Point(218, 0);
            this.套餐名称.Name = "套餐名称";
            this.套餐名称.Size = new System.Drawing.Size(218, 26);
            this.套餐名称.Text = "套餐名称：";
            this.套餐名称.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FrmTCTJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 530);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTCTJ";
            this.Text = "套餐统计";
            this.Load += new System.EventHandler(this.FrmTCTJ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemSuit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Starte.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Starte.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.开始时间)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_End.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.结束时间)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueItemSuitType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.套餐类别)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbItemSuit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.套餐名称)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl dgc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemSuit;
        private DevExpress.XtraGrid.Columns.GridColumn colTaoCanLeiBie;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.DateEdit dt_Starte;
        private DevExpress.XtraEditors.DateEdit dt_End;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem 结束时间;
        private DevExpress.XtraLayout.LayoutControlItem 开始时间;
        private DevExpress.XtraEditors.SimpleButton btn_Select;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colTaoCanMingCheng;
        private DevExpress.XtraGrid.Columns.GridColumn colChengBenJia;
        private DevExpress.XtraGrid.Columns.GridColumn colDanJia;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraEditors.LookUpEdit lueItemSuitType;
        private DevExpress.XtraLayout.LayoutControlItem 套餐类别;
        private DevExpress.XtraGrid.Columns.GridColumn colShuLiang;
        private DevExpress.XtraGrid.Columns.GridColumn colShouldmoney;
        private DevExpress.XtraGrid.Columns.GridColumn colActualmoney;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbItemSuit;
        private DevExpress.XtraLayout.LayoutControlItem 套餐名称;
    }
}