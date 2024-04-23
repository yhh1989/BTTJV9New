namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    partial class ItemDictionaryControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControlXiangMuZiDian = new DevExpress.XtraGrid.GridControl();
            this.gridViewXiangMuZiDian = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colZiDianMingCheng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColZiDianNeiRong = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlXiangMuZiDian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewXiangMuZiDian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(469, 396);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.gridControlXiangMuZiDian);
            this.layoutControlBase.Size = new System.Drawing.Size(469, 396);
            // 
            // gridControlXiangMuZiDian
            // 
            this.gridControlXiangMuZiDian.Location = new System.Drawing.Point(12, 12);
            this.gridControlXiangMuZiDian.MainView = this.gridViewXiangMuZiDian;
            this.gridControlXiangMuZiDian.Name = "gridControlXiangMuZiDian";
            this.gridControlXiangMuZiDian.Size = new System.Drawing.Size(445, 372);
            this.gridControlXiangMuZiDian.TabIndex = 4;
            this.gridControlXiangMuZiDian.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewXiangMuZiDian});
            // 
            // gridViewXiangMuZiDian
            // 
            this.gridViewXiangMuZiDian.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colZiDianMingCheng,
            this.ColZiDianNeiRong});
            this.gridViewXiangMuZiDian.GridControl = this.gridControlXiangMuZiDian;
            this.gridViewXiangMuZiDian.Name = "gridViewXiangMuZiDian";
            this.gridViewXiangMuZiDian.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlXiangMuZiDian;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(449, 376);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // colZiDianMingCheng
            // 
            this.colZiDianMingCheng.Caption = "名称";
            this.colZiDianMingCheng.Name = "colZiDianMingCheng";
            this.colZiDianMingCheng.Visible = true;
            this.colZiDianMingCheng.VisibleIndex = 0;
            // 
            // ColZiDianNeiRong
            // 
            this.ColZiDianNeiRong.Caption = "内容";
            this.ColZiDianNeiRong.Name = "ColZiDianNeiRong";
            this.ColZiDianNeiRong.Visible = true;
            this.ColZiDianNeiRong.VisibleIndex = 1;
            // 
            // ItemDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ItemDictionary";
            this.Size = new System.Drawing.Size(469, 396);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlXiangMuZiDian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewXiangMuZiDian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlXiangMuZiDian;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewXiangMuZiDian;
        private DevExpress.XtraGrid.Columns.GridColumn colZiDianMingCheng;
        private DevExpress.XtraGrid.Columns.GridColumn ColZiDianNeiRong;
    }
}
