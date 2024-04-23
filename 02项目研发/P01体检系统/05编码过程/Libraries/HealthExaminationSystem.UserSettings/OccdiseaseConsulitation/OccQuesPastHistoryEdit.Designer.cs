namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseConsulitation
{
    partial class OccQuesPastHistoryEdit
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
            this.method = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.date = new DevExpress.XtraEditors.DateEdit();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ishave = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lookUpEdit4 = new System.Windows.Forms.ComboBox();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditDiagnosticCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.method.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ishave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDiagnosticCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.layoutControlItem6,
            this.layoutControlItem4});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(406, 210);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.lookUpEdit4);
            this.layoutControlBase.Controls.Add(this.simpleButton6);
            this.layoutControlBase.Controls.Add(this.method);
            this.layoutControlBase.Controls.Add(this.textEdit1);
            this.layoutControlBase.Controls.Add(this.ishave);
            this.layoutControlBase.Controls.Add(this.date);
            this.layoutControlBase.Controls.Add(this.textEditDiagnosticCode);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(406, 210);
            // 
            // method
            // 
            this.method.Location = new System.Drawing.Point(100, 85);
            this.method.Margin = new System.Windows.Forms.Padding(8, 13, 8, 13);
            this.method.Name = "method";
            this.method.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.method.Properties.DisplayMember = "Text";
            this.method.Properties.NullText = "";
            this.method.Properties.PopupSizeable = false;
            this.method.Properties.ValueMember = "Text";
            this.method.Properties.View = this.gridView1;
            this.method.Size = new System.Drawing.Size(294, 20);
            this.method.StyleController = this.layoutControlBase;
            this.method.TabIndex = 7;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "治疗方式";
            this.gridColumn1.FieldName = "Text";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(100, 61);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(8, 13, 8, 13);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(294, 20);
            this.textEdit1.StyleController = this.layoutControlBase;
            this.textEdit1.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.date;
            this.layoutControlItem1.CustomizationFormText = "诊断日期：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(386, 24);
            this.layoutControlItem1.Text = "诊断日期：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(84, 14);
            // 
            // date
            // 
            this.date.EditValue = null;
            this.date.Location = new System.Drawing.Point(100, 37);
            this.date.Margin = new System.Windows.Forms.Padding(8, 13, 8, 13);
            this.date.Name = "date";
            this.date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date.Properties.DisplayFormat.FormatString = "";
            this.date.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date.Properties.EditFormat.FormatString = "";
            this.date.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.date.Properties.Mask.EditMask = "";
            this.date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.date.Size = new System.Drawing.Size(294, 20);
            this.date.StyleController = this.layoutControlBase;
            this.date.TabIndex = 0;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 164);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(162, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.method;
            this.layoutControlItem5.CustomizationFormText = "治疗方式：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 73);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(386, 24);
            this.layoutControlItem5.Text = "治疗方式：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.textEdit1;
            this.layoutControlItem9.CustomizationFormText = "诊断单位：";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(386, 24);
            this.layoutControlItem9.Text = "诊断单位：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(84, 14);
            // 
            // ishave
            // 
            this.ishave.EditValue = true;
            this.ishave.Location = new System.Drawing.Point(100, 109);
            this.ishave.Margin = new System.Windows.Forms.Padding(8, 13, 8, 13);
            this.ishave.Name = "ishave";
            this.ishave.Properties.Columns = 2;
            this.ishave.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "否")});
            this.ishave.Size = new System.Drawing.Size(294, 25);
            this.ishave.StyleController = this.layoutControlBase;
            this.ishave.TabIndex = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.ishave;
            this.layoutControlItem7.CustomizationFormText = "是否痊愈：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 97);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(386, 29);
            this.layoutControlItem7.Text = "是否痊愈：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(84, 14);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(174, 176);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(57, 22);
            this.simpleButton6.StyleController = this.layoutControlBase;
            this.simpleButton6.TabIndex = 26;
            this.simpleButton6.Text = "保存";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton6;
            this.layoutControlItem2.Location = new System.Drawing.Point(162, 164);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem1.Location = new System.Drawing.Point(223, 164);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(163, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem2";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 150);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(386, 14);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lookUpEdit4
            // 
            this.lookUpEdit4.FormattingEnabled = true;
            this.lookUpEdit4.Location = new System.Drawing.Point(100, 12);
            this.lookUpEdit4.Name = "lookUpEdit4";
            this.lookUpEdit4.Size = new System.Drawing.Size(294, 22);
            this.lookUpEdit4.TabIndex = 28;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lookUpEdit4;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(386, 25);
            this.layoutControlItem6.Text = "疾病小类：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(84, 14);
            // 
            // textEditDiagnosticCode
            // 
            this.textEditDiagnosticCode.Location = new System.Drawing.Point(100, 138);
            this.textEditDiagnosticCode.Margin = new System.Windows.Forms.Padding(8, 13, 8, 13);
            this.textEditDiagnosticCode.Name = "textEditDiagnosticCode";
            this.textEditDiagnosticCode.Size = new System.Drawing.Size(294, 20);
            this.textEditDiagnosticCode.StyleController = this.layoutControlBase;
            this.textEditDiagnosticCode.TabIndex = 4;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditDiagnosticCode;
            this.layoutControlItem4.CustomizationFormText = "诊断单位：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 126);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(386, 24);
            this.layoutControlItem4.Text = "诊断证书编号：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(84, 14);
            // 
            // OccQuesPastHistoryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 210);
            this.Name = "OccQuesPastHistoryEdit";
            this.Text = "既往史编辑";
            this.Load += new System.EventHandler(this.isover_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.method.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ishave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDiagnosticCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchLookUpEdit method;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.RadioGroup ishave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit date;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private System.Windows.Forms.ComboBox lookUpEdit4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit textEditDiagnosticCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}