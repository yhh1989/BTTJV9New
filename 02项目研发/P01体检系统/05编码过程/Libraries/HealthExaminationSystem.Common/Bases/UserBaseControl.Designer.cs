namespace Sw.Hospital.HealthExaminationSystem.Common.Bases
{
    partial class UserBaseControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControlBase = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupBase = new DevExpress.XtraLayout.LayoutControlGroup();
            this.permissionManager = new Sw.Hospital.HealthExaminationSystem.Common.Bases.PermissionManager();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
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
            this.layoutControlBase.Root = this.layoutControlGroupBase;
            this.layoutControlBase.Size = new System.Drawing.Size(450, 450);
            this.layoutControlBase.TabIndex = 0;
            this.layoutControlBase.Text = "layoutControl1";
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupBase.GroupBordersVisible = false;
            this.layoutControlGroupBase.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupBase.Name = "layoutControlGroupBase";
            this.layoutControlGroupBase.Size = new System.Drawing.Size(450, 450);
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
            // UserBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlBase);
            this.Name = "UserBaseControl";
            this.Size = new System.Drawing.Size(450, 450);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected PermissionManager permissionManager;
        protected DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupBase;
        protected DevExpress.XtraLayout.LayoutControl layoutControlBase;
    }
}
