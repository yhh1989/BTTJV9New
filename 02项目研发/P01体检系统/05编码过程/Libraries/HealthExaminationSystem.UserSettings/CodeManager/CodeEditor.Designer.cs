namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CodeManager
{
    partial class CodeEditor
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
            this.lookUpEditCode = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemCodeInfo = new DevExpress.XtraLayout.EmptySpaceItem();
            this.textEditPrefix = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditDatePrefix = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditValue = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCodeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatePrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItemCodeInfo,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(509, 143);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonCancel);
            this.layoutControlBase.Controls.Add(this.simpleButtonSave);
            this.layoutControlBase.Controls.Add(this.textEditValue);
            this.layoutControlBase.Controls.Add(this.textEditDatePrefix);
            this.layoutControlBase.Controls.Add(this.textEditPrefix);
            this.layoutControlBase.Controls.Add(this.lookUpEditCode);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 172, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(509, 143);
            // 
            // lookUpEditCode
            // 
            this.lookUpEditCode.Location = new System.Drawing.Point(76, 12);
            this.lookUpEditCode.MaximumSize = new System.Drawing.Size(150, 0);
            this.lookUpEditCode.MinimumSize = new System.Drawing.Size(150, 0);
            this.lookUpEditCode.Name = "lookUpEditCode";
            this.lookUpEditCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditCode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "列名")});
            this.lookUpEditCode.Properties.DisplayMember = "Value";
            this.lookUpEditCode.Properties.NullText = "";
            this.lookUpEditCode.Properties.NullValuePrompt = "选择一个编码进行编辑或更新";
            this.lookUpEditCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.lookUpEditCode.Properties.ShowNullValuePromptWhenFocused = true;
            this.lookUpEditCode.Properties.ValueMember = "Id";
            this.lookUpEditCode.Size = new System.Drawing.Size(150, 20);
            this.lookUpEditCode.StyleController = this.layoutControlBase;
            this.lookUpEditCode.TabIndex = 4;
            this.lookUpEditCode.EditValueChanged += new System.EventHandler(this.lookUpEditCode_EditValueChanged);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lookUpEditCode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(218, 24);
            this.layoutControlItem1.Text = "选择编码：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 96);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(349, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemCodeInfo
            // 
            this.emptySpaceItemCodeInfo.AllowHotTrack = false;
            this.emptySpaceItemCodeInfo.CustomizationFormText = "列名：{0} | 类型：{1}";
            this.emptySpaceItemCodeInfo.Location = new System.Drawing.Point(218, 0);
            this.emptySpaceItemCodeInfo.Name = "emptySpaceItemCodeInfo";
            this.emptySpaceItemCodeInfo.Size = new System.Drawing.Size(271, 24);
            this.emptySpaceItemCodeInfo.Text = "列名：{0} | 类型：{1}";
            this.emptySpaceItemCodeInfo.TextSize = new System.Drawing.Size(60, 0);
            this.emptySpaceItemCodeInfo.TextVisible = true;
            // 
            // textEditPrefix
            // 
            this.textEditPrefix.Location = new System.Drawing.Point(76, 36);
            this.textEditPrefix.Name = "textEditPrefix";
            this.textEditPrefix.Size = new System.Drawing.Size(421, 20);
            this.textEditPrefix.StyleController = this.layoutControlBase;
            this.textEditPrefix.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditPrefix;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(489, 24);
            this.layoutControlItem2.Text = "前缀：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // textEditDatePrefix
            // 
            this.textEditDatePrefix.Location = new System.Drawing.Point(76, 60);
            this.textEditDatePrefix.Name = "textEditDatePrefix";
            this.textEditDatePrefix.Size = new System.Drawing.Size(421, 20);
            this.textEditDatePrefix.StyleController = this.layoutControlBase;
            this.textEditDatePrefix.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditDatePrefix;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(489, 24);
            this.layoutControlItem3.Text = "日期前缀：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // textEditValue
            // 
            this.textEditValue.EditValue = "dfsadf";
            this.textEditValue.Location = new System.Drawing.Point(76, 84);
            this.textEditValue.Name = "textEditValue";
            this.textEditValue.Properties.Mask.EditMask = "d";
            this.textEditValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditValue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditValue.Size = new System.Drawing.Size(421, 20);
            this.textEditValue.StyleController = this.layoutControlBase;
            this.textEditValue.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AllowHtmlStringInCaption = true;
            this.layoutControlItem4.Control = this.textEditValue;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(489, 24);
            this.layoutControlItem4.Text = "<Color=Red>✶</Color>起始值：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.AutoWidthInLayoutControl = true;
            this.simpleButtonSave.Location = new System.Drawing.Point(361, 108);
            this.simpleButtonSave.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonSave.StyleController = this.layoutControlBase;
            this.simpleButtonSave.TabIndex = 8;
            this.simpleButtonSave.Text = "保存(&S)";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonSave;
            this.layoutControlItem5.Location = new System.Drawing.Point(349, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(70, 27);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AutoWidthInLayoutControl = true;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(431, 108);
            this.simpleButtonCancel.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonCancel.StyleController = this.layoutControlBase;
            this.simpleButtonCancel.TabIndex = 9;
            this.simpleButtonCancel.Text = "取消(&C)";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonCancel;
            this.layoutControlItem6.Location = new System.Drawing.Point(419, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(70, 27);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // CodeEditor
            // 
            this.AcceptButton = this.simpleButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(509, 143);
            this.Name = "CodeEditor";
            this.Text = "编码编辑";
            this.Load += new System.EventHandler(this.CodeEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCodeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatePrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCodeInfo;
        private DevExpress.XtraEditors.TextEdit textEditValue;
        private DevExpress.XtraEditors.TextEdit textEditDatePrefix;
        private DevExpress.XtraEditors.TextEdit textEditPrefix;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}