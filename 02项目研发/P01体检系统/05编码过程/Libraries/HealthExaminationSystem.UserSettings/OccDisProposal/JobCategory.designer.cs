namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    partial class JobCategory
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
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlJobCategory = new DevExpress.XtraGrid.GridControl();
            this.gridViewJobCategory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnPostStateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnHelpChar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonSelect = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonModify = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.comboBoxEditState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonInsert = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlJobCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewJobCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
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
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem4,
            this.layoutControlItem8});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(477, 525);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.labelControl1);
            this.layoutControlBase.Controls.Add(this.simpleButtonInsert);
            this.layoutControlBase.Controls.Add(this.comboBoxEditState);
            this.layoutControlBase.Controls.Add(this.simpleButtonModify);
            this.layoutControlBase.Controls.Add(this.simpleButtonSelect);
            this.layoutControlBase.Controls.Add(this.textEditName);
            this.layoutControlBase.Controls.Add(this.gridControlJobCategory);
            this.layoutControlBase.Controls.Add(this.textEdit1);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(477, 525);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(52, 12);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(312, 20);
            this.textEdit1.StyleController = this.layoutControlBase;
            this.textEdit1.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(356, 26);
            this.layoutControlItem1.Text = "岗位：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // gridControlJobCategory
            // 
            this.gridControlJobCategory.Location = new System.Drawing.Point(12, 38);
            this.gridControlJobCategory.MainView = this.gridViewJobCategory;
            this.gridControlJobCategory.Name = "gridControlJobCategory";
            this.gridControlJobCategory.Size = new System.Drawing.Size(453, 391);
            this.gridControlJobCategory.TabIndex = 5;
            this.gridControlJobCategory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewJobCategory});
            // 
            // gridViewJobCategory
            // 
            this.gridViewJobCategory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPostStateName,
            this.gridColumnHelpChar,
            this.gridColumnIsActive,
            this.gridColumnid});
            this.gridViewJobCategory.GridControl = this.gridControlJobCategory;
            this.gridViewJobCategory.Name = "gridViewJobCategory";
            this.gridViewJobCategory.OptionsBehavior.Editable = false;
            this.gridViewJobCategory.OptionsView.ShowGroupPanel = false;
            this.gridViewJobCategory.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewJobCategory_RowClick);
            // 
            // gridColumnPostStateName
            // 
            this.gridColumnPostStateName.Caption = "名称";
            this.gridColumnPostStateName.FieldName = "Name";
            this.gridColumnPostStateName.Name = "gridColumnPostStateName";
            this.gridColumnPostStateName.Visible = true;
            this.gridColumnPostStateName.VisibleIndex = 0;
            // 
            // gridColumnHelpChar
            // 
            this.gridColumnHelpChar.Caption = "简称";
            this.gridColumnHelpChar.FieldName = "HelpChar";
            this.gridColumnHelpChar.Name = "gridColumnHelpChar";
            this.gridColumnHelpChar.Visible = true;
            this.gridColumnHelpChar.VisibleIndex = 1;
            // 
            // gridColumnIsActive
            // 
            this.gridColumnIsActive.Caption = "启用";
            this.gridColumnIsActive.FieldName = "IsActive";
            this.gridColumnIsActive.Name = "gridColumnIsActive";
            this.gridColumnIsActive.Visible = true;
            this.gridColumnIsActive.VisibleIndex = 2;
            // 
            // gridColumnid
            // 
            this.gridColumnid.Caption = "Id";
            this.gridColumnid.FieldName = "Id";
            this.gridColumnid.Name = "gridColumnid";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlJobCategory;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(457, 395);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(52, 433);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(322, 20);
            this.textEditName.StyleController = this.layoutControlBase;
            this.textEditName.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditName;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 421);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(366, 24);
            this.layoutControlItem3.Text = "岗位：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonSelect
            // 
            this.simpleButtonSelect.Location = new System.Drawing.Point(368, 12);
            this.simpleButtonSelect.Name = "simpleButtonSelect";
            this.simpleButtonSelect.Size = new System.Drawing.Size(97, 22);
            this.simpleButtonSelect.StyleController = this.layoutControlBase;
            this.simpleButtonSelect.TabIndex = 8;
            this.simpleButtonSelect.Text = "查询";
            this.simpleButtonSelect.Click += new System.EventHandler(this.simpleButtonSelect_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButtonSelect;
            this.layoutControlItem5.Location = new System.Drawing.Point(356, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(101, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // simpleButtonModify
            // 
            this.simpleButtonModify.Location = new System.Drawing.Point(378, 459);
            this.simpleButtonModify.Name = "simpleButtonModify";
            this.simpleButtonModify.Size = new System.Drawing.Size(87, 22);
            this.simpleButtonModify.StyleController = this.layoutControlBase;
            this.simpleButtonModify.TabIndex = 9;
            this.simpleButtonModify.Text = "保存";
            this.simpleButtonModify.Click += new System.EventHandler(this.simpleButtonModify_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonModify;
            this.layoutControlItem6.Location = new System.Drawing.Point(366, 447);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // comboBoxEditState
            // 
            this.comboBoxEditState.Location = new System.Drawing.Point(52, 457);
            this.comboBoxEditState.Name = "comboBoxEditState";
            this.comboBoxEditState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditState.Properties.Items.AddRange(new object[] {
            "启用",
            "不启用"});
            this.comboBoxEditState.Size = new System.Drawing.Size(322, 20);
            this.comboBoxEditState.StyleController = this.layoutControlBase;
            this.comboBoxEditState.TabIndex = 10;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.comboBoxEditState;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 445);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(366, 28);
            this.layoutControlItem7.Text = "启用：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(36, 14);
            // 
            // simpleButtonInsert
            // 
            this.simpleButtonInsert.Location = new System.Drawing.Point(378, 433);
            this.simpleButtonInsert.Name = "simpleButtonInsert";
            this.simpleButtonInsert.Size = new System.Drawing.Size(87, 22);
            this.simpleButtonInsert.StyleController = this.layoutControlBase;
            this.simpleButtonInsert.TabIndex = 11;
            this.simpleButtonInsert.Text = "新增";
            this.simpleButtonInsert.Click += new System.EventHandler(this.simpleButtonInsert_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonInsert;
            this.layoutControlItem4.Location = new System.Drawing.Point(366, 421);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 485);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(276, 28);
            this.labelControl1.StyleController = this.layoutControlBase;
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "新增：点击新增按钮输入再点击保存即可。\r\n修改：选择数据后直接修改文本框内容点保存即可。";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl1;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 473);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(457, 32);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // JobCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 525);
            this.Name = "JobCategory";
            this.Text = "岗位";
            this.Load += new System.EventHandler(this.JobCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlJobCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewJobCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelect;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraGrid.GridControl gridControlJobCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewJobCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButtonModify;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditState;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPostStateName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnHelpChar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsActive;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInsert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnid;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}