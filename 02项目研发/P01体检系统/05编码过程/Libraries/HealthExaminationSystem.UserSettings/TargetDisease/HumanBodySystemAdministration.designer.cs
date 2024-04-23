namespace Sw.Hospital.HealthExaminationSystem.UserSettings.TargetDisease
{
    partial class HumanBodySystemAdministration
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
            this.gridControlIn = new DevExpress.XtraGrid.GridControl();
            this.gridViewIn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnInId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnInName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlNot = new DevExpress.XtraGrid.GridControl();
            this.gridViewNot = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNotId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNotName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonNew = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemName,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButtonOk);
            this.layoutControlBase.Controls.Add(this.simpleButtonNew);
            this.layoutControlBase.Controls.Add(this.textBoxName);
            this.layoutControlBase.Controls.Add(this.gridControlNot);
            this.layoutControlBase.Controls.Add(this.gridControlIn);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(298, 253, 450, 400);
            // 
            // gridControlIn
            // 
            this.gridControlIn.Location = new System.Drawing.Point(12, 38);
            this.gridControlIn.MainView = this.gridViewIn;
            this.gridControlIn.Name = "gridControlIn";
            this.gridControlIn.Size = new System.Drawing.Size(367, 485);
            this.gridControlIn.TabIndex = 4;
            this.gridControlIn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewIn});
            // 
            // gridViewIn
            // 
            this.gridViewIn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnInId,
            this.gridColumnInName});
            this.gridViewIn.GridControl = this.gridControlIn;
            this.gridViewIn.Name = "gridViewIn";
            this.gridViewIn.OptionsBehavior.Editable = false;
            this.gridViewIn.OptionsView.ShowGroupPanel = false;
            this.gridViewIn.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewIn_RowClick);
            // 
            // gridColumnInId
            // 
            this.gridColumnInId.Caption = "Id";
            this.gridColumnInId.FieldName = "Id";
            this.gridColumnInId.Name = "gridColumnInId";
            // 
            // gridColumnInName
            // 
            this.gridColumnInName.Caption = "gridColumnInName";
            this.gridColumnInName.FieldName = "Name";
            this.gridColumnInName.Name = "gridColumnInName";
            this.gridColumnInName.Visible = true;
            this.gridColumnInName.VisibleIndex = 0;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlIn;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(371, 489);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridControlNot
            // 
            this.gridControlNot.Location = new System.Drawing.Point(383, 38);
            this.gridControlNot.MainView = this.gridViewNot;
            this.gridControlNot.Name = "gridControlNot";
            this.gridControlNot.Size = new System.Drawing.Size(389, 485);
            this.gridControlNot.TabIndex = 5;
            this.gridControlNot.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNot});
            // 
            // gridViewNot
            // 
            this.gridViewNot.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNotId,
            this.gridColumnNotName});
            this.gridViewNot.GridControl = this.gridControlNot;
            this.gridViewNot.Name = "gridViewNot";
            this.gridViewNot.OptionsBehavior.Editable = false;
            this.gridViewNot.OptionsView.ShowGroupPanel = false;
            this.gridViewNot.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewNot_RowClick);
            // 
            // gridColumnNotId
            // 
            this.gridColumnNotId.Caption = "id";
            this.gridColumnNotId.FieldName = "Id";
            this.gridColumnNotId.Name = "gridColumnNotId";
            // 
            // gridColumnNotName
            // 
            this.gridColumnNotName.Caption = "名称";
            this.gridColumnNotName.FieldName = "Name";
            this.gridColumnNotName.Name = "gridColumnNotName";
            this.gridColumnNotName.Visible = true;
            this.gridColumnNotName.VisibleIndex = 0;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlNot;
            this.layoutControlItem2.Location = new System.Drawing.Point(371, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(393, 489);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(52, 527);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(629, 20);
            this.textBoxName.TabIndex = 6;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.textBoxName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 515);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(673, 26);
            this.layoutControlItemName.Text = "名称：";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonNew
            // 
            this.simpleButtonNew.Location = new System.Drawing.Point(685, 527);
            this.simpleButtonNew.Name = "simpleButtonNew";
            this.simpleButtonNew.Size = new System.Drawing.Size(87, 22);
            this.simpleButtonNew.StyleController = this.layoutControlBase;
            this.simpleButtonNew.TabIndex = 7;
            this.simpleButtonNew.Text = "新增";
            this.simpleButtonNew.Click += new System.EventHandler(this.simpleButtonNew_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonNew;
            this.layoutControlItem4.Location = new System.Drawing.Point(673, 515);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Location = new System.Drawing.Point(685, 12);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(87, 22);
            this.simpleButtonOk.StyleController = this.layoutControlBase;
            this.simpleButtonOk.TabIndex = 8;
            this.simpleButtonOk.Text = "保存";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonOk;
            this.layoutControlItem3.Location = new System.Drawing.Point(673, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(673, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // HumanBodySystemAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "HumanBodySystemAdministration";
            this.Text = "体格检查";
            this.Load += new System.EventHandler(this.HumanBodySystemAdministration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlIn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControlNot;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNot;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private System.Windows.Forms.TextBox textBoxName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonNew;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnInId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnInName;
    }
}