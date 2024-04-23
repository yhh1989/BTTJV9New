namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    partial class DictionariesSet
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
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonOut = new DevExpress.XtraEditors.SimpleButton();
            this.textEditWorkNamejp = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemWorkNamejp = new DevExpress.XtraLayout.LayoutControlItem();
            this.comtype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkNamejp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWorkNamejp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItemWorkNamejp,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(266, 181);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.comtype);
            this.layoutControlBase.Controls.Add(this.textEditName);
            this.layoutControlBase.Controls.Add(this.simpleButtonOk);
            this.layoutControlBase.Controls.Add(this.simpleButtonOut);
            this.layoutControlBase.Controls.Add(this.textEditWorkNamejp);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(266, 181);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.AutoWidthInLayoutControl = true;
            this.simpleButtonOk.Location = new System.Drawing.Point(76, 147);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(60, 22);
            this.simpleButtonOk.StyleController = this.layoutControlBase;
            this.simpleButtonOk.TabIndex = 7;
            this.simpleButtonOk.Text = "确定(&O)";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(52, 12);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(202, 20);
            this.textEditName.StyleController = this.layoutControlBase;
            this.textEditName.TabIndex = 4;
            this.textEditName.Leave += new System.EventHandler(this.textEditName_Leave);
            // 
            // simpleButtonOut
            // 
            this.simpleButtonOut.AutoWidthInLayoutControl = true;
            this.simpleButtonOut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonOut.Location = new System.Drawing.Point(140, 147);
            this.simpleButtonOut.Name = "simpleButtonOut";
            this.simpleButtonOut.Size = new System.Drawing.Size(58, 22);
            this.simpleButtonOut.StyleController = this.layoutControlBase;
            this.simpleButtonOut.TabIndex = 8;
            this.simpleButtonOut.Text = "取消(&C)";
            this.simpleButtonOut.Click += new System.EventHandler(this.simpleButtonOut_Click);
            // 
            // textEditWorkNamejp
            // 
            this.textEditWorkNamejp.Location = new System.Drawing.Point(52, 36);
            this.textEditWorkNamejp.Name = "textEditWorkNamejp";
            this.textEditWorkNamejp.Size = new System.Drawing.Size(202, 20);
            this.textEditWorkNamejp.StyleController = this.layoutControlBase;
            this.textEditWorkNamejp.TabIndex = 9;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditName;
            this.layoutControlItem1.CustomizationFormText = "名称：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(246, 24);
            this.layoutControlItem1.Text = "名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonOk;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(64, 135);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonOut;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(128, 135);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 135);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(64, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(190, 135);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(56, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemWorkNamejp
            // 
            this.layoutControlItemWorkNamejp.Control = this.textEditWorkNamejp;
            this.layoutControlItemWorkNamejp.CustomizationFormText = "简拼：";
            this.layoutControlItemWorkNamejp.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemWorkNamejp.Name = "layoutControlItemWorkNamejp";
            this.layoutControlItemWorkNamejp.Size = new System.Drawing.Size(246, 24);
            this.layoutControlItemWorkNamejp.Text = "简拼：";
            this.layoutControlItemWorkNamejp.TextSize = new System.Drawing.Size(36, 14);
            // 
            // comtype
            // 
            this.comtype.Location = new System.Drawing.Point(52, 60);
            this.comtype.Name = "comtype";
            this.comtype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comtype.Properties.Items.AddRange(new object[] {
            "症状分类",
            "职业健康分类",
            "危害因素类别"});
            this.comtype.Size = new System.Drawing.Size(202, 20);
            this.comtype.StyleController = this.layoutControlBase;
            this.comtype.TabIndex = 10;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comtype;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(246, 24);
            this.layoutControlItem2.Text = "类别：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(246, 63);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DictionariesSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 181);
            this.Name = "DictionariesSet";
            this.Text = "类别设置";
            this.Load += new System.EventHandler(this.DictionariesSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditWorkNamejp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWorkNamejp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOut;
        private DevExpress.XtraEditors.TextEdit textEditWorkNamejp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemWorkNamejp;
        private DevExpress.XtraEditors.ComboBoxEdit comtype;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}