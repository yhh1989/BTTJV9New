namespace Sw.Hospital.HealthExaminationSystem.Common.Bases
{
    partial class UserBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserBaseForm));
            this.layoutControlBase = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupBase = new DevExpress.XtraLayout.LayoutControlGroup();
            this.permissionManager = new Sw.Hospital.HealthExaminationSystem.Common.Bases.PermissionManager();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.splashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Sw.Hospital.HealthExaminationSystem.Common.Bases.UserWaitForm), true, true, true);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlBase.Location = new System.Drawing.Point(0, 0);
            this.layoutControlBase.Name = "layoutControlBase";
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Root = this.layoutControlGroupBase;
            this.layoutControlBase.Size = new System.Drawing.Size(784, 561);
            this.layoutControlBase.TabIndex = 0;
            this.layoutControlBase.Text = "layoutControl1";
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupBase.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupBase.Name = "Root";
            this.layoutControlGroupBase.Size = new System.Drawing.Size(784, 561);
            this.layoutControlGroupBase.TextVisible = false;
            // 
            // permissionManager
            // 
            this.permissionManager.Enabled = false;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // splashScreenManager
            // 
            this.splashScreenManager.ClosingDelay = 500;
            // 
            // UserBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.layoutControlBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserBaseForm";
            this.Load += new System.EventHandler(this.UserBaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupBase;
        protected DevExpress.XtraLayout.LayoutControl layoutControlBase;
        protected DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        protected PermissionManager permissionManager;
        protected DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager;
    }
}