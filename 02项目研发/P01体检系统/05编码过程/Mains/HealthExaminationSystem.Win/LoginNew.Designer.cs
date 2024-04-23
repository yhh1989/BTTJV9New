namespace HealthExaminationSystem.Win
{
    partial class LoginNew
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginNew));
            this.simpleButtonCancel = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.textEditUserPwd = new DevExpress.XtraEditors.TextEdit();
            this.textEditUserName = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::HealthExaminationSystem.Win.WaitForm), true, true, true);
            this.labelLogo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonCancel.Appearance.Options.UseFont = true;
            this.simpleButtonCancel.Location = new System.Drawing.Point(640, 414);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(46, 23);
            this.simpleButtonCancel.TabIndex = 8;
            this.simpleButtonCancel.Text = "取 消";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            this.simpleButtonCancel.MouseEnter += new System.EventHandler(this.labelControl5_MouseEnter);
            this.simpleButtonCancel.MouseLeave += new System.EventHandler(this.labelControl5_MouseLeave);
            this.simpleButtonCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelControl5_MouseUp);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOk.Appearance.BackColor = System.Drawing.Color.Green;
            this.simpleButtonOk.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonOk.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButtonOk.Appearance.Options.UseBackColor = true;
            this.simpleButtonOk.Appearance.Options.UseFont = true;
            this.simpleButtonOk.Appearance.Options.UseForeColor = true;
            this.simpleButtonOk.BackgroundImage = global::HealthExaminationSystem.Win.Properties.Resources.登录按钮;
            this.simpleButtonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButtonOk.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.simpleButtonOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.simpleButtonOk.Location = new System.Drawing.Point(575, 373);
            this.simpleButtonOk.MaximumSize = new System.Drawing.Size(180, 27);
            this.simpleButtonOk.MinimumSize = new System.Drawing.Size(180, 35);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(180, 35);
            this.simpleButtonOk.TabIndex = 6;
            this.simpleButtonOk.Text = "登录";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // textEditUserPwd
            // 
            this.textEditUserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider.SetIconAlignment(this.textEditUserPwd, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditUserPwd.Location = new System.Drawing.Point(595, 262);
            this.textEditUserPwd.MaximumSize = new System.Drawing.Size(182, 20);
            this.textEditUserPwd.MinimumSize = new System.Drawing.Size(0, 35);
            this.textEditUserPwd.Name = "textEditUserPwd";
            this.textEditUserPwd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.textEditUserPwd.Properties.Appearance.Options.UseFont = true;
            this.textEditUserPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEditUserPwd.Properties.NullValuePrompt = "请输入密码";
            this.textEditUserPwd.Properties.NullValuePromptShowForEmptyValue = true;
            this.textEditUserPwd.Properties.PasswordChar = '*';
            this.textEditUserPwd.Properties.ShowNullValuePromptWhenFocused = true;
            this.textEditUserPwd.Size = new System.Drawing.Size(161, 35);
            this.textEditUserPwd.TabIndex = 4;
            this.textEditUserPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditUserPwd_KeyDown);
            // 
            // textEditUserName
            // 
            this.textEditUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider.SetIconAlignment(this.textEditUserName, System.Windows.Forms.ErrorIconAlignment.BottomRight);
            this.textEditUserName.Location = new System.Drawing.Point(596, 191);
            this.textEditUserName.MaximumSize = new System.Drawing.Size(182, 20);
            this.textEditUserName.MinimumSize = new System.Drawing.Size(0, 35);
            this.textEditUserName.Name = "textEditUserName";
            this.textEditUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.textEditUserName.Properties.Appearance.Options.UseFont = true;
            this.textEditUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEditUserName.Properties.NullValuePrompt = "请输入用户名";
            this.textEditUserName.Properties.NullValuePromptShowForEmptyValue = true;
            this.textEditUserName.Size = new System.Drawing.Size(160, 35);
            this.textEditUserName.TabIndex = 1;
            this.textEditUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditUserName_KeyDown);
            this.textEditUserName.Leave += new System.EventHandler(this.textEditUserName_Leave);
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // splashScreenManager
            // 
            this.splashScreenManager.ClosingDelay = 500;
            // 
            // labelLogo
            // 
            this.labelLogo.Appearance.Image = global::HealthExaminationSystem.Win.Properties.Resources.logonew;
            this.labelLogo.Appearance.Options.UseImage = true;
            this.labelLogo.Location = new System.Drawing.Point(595, 86);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(157, 35);
            this.labelLogo.TabIndex = 9;
            this.labelLogo.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = global::HealthExaminationSystem.Win.Properties.Resources.usernew;
            this.labelControl2.Appearance.Options.UseImage = true;
            this.labelControl2.Location = new System.Drawing.Point(575, 198);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(16, 16);
            this.labelControl2.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = global::HealthExaminationSystem.Win.Properties.Resources.password;
            this.labelControl3.Appearance.Options.UseImage = true;
            this.labelControl3.Location = new System.Drawing.Point(575, 268);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(14, 16);
            this.labelControl3.TabIndex = 11;
            // 
            // separatorControl1
            // 
            this.separatorControl1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl1.Location = new System.Drawing.Point(554, 226);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(252, 20);
            this.separatorControl1.TabIndex = 12;
            // 
            // separatorControl2
            // 
            this.separatorControl2.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl2.Location = new System.Drawing.Point(554, 297);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(252, 20);
            this.separatorControl2.TabIndex = 13;
            // 
            // LoginNew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::HealthExaminationSystem.Win.Properties.Resources.backgroundnew;
            this.ClientSize = new System.Drawing.Size(800, 599);
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelLogo);
            this.Controls.Add(this.textEditUserName);
            this.Controls.Add(this.textEditUserPwd);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleButtonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 599);
            this.MinimumSize = new System.Drawing.Size(800, 599);
            this.Name = "LoginNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "体检软件";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginNew_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textEditUserName;
        private DevExpress.XtraEditors.TextEdit textEditUserPwd;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager;
        private DevExpress.XtraEditors.LabelControl simpleButtonCancel;
        private DevExpress.XtraEditors.LabelControl labelLogo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}