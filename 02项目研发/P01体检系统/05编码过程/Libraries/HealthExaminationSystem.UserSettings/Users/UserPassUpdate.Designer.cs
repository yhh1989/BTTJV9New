namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Users
{
    partial class UserPassUpdate
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
            this.textEditOldPass = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditNewPass = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditNewPass2 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOldPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPass2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
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
            this.emptySpaceItem5,
            this.simpleSeparator1});
            this.layoutControlGroupBase.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupBase.Size = new System.Drawing.Size(333, 122);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonExit);
            this.layoutControlBase.Controls.Add(this.simpleButtonSave);
            this.layoutControlBase.Controls.Add(this.textEditNewPass2);
            this.layoutControlBase.Controls.Add(this.textEditNewPass);
            this.layoutControlBase.Controls.Add(this.textEditOldPass);
            this.layoutControlBase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 173, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(333, 122);
            // 
            // textEditOldPass
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditOldPass, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditOldPass.Location = new System.Drawing.Point(88, 12);
            this.textEditOldPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEditOldPass.Name = "textEditOldPass";
            this.textEditOldPass.Properties.PasswordChar = '✶';
            this.textEditOldPass.Size = new System.Drawing.Size(233, 20);
            this.textEditOldPass.StyleController = this.layoutControlBase;
            this.textEditOldPass.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditOldPass;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(313, 24);
            this.layoutControlItem1.Text = "原密码：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // textEditNewPass
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditNewPass, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditNewPass.Location = new System.Drawing.Point(88, 36);
            this.textEditNewPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEditNewPass.Name = "textEditNewPass";
            this.textEditNewPass.Properties.PasswordChar = '✶';
            this.textEditNewPass.Size = new System.Drawing.Size(233, 20);
            this.textEditNewPass.StyleController = this.layoutControlBase;
            this.textEditNewPass.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditNewPass;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(313, 24);
            this.layoutControlItem2.Text = "新密码：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // textEditNewPass2
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditNewPass2, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditNewPass2.Location = new System.Drawing.Point(88, 60);
            this.textEditNewPass2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEditNewPass2.Name = "textEditNewPass2";
            this.textEditNewPass2.Properties.PasswordChar = '✶';
            this.textEditNewPass2.Size = new System.Drawing.Size(233, 20);
            this.textEditNewPass2.StyleController = this.layoutControlBase;
            this.textEditNewPass2.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditNewPass2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(313, 24);
            this.layoutControlItem3.Text = "确认新密码：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.AutoWidthInLayoutControl = true;
            this.simpleButtonSave.Location = new System.Drawing.Point(233, 86);
            this.simpleButtonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButtonSave.MinimumSize = new System.Drawing.Size(42, 0);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(42, 22);
            this.simpleButtonSave.StyleController = this.layoutControlBase;
            this.simpleButtonSave.TabIndex = 7;
            this.simpleButtonSave.Text = "确认";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonSave;
            this.layoutControlItem4.Location = new System.Drawing.Point(221, 74);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(46, 28);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButtonExit
            // 
            this.simpleButtonExit.AutoWidthInLayoutControl = true;
            this.simpleButtonExit.Location = new System.Drawing.Point(279, 86);
            this.simpleButtonExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButtonExit.MinimumSize = new System.Drawing.Size(42, 0);
            this.simpleButtonExit.Name = "simpleButtonExit";
            this.simpleButtonExit.Size = new System.Drawing.Size(42, 22);
            this.simpleButtonExit.StyleController = this.layoutControlBase;
            this.simpleButtonExit.TabIndex = 8;
            this.simpleButtonExit.Text = "退出";
            this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonExit;
            this.layoutControlItem5.Location = new System.Drawing.Point(267, 74);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(46, 28);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 74);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(221, 28);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 72);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(313, 2);
            // 
            // UserPassUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 122);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserPassUpdate";
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOldPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewPass2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEditOldPass;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.TextEdit textEditNewPass2;
        private DevExpress.XtraEditors.TextEdit textEditNewPass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
    }
}