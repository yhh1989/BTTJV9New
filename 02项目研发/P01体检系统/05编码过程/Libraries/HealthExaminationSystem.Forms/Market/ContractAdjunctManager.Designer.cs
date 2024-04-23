namespace Sw.Hospital.HealthExaminationSystem.Market
{
    partial class ContractAdjunctManager
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractAdjunctManager));
            this.gridControl合同附件列表 = new DevExpress.XtraGrid.GridControl();
            this.gridView合同附件列表 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit操作 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton上传附件 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButton删除附件 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.openFileDialog打开文件 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog下载文件 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同附件列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同附件列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit操作)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.simpleButton删除附件);
            this.layoutControlBase.Controls.Add(this.simpleButton上传附件);
            this.layoutControlBase.Controls.Add(this.gridControl合同附件列表);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(582, 171, 450, 400);
            // 
            // gridControl合同附件列表
            // 
            this.gridControl合同附件列表.Location = new System.Drawing.Point(12, 38);
            this.gridControl合同附件列表.MainView = this.gridView合同附件列表;
            this.gridControl合同附件列表.Name = "gridControl合同附件列表";
            this.gridControl合同附件列表.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit操作});
            this.gridControl合同附件列表.Size = new System.Drawing.Size(760, 511);
            this.gridControl合同附件列表.TabIndex = 4;
            this.gridControl合同附件列表.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView合同附件列表});
            this.gridControl合同附件列表.DataSourceChanged += new System.EventHandler(this.gridControl合同附件列表_DataSourceChanged);
            // 
            // gridView合同附件列表
            // 
            this.gridView合同附件列表.AutoFillColumn = this.gridColumn2;
            this.gridView合同附件列表.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView合同附件列表.GridControl = this.gridControl合同附件列表;
            this.gridView合同附件列表.Name = "gridView合同附件列表";
            this.gridView合同附件列表.OptionsView.ColumnAutoWidth = false;
            this.gridView合同附件列表.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "标识";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "类型";
            this.gridColumn3.FieldName = "ContentType";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "操作";
            this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit操作;
            this.gridColumn4.FieldName = "FileName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // repositoryItemButtonEdit操作
            // 
            this.repositoryItemButtonEdit操作.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repositoryItemButtonEdit操作.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions1, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)});
            this.repositoryItemButtonEdit操作.Name = "repositoryItemButtonEdit操作";
            this.repositoryItemButtonEdit操作.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit操作.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit操作_ButtonClick);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "大小";
            this.gridColumn5.DisplayFormat.FormatString = "{0} Byte";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "ContentLength";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl合同附件列表;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 515);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleButton上传附件
            // 
            this.simpleButton上传附件.AutoWidthInLayoutControl = true;
            this.simpleButton上传附件.Location = new System.Drawing.Point(602, 12);
            this.simpleButton上传附件.Name = "simpleButton上传附件";
            this.simpleButton上传附件.Size = new System.Drawing.Size(83, 22);
            this.simpleButton上传附件.StyleController = this.layoutControlBase;
            this.simpleButton上传附件.TabIndex = 5;
            this.simpleButton上传附件.Text = "上传附件(&U)";
            this.simpleButton上传附件.Click += new System.EventHandler(this.simpleButton上传附件_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton上传附件;
            this.layoutControlItem2.Location = new System.Drawing.Point(590, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(590, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButton删除附件
            // 
            this.simpleButton删除附件.AutoWidthInLayoutControl = true;
            this.simpleButton删除附件.Location = new System.Drawing.Point(689, 12);
            this.simpleButton删除附件.Name = "simpleButton删除附件";
            this.simpleButton删除附件.Size = new System.Drawing.Size(83, 22);
            this.simpleButton删除附件.StyleController = this.layoutControlBase;
            this.simpleButton删除附件.TabIndex = 6;
            this.simpleButton删除附件.Text = "删除附件(&D)";
            this.simpleButton删除附件.Click += new System.EventHandler(this.simpleButton删除附件_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton删除附件;
            this.layoutControlItem3.Location = new System.Drawing.Point(677, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // openFileDialog打开文件
            // 
            this.openFileDialog打开文件.Title = "选择合同附件";
            this.openFileDialog打开文件.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog打开文件_FileOk);
            // 
            // saveFileDialog下载文件
            // 
            this.saveFileDialog下载文件.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog下载文件_FileOk);
            // 
            // ContractAdjunctManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ContractAdjunctManager";
            this.Text = "{0} - 合同附件管理";
            this.Shown += new System.EventHandler(this.ContractAdjunctManager_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl合同附件列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView合同附件列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit操作)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl合同附件列表;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView合同附件列表;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton上传附件;
        private DevExpress.XtraEditors.SimpleButton simpleButton删除附件;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit操作;
        private System.Windows.Forms.OpenFileDialog openFileDialog打开文件;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog下载文件;
    }
}