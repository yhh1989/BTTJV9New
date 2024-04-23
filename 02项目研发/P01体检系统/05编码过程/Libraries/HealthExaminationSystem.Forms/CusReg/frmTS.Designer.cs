namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    partial class frmTS
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
            this.labSum = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.butEdit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.richInfo = new System.Windows.Forms.RichTextBox();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.butClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem7});
            this.layoutControlGroupBase.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupBase.Size = new System.Drawing.Size(697, 489);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.butClose);
            this.layoutControlBase.Controls.Add(this.labelControl1);
            this.layoutControlBase.Controls.Add(this.richInfo);
            this.layoutControlBase.Controls.Add(this.butEdit);
            this.layoutControlBase.Controls.Add(this.labelControl2);
            this.layoutControlBase.Controls.Add(this.labSum);
            this.layoutControlBase.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(574, 172, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(697, 489);
            // 
            // labSum
            // 
            this.labSum.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labSum.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labSum.Appearance.Options.UseFont = true;
            this.labSum.Appearance.Options.UseForeColor = true;
            this.labSum.Location = new System.Drawing.Point(15, 15);
            this.labSum.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labSum.Name = "labSum";
            this.labSum.Size = new System.Drawing.Size(150, 36);
            this.labSum.StyleController = this.layoutControlBase;
            this.labSum.TabIndex = 12;
            this.labSum.Text = "温馨提示：";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labSum;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(673, 42);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(15, 57);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(420, 36);
            this.labelControl2.StyleController = this.layoutControlBase;
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "该体检人有未完成的体检信息：";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(673, 42);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(257, 447);
            this.butEdit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.butEdit.MinimumSize = new System.Drawing.Size(143, 0);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(143, 27);
            this.butEdit.StyleController = this.layoutControlBase;
            this.butEdit.TabIndex = 15;
            this.butEdit.Text = "修改预约";
            this.butEdit.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.butEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(242, 432);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(149, 33);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // richInfo
            // 
            this.richInfo.Location = new System.Drawing.Point(15, 99);
            this.richInfo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.richInfo.Name = "richInfo";
            this.richInfo.ReadOnly = true;
            this.richInfo.Size = new System.Drawing.Size(667, 300);
            this.richInfo.TabIndex = 17;
            this.richInfo.Text = "";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.richInfo;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 84);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(673, 306);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 405);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(510, 36);
            this.labelControl1.StyleController = this.layoutControlBase;
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "请选择是否修改上面选中的体检预约？";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 390);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(673, 42);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 432);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(242, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(469, 432);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(204, 33);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(406, 447);
            this.butClose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.butClose.MinimumSize = new System.Drawing.Size(72, 0);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(72, 27);
            this.butClose.StyleController = this.layoutControlBase;
            this.butClose.TabIndex = 19;
            this.butClose.Text = "取消";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.butClose;
            this.layoutControlItem7.Location = new System.Drawing.Point(391, 432);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(78, 33);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // frmTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 489);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "frmTS";
            this.Text = "提示";
            this.Load += new System.EventHandler(this.frmTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl labSum;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.RichTextBox richInfo;
        private DevExpress.XtraEditors.SimpleButton butEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton butClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}