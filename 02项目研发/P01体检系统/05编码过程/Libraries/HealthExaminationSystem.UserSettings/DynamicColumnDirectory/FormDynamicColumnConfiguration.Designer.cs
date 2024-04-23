namespace Sw.Hospital.HealthExaminationSystem.UserSettings.DynamicColumnDirectory
{
    partial class FormDynamicColumnConfiguration
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions21 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDynamicColumnConfiguration));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions22 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions23 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions24 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions25 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.gridControlColumn = new DevExpress.XtraGrid.GridControl();
            this.gridViewColumn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEditId = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonBC保存 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup1});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.gridControlColumn);
            this.layoutControlBase.Controls.Add(this.simpleButtonBC保存);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 171, 450, 400);
            // 
            // gridControlColumn
            // 
            this.gridControlColumn.Location = new System.Drawing.Point(12, 62);
            this.gridControlColumn.MainView = this.gridViewColumn;
            this.gridControlColumn.Name = "gridControlColumn";
            this.gridControlColumn.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditId});
            this.gridControlColumn.Size = new System.Drawing.Size(760, 487);
            this.gridControlColumn.TabIndex = 4;
            this.gridControlColumn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewColumn});
            // 
            // gridViewColumn
            // 
            this.gridViewColumn.AutoFillColumn = this.gridColumn3;
            this.gridViewColumn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn1});
            this.gridViewColumn.GridControl = this.gridControlColumn;
            this.gridViewColumn.Name = "gridViewColumn";
            this.gridViewColumn.OptionsView.ColumnAutoWidth = false;
            this.gridViewColumn.OptionsView.ShowGroupPanel = false;
            this.gridViewColumn.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn5, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "列标题";
            this.gridColumn3.FieldName = "GridViewColumnCaption";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "表格视图标识";
            this.gridColumn2.FieldName = "GridViewId";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "显示";
            this.gridColumn4.FieldName = "Visible";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "显示顺序";
            this.gridColumn5.FieldName = "VisibleIndex";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "活动/固定";
            this.gridColumn7.FieldName = "FixedFormat";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "左/右固定";
            this.gridColumn6.FieldName = "IsLeftFormat";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "操作";
            this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditId;
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            // 
            // repositoryItemButtonEditId
            // 
            this.repositoryItemButtonEditId.AutoHeight = false;
            editorButtonImageOptions21.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions21.Image")));
            editorButtonImageOptions22.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions22.Image")));
            editorButtonImageOptions23.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions23.Image")));
            editorButtonImageOptions24.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions24.Image")));
            editorButtonImageOptions25.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions25.Image")));
            this.repositoryItemButtonEditId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions21, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "向上移动", 1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Separator),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions22, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "向下移动", 2),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Separator),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions23, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "切换显示/隐藏状态", 3),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Separator),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions24, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "切换活动/固定列", 4),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Separator),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions25, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "切换左/右固定", 5)});
            this.repositoryItemButtonEditId.Name = "repositoryItemButtonEditId";
            this.repositoryItemButtonEditId.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditId.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditId_ButtonClick);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlColumn;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 491);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButtonBC保存
            // 
            this.simpleButtonBC保存.AutoWidthInLayoutControl = true;
            this.simpleButtonBC保存.Location = new System.Drawing.Point(702, 24);
            this.simpleButtonBC保存.Name = "simpleButtonBC保存";
            this.simpleButtonBC保存.Size = new System.Drawing.Size(58, 22);
            this.simpleButtonBC保存.StyleController = this.layoutControlBase;
            this.simpleButtonBC保存.TabIndex = 5;
            this.simpleButtonBC保存.Text = "保存(&S)";
            this.simpleButtonBC保存.Click += new System.EventHandler(this.simpleButtonBC保存_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonBC保存;
            this.layoutControlItem2.Location = new System.Drawing.Point(678, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Flow;
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(764, 50);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // FormDynamicColumnConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "FormDynamicColumnConfiguration";
            this.Text = "动态列配置";
            this.Load += new System.EventHandler(this.FormDynamicColumnConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlColumn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewColumn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonBC保存;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}