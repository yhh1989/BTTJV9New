namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics
{
    partial class StatistalChart
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
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.ChartControl = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).BeginInit();
            this.layoutControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroupBase
            // 
            this.layoutControlGroupBase.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroupBase.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem23});
            this.layoutControlGroupBase.Size = new System.Drawing.Size(797, 525);
            // 
            // layoutControlBase
            // 
            this.layoutControlBase.Controls.Add(this.ChartControl);
            this.layoutControlBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 171, 450, 400);
            this.layoutControlBase.Size = new System.Drawing.Size(797, 525);
            // 
            // ChartControl
            // 
            this.ChartControl.DataBindings = null;
            this.ChartControl.Legend.Name = "Default Legend";
            this.ChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartControl.Location = new System.Drawing.Point(12, 12);
            this.ChartControl.Name = "ChartControl";
            this.ChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.ChartControl.Size = new System.Drawing.Size(773, 501);
            this.ChartControl.TabIndex = 13;
            chartTitle1.Text = "体检结论统计";
            this.ChartControl.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.ChartControl;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(777, 505);
            this.layoutControlItem23.Text = "layoutControlItem2";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextVisible = false;
            // 
            // StatistalChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 525);
            this.Name = "StatistalChart";
            this.Text = "StatistalChart";
            this.Load += new System.EventHandler(this.StatistalChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBase)).EndInit();
            this.layoutControlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraCharts.ChartControl ChartControl;
    }
}