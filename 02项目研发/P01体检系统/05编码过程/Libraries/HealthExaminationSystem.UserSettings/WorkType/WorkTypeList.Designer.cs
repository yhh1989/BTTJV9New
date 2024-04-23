namespace Sw.Hospital.HealthExaminationSystem.UserSettings.WorkType
{
    partial class WorkTypeList
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
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnWorkTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnWorkTypeWorkName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnWorkTypeValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnWorkTypeOrdernum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnWorkTypeZyWorkTypes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbReload = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbAdd = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbEdit = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sbDel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.radioGroupType = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(784, 562);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.radioGroupType);
            this.layoutControlBase.Controls.Add(this.sbDel);
            this.layoutControlBase.Controls.Add(this.sbEdit);
            this.layoutControlBase.Controls.Add(this.sbAdd);
            this.layoutControlBase.Controls.Add(this.sbReload);
            this.layoutControlBase.Controls.Add(this.gridControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControlBase.Size = new System.Drawing.Size(784, 562);
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(12, 84);
            this.gridControl.MainView = this.gridViewWorkType;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(760, 466);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkType});
            this.gridControl.DoubleClick += new System.EventHandler(this.gridControl_DoubleClick);
            // 
            // gridViewWorkType
            // 
            this.gridViewWorkType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnWorkTypeId,
            this.gridColumnWorkTypeWorkName,
            this.gridColumnWorkTypeValid,
            this.gridColumnWorkTypeOrdernum,
            this.gridColumnWorkTypeZyWorkTypes});
            this.gridViewWorkType.GridControl = this.gridControl;
            this.gridViewWorkType.GroupCount = 1;
            this.gridViewWorkType.Name = "gridViewWorkType";
            this.gridViewWorkType.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewWorkType.OptionsBehavior.Editable = false;
            this.gridViewWorkType.OptionsView.ShowGroupPanel = false;
            this.gridViewWorkType.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnWorkTypeZyWorkTypes, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewWorkType.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridViewWorkType_CustomColumnDisplayText);
            // 
            // gridColumnWorkTypeId
            // 
            this.gridColumnWorkTypeId.Caption = "Id";
            this.gridColumnWorkTypeId.FieldName = "Id";
            this.gridColumnWorkTypeId.Name = "gridColumnWorkTypeId";
            // 
            // gridColumnWorkTypeWorkName
            // 
            this.gridColumnWorkTypeWorkName.Caption = "名称";
            this.gridColumnWorkTypeWorkName.FieldName = "Name";
            this.gridColumnWorkTypeWorkName.Name = "gridColumnWorkTypeWorkName";
            this.gridColumnWorkTypeWorkName.Visible = true;
            this.gridColumnWorkTypeWorkName.VisibleIndex = 0;
            // 
            // gridColumnWorkTypeValid
            // 
            this.gridColumnWorkTypeValid.Caption = "状态";
            this.gridColumnWorkTypeValid.FieldName = "IsActive";
            this.gridColumnWorkTypeValid.Name = "gridColumnWorkTypeValid";
            this.gridColumnWorkTypeValid.Visible = true;
            this.gridColumnWorkTypeValid.VisibleIndex = 1;
            // 
            // gridColumnWorkTypeOrdernum
            // 
            this.gridColumnWorkTypeOrdernum.Caption = "顺序";
            this.gridColumnWorkTypeOrdernum.FieldName = "Order";
            this.gridColumnWorkTypeOrdernum.Name = "gridColumnWorkTypeOrdernum";
            this.gridColumnWorkTypeOrdernum.Visible = true;
            this.gridColumnWorkTypeOrdernum.VisibleIndex = 2;
            // 
            // gridColumnWorkTypeZyWorkTypes
            // 
            this.gridColumnWorkTypeZyWorkTypes.Caption = "类别";
            this.gridColumnWorkTypeZyWorkTypes.FieldName = "Category";
            this.gridColumnWorkTypeZyWorkTypes.Name = "gridColumnWorkTypeZyWorkTypes";
            this.gridColumnWorkTypeZyWorkTypes.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.None, "Category", "工种", ((short)(1))),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.None, "Category", "车间", ((short)(2))),
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.None, "Category", "行业", ((short)(3)))});
            this.gridColumnWorkTypeZyWorkTypes.Visible = true;
            this.gridColumnWorkTypeZyWorkTypes.VisibleIndex = 0;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(474, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(764, 470);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "职业字典设置";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(764, 72);
            this.layoutControlGroup1.Text = "职业字典设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbReload;
            this.layoutControlItem2.Location = new System.Drawing.Point(484, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // sbReload
            // 
            this.sbReload.AutoWidthInLayoutControl = true;
            this.sbReload.Location = new System.Drawing.Point(508, 43);
            this.sbReload.MinimumSize = new System.Drawing.Size(60, 0);
            this.sbReload.Name = "sbReload";
            this.sbReload.Size = new System.Drawing.Size(60, 22);
            this.sbReload.StyleController = this.layoutControlBase;
            this.sbReload.TabIndex = 5;
            this.sbReload.Text = "刷新";
            this.sbReload.Click += new System.EventHandler(this.sbReload_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbAdd;
            this.layoutControlItem3.Location = new System.Drawing.Point(548, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // sbAdd
            // 
            this.sbAdd.AutoWidthInLayoutControl = true;
            this.sbAdd.Location = new System.Drawing.Point(572, 43);
            this.sbAdd.MinimumSize = new System.Drawing.Size(60, 0);
            this.sbAdd.Name = "sbAdd";
            this.sbAdd.Size = new System.Drawing.Size(60, 22);
            this.sbAdd.StyleController = this.layoutControlBase;
            this.sbAdd.TabIndex = 6;
            this.sbAdd.Text = "新增";
            this.sbAdd.Click += new System.EventHandler(this.sbAdd_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(612, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // sbEdit
            // 
            this.sbEdit.AutoWidthInLayoutControl = true;
            this.sbEdit.Location = new System.Drawing.Point(636, 43);
            this.sbEdit.MinimumSize = new System.Drawing.Size(60, 0);
            this.sbEdit.Name = "sbEdit";
            this.sbEdit.Size = new System.Drawing.Size(60, 22);
            this.sbEdit.StyleController = this.layoutControlBase;
            this.sbEdit.TabIndex = 7;
            this.sbEdit.Text = "修改";
            this.sbEdit.Click += new System.EventHandler(this.sbEdit_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbDel;
            this.layoutControlItem5.Location = new System.Drawing.Point(676, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // sbDel
            // 
            this.sbDel.AutoWidthInLayoutControl = true;
            this.sbDel.Location = new System.Drawing.Point(700, 43);
            this.sbDel.MinimumSize = new System.Drawing.Size(60, 0);
            this.sbDel.Name = "sbDel";
            this.sbDel.Size = new System.Drawing.Size(60, 22);
            this.sbDel.StyleController = this.layoutControlBase;
            this.sbDel.TabIndex = 8;
            this.sbDel.Text = "删除";
            this.sbDel.Click += new System.EventHandler(this.sbDel_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.radioGroupType;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(474, 29);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // radioGroupType
            // 
            this.radioGroupType.AutoSizeInLayoutControl = true;
            this.radioGroupType.Location = new System.Drawing.Point(24, 43);
            this.radioGroupType.Name = "radioGroupType";
            this.radioGroupType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "工种"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "车间"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(5)), "检查类别"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "问诊疾病"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(4)), "处理意见"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(10)), "全部")});
            this.radioGroupType.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.radioGroupType.Size = new System.Drawing.Size(470, 25);
            this.radioGroupType.StyleController = this.layoutControlBase;
            this.radioGroupType.TabIndex = 9;
            // 
            // WorkTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Name = "WorkTypeList";
            this.Text = "职业字典设置";
            this.Load += new System.EventHandler(this.WorkType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbReload;
        private DevExpress.XtraEditors.SimpleButton sbDel;
        private DevExpress.XtraEditors.SimpleButton sbEdit;
        private DevExpress.XtraEditors.SimpleButton sbAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkTypeWorkName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkTypeValid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkTypeOrdernum;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnWorkTypeZyWorkTypes;
        private DevExpress.XtraEditors.RadioGroup radioGroupType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}