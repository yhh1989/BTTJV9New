namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    partial class FrmIntroduce
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
            this.gridControlItemGroup = new DevExpress.XtraGrid.GridControl();
            this.ItemInfos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.XMName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Introduce = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnBM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.memoEditInt = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelText = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemInfos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleSeparator1,
            this.layoutControlItem5,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem11,
            this.emptySpaceItem1});
            this.layoutControlGroupBase.OptionsItemText.TextToControlDistance = 4;
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.labelText);
            this.layoutControlBase.Controls.Add(this.labelControl2);
            this.layoutControlBase.Controls.Add(this.simpleButton1);
            this.layoutControlBase.Controls.Add(this.memoEditInt);
            this.layoutControlBase.Controls.Add(this.gridControlItemGroup);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(256, 260, 450, 400);
            // 
            // gridControlItemGroup
            // 
            this.gridControlItemGroup.Location = new System.Drawing.Point(15, 138);
            this.gridControlItemGroup.MainView = this.ItemInfos;
            this.gridControlItemGroup.Name = "gridControlItemGroup";
            this.gridControlItemGroup.Size = new System.Drawing.Size(866, 568);
            this.gridControlItemGroup.TabIndex = 4;
            this.gridControlItemGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ItemInfos});
            // 
            // ItemInfos
            // 
            this.ItemInfos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.XMName,
            this.Introduce,
            this.gridColumnBM});
            this.ItemInfos.GridControl = this.gridControlItemGroup;
            this.ItemInfos.Name = "ItemInfos";
            this.ItemInfos.OptionsView.ShowGroupPanel = false;
            // 
            // XMName
            // 
            this.XMName.Caption = "项目名称";
            this.XMName.FieldName = "Name";
            this.XMName.Name = "XMName";
            this.XMName.Visible = true;
            this.XMName.VisibleIndex = 0;
            this.XMName.Width = 133;
            // 
            // Introduce
            // 
            this.Introduce.Caption = "项目介绍";
            this.Introduce.FieldName = "Remark";
            this.Introduce.Name = "Introduce";
            this.Introduce.Visible = true;
            this.Introduce.VisibleIndex = 2;
            this.Introduce.Width = 713;
            // 
            // gridColumnBM
            // 
            this.gridColumnBM.Caption = "编码";
            this.gridColumnBM.FieldName = "ItemBM";
            this.gridColumnBM.Name = "gridColumnBM";
            this.gridColumnBM.Visible = true;
            this.gridColumnBM.VisibleIndex = 1;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlItemGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 123);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(872, 574);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 120);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(872, 3);
            // 
            // memoEditInt
            // 
            this.memoEditInt.Location = new System.Drawing.Point(94, 48);
            this.memoEditInt.Name = "memoEditInt";
            this.memoEditInt.Size = new System.Drawing.Size(787, 81);
            this.memoEditInt.StyleController = this.layoutControlBase;
            this.memoEditInt.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.memoEditInt;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 33);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(872, 87);
            this.layoutControlItem5.Text = "组合介绍：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(75, 18);
            // 
            // simpleButton1
            // 
            this.simpleButton1.AutoWidthInLayoutControl = true;
            this.simpleButton1.Location = new System.Drawing.Point(768, 15);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(71, 27);
            this.simpleButton1.StyleController = this.layoutControlBase;
            this.simpleButton1.TabIndex = 17;
            this.simpleButton1.Text = "   退出   ";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.simpleButton1;
            this.layoutControlItem11.Location = new System.Drawing.Point(753, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(77, 33);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 27);
            this.labelControl2.StyleController = this.layoutControlBase;
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "组合名称：";
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.labelControl2;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(81, 33);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(81, 33);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(81, 33);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(96, 15);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(666, 27);
            this.labelText.StyleController = this.layoutControlBase;
            this.labelText.TabIndex = 19;
            this.labelText.Tag = "";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.labelText;
            this.layoutControlItem13.Location = new System.Drawing.Point(81, 0);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(672, 33);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(830, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(42, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmIntroduce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 721);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "FrmIntroduce";
            this.Text = "组合介绍";
            this.Load += new System.EventHandler(this.FrmIntroduce_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemInfos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditInt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlItemGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView ItemInfos;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraEditors.MemoEdit memoEditInt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.Columns.GridColumn XMName;
        private DevExpress.XtraGrid.Columns.GridColumn Introduce;
        private DevExpress.XtraEditors.LabelControl labelText;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBM;
    }
}