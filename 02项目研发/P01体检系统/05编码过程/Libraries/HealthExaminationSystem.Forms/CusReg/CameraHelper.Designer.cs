namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    partial class CameraHelper
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
            this.cameraControl = new DevExpress.XtraEditors.Camera.CameraControl();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSnapshot = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSnapshotAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditCustomerBM = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemCustomerBM = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCustomerBM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomerBM)).BeginInit();
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
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItemCustomerBM});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.textEditCustomerBM);
            this.layoutControlBase.Controls.Add(this.simpleButtonCancel);
            this.layoutControlBase.Controls.Add(this.simpleButtonSnapshotAndClose);
            this.layoutControlBase.Controls.Add(this.simpleButtonSnapshot);
            this.layoutControlBase.Controls.Add(this.cameraControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 172, 450, 400);
            // 
            // cameraControl
            // 
            this.cameraControl.AutoStartDefaultDevice = false;
            this.cameraControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.cameraControl.Location = new System.Drawing.Point(12, 12);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = new System.Drawing.Size(760, 511);
            this.cameraControl.StyleController = this.layoutControlBase;
            this.cameraControl.TabIndex = 4;
            this.cameraControl.Text = "cameraControl1";
            this.cameraControl.DeviceChanged += new DevExpress.XtraEditors.Camera.CameraDeviceChangedEventHandler(this.cameraControl_DeviceChanged);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cameraControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 515);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButtonSnapshot
            // 
            this.simpleButtonSnapshot.AutoWidthInLayoutControl = true;
            this.simpleButtonSnapshot.Location = new System.Drawing.Point(530, 527);
            this.simpleButtonSnapshot.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonSnapshot.Name = "simpleButtonSnapshot";
            this.simpleButtonSnapshot.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonSnapshot.StyleController = this.layoutControlBase;
            this.simpleButtonSnapshot.TabIndex = 5;
            this.simpleButtonSnapshot.Text = "拍照(&S)";
            this.simpleButtonSnapshot.Click += new System.EventHandler(this.simpleButtonSnapshot_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonSnapshot;
            this.layoutControlItem2.Location = new System.Drawing.Point(518, 515);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleButtonSnapshotAndClose
            // 
            this.simpleButtonSnapshotAndClose.AutoWidthInLayoutControl = true;
            this.simpleButtonSnapshotAndClose.Location = new System.Drawing.Point(600, 527);
            this.simpleButtonSnapshotAndClose.MinimumSize = new System.Drawing.Size(102, 0);
            this.simpleButtonSnapshotAndClose.Name = "simpleButtonSnapshotAndClose";
            this.simpleButtonSnapshotAndClose.Size = new System.Drawing.Size(102, 22);
            this.simpleButtonSnapshotAndClose.StyleController = this.layoutControlBase;
            this.simpleButtonSnapshotAndClose.TabIndex = 6;
            this.simpleButtonSnapshotAndClose.Text = "拍照并关闭(&A)";
            this.simpleButtonSnapshotAndClose.Click += new System.EventHandler(this.simpleButtonSnapshotAndClose_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonSnapshotAndClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(588, 515);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(106, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 515);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(259, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AutoWidthInLayoutControl = true;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Location = new System.Drawing.Point(706, 527);
            this.simpleButtonCancel.MinimumSize = new System.Drawing.Size(66, 0);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(66, 22);
            this.simpleButtonCancel.StyleController = this.layoutControlBase;
            this.simpleButtonCancel.TabIndex = 7;
            this.simpleButtonCancel.Text = "取消(&C)";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(694, 515);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // textEditCustomerBM
            // 
            this.textEditCustomerBM.Location = new System.Drawing.Point(311, 527);
            this.textEditCustomerBM.Name = "textEditCustomerBM";
            this.textEditCustomerBM.Size = new System.Drawing.Size(215, 20);
            this.textEditCustomerBM.StyleController = this.layoutControlBase;
            this.textEditCustomerBM.TabIndex = 8;
            this.textEditCustomerBM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCustomerBM_KeyDown);
            // 
            // layoutControlItemCustomerBM
            // 
            this.layoutControlItemCustomerBM.Control = this.textEditCustomerBM;
            this.layoutControlItemCustomerBM.Location = new System.Drawing.Point(259, 515);
            this.layoutControlItemCustomerBM.Name = "layoutControlItemCustomerBM";
            this.layoutControlItemCustomerBM.Size = new System.Drawing.Size(259, 26);
            this.layoutControlItemCustomerBM.Text = "体检号";
            this.layoutControlItemCustomerBM.TextSize = new System.Drawing.Size(36, 14);
            // 
            // CameraHelper
            // 
            this.AcceptButton = this.simpleButtonSnapshot;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "CameraHelper";
            this.Text = "摄像窗体";
            this.Load += new System.EventHandler(this.CameraHelper_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCustomerBM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomerBM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControl;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSnapshotAndClose;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSnapshot;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textEditCustomerBM;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCustomerBM;
    }
}