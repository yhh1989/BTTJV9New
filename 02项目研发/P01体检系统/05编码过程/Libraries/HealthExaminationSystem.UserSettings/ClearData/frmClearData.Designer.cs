namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ClearData
{
    partial class frmClearData
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
            this.login = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditUserName = new DevExpress.XtraEditors.TextEdit();
            this.user = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditUserPwd = new DevExpress.XtraEditors.TextEdit();
            this.password = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.退出 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.emptySpaceItem4,
            this.layoutControlGroup1,
            this.emptySpaceItem3});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(436, 327);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.退出);
            this.layoutControlBase.Controls.Add(this.textEditUserPwd);
            this.layoutControlBase.Controls.Add(this.textEditUserName);
            this.layoutControlBase.Controls.Add(this.login);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(828, 213, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(436, 327);
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(34, 115);
            this.login.MinimumSize = new System.Drawing.Size(48, 0);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(182, 22);
            this.login.StyleController = this.layoutControlBase;
            this.login.TabIndex = 4;
            this.login.Text = "登录";
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.login;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // textEditUserName
            // 
            this.textEditUserName.Location = new System.Drawing.Point(78, 43);
            this.textEditUserName.Name = "textEditUserName";
            this.textEditUserName.Properties.ReadOnly = true;
            this.textEditUserName.Size = new System.Drawing.Size(324, 20);
            this.textEditUserName.StyleController = this.layoutControlBase;
            this.textEditUserName.TabIndex = 5;
            // 
            // user
            // 
            this.user.Control = this.textEditUserName;
            this.user.CustomizationFormText = "user";
            this.user.Location = new System.Drawing.Point(0, 0);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(372, 24);
            this.user.Text = "用户名:";
            this.user.TextSize = new System.Drawing.Size(40, 14);
            // 
            // textEditUserPwd
            // 
            this.textEditUserPwd.Location = new System.Drawing.Point(78, 67);
            this.textEditUserPwd.Name = "textEditUserPwd";
            this.textEditUserPwd.Properties.PasswordChar = '*';
            this.textEditUserPwd.Properties.UseSystemPasswordChar = true;
            this.textEditUserPwd.Size = new System.Drawing.Size(324, 20);
            this.textEditUserPwd.StyleController = this.layoutControlBase;
            this.textEditUserPwd.TabIndex = 6;
            // 
            // password
            // 
            this.password.Control = this.textEditUserPwd;
            this.password.CustomizationFormText = "password";
            this.password.Location = new System.Drawing.Point(0, 24);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(372, 24);
            this.password.Text = "密码:";
            this.password.TextSize = new System.Drawing.Size(40, 14);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(372, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(406, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 307);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // 退出
            // 
            this.退出.Location = new System.Drawing.Point(220, 115);
            this.退出.MinimumSize = new System.Drawing.Size(48, 0);
            this.退出.Name = "退出";
            this.退出.Size = new System.Drawing.Size(182, 22);
            this.退出.StyleController = this.layoutControlBase;
            this.退出.TabIndex = 7;
            this.退出.Text = "退出";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.退出;
            this.layoutControlItem2.Location = new System.Drawing.Point(186, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(10, 141);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(396, 166);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 307);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.password,
            this.user});
            this.layoutControlGroup1.Location = new System.Drawing.Point(10, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(396, 141);
            this.layoutControlGroup1.Text = "请验证管理员账户";
            // 
            // frmClearData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 327);
            this.Name = "frmClearData";
            this.Text = "清除数据";
            this.Load += new System.EventHandler(this.frmClearData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton login;
        private DevExpress.XtraEditors.TextEdit textEditUserPwd;
        private DevExpress.XtraEditors.TextEdit textEditUserName;
        private DevExpress.XtraLayout.LayoutControlItem user;
        private DevExpress.XtraLayout.LayoutControlItem password;
        private DevExpress.XtraEditors.SimpleButton 退出;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    }
}