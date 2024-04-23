namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CodeManager
{
    partial class CodeConfig
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
            this.simpleButtonCodeCreate = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewCode = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnCodeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCodeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCodePrefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCodeDatPrefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCodeValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.simpleSeparator1,
            this.layoutControlItem3});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.Controls.Add(this.simpleButtonCodeCreate);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            // 
            // simpleButtonCodeCreate
            // 
            this.simpleButtonCodeCreate.AutoWidthInLayoutControl = true;
            this.simpleButtonCodeCreate.Location = new System.Drawing.Point(676, 12);
            this.simpleButtonCodeCreate.MinimumSize = new System.Drawing.Size(96, 0);
            this.simpleButtonCodeCreate.Name = "simpleButtonCodeCreate";
            this.simpleButtonCodeCreate.Size = new System.Drawing.Size(96, 22);
            this.simpleButtonCodeCreate.StyleController = this.layoutControlBase;
            this.simpleButtonCodeCreate.TabIndex = 4;
            this.simpleButtonCodeCreate.Text = "添加/修改(&C)";
            this.simpleButtonCodeCreate.Click += new System.EventHandler(this.simpleButtonCodeCreate_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonCodeCreate;
            this.layoutControlItem1.Location = new System.Drawing.Point(664, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(664, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 26);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(764, 2);
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(12, 40);
            this.gridControl.MainView = this.gridViewCode;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(760, 509);
            this.gridControl.TabIndex = 6;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCode});
            // 
            // gridViewCode
            // 
            this.gridViewCode.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnCodeId,
            this.gridColumnCodeName,
            this.gridColumnCodeType,
            this.gridColumnCodePrefix,
            this.gridColumnCodeDatPrefix,
            this.gridColumnCodeValue});
            this.gridViewCode.GridControl = this.gridControl;
            this.gridViewCode.Name = "gridViewCode";
            this.gridViewCode.OptionsBehavior.Editable = false;
            this.gridViewCode.OptionsView.ShowGroupPanel = false;
            this.gridViewCode.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewCode_RowClick);
            // 
            // gridColumnCodeId
            // 
            this.gridColumnCodeId.Caption = "Id";
            this.gridColumnCodeId.FieldName = "Id";
            this.gridColumnCodeId.Name = "gridColumnCodeId";
            // 
            // gridColumnCodeName
            // 
            this.gridColumnCodeName.Caption = "名称";
            this.gridColumnCodeName.FieldName = "IDName";
            this.gridColumnCodeName.Name = "gridColumnCodeName";
            this.gridColumnCodeName.Visible = true;
            this.gridColumnCodeName.VisibleIndex = 0;
            // 
            // gridColumnCodeType
            // 
            this.gridColumnCodeType.Caption = "类别";
            this.gridColumnCodeType.FieldName = "IDType";
            this.gridColumnCodeType.Name = "gridColumnCodeType";
            this.gridColumnCodeType.Visible = true;
            this.gridColumnCodeType.VisibleIndex = 1;
            // 
            // gridColumnCodePrefix
            // 
            this.gridColumnCodePrefix.Caption = "前缀";
            this.gridColumnCodePrefix.FieldName = "prefix";
            this.gridColumnCodePrefix.Name = "gridColumnCodePrefix";
            this.gridColumnCodePrefix.Visible = true;
            this.gridColumnCodePrefix.VisibleIndex = 2;
            // 
            // gridColumnCodeDatPrefix
            // 
            this.gridColumnCodeDatPrefix.Caption = "日期前缀";
            this.gridColumnCodeDatPrefix.FieldName = "Dateprefix";
            this.gridColumnCodeDatPrefix.Name = "gridColumnCodeDatPrefix";
            this.gridColumnCodeDatPrefix.Visible = true;
            this.gridColumnCodeDatPrefix.VisibleIndex = 3;
            // 
            // gridColumnCodeValue
            // 
            this.gridColumnCodeValue.Caption = "值";
            this.gridColumnCodeValue.FieldName = "IDValue";
            this.gridColumnCodeValue.Name = "gridColumnCodeValue";
            this.gridColumnCodeValue.Visible = true;
            this.gridColumnCodeValue.VisibleIndex = 4;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(764, 513);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // CodeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "CodeConfig";
            this.Text = "编码";
            this.Load += new System.EventHandler(this.CodeConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCodeCreate;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodeId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodeType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodePrefix;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodeDatPrefix;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCodeValue;
    }
}